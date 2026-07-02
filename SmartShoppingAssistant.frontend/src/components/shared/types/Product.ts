import type { ProductModel } from "../../../api/models/ProductModel"
import { toCategory, type Category } from "./Category"

/** Shown when a product's (external) image fails to load — avoids the broken-image icon. */
export const PRODUCT_IMAGE_FALLBACK =
    "data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='400' height='400'%3E%3Crect width='400' height='400' fill='%23EFEFE3'/%3E%3Ctext x='50%25' y='50%25' font-size='160' text-anchor='middle' dominant-baseline='central'%3E%26%239917%3B%3C/text%3E%3C/svg%3E"

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
