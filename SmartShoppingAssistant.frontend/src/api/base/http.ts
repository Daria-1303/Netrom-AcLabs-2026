import axios from 'axios'

const STORAGE_KEY = 'ssa_auth'

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    headers: { 'Content-Type': 'application/json' },
})

api.interceptors.request.use((config) => {
    const stored = localStorage.getItem(STORAGE_KEY)
    if (stored) {
        const { token } = JSON.parse(stored)
        if (token) config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem(STORAGE_KEY)
            if (window.location.pathname !== '/login') {
                window.location.href = '/login'
            }
        }
        const data = error.response?.data
        const message = typeof data === 'string' && data !== '' ? data : error.message || 'Request failed'
        return Promise.reject(new Error(message))
    }
)

export const http = {
    get: async <T>(path: string, params?: Record<string, unknown>): Promise<T> => {
        const response = await api.get<T>(path, { params })
        return response.data
    },
    post: async <T>(path: string, body: unknown): Promise<T> => {
        const response = await api.post<T>(path, body)
        return response.data
    },
    put: async <T>(path: string, body: unknown): Promise<T> => {
        const response = await api.put<T>(path, body)
        return response.data
    },
    remove: async <T>(path: string): Promise<T> => {
        const response = await api.delete<T>(path)
        return response.data
    },
}
