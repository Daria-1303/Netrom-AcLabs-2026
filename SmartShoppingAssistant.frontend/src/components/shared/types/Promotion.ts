import type { PromotionModel } from "../../../api/models/PromotionModel"
import { PromotionReward, PromotionType } from "../../../api/models/PromotionModel"

export { PromotionType, PromotionReward }

export interface Promotion {
    id: number
    name: string
    type: PromotionType
    typeLabel: string
    threshold: number
    reward: PromotionReward
    rewardValue: number
    rewardLabel: string
    productId: number | null
    categoryId: number | null
    isActive: boolean
    activeLabel: string
}

const typeLabels: Record<PromotionType, string> = {
    [PromotionType.Quantity]: 'Quantity',
    [PromotionType.CartTotal]: 'Cart Total',
}

const rewardLabels: Record<PromotionReward, string> = {
    [PromotionReward.FreeItems]: 'Free Items',
    [PromotionReward.PercentDiscount]: 'Percent Discount',
}

/** A promotion targets a product if it's set for that exact product or one of its categories. */
export function promotionTargetsProduct(
    promotion: Promotion,
    productId: number,
    categoryIds: number[],
): boolean {
    if (promotion.productId !== null) return promotion.productId === productId
    if (promotion.categoryId !== null) return categoryIds.includes(promotion.categoryId)
    return false
}

/** Short human-friendly description of what a promotion gives, e.g. "Buy 3, get 2 free". */
export function promotionOfferLabel(promotion: Promotion): string {
    const reward =
        promotion.reward === PromotionReward.PercentDiscount
            ? `${promotion.rewardValue}% off`
            : `${promotion.rewardValue} free`
    const condition =
        promotion.type === PromotionType.CartTotal
            ? `on carts over ${promotion.threshold} RON`
            : `when you buy ${promotion.threshold}+`
    return `${reward} ${condition}`
}

export function toPromotion(dto: PromotionModel): Promotion {
    return {
        id: dto.id,
        name: dto.name,
        type: dto.type,
        typeLabel: typeLabels[dto.type] ?? 'Unknown',
        threshold: dto.threshold,
        reward: dto.reward,
        rewardValue: dto.rewardValue,
        rewardLabel: `${dto.rewardValue} ${rewardLabels[dto.reward] ?? 'Unknown'}`,
        productId: dto.productId ?? null,
        categoryId: dto.categoryId ?? null,
        isActive: dto.isActive,
        activeLabel: dto.isActive ? 'Yes' : 'No',
    }
}
