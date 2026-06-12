import { useState } from "react"
import {
    Alert, Box, Button, Checkbox, Dialog, DialogActions, DialogContent, DialogTitle,
    FormControl, InputLabel, ListItemText, MenuItem, OutlinedInput, Select,
    Stack, TextField,
} from "@mui/material"
import type { Product } from "../../shared/types/Product"
import type { Category } from "../../shared/types/Category"
import { ProductsApi } from "../../../api/clients/ProductApiClient"

interface ProductFormDialogProps {
    product: Product | null
    categories: Category[]
    onClose: () => void
    onSaved: () => void
}

function ProductFormDialog({ product, categories, onClose, onSaved }: ProductFormDialogProps) {
    const isEditing = product !== null

    const [name, setName] = useState(product?.name ?? '')
    const [description, setDescription] = useState(product?.description ?? '')
    const [price, setPrice] = useState(product?.price?.toString() ?? '')
    const [imageUrl, setImageUrl] = useState(product?.imageUrl ?? '')
    const [selectedCategoryIds, setSelectedCategoryIds] = useState<number[]>(
        product?.categories.map((c) => c.id) ?? []
    )

    const [error, setError] = useState('')
    const [saving, setSaving] = useState(false)

    async function handleSave() {
        if (name.trim() === '') {
            setError('Name is required.')
            return
        }
        const parsedPrice = parseFloat(price)
        if (isNaN(parsedPrice) || parsedPrice < 0) {
            setError('Price must be a valid positive number.')
            return
        }
        setSaving(true)
        setError('')
        try {
            const data = {
                name,
                description,
                price: parsedPrice,
                imageUrl: imageUrl || undefined,
                categoryIds: selectedCategoryIds,
            }
            if (isEditing) {
                await ProductsApi.update(product.id, data)
            } else {
                await ProductsApi.create(data)
            }
            onSaved()
        } catch (err) {
            setError((err as Error).message)
            setSaving(false)
        }
    }

    return (
        <Dialog open onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>{isEditing ? 'Edit Product' : 'Add Product'}</DialogTitle>
            <DialogContent>
                <Stack spacing={2} sx={{ mt: 1 }}>
                    {error !== '' && <Alert severity="error">{error}</Alert>}
                    <TextField
                        label="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        fullWidth
                    />
                    <TextField
                        label="Description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        fullWidth
                        multiline
                        rows={3}
                    />
                    <TextField
                        label="Price (RON)"
                        value={price}
                        onChange={(e) => setPrice(e.target.value)}
                        fullWidth
                        type="number"
                        slotProps={{ htmlInput: { min: 0, step: 0.01 } }}
                    />
                    <TextField
                        label="Image URL"
                        value={imageUrl}
                        onChange={(e) => setImageUrl(e.target.value)}
                        fullWidth
                    />
                    {imageUrl !== '' && (
                        <Box
                            component="img"
                            src={imageUrl}
                            alt="Preview"
                            sx={{ width: 80, height: 80, objectFit: 'cover' }}
                        />
                    )}
                    <FormControl fullWidth>
                        <InputLabel>Categories</InputLabel>
                        <Select
                            multiple
                            value={selectedCategoryIds}
                            onChange={(e) => setSelectedCategoryIds(e.target.value as number[])}
                            input={<OutlinedInput label="Categories" />}
                            renderValue={(selected) =>
                                categories
                                    .filter((c) => (selected as number[]).includes(c.id))
                                    .map((c) => c.name)
                                    .join(', ')
                            }
                        >
                            {categories.map((cat) => (
                                <MenuItem key={cat.id} value={cat.id}>
                                    <Checkbox checked={selectedCategoryIds.includes(cat.id)} />
                                    <ListItemText primary={cat.name} />
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
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

export default ProductFormDialog
