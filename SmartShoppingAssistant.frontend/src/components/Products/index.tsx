import {
    Alert, Box, Chip, CircularProgress, Container, IconButton, InputAdornment,
    Paper, Table, TableBody, TableCell, TableContainer, TableHead,
    TablePagination, TableRow, TableSortLabel, TextField, Tooltip,
} from '@mui/material'
import { useEffect, useState } from 'react'
import type { Product } from '../shared/types/Product'
import type { Category } from '../shared/types/Category'
import { ProductsApi } from '../../api/clients/ProductApiClient'
import { CategoriesApi } from '../../api/clients/CategoryApiClient'
import { useTableState } from '../../hooks/useTableState'
import { useAuth } from '../../context/AuthContext'
import EditIcon from '@mui/icons-material/Edit'
import DeleteIcon from '@mui/icons-material/Delete'
import SearchIcon from '@mui/icons-material/Search'
import PageHeader from '../common/PageHeader'
import ProductFormDialog from './ProductFormDialog'
import ConfirmDialog from '../common/ConfirmDialog'
import EmptyState from '../common/EmptyState'

function Products() {
    const { isAdmin } = useAuth()
    const { items, totalCount, page, pageSize, search, sortBy, sortDir, loading, error, setPage, setPageSize, setSearch, toggleSort, reload } = useTableState({
        fetchFn: ProductsApi.getAll,
        initialSortBy: 'name',
    })

    const [categories, setCategories] = useState<Category[]>([])
    const [categoriesError, setCategoriesError] = useState(false)
    const [formOpen, setFormOpen] = useState(false)
    const [editing, setEditing] = useState<Product | null>(null)
    const [deleting, setDeleting] = useState<Product | null>(null)
    const [confirmOpen, setConfirmOpen] = useState(false)

    useEffect(() => {
        CategoriesApi.getAll({ pageSize: 50 })
            .then((r) => setCategories(r.items))
            .catch(() => setCategoriesError(true))
    }, [])

    function handleAdd() {
        setEditing(null)
        setFormOpen(true)
    }

    function handleEdit(product: Product) {
        setEditing(product)
        setFormOpen(true)
    }

    function handleDeleteClick(product: Product) {
        setDeleting(product)
        setConfirmOpen(true)
    }

    async function handleDelete() {
        if (deleting === null) return
        setConfirmOpen(false)
        try {
            await ProductsApi.remove(deleting.id)
            reload()
        } catch (err) {
            console.error(err)
        }
    }

    return (
        <Container maxWidth='xl' sx={{ py: 4 }}>
            <PageHeader title='Products' actionLabel={isAdmin() ? 'Add Product' : undefined} onAction={isAdmin() ? handleAdd : undefined} />

            <TextField
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                placeholder='Search products...'
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
            {categoriesError && (
                <Alert severity='warning' sx={{ mb: 2 }}>
                    Could not load categories — the category selector in the form will be empty.
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
                                <TableCell>Description</TableCell>
                                <TableCell>
                                    <TableSortLabel active={sortBy === 'price'} direction={sortBy === 'price' ? sortDir : 'asc'} onClick={() => toggleSort('price')}>
                                        Price (RON)
                                    </TableSortLabel>
                                </TableCell>
                                <TableCell>Categories</TableCell>
                                {isAdmin() && <TableCell align='right'>Actions</TableCell>}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {items.map((product) => (
                                <TableRow key={product.id} hover>
                                    <TableCell>{product.name}</TableCell>
                                    <TableCell>{product.description}</TableCell>
                                    <TableCell>{product.price.toFixed(2)}</TableCell>
                                    <TableCell>
                                        <Box sx={{ display: 'flex', gap: 0.5, flexWrap: 'wrap' }}>
                                            {product.categories.map((cat) => (
                                                <Chip key={cat.id} label={cat.name} size='small' />
                                            ))}
                                        </Box>
                                    </TableCell>
                                    {isAdmin() && (
                                        <TableCell align='right'>
                                            <Tooltip title='Edit'>
                                                <IconButton color='primary' onClick={() => handleEdit(product)}>
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title='Delete'>
                                                <IconButton color='error' onClick={() => handleDeleteClick(product)}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                        </TableCell>
                                    )}
                                </TableRow>
                            ))}
                            {items.length === 0 && (
                                <TableRow>
                                    <TableCell colSpan={isAdmin() ? 5 : 4} sx={{ border: 0 }}>
                                        <EmptyState message='No products yet.' />
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
                <ProductFormDialog
                    product={editing}
                    categories={categories}
                    onClose={() => setFormOpen(false)}
                    onSaved={() => { setFormOpen(false); reload() }}
                />
            )}

            <ConfirmDialog
                open={confirmOpen}
                title='Delete product'
                description={`Are you sure you want to delete "${deleting?.name}"?`}
                confirmLabel='Delete'
                onConfirm={handleDelete}
                onCancel={() => setConfirmOpen(false)}
            />
        </Container>
    )
}

export default Products
