import { Navigate, Outlet } from 'react-router-dom'
import { useAuth } from '../../../context/AuthContext'

interface Props {
    requiredRole?: string
}

function ProtectedRoute({ requiredRole }: Props) {
    const { user } = useAuth()

    if (!user) return <Navigate to='/login' replace />
    if (requiredRole && user.role !== requiredRole) {
        const home = user.role === 'Admin' ? '/categories' : '/'
        return <Navigate to={home} replace />
    }

    return <Outlet />
}

export default ProtectedRoute
