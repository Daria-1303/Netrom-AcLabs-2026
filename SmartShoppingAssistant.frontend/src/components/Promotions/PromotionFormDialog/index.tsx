import { useState } from "react"
import {
    Alert, Button, Dialog, DialogActions, DialogContent, DialogTitle,
    FormControl, FormControlLabel, InputLabel, MenuItem, Select, Stack, Switch, TextField,
} from "@mui/material"
import type { Promotion } from "../../shared/types/Promotion"
import { PromotionReward, PromotionType } from "../../shared/types/Promotion"
import { PromotionsApi } from "../../../api/clients/PromotionApiClient"
import type { Category } from "../../shared/types/Category"
import type { Product } from "../../shared/types/Product"

interface PromotionFormDialogProps {
    promotion: Promotion | null
    categories: Category[]
    products: Product[]
    onClose: () => void
    onSaved: () => void
}

// valoarea speciala pentru "niciun produs/categorie selectata"
const NONE = ''

function PromotionFormDialog({ promotion, categories, products, onClose, onSaved }: PromotionFormDialogProps) {
    const isEditing = promotion !== null

    const [name, setName] = useState(promotion?.name ?? '')
    const [type, setType] = useState<PromotionType>(promotion?.type ?? PromotionType.Quantity)
    const [threshold, setThreshold] = useState(promotion?.threshold?.toString() ?? '')
    const [reward, setReward] = useState<PromotionReward>(promotion?.reward ?? PromotionReward.PercentDiscount)
    const [rewardValue, setRewardValue] = useState(promotion?.rewardValue?.toString() ?? '')
    const [productId, setProductId] = useState<number | ''>(promotion?.productId ?? NONE)
    const [categoryId, setCategoryId] = useState<number | ''>(promotion?.categoryId ?? NONE)
    const [isActive, setIsActive] = useState(promotion?.isActive ?? true)

    const [error, setError] = useState('')
    const [saving, setSaving] = useState(false)

    async function handleSave() {
        if (name.trim() === '') {
            setError('Name is required.')
            return
        }
        const parsedThreshold = parseFloat(threshold)
        if (isNaN(parsedThreshold) || parsedThreshold < 0) {
            setError('Threshold must be a valid positive number.')
            return
        }
        const parsedRewardValue = parseInt(rewardValue, 10)
        if (isNaN(parsedRewardValue) || parsedRewardValue < 0) {
            setError('Reward value must be a valid positive integer.')
            return
        }
        setSaving(true)
        setError('')
        try {
            const data = {
                name,
                type,
                threshold: parsedThreshold,
                reward,
                rewardValue: parsedRewardValue,
                productId: productId !== NONE ? productId : null,
                categoryId: categoryId !== NONE ? categoryId : null,
                isActive,
            }
            if (isEditing) {
                await PromotionsApi.update(promotion.id, data)
            } else {
                await PromotionsApi.create(data)
            }
            onSaved()
        } catch (err) {
            setError((err as Error).message)
            setSaving(false)
        }
    }

    return (
        <Dialog open onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>{isEditing ? 'Edit Promotion' : 'Add Promotion'}</DialogTitle>
            <DialogContent>
                <Stack spacing={2} sx={{ mt: 1 }}>
                    {error !== '' && <Alert severity="error">{error}</Alert>}
                    <TextField
                        label="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        fullWidth
                    />
                    <FormControl fullWidth>
                        <InputLabel>Type</InputLabel>
                        <Select
                            value={type}
                            label="Type"
                            onChange={(e) => setType(e.target.value as PromotionType)}
                        >
                            <MenuItem value={PromotionType.Quantity}>Quantity</MenuItem>
                            <MenuItem value={PromotionType.CartTotal}>Cart Total</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        label={type === PromotionType.Quantity ? "Threshold (items)" : "Threshold (RON)"}
                        value={threshold}
                        onChange={(e) => setThreshold(e.target.value)}
                        fullWidth
                        type="number"
                        slotProps={{ htmlInput: { min: 0, step: type === PromotionType.Quantity ? 1 : 0.01 } }}
                    />
                    <FormControl fullWidth>
                        <InputLabel>Reward</InputLabel>
                        <Select
                            value={reward}
                            label="Reward"
                            onChange={(e) => setReward(e.target.value as PromotionReward)}
                        >
                            <MenuItem value={PromotionReward.FreeItems}>Free Items</MenuItem>
                            <MenuItem value={PromotionReward.PercentDiscount}>Percent Discount</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        label={reward === PromotionReward.FreeItems ? "Free items count" : "Discount (%)"}
                        value={rewardValue}
                        onChange={(e) => setRewardValue(e.target.value)}
                        fullWidth
                        type="number"
                        slotProps={{ htmlInput: { min: 0, step: 1 } }}
                    />
                    <FormControl fullWidth>
                        <InputLabel>Product (optional)</InputLabel>
                        <Select
                            value={productId}
                            label="Product (optional)"
                            onChange={(e) => setProductId(e.target.value as number | '')}
                        >
                            <MenuItem value={NONE}>
                                <em>None</em>
                            </MenuItem>
                            {products.map((p) => (
                                <MenuItem key={p.id} value={p.id}>
                                    {p.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl fullWidth>
                        <InputLabel>Category (optional)</InputLabel>
                        <Select
                            value={categoryId}
                            label="Category (optional)"
                            onChange={(e) => setCategoryId(e.target.value as number | '')}
                        >
                            <MenuItem value={NONE}>
                                <em>None</em>
                            </MenuItem>
                            {categories.map((c) => (
                                <MenuItem key={c.id} value={c.id}>
                                    {c.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControlLabel
                        control={
                            <Switch
                                checked={isActive}
                                onChange={(e) => setIsActive(e.target.checked)}
                            />
                        }
                        label="Active"
                    />
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Cancel</Button>
                <Button variant="contained" onClick={handleSave} disabled={saving}>
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    )
}

export default PromotionFormDialog
