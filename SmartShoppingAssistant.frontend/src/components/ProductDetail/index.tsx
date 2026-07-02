import {
    Alert,
    Box,
    Button,
    Chip,
    CircularProgress,
    Container,
    Divider,
    IconButton,
    Paper,
    Stack,
    Typography,
} from "@mui/material"
import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart"
import ArrowBackIcon from "@mui/icons-material/ArrowBack"
import AddIcon from "@mui/icons-material/Add"
import RemoveIcon from "@mui/icons-material/Remove"
import LocalOfferIcon from "@mui/icons-material/LocalOffer"
import { ProductsApi } from "../../api/clients/ProductApiClient"
import { PromotionsApi } from "../../api/clients/PromotionApiClient"
import { PRODUCT_IMAGE_FALLBACK, type Product } from "../shared/types/Product"
import {
    promotionTargetsProduct,
    promotionOfferLabel,
    type Promotion,
} from "../shared/types/Promotion"
import { useCart } from "../../context/CartContext/cart-context"

function ProductDetail() {
    const { id } = useParams<{ id: string }>()
    const navigate = useNavigate()
    const { addItem, openCart } = useCart()

    const [product, setProduct] = useState<Product | null>(null)
    const [promotions, setPromotions] = useState<Promotion[]>([])
    const [quantity, setQuantity] = useState(1)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState("")

    useEffect(() => {
        if (!id) return
        setLoading(true)
        ProductsApi.getById(Number(id))
            .then((data) => {
                setProduct(data)
                setError("")
            })
            .catch((err) => setError((err as Error).message))
            .finally(() => setLoading(false))

        PromotionsApi.getAll({ pageSize: 50 })
            .then((r) => setPromotions(r.items.filter((p) => p.isActive)))
            .catch(() => {})
    }, [id])

    const applicablePromotions = product
        ? promotions.filter((p) =>
              promotionTargetsProduct(
                  p,
                  product.id,
                  product.categories.map((c) => c.id),
              ),
          )
        : []

    async function handleAddToCart() {
        if (!product) return
        await addItem(product.id, quantity)
        openCart()
    }

    if (loading) {
        return (
            <Box sx={{ display: "flex", justifyContent: "center", mt: 8 }}>
                <CircularProgress />
            </Box>
        )
    }

    if (error !== "" || !product) {
        return (
            <Container maxWidth="md" sx={{ py: 4 }}>
                <Button startIcon={<ArrowBackIcon />} onClick={() => navigate("/shop")} sx={{ mb: 2 }}>
                    Back to shop
                </Button>
                <Alert severity="error">{error || "Product not found."}</Alert>
            </Container>
        )
    }

    return (
        <Container maxWidth="lg" sx={{ py: 4 }}>
            <Button startIcon={<ArrowBackIcon />} onClick={() => navigate("/shop")} sx={{ mb: 2 }}>
                Back to shop
            </Button>

            <Box
                sx={{
                    display: "grid",
                    gap: 4,
                    gridTemplateColumns: { xs: "1fr", md: "1fr 1fr" },
                    alignItems: "start",
                }}
            >
                <Box
                    component="img"
                    src={product.imageUrl}
                    alt={product.name}
                    onError={(e) => {
                        e.currentTarget.onerror = null
                        e.currentTarget.src = PRODUCT_IMAGE_FALLBACK
                    }}
                    sx={{
                        width: "100%",
                        borderRadius: 3,
                        objectFit: "cover",
                        aspectRatio: "1 / 1",
                        bgcolor: "#EFEFE3",
                    }}
                />

                <Box>
                    <Typography variant="h4" sx={{ mb: 1 }}>
                        {product.name}
                    </Typography>
                    <Stack direction="row" spacing={1} sx={{ mb: 2, flexWrap: "wrap", gap: 1 }}>
                        {product.categories.map((c) => (
                            <Chip key={c.id} label={c.name} size="small" variant="outlined" />
                        ))}
                    </Stack>
                    <Typography variant="h5" color="primary.dark" sx={{ mb: 2 }}>
                        {product.priceLabel}
                    </Typography>
                    <Typography color="text.secondary" sx={{ mb: 3 }}>
                        {product.description || "No description available."}
                    </Typography>

                    {/* Quantity + add */}
                    <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 4 }}>
                        <Box sx={{ display: "flex", alignItems: "center", border: "1px solid #E4E4D0", borderRadius: 9999 }}>
                            <IconButton
                                size="small"
                                onClick={() => setQuantity((q) => Math.max(1, q - 1))}
                                disabled={quantity <= 1}
                            >
                                <RemoveIcon fontSize="small" />
                            </IconButton>
                            <Typography sx={{ mx: 1.5, minWidth: 20, textAlign: "center" }}>{quantity}</Typography>
                            <IconButton size="small" onClick={() => setQuantity((q) => q + 1)}>
                                <AddIcon fontSize="small" />
                            </IconButton>
                        </Box>
                        <Button variant="contained" startIcon={<AddShoppingCartIcon />} onClick={handleAddToCart}>
                            Add to Cart
                        </Button>
                    </Box>

                    {/* Applicable promotions */}
                    {applicablePromotions.length > 0 && (
                        <Paper sx={{ p: 2.5 }}>
                            <Box sx={{ display: "flex", alignItems: "center", gap: 1, mb: 1.5 }}>
                                <LocalOfferIcon color="primary" fontSize="small" />
                                <Typography variant="h6">Promotions for this product</Typography>
                            </Box>
                            <Divider sx={{ mb: 1.5 }} />
                            <Stack spacing={1.5}>
                                {applicablePromotions.map((promotion) => (
                                    <Box key={promotion.id}>
                                        <Typography variant="subtitle2">{promotion.name}</Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            {promotionOfferLabel(promotion)}
                                        </Typography>
                                    </Box>
                                ))}
                            </Stack>
                        </Paper>
                    )}
                </Box>
            </Box>
        </Container>
    )
}

export default ProductDetail
