export interface CartItem {
    id: number
    productId: number
    productName: string
    unitPrice: number
    quantity: number
    itemTypeTotal: number
}

export interface AppliedPromotion {
    promotionName: string
    discount: number
}

export interface CartModel {
    items: CartItem[]
    subtotal: number
    appliedPromotions: AppliedPromotion[]
    discount: number
    total: number
}

export interface AddCartItemInput {
    productId: number
    quantity: number
}

export interface UpdateCartItemInput {
    quantity: number
}
