import { http } from '../base/http'
import type { ProductInput, ProductModel } from '../models/ProductModel'
import type { PagedResult, QueryParams } from '../models/PagedResult'
import { toProduct, type Product } from '../../components/shared/types/Product'

export const ProductsApi = {
    getAll: async (params?: QueryParams): Promise<PagedResult<Product>> => {
        const data = await http.get<PagedResult<ProductModel>>('/products', params as Record<string, unknown>)
        return { ...data, items: data.items.map(toProduct) }
    },
    getById: async (id: number): Promise<Product> => {
        return toProduct(await http.get<ProductModel>(`/products/${id}`))
    },
    create: async (data: ProductInput): Promise<Product> => {
        return toProduct(await http.post<ProductModel>('/products', data))
    },
    update: async (id: number, data: ProductInput): Promise<Product> => {
        return toProduct(await http.put<ProductModel>(`/products/${id}`, data))
    },
    remove: (id: number) => http.remove<void>(`/products/${id}`),
}
