import { createContext, useContext, useState, useEffect, type ReactNode } from 'react'

interface AuthUser {
    email: string
    role: string
    token: string
}

interface AuthContextType {
    user: AuthUser | null
    login: (data: AuthUser) => void
    logout: () => void
    isAdmin: () => boolean
}

const AuthContext = createContext<AuthContextType | null>(null)

const STORAGE_KEY = 'ssa_auth'

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<AuthUser | null>(() => {
        const stored = localStorage.getItem(STORAGE_KEY)
        return stored ? JSON.parse(stored) : null
    })

    useEffect(() => {
        if (user) {
            localStorage.setItem(STORAGE_KEY, JSON.stringify(user))
        } else {
            localStorage.removeItem(STORAGE_KEY)
        }
    }, [user])

    function login(data: AuthUser) {
        setUser(data)
    }

    function logout() {
        localStorage.removeItem(STORAGE_KEY)
        setUser(null)
        window.location.href = '/login'
    }

    function isAdmin() {
        return user?.role === 'Admin'
    }

    return (
        <AuthContext.Provider value={{ user, login, logout, isAdmin }}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    const ctx = useContext(AuthContext)
    if (!ctx) throw new Error('useAuth must be used inside AuthProvider')
    return ctx
}
