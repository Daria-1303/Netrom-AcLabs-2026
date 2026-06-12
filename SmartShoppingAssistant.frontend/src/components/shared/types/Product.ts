import type { ProductModel } from "../../../api/models/ProductModel"
import { toCategory, type Category } from "./Category"

export interface Product {
    id: number
    name: string
    description: string
    imageUrl: string
    price: number
    priceLabel: string
    categories: Category[]
    categoriesLabel: string
}

export function toProduct(dto: ProductModel): Product {
    const categories = dto.categories.map(toCategory)
    return {
        id: dto.id,
        name: dto.name,
        description: dto.description ?? "",
        imageUrl: dto.imageUrl ?? "",
        price: dto.price,
        priceLabel: `${dto.price.toFixed(2)} RON`,
        categories,
        categoriesLabel: categories.map((c) => c.name).join(', '),
    }
}
