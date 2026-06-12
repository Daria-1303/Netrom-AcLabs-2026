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
