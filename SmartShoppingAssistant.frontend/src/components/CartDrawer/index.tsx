import {
    Box,
    Button,
    Divider,
    Drawer,
    IconButton,
    LinearProgress,
    List,
    ListItem,
    Typography,
} from '@mui/material'
import AddIcon from '@mui/icons-material/Add'
import RemoveIcon from '@mui/icons-material/Remove'
import DeleteIcon from '@mui/icons-material/Delete'
import CloseIcon from '@mui/icons-material/Close'
import ShoppingBagOutlinedIcon from '@mui/icons-material/ShoppingBagOutlined'
import LocalOfferIcon from '@mui/icons-material/LocalOffer'
import { useCart } from '../../context/CartContext/cart-context'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import AutoAwesomeIcon from '@mui/icons-material/AutoAwesome'
import AnalyzeDialog from './AnalyzeDialog'
import { PromotionsApi } from '../../api/clients/PromotionApiClient'
import { PromotionType, type Promotion } from '../shared/types/Promotion'

function CartDrawer() {
    const { cart, open, closeCart, updateQuantity, removeProduct } = useCart()
    const navigate = useNavigate()

    const isEmpty = cart === null || cart.items.length === 0

    const [analyzeOpen, setAnalyzeOpen] = useState(false)
    const [promotions, setPromotions] = useState<Promotion[]>([])

    useEffect(() => {
        PromotionsApi.getAll({ pageSize: 50 })
            .then((r) => setPromotions(r.items.filter((p) => p.isActive)))
            .catch(() => {})
    }, [])

    // Closest cart-total promotion the user hasn't unlocked yet — drives the progress bar.
    const nextPromotion = (() => {
        if (cart === null) return null
        const candidates = promotions
            .filter((p) => p.type === PromotionType.CartTotal && cart.subtotal < p.threshold)
            .sort((a, b) => a.threshold - b.threshold)
        return candidates[0] ?? null
    })()

    const handleAnalyzeClose = () => {
        setAnalyzeOpen(false)
    }

    const handleBrowse = () => {
        closeCart()
        navigate('/shop')
    }

    return (
        <Drawer anchor="right" open={open} onClose={closeCart}>
            <Box
                sx={{
                    width: 800,
                    p: 2,
                    height: '100%',
                    display: 'flex',
                    flexDirection: 'column',
                }}
            >
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        mb: 1,
                    }}
                >
                    <Typography variant="h6">Your Cart</Typography>
                    <IconButton onClick={closeCart}>
                        <CloseIcon />
                    </IconButton>
                </Box>

                {isEmpty ? (
                    <Box
                        sx={{
                            flexGrow: 1,
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            justifyContent: 'center',
                            gap: 2,
                            textAlign: 'center',
                        }}
                    >
                        <ShoppingBagOutlinedIcon sx={{ fontSize: 64, color: '#D8D8C4' }} />
                        <Box>
                            <Typography variant="h6">Your cart is empty</Typography>
                            <Typography variant="body2" color="text.secondary">
                                Discover products and unlock great promotions.
                            </Typography>
                        </Box>
                        <Button variant="contained" onClick={handleBrowse}>
                            Browse products
                        </Button>
                    </Box>
                ) : (
                    <>
                        <List sx={{ flexGrow: 1, overflowY: 'auto' }}>
                            {cart.items.map((item) => (
                                <ListItem
                                    key={item.id}
                                    divider
                                    disableGutters
                                    sx={{ display: 'block', py: 1.5 }}
                                >
                                    <Box
                                        sx={{
                                            display: 'flex',
                                            justifyContent: 'space-between',
                                            alignItems: 'center',
                                        }}
                                    >
                                        <Typography>{item.productName}</Typography>
                                        <IconButton
                                            size="small"
                                            color="error"
                                            onClick={() => removeProduct(item.id)}
                                        >
                                            <DeleteIcon fontSize="small" />
                                        </IconButton>
                                    </Box>
                                    <Typography variant="body2" color="text.secondary">
                                        {item.unitPriceLabel} each
                                    </Typography>
                                    <Box
                                        sx={{
                                            display: 'flex',
                                            justifyContent: 'space-between',
                                            alignItems: 'center',
                                            mt: 1,
                                        }}
                                    >
                                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                                            <IconButton
                                                size="small"
                                                onClick={() => updateQuantity(item.id, item.quantity - 1)}
                                                disabled={item.quantity <= 1}
                                            >
                                                <RemoveIcon fontSize="small" />
                                            </IconButton>
                                            <Typography sx={{ mx: 1, minWidth: 24, textAlign: 'center' }}>
                                                {item.quantity}
                                            </Typography>
                                            <IconButton
                                                size="small"
                                                onClick={() => updateQuantity(item.id, item.quantity + 1)}
                                            >
                                                <AddIcon fontSize="small" />
                                            </IconButton>
                                        </Box>
                                        <Typography>{item.subtotalLabel}</Typography>
                                    </Box>
                                </ListItem>
                            ))}
                        </List>

                        <Divider />

                        <Box sx={{ pt: 2 }}>
                            <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 0.5 }}>
                                <Typography>Subtotal</Typography>
                                <Typography>{cart.subtotalLabel}</Typography>
                            </Box>
                            {cart.appliedPromotions.map((promotion) => (
                                <Box
                                    key={promotion.promotionId}
                                    sx={{ display: 'flex', justifyContent: 'space-between', mb: 0.5 }}
                                >
                                    <Typography color="success.main">{promotion.promotionName}</Typography>
                                    <Typography color="success.main">-{promotion.discountLabel}</Typography>
                                </Box>
                            ))}
                            <Divider sx={{ my: 1 }} />
                            <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                                <Typography variant="h6">Total</Typography>
                                <Typography variant="h6">{cart.totalLabel}</Typography>
                            </Box>

                            {nextPromotion && cart && (
                                <Box
                                    sx={{
                                        mt: 2,
                                        p: 1.5,
                                        borderRadius: 2,
                                        bgcolor: 'rgba(200,192,0,0.08)',
                                        border: '1px solid rgba(200,192,0,0.25)',
                                    }}
                                >
                                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 0.75, mb: 1 }}>
                                        <LocalOfferIcon fontSize="small" color="primary" />
                                        <Typography variant="body2">
                                            Add{' '}
                                            <strong>{(nextPromotion.threshold - cart.subtotal).toFixed(2)} RON</strong>{' '}
                                            more to unlock <strong>{nextPromotion.name}</strong>
                                        </Typography>
                                    </Box>
                                    <LinearProgress
                                        variant="determinate"
                                        value={Math.min(100, (cart.subtotal / nextPromotion.threshold) * 100)}
                                        sx={{ height: 8, borderRadius: 9999 }}
                                    />
                                </Box>
                            )}

                            <Button
                                fullWidth
                                variant="outlined"
                                startIcon={<AutoAwesomeIcon />}
                                onClick={() => setAnalyzeOpen(true)}
                                sx={{ mt: 2 }}
                            >
                                AI Analyze
                            </Button>
                        </Box>
                    </>
                )}
            </Box>
            {analyzeOpen && <AnalyzeDialog open={analyzeOpen} onClose={handleAnalyzeClose} />}
        </Drawer>
    )
}

export default CartDrawer
