import {
    Alert,
    Box,
    Button,
    Card,
    CardContent,
    CardMedia,
    Chip,
    Stack,
    ToggleButton,
    ToggleButtonGroup,
    Typography,
    useTheme,
} from "@mui/material"
import { keyframes } from "@mui/system"
import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import StorefrontIcon from "@mui/icons-material/Storefront"
import SportsSoccerIcon from "@mui/icons-material/SportsSoccer"
import { PromotionsApi } from "../../api/clients/PromotionApiClient"
import { ProductsApi } from "../../api/clients/ProductApiClient"
import { PromotionReward, type Promotion, promotionOfferLabel } from "../shared/types/Promotion"
import { PRODUCT_IMAGE_FALLBACK, type Product } from "../shared/types/Product"
import { useAuth } from "../../context/AuthContext"
import EmptyState from "../common/EmptyState"

type PromoFilter = "active" | "all"

const DISPLAY = '"Anton", system-ui, sans-serif'

// Staggered page-load reveal.
const rise = keyframes`
  from { opacity: 0; transform: translateY(18px); }
  to { opacity: 1; transform: translateY(0); }
`

// Metallic shine sweep across a card's accent bar on hover.
const shine = keyframes`
  from { background-position: 200% 0; }
  to { background-position: -120% 0; }
`

// Broadcast ticker + product ribbon both ride this.
const marquee = keyframes`
  from { transform: translateX(0); }
  to { transform: translateX(-50%); }
`

// Spinning ball loader.
const spin = keyframes`
  to { transform: rotate(360deg); }
`

// Hand-rolled grain so the hero reads as printed/broadcast, not a flat fill.
const GRAIN =
    "url(\"data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='140' height='140'%3E%3Cfilter id='n'%3E%3CfeTurbulence type='fractalNoise' baseFrequency='0.85' numOctaves='2' stitchTiles='stitch'/%3E%3C/filter%3E%3Crect width='100%25' height='100%25' filter='url(%23n)'/%3E%3C/svg%3E\")"

function reveal(delayMs: number) {
    return {
        animation: `${rise} 0.7s cubic-bezier(0.16,1,0.3,1) both`,
        animationDelay: `${delayMs}ms`,
    }
}

/** Counts up to `value` on mount / when it changes — scoreboard flourish. */
function CountUp({ value, duration = 1000 }: { value: number; duration?: number }) {
    const [n, setN] = useState(0)
    useEffect(() => {
        let raf = 0
        const start = performance.now()
        const tick = (now: number) => {
            const t = Math.min(1, (now - start) / duration)
            const eased = 1 - Math.pow(1 - t, 3)
            setN(Math.round(eased * value))
            if (t < 1) raf = requestAnimationFrame(tick)
        }
        raf = requestAnimationFrame(tick)
        return () => cancelAnimationFrame(raf)
    }, [value, duration])
    return <>{n}</>
}

