export interface SuggestionModel {
    productId: number;
    name: string;
    price: number;
    quantity: number;
    reason: string;
    savings: number | null
}

export interface PromotionSuggestion {
    summary: string;
    suggestions: SuggestionModel[];
}