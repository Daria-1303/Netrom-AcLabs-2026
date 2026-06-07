import { http } from '../base/http'

export interface UserDTO {
    id: number
    email: string
    role: string
}

export const UsersApi = {
    getAll: () => http.get<UserDTO[]>('/users'),
    updateRole: (id: number, role: string) => http.put<void>(`/users/${id}/role`, { role }),
}