function Home() {
    const navigate = useNavigate()
    const theme = useTheme()
    const isDark = theme.palette.mode === "dark"
    const { isAdmin } = useAuth()
    const [promotions, setPromotions] = useState<Promotion[]>([])
    const [products, setProducts] = useState<Product[]>([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState("")
    const [filter, setFilter] = useState<PromoFilter>("active")

    useEffect(() => {
        PromotionsApi.getAll({ pageSize: 50 })
            .then((data) => {
                setPromotions(data.items)
                setError("")
            })
            .catch((err) => setError((err as Error).message))
            .finally(() => setLoading(false))

        ProductsApi.getAll({ pageSize: 100 })
            .then((data) => setProducts(data.items))
            .catch(() => {})
    }, [])

    const activeCount = promotions.filter((p) => p.isActive).length
    const visiblePromotions =
        filter === "active" ? promotions.filter((p) => p.isActive) : promotions

    /** The product photo to show for a deal: its exact product, else a member of its category. */
    function promoImage(promo: Promotion): string {
        if (promo.productId !== null) {
            const p = products.find((x) => x.id === promo.productId)
            if (p?.imageUrl) return p.imageUrl
        }
        if (promo.categoryId !== null) {
            const p = products.find(
                (x) => x.imageUrl && x.categories.some((c) => c.id === promo.categoryId),
            )
            if (p?.imageUrl) return p.imageUrl
        }
        return PRODUCT_IMAGE_FALLBACK
    }

    function rewardBadge(promo: Promotion) {
        return promo.reward === PromotionReward.PercentDiscount
            ? { big: String(promo.rewardValue), unit: "%" }
            : { big: String(promo.rewardValue), unit: "FREE" }
    }

    const ribbon = products.filter((p) => p.imageUrl)
    const ticker = promotions
        .filter((p) => p.isActive)
        .map((p) => `${p.name} — ${promotionOfferLabel(p)}`)

    // "Stadium night" on dark, "matchday afternoon" cream/gold on light.
    const ink = isDark ? "#F4F3EA" : "#2C2C1F"
    const muted = isDark ? "rgba(244,243,234,0.62)" : "#787868"
    const tile = isDark ? "#23231A" : "#FFFFFF"
    const tileBorder = isDark ? "#36362A" : "#E7E5CF"
    const pitchStripes = isDark
        ? "repeating-linear-gradient(115deg, rgba(255,255,255,0.022) 0 46px, transparent 46px 92px)"
        : "repeating-linear-gradient(115deg, rgba(63,63,46,0.03) 0 46px, transparent 46px 92px)"
    const heroBackground = isDark
        ? "radial-gradient(120% 90% at 50% -20%, rgba(200,192,0,0.22), transparent 55%), linear-gradient(180deg, #20201A 0%, #15150E 100%)"
        : "radial-gradient(120% 90% at 50% -20%, rgba(200,192,0,0.26), transparent 60%), linear-gradient(180deg, #FFFDF0 0%, #F2F0DA 100%)"
    const edgeMask =
        "linear-gradient(90deg, transparent 0, #000 6%, #000 94%, transparent 100%)"

    const stats = [
        { value: 48, label: "Nations" },
        { value: promotions.length, label: "Deals" },
        { value: activeCount, label: "Live now" },
    ]

    return (
        <Box sx={{ maxWidth: 1600, mx: "auto", px: { xs: 2, sm: 3, md: 5 }, py: { xs: 3, md: 5 } }}>
            {/* ── Broadcast ticker ─────────────────────────────────── */}
            {ticker.length > 0 && (
                <Box
                    sx={{
                        mb: { xs: 3, md: 4 },
                        borderRadius: 9999,
                        bgcolor: "#1C1C14",
                        border: "1px solid #3A3A28",
                        overflow: "hidden",
                        display: "flex",
                        alignItems: "center",
                    }}
                >
                    <Box
                        sx={{
                            flexShrink: 0,
                            display: "flex",
                            alignItems: "center",
                            gap: 0.75,
                            px: 2,
                            py: 1,
                            bgcolor: "primary.main",
                            color: "#1C1C14",
                            fontFamily: DISPLAY,
                            fontSize: "0.72rem",
                            letterSpacing: "0.18em",
                            textTransform: "uppercase",
                        }}
                    >
                        <Box
                            sx={{
                                width: 7,
                                height: 7,
                                borderRadius: "50%",
                                bgcolor: "#1C1C14",
                                animation: `${spin} 1.6s steps(2) infinite`,
                                opacity: 0.85,
                            }}
                        />
                        Live
                    </Box>
                    <Box sx={{ overflow: "hidden", flexGrow: 1, maskImage: edgeMask, WebkitMaskImage: edgeMask }}>
                        <Box
                            sx={{
                                display: "flex",
                                width: "max-content",
                                animation: `${marquee} 32s linear infinite`,
                                "&:hover": { animationPlayState: "paused" },
                            }}
                        >
                            {[...ticker, ...ticker].map((text, i) => (
                                <Box
                                    key={i}
                                    sx={{
                                        display: "flex",
                                        alignItems: "center",
                                        gap: 1.25,
                                        px: 2.5,
                                        py: 1,
                                        whiteSpace: "nowrap",
                                        color: "rgba(249,248,242,0.85)",
                                        fontSize: "0.82rem",
                                    }}
                                >
                                    <SportsSoccerIcon sx={{ fontSize: 14, color: "primary.main" }} />
                                    {text}
                                </Box>
                            ))}
                        </Box>
                    </Box>
                </Box>
            )}

            {/* ── Hero ─────────────────────────────────────────────── */}
            <Box
                sx={{
                    position: "relative",
                    isolation: "isolate",
                    overflow: "hidden",
                    borderRadius: 4,
                    border: `1px solid ${tileBorder}`,
                    background: heroBackground,
                    color: ink,
                    px: { xs: 3, md: 7 },
                    py: { xs: 6, md: 11 },
                    mb: { xs: 4, md: 5 },
                    "&::before": {
                        content: '""',
                        position: "absolute",
                        inset: 0,
                        zIndex: 0,
                        background: pitchStripes,
                        pointerEvents: "none",
                    },
                    "&::after": {
                        content: '""',
                        position: "absolute",
                        inset: 0,
                        zIndex: 0,
                        backgroundImage: GRAIN,
                        opacity: isDark ? 0.4 : 0.5,
                        mixBlendMode: isDark ? "overlay" : "multiply",
                        pointerEvents: "none",
                    },
                }}
            >
                <Box
                    aria-hidden
                    sx={{
                        position: "absolute",
                        top: { xs: -28, md: -64 },
                        right: { xs: -10, md: 24 },
                        zIndex: 0,
                        fontFamily: DISPLAY,
                        fontSize: { xs: "9rem", md: "20rem" },
                        lineHeight: 0.8,
                        letterSpacing: "-0.04em",
                        color: "primary.main",
                        opacity: isDark ? 0.07 : 0.08,
                        pointerEvents: "none",
                        userSelect: "none",
                    }}
                >
                    26
                </Box>

                <Box sx={{ position: "relative", zIndex: 1, textAlign: "center" }}>
                    <Box
                        sx={{
                            ...reveal(0),
                            display: "inline-flex",
                            alignItems: "center",
                            gap: 1,
                            mb: { xs: 3, md: 4 },
                        }}
                    >
                        <Box sx={{ width: 22, height: 3, bgcolor: "primary.main", borderRadius: 2 }} />
                        <Typography
                            sx={{
                                fontFamily: DISPLAY,
                                fontSize: "0.8rem",
                                letterSpacing: "0.32em",
                                textTransform: "uppercase",
                                color: isDark ? "primary.main" : "primary.dark",
                            }}
                        >
                            FIFA World Cup 2026
                        </Typography>
                        <Box sx={{ width: 22, height: 3, bgcolor: "primary.main", borderRadius: 2 }} />
                    </Box>

                    <Typography
                        component="h1"
                        sx={{
                            ...reveal(90),
                            fontFamily: DISPLAY,
                            fontWeight: 400,
                            textTransform: "uppercase",
                            lineHeight: 0.92,
                            letterSpacing: { xs: "-0.01em", md: "-0.015em" },
                            fontSize: { xs: "2.9rem", sm: "4rem", md: "5.5rem" },
                            mb: { xs: 2.5, md: 3 },
                        }}
                    >
                        Smart shopping,
                        <br />
                        <Box
                            component="span"
                            sx={{
                                color: "primary.main",
                                textShadow: isDark ? "0 0 40px rgba(200,192,0,0.35)" : "none",
                            }}
                        >
                            championship savings.
                        </Box>
                    </Typography>

                    <Typography
                        sx={{
                            ...reveal(180),
                            maxWidth: 540,
                            mx: "auto",
                            mb: { xs: 4, md: 4.5 },
                            color: muted,
                            fontSize: "1.0625rem",
                            lineHeight: 1.55,
                        }}
                    >
                        Gear up for the tournament. Browse the catalogue, fill your cart, and let
                        live promotions and our AI assistant find the best deals for you.
                    </Typography>

                    <Stack
                        direction={{ xs: "column", sm: "row" }}
                        spacing={1.5}
                        sx={{
                            ...reveal(270),
                            width: "100%",
                            justifyContent: "center",
                            alignItems: "center",
                        }}
                    >
                        <Button
                            variant="contained"
                            size="large"
                            startIcon={<StorefrontIcon />}
                            onClick={() => navigate("/shop")}
                        >
                            Shop now
                        </Button>
                        <Button
                            variant="outlined"
                            size="large"
                            startIcon={<SportsSoccerIcon />}
                            href="#deals"
                            sx={{
                                color: ink,
                                borderColor: isDark ? "rgba(244,243,234,0.3)" : "rgba(63,63,46,0.28)",
                                "&:hover": {
                                    borderColor: isDark ? "#F4F3EA" : "primary.dark",
                                    bgcolor: isDark ? "rgba(244,243,234,0.06)" : "rgba(63,63,46,0.04)",
                                },
                            }}
                        >
                            View deals
                        </Button>
                    </Stack>

                    <Box
                        sx={{
                            ...reveal(360),
                            mt: { xs: 5, md: 7 },
                            display: "inline-flex",
                            alignItems: "stretch",
                            borderTop: `1px solid ${isDark ? "rgba(244,243,234,0.14)" : "rgba(63,63,46,0.12)"}`,
                            borderBottom: `1px solid ${isDark ? "rgba(244,243,234,0.14)" : "rgba(63,63,46,0.12)"}`,
                            py: 2,
                        }}
                    >
                        {stats.map((stat, i) => (
                            <Box
                                key={stat.label}
                                sx={{
                                    px: { xs: 2.5, sm: 4.5 },
                                    textAlign: "center",
                                    borderLeft:
                                        i === 0
                                            ? "none"
                                            : `1px solid ${isDark ? "rgba(244,243,234,0.12)" : "rgba(63,63,46,0.1)"}`,
                                }}
                            >
                                <Typography
                                    sx={{
                                        fontFamily: DISPLAY,
                                        fontSize: { xs: "2.4rem", md: "3.25rem" },
                                        lineHeight: 0.95,
                                        color: "primary.main",
                                        fontVariantNumeric: "tabular-nums",
                                    }}
                                >
                                    <CountUp value={stat.value} />
                                </Typography>
                                <Typography
                                    sx={{
                                        fontSize: "0.7rem",
                                        letterSpacing: "0.22em",
                                        textTransform: "uppercase",
                                        color: muted,
                                        mt: 0.5,
                                    }}
                                >
                                    {stat.label}
                                </Typography>
                            </Box>
                        ))}
                    </Box>
                </Box>
            </Box>

            {/* ── Product ribbon ───────────────────────────────────── */}
            {ribbon.length > 0 && (
                <Box
                    sx={{
                        mb: { xs: 5, md: 7 },
                        overflow: "hidden",
                        maskImage: edgeMask,
                        WebkitMaskImage: edgeMask,
                    }}
                >
                    <Box
                        sx={{
                            display: "flex",
                            gap: 2,
                            width: "max-content",
                            animation: `${marquee} 55s linear infinite`,
                            "&:hover": { animationPlayState: "paused" },
                        }}
                    >
                        {[...ribbon, ...ribbon].map((p, i) => (
                            <Box
                                key={`${p.id}-${i}`}
                                onClick={() => navigate(`/product/${p.id}`)}
                                sx={{
                                    flex: "0 0 auto",
                                    width: 132,
                                    cursor: "pointer",
                                    transition: "transform 0.2s ease",
                                    "&:hover": { transform: "translateY(-4px)" },
                                }}
                            >
                                <Box
                                    component="img"
                                    src={p.imageUrl}
                                    alt={p.name}
                                    loading="lazy"
                                    onError={(e) => {
                                        e.currentTarget.onerror = null
                                        e.currentTarget.src = PRODUCT_IMAGE_FALLBACK
                                    }}
                                    sx={{
                                        width: 132,
                                        height: 168,
                                        objectFit: "cover",
                                        borderRadius: 2,
                                        border: `1px solid ${tileBorder}`,
                                        bgcolor: tile,
                                        display: "block",
                                    }}
                                />
                            </Box>
                        ))}
                    </Box>
                </Box>
            )}

            {/* ── Deals ────────────────────────────────────────────── */}
            <Box
                id="deals"
                sx={{
                    display: "flex",
                    alignItems: "flex-end",
                    justifyContent: "space-between",
                    gap: 2,
                    mb: 3,
                    scrollMarginTop: 16,
                }}
            >
                <Box sx={{ display: "flex", alignItems: "stretch", gap: 1.5 }}>
                    <Box sx={{ width: 4, borderRadius: 2, bgcolor: "primary.main" }} />
                    <Box>
                        <Typography
                            sx={{
                                fontFamily: DISPLAY,
                                fontSize: "0.72rem",
                                letterSpacing: "0.28em",
                                textTransform: "uppercase",
                                color: "primary.main",
                            }}
                        >
                            Matchday deals
                        </Typography>
                        <Typography variant="h4" sx={{ mt: 0.25 }}>
                            Available promotions
                        </Typography>
                    </Box>
                </Box>
                {isAdmin() && (
                    <ToggleButtonGroup
                        size="small"
                        exclusive
                        value={filter}
                        onChange={(_, val) => val && setFilter(val)}
                    >
                        <ToggleButton value="active">Active</ToggleButton>
                        <ToggleButton value="all">All</ToggleButton>
                    </ToggleButtonGroup>
                )}
            </Box>

            {error !== "" && (
                <Alert severity="error" sx={{ mb: 2 }}>
                    {error}
                </Alert>
            )}

            {loading ? (
                <Box sx={{ display: "flex", justifyContent: "center", mt: 6 }}>
                    <SportsSoccerIcon
                        sx={{ fontSize: 44, color: "primary.main", animation: `${spin} 1.1s linear infinite` }}
                    />
                </Box>
            ) : visiblePromotions.length === 0 ? (
                <EmptyState message="No promotions available right now. Check back soon!" />
            ) : (
                <Box
                    sx={{
                        display: "grid",
                        gap: 2,
                        gridTemplateColumns: "repeat(auto-fill, minmax(264px, 1fr))",
                    }}
                >
                    {visiblePromotions.map((promotion) => {
                        const badge = rewardBadge(promotion)
                        return (
                            <Card
                                key={promotion.id}
                                sx={{
                                    display: "flex",
                                    flexDirection: "column",
                                    overflow: "hidden",
                                    position: "relative",
                                    transition: "transform 0.18s ease, box-shadow 0.18s ease",
                                    "&:hover": {
                                        transform: "translateY(-4px)",
                                        boxShadow: isDark
                                            ? "0 14px 32px rgba(0,0,0,0.5)"
                                            : "0 14px 32px rgba(44,44,31,0.13)",
                                    },
                                    "&:hover .deal-holo": { opacity: 1 },
                                    "&::before": {
                                        content: '""',
                                        position: "absolute",
                                        top: 0,
                                        left: 0,
                                        right: 0,
                                        height: 4,
                                        zIndex: 3,
                                        backgroundImage:
                                            "linear-gradient(90deg,#8f8a00,#C8C000 35%,#fff4a0 50%,#C8C000 65%,#8f8a00)",
                                        backgroundSize: "220% 100%",
                                        backgroundPosition: "200% 0",
                                    },
                                    "&:hover::before": {
                                        animation: `${shine} 1.1s ease-out`,
                                        backgroundPosition: "0 0",
                                    },
                                }}
                            >
                                {/* sticker photo */}
                                <Box sx={{ position: "relative" }}>
                                    <CardMedia
                                        component="img"
                                        height="156"
                                        image={promoImage(promotion)}
                                        alt={promotion.name}
                                        sx={{ objectFit: "cover" }}
                                        onError={(e) => {
                                            e.currentTarget.onerror = null
                                            e.currentTarget.src = PRODUCT_IMAGE_FALLBACK
                                        }}
                                    />
                                    {/* holographic sheen on hover */}
                                    <Box
                                        className="deal-holo"
                                        sx={{
                                            position: "absolute",
                                            inset: 0,
                                            opacity: 0,
                                            transition: "opacity 0.25s ease",
                                            pointerEvents: "none",
                                            background:
                                                "linear-gradient(115deg, transparent 30%, rgba(255,244,160,0.35) 47%, rgba(200,192,0,0.15) 53%, transparent 70%)",
                                        }}
                                    />
                                    {/* reward number — jersey/sticker stat */}
                                    <Box
                                        sx={{
                                            position: "absolute",
                                            left: 10,
                                            bottom: 10,
                                            display: "flex",
                                            alignItems: "baseline",
                                            gap: 0.5,
                                            px: 1.25,
                                            py: 0.5,
                                            borderRadius: 2,
                                            bgcolor: "rgba(28,28,20,0.82)",
                                            backdropFilter: "blur(2px)",
                                            color: "#F4F3EA",
                                            boxShadow: "0 0 0 1px rgba(200,192,0,0.4)",
                                        }}
                                    >
                                        <Typography
                                            component="span"
                                            sx={{
                                                fontFamily: DISPLAY,
                                                fontSize: "1.85rem",
                                                lineHeight: 0.9,
                                                color: "primary.main",
                                            }}
                                        >
                                            {badge.big}
                                        </Typography>
                                        <Typography
                                            component="span"
                                            sx={{
                                                fontFamily: DISPLAY,
                                                fontSize: "0.7rem",
                                                letterSpacing: "0.08em",
                                            }}
                                        >
                                            {badge.unit}
                                        </Typography>
                                    </Box>
                                </Box>

                                <CardContent sx={{ flexGrow: 1 }}>
                                    <Box
                                        sx={{
                                            display: "flex",
                                            justifyContent: "space-between",
                                            alignItems: "start",
                                            gap: 1,
                                            mb: 1,
                                        }}
                                    >
                                        <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                                            <SportsSoccerIcon sx={{ fontSize: 18, color: "primary.main" }} />
                                            <Typography variant="h6">{promotion.name}</Typography>
                                        </Box>
                                        <Chip
                                            label={
                                                promotion.reward === PromotionReward.PercentDiscount
                                                    ? "Discount"
                                                    : "Free items"
                                            }
                                            color="primary"
                                            size="small"
                                        />
                                    </Box>
                                    <Typography variant="body2" color="text.secondary">
                                        {promotionOfferLabel(promotion)}
                                    </Typography>
                                    {isAdmin() && !promotion.isActive && (
                                        <Chip label="Inactive" size="small" sx={{ mt: 1.5 }} />
                                    )}
                                </CardContent>
                            </Card>
                        )
                    })}
                </Box>
            )}
        </Box>
    )
}

export default Home
