import { AppBar, Badge, Box, Button, IconButton, Toolbar, Tooltip } from '@mui/material'
import { NavLink, Link } from 'react-router-dom'
import logo from '../../assets/logo.png'
import { useAuth } from '../../context/AuthContext'
import { useCart } from '../../context/CartContext/cart-context'
import { useColorMode } from '../../context/ColorModeContext'
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart'
import DarkModeIcon from '@mui/icons-material/DarkMode'
import LightModeIcon from '@mui/icons-material/LightMode'

const userNavLinks = [
    { label: 'Home', to: '/' },
    { label: 'Shop', to: '/shop' },
]

const adminNavLinks = [
    { label: 'Categories', to: '/categories' },
    { label: 'Products', to: '/products' },
    { label: 'Promotions', to: '/promotions' },
    { label: 'Users', to: '/users' },
]

const linkSx = {
    px: 1.5,
    py: 0.75,
    borderRadius: '9999px',
    fontSize: '1rem',
    fontWeight: 420,
    color: 'rgba(249,248,242,0.65)',
    textDecoration: 'none',
    transition: 'color 0.15s, background-color 0.15s',
    '&:hover': { color: '#F9F8F2', backgroundColor: 'rgba(249,248,242,0.06)' },
    '&.active': { color: '#C8C000', backgroundColor: 'rgba(200,192,0,0.10)' },
}

function Navbar() {
    const { user, logout, isAdmin } = useAuth()
    const { cart, openCart } = useCart()
    const { mode, toggleColorMode } = useColorMode()

    return (
        <AppBar position='static'>
            <Toolbar sx={{ gap: 3, px: 3 }}>
                <Link to='/'>
                    <Box
                        component='img'
                        src={logo}
                        alt='Smart Shopping Assistant'
                        sx={{ height: 48, display: 'block' }}
                    />
                </Link>
                {user && (
                    <Box sx={{ display: 'flex', gap: 0.5 }}>
                        {(isAdmin() ? adminNavLinks : userNavLinks).map(({ label, to }) => (
                            <Box key={to} component={NavLink} to={to} end={to === '/'} sx={linkSx}>
                                {label}
                            </Box>
                        ))}
                    </Box>
                )}
                <Box sx={{ ml: 'auto', display: 'flex', alignItems: 'center', gap: 1 }}>
                    <Tooltip title={mode === 'dark' ? 'Switch to light' : 'Switch to dark'}>
                        <IconButton color='inherit' onClick={toggleColorMode}>
                            {mode === 'dark' ? <LightModeIcon /> : <DarkModeIcon />}
                        </IconButton>
                    </Tooltip>
                    {user && !isAdmin() && (
                        <IconButton color='inherit' onClick={openCart}>
                            <Badge badgeContent={cart?.itemCount ?? 0} color='primary'>
                                <ShoppingCartIcon />
                            </Badge>
                        </IconButton>
                    )}
                    {user && (
                        <Button
                            variant='outlined'
                            size='small'
                            onClick={logout}
                            sx={{ color: 'rgba(249,248,242,0.65)', borderColor: 'rgba(249,248,242,0.25)', borderRadius: '9999px' }}
                        >
                            Logout
                        </Button>
                    )}
                </Box>
            </Toolbar>
        </AppBar>
    )
}

export default Navbar
