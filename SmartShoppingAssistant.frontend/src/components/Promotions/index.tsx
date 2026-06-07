import {
    Alert, Box, Chip, CircularProgress, Container, IconButton, InputAdornment,
    Paper, Table, TableBody, TableCell, TableContainer, TableHead,
    TablePagination, TableRow, TableSortLabel, TextField, Tooltip,
} from '@mui/material'
import { useEffect, useState } from 'react'
import { useAuth } from '../../context/AuthContext'
import type { Promotion } from '../shared/types/Promotion'
import { PromotionReward, PromotionType } from '../shared/types/Promotion'
import type { Category } from '../shared/types/Category'
import type { Product } from '../shared/types/Product'
import { PromotionsApi } from '../../api/clients/PromotionApiClient'
import { CategoriesApi } from '../../api/clients/CategoryApiClient'
import { ProductsApi } from '../../api/clients/ProductApiClient'
import { useTableState } from '../../hooks/useTableState'
import EditIcon from '@mui/icons-material/Edit'
import DeleteIcon from '@mui/icons-material/Delete'
import SearchIcon from '@mui/icons-material/Search'
import PageHeader from '../common/PageHeader'
import PromotionFormDialog from './PromotionFormDialog'
import ConfirmDialog from '../common/ConfirmDialog'
import EmptyState from '../common/EmptyState'

const promotionTypeLabel: Record<PromotionType, string> = {
    [PromotionType.Quantity]: 'Quantity',
    [PromotionType.CartTotal]: 'Cart Total',
}

const promotionRewardLabel: Record<PromotionReward, string> = {
    [PromotionReward.FreeItems]: 'Free Items',
    [PromotionReward.PercentDiscount]: '% Discount',
}

function Promotions() {
    const { isAdmin } = useAuth()
    const { items, totalCount, page, pageSize, search, sortBy, sortDir, loading, error, setPage, setPageSize, setSearch, toggleSort, reload } = useTableState({
        fetchFn: PromotionsApi.getAll,
        initialSortBy: 'name',
    })

    const [categories, setCategories] = useState<Category[]>([])
    const [products, setProducts] = useState<Product[]>([])
    const [lookupError, setLookupError] = useState(false)
    const [formOpen, setFormOpen] = useState(false)
    const [editing, setEditing] = useState<Promotion | null>(null)
    const [deleting, setDeleting] = useState<Promotion | null>(null)
    const [confirmOpen, setConfirmOpen] = useState(false)

    useEffect(() => {
        CategoriesApi.getAll({ pageSize: 50 }).then((r) => setCategories(r.items)).catch(() => setLookupError(true))
        ProductsApi.getAll({ pageSize: 50 }).then((r) => setProducts(r.items)).catch(() => setLookupError(true))
    }, [])

    function handleAdd() {
        setEditing(null)
        setFormOpen(true)
    }

    function handleEdit(promotion: Promotion) {
        setEditing(promotion)
        setFormOpen(true)
    }

    function handleDeleteClick(promotion: Promotion) {
        setDeleting(promotion)
        setConfirmOpen(true)
    }

    async function handleDelete() {
        if (deleting === null) return
        setConfirmOpen(false)
        try {
            await PromotionsApi.remove(deleting.id)
            reload()
        } catch (err) {
            console.error(err)
        }
    }

    return (
        <Container maxWidth='xl' sx={{ py: 4 }}>
            <PageHeader title='Promotions' actionLabel={isAdmin() ? 'Add Promotion' : undefined} onAction={isAdmin() ? handleAdd : undefined} />

            <TextField
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                placeholder='Search promotions...'
                size='small'
                sx={{ mb: 2, width: 320 }}
                slotProps={{
                    input: {
                        startAdornment: (
                            <InputAdornment position='start'>
                                <SearchIcon fontSize='small' sx={{ color: 'text.secondary' }} />
                            </InputAdornment>
                        ),
                    },
                }}
            />

            {error !== '' && <Alert severity='error' sx={{ mb: 2 }}>{error}</Alert>}
            {lookupError && (
                <Alert severity='warning' sx={{ mb: 2 }}>
                    Could not load products or categories — some selectors in the form will be empty.
                </Alert>
            )}

            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
                    <CircularProgress />
                </Box>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>
                                    <TableSortLabel active={sortBy === 'name'} direction={sortBy === 'name' ? sortDir : 'asc'} onClick={() => toggleSort('name')}>
                                        Name
                                    </TableSortLabel>
                                </TableCell>
                                <TableCell>Type</TableCell>
                                <TableCell>
                                    <TableSortLabel active={sortBy === 'threshold'} direction={sortBy === 'threshold' ? sortDir : 'asc'} onClick={() => toggleSort('threshold')}>
                                        Threshold
                                    </TableSortLabel>
                                </TableCell>
                                <TableCell>Reward</TableCell>
                                <TableCell>
                                    <TableSortLabel active={sortBy === 'rewardValue'} direction={sortBy === 'rewardValue' ? sortDir : 'asc'} onClick={() => toggleSort('rewardValue')}>
                                        Value
                                    </TableSortLabel>
                                </TableCell>
                                <TableCell>Status</TableCell>
                                {isAdmin() && <TableCell align='right'>Actions</TableCell>}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {items.map((promotion) => (
                                <TableRow key={promotion.id} hover>
                                    <TableCell>{promotion.name}</TableCell>
                                    <TableCell>{promotionTypeLabel[promotion.type]}</TableCell>
                                    <TableCell>{promotion.threshold}</TableCell>
                                    <TableCell>{promotionRewardLabel[promotion.reward]}</TableCell>
                                    <TableCell>
                                        {promotion.reward === PromotionReward.PercentDiscount
                                            ? `${promotion.rewardValue}%`
                                            : `${promotion.rewardValue} items`}
                                    </TableCell>
                                    <TableCell>
                                        <Chip
                                            label={promotion.isActive ? 'Active' : 'Inactive'}
                                            color={promotion.isActive ? 'success' : 'default'}
                                            size='small'
                                        />
                                    </TableCell>
                                    {isAdmin() && (
                                        <TableCell align='right'>
                                            <Tooltip title='Edit'>
                                                <IconButton color='primary' onClick={() => handleEdit(promotion)}>
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title='Delete'>
                                                <IconButton color='error' onClick={() => handleDeleteClick(promotion)}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                        </TableCell>
                                    )}
                                </TableRow>
                            ))}
                            {items.length === 0 && (
                                <TableRow>
                                    <TableCell colSpan={isAdmin() ? 7 : 6} sx={{ border: 0 }}>
                                        <EmptyState message='No promotions yet.' />
                                    </TableCell>
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                    <TablePagination
                        component='div'
                        count={totalCount}
                        page={page - 1}
                        rowsPerPage={pageSize}
                        rowsPerPageOptions={[10, 25, 50]}
                        onPageChange={(_, p) => setPage(p + 1)}
                        onRowsPerPageChange={(e) => setPageSize(parseInt(e.target.value, 10))}
                    />
                </TableContainer>
            )}

            {isAdmin() && formOpen && (
                <PromotionFormDialog
                    promotion={editing}
                    categories={categories}
                    products={products}
                    onClose={() => setFormOpen(false)}
                    onSaved={() => { setFormOpen(false); reload() }}
                />
            )}

            <ConfirmDialog
                open={confirmOpen}
                title='Delete promotion'
                description={`Are you sure you want to delete "${deleting?.name}"?`}
                confirmLabel='Delete'
                onConfirm={handleDelete}
                onCancel={() => setConfirmOpen(false)}
            />
        </Container>
    )
}

export default Promotions
