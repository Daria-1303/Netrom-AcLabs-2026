import {
    Alert, Box, CircularProgress, Container, IconButton, InputAdornment,
    Paper, Table, TableBody, TableCell, TableContainer, TableHead,
    TablePagination, TableRow, TableSortLabel, TextField, Tooltip,
} from '@mui/material'
import { useState } from 'react'
import type { Category } from '../shared/types/Category'
import { CategoriesApi } from '../../api/clients/CategoryApiClient'
import { useTableState } from '../../hooks/useTableState'
import { useAuth } from '../../context/AuthContext'
import EditIcon from '@mui/icons-material/Edit'
import DeleteIcon from '@mui/icons-material/Delete'
import SearchIcon from '@mui/icons-material/Search'
import PageHeader from '../common/PageHeader'
import CategoryFormDialog from './CategoryFormDialog'
import ConfirmDialog from '../common/ConfirmDialog'
import EmptyState from '../common/EmptyState'

function Categories() {
    const { isAdmin } = useAuth()
    const { items, totalCount, page, pageSize, search, sortBy, sortDir, loading, error, setPage, setPageSize, setSearch, toggleSort, reload } = useTableState({
        fetchFn: CategoriesApi.getAll,
        initialSortBy: 'name',
    })

    const [formOpen, setFormOpen] = useState(false)
    const [editing, setEditing] = useState<Category | null>(null)
    const [deleting, setDeleting] = useState<Category | null>(null)
    const [confirmOpen, setConfirmOpen] = useState(false)

    function handleAdd() {
        setEditing(null)
        setFormOpen(true)
    }

    function handleEdit(category: Category) {
        setEditing(category)
        setFormOpen(true)
    }

    function handleDeleteClick(category: Category) {
        setDeleting(category)
        setConfirmOpen(true)
    }

    async function handleDelete() {
        if (deleting === null) return
        setConfirmOpen(false)
        try {
            await CategoriesApi.remove(deleting.id)
            reload()
        } catch (err) {
            console.error(err)
        }
    }

    return (
        <Container maxWidth='xl' sx={{ py: 4 }}>
            <PageHeader
                title='Categories'
                actionLabel={isAdmin() ? 'Add Category' : undefined}
                onAction={isAdmin() ? handleAdd : undefined}
            />

            <TextField
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                placeholder='Search categories...'
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
                                <TableCell>
                                    <TableSortLabel active={sortBy === 'description'} direction={sortBy === 'description' ? sortDir : 'asc'} onClick={() => toggleSort('description')}>
                                        Description
                                    </TableSortLabel>
                                </TableCell>
                                {isAdmin() && <TableCell align='right'>Actions</TableCell>}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {items.map((category) => (
                                <TableRow key={category.id} hover>
                                    <TableCell>{category.name}</TableCell>
                                    <TableCell>{category.description}</TableCell>
                                    {isAdmin() && (
                                        <TableCell align='right'>
                                            <Tooltip title='Edit'>
                                                <IconButton color='primary' onClick={() => handleEdit(category)}>
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title='Delete'>
                                                <IconButton color='error' onClick={() => handleDeleteClick(category)}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                        </TableCell>
                                    )}
                                </TableRow>
                            ))}
                            {items.length === 0 && (
                                <TableRow>
                                    <TableCell colSpan={isAdmin() ? 3 : 2} sx={{ border: 0 }}>
                                        <EmptyState message='No categories yet.' />
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
                <CategoryFormDialog
                    category={editing}
                    onClose={() => setFormOpen(false)}
                    onSaved={() => { setFormOpen(false); reload() }}
                />
            )}

            <ConfirmDialog
                open={confirmOpen}
                title='Delete category'
                description={`Are you sure you want to delete "${deleting?.name}"?`}
                confirmLabel='Delete'
                onConfirm={handleDelete}
                onCancel={() => setConfirmOpen(false)}
            />
        </Container>
    )
}

export default Categories
