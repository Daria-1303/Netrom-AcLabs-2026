import {
    Alert, Box, CircularProgress, Container, MenuItem,
    Paper, Select, Table, TableBody, TableCell,
    TableContainer, TableHead, TableRow, type SelectChangeEvent,
} from '@mui/material'
import { useEffect, useState } from 'react'
import { UsersApi, type UserDTO } from '../../api/clients/UsersApiClient'
import PageHeader from '../common/PageHeader'

function Users() {
    const [users, setUsers] = useState<UserDTO[]>([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    useEffect(() => {
        UsersApi.getAll()
            .then(setUsers)
            .catch((err) => setError((err as Error).message))
            .finally(() => setLoading(false))
    }, [])

    async function handleRoleChange(id: number, role: string) {
        try {
            await UsersApi.updateRole(id, role)
            setUsers((prev) => prev.map((u) => u.id === id ? { ...u, role } : u))
        } catch (err) {
            setError((err as Error).message)
        }
    }

    return (
        <Container maxWidth='xl' sx={{ py: 4 }}>
            <PageHeader title='Users' />

            {error && <Alert severity='error' sx={{ mb: 2 }}>{error}</Alert>}

            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
                    <CircularProgress />
                </Box>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Email</TableCell>
                                <TableCell>Role</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {users.map((user) => (
                                <TableRow key={user.id} hover>
                                    <TableCell>{user.email}</TableCell>
                                    <TableCell>
                                        <Select
                                            value={user.role}
                                            size='small'
                                            onChange={(e: SelectChangeEvent) => handleRoleChange(user.id, e.target.value)}
                                        >
                                            <MenuItem value='Admin'>Admin</MenuItem>
                                            <MenuItem value='User'>User</MenuItem>
                                        </Select>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Container>
    )
}

export default Users
