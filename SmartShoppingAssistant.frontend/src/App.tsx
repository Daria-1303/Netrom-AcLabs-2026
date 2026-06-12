import { Box } from '@mui/material'
import { Routes, Route } from 'react-router-dom'
import { AuthProvider } from './context/AuthContext'
import CartProvider from './context/CartContext/CartProvider'
import Navbar from './components/Navbar'
import Home from './components/Home'
import Shop from './components/Shop'
import Categories from './components/Categories'
import Products from './components/Products'
import Promotions from './components/Promotions'
import Users from './components/Users'
import NotFound from './components/NotFound'
import LoginPage from './components/Auth/LoginPage'
import RegisterPage from './components/Auth/RegisterPage'
import ProtectedRoute from './components/common/ProtectedRoute'
import CartDrawer from './components/CartDrawer'
import './App.css'

function App() {
    return (
        <AuthProvider>
            <CartProvider>
                <Box className='app'>
                    <Navbar />
                    <Routes>
                        <Route path='/login' element={<LoginPage />} />
                        <Route path='/register' element={<RegisterPage />} />

                        <Route element={<ProtectedRoute />}>
                            <Route path='/' element={<Home />} />
                            <Route path='/shop' element={<Shop />} />
                        </Route>

                        <Route element={<ProtectedRoute requiredRole='Admin' />}>
                            <Route path='/categories' element={<Categories />} />
                            <Route path='/products' element={<Products />} />
                            <Route path='/promotions' element={<Promotions />} />
                            <Route path='/users' element={<Users />} />
                        </Route>

                        <Route path='*' element={<NotFound />} />
                    </Routes>
                    <CartDrawer />
                </Box>
            </CartProvider>
        </AuthProvider>
    )
}

export default App
