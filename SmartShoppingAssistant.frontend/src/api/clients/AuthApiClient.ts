import { http } from '../base/http'

export interface TokenDTO {
    accessToken: string
    expiresAt: string
    email: string
    role: string
}

export const AuthApi = {
    login: (email: string, password: string) =>
        http.post<TokenDTO>('/auth/login', { email, password }),

    register: (email: string, password: string) =>
        http.post<void>('/auth/register', { email, password }),
}
