import { Alert, Box, Button, Container, Paper, TextField, Typography } from '@mui/material'
import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { AuthApi } from '../../../api/clients/AuthApiClient'
import { useAuth } from '../../../context/AuthContext'

function LoginPage() {
    const { login } = useAuth()
    const navigate = useNavigate()

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const [loading, setLoading] = useState(false)

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')
        setLoading(true)
        try {
            const data = await AuthApi.login(email, password)
            login({ email: data.email, role: data.role, token: data.accessToken })
            navigate('/')
        } catch {
            setError('Invalid credentials.')
        } finally {
            setLoading(false)
        }
    }

    return (
        <Container maxWidth='xs' sx={{ py: 8 }}>
            <Paper sx={{ p: 4 }}>
                <Typography variant='h5' sx={{ fontWeight: 300, mb: 3 }}>Sign in</Typography>
                {error && <Alert severity='error' sx={{ mb: 2 }}>{error}</Alert>}
                <Box component='form' onSubmit={handleSubmit} sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                    <TextField
                        label='Email'
                        type='email'
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                        fullWidth
                    />
                    <TextField
                        label='Password'
                        type='password'
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                        fullWidth
                    />
                    <Button type='submit' variant='contained' fullWidth disabled={loading}>
                        {loading ? 'Signing in...' : 'Sign in'}
                    </Button>
                    <Typography variant='body2' sx={{ textAlign: 'center' }} color='text.secondary'>
                        No account?{' '}
                        <Link to='/register' style={{ color: 'inherit' }}>Register</Link>
                    </Typography>
                </Box>
            </Paper>
        </Container>
    )
}

export default LoginPage
