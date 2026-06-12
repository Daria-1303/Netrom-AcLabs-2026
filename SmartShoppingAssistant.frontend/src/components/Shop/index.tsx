import {
    Alert,
    Box,
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Checkbox,
    CircularProgress,
    Container,
    Divider,
    FormControl,
    FormControlLabel,
    FormGroup,
    InputLabel,
    MenuItem,
    Select,
    Slider,
    TextField,
    Typography,
} from "@mui/material"
import { useEffect, useMemo, useState } from "react"
import { ProductsApi } from "../../api/clients/ProductApiClient"
import { CategoriesApi } from "../../api/clients/CategoryApiClient"
import type { Product } from "../shared/types/Product"
import type { Category } from "../shared/types/Category"
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart'
import { useCart } from "../../context/CartContext/cart-context"

type SortOption = 'price-asc' | 'price-desc' | 'name-asc' | 'name-desc'

function Shop() {
    const [products, setProducts] = useState<Product[]>([])
    const [categories, setCategories] = useState<Category[]>([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState("")
    const [search, setSearch] = useState("")
    const [sortBy, setSortBy] = useState<SortOption>('price-asc')
    const [selectedCategories, setSelectedCategories] = useState<Set<number>>(new Set())
    const [priceRange, setPriceRange] = useState<[number, number]>([0, 10000])
    const [globalPriceRange, setGlobalPriceRange] = useState<[number, number]>([0, 10000])
    const { addItem } = useCart()

    useEffect(() => {
        ProductsApi.getAll({ pageSize: 100 })
            .then((data) => {
                setProducts(data.items)
                if (data.items.length > 0) {
                    const prices = data.items.map((p) => p.price)
                    const min = Math.floor(Math.min(...prices))
                    const max = Math.ceil(Math.max(...prices))
                    setGlobalPriceRange([min, max])
                    setPriceRange([min, max])
                }
                setError("")
            })
            .catch((err) => setError((err as Error).message))
            .finally(() => setLoading(false))

        CategoriesApi.getAll({ pageSize: 100 }).then((r) => setCategories(r.items))
    }, [])

    function toggleCategory(id: number) {
        setSelectedCategories((prev) => {
            const next = new Set(prev)
            if (next.has(id)) next.delete(id)
            else next.add(id)
            return next
        })
    }

    const visibleProducts = useMemo(() => {
        const filtered = products.filter((p) => {
            const matchesSearch = p.name
                .toLocaleLowerCase()
                .includes(search.trim().toLocaleLowerCase())
            const matchesCategory =
                selectedCategories.size === 0 ||
                p.categories.some((c) => selectedCategories.has(c.id))
            const matchesPrice = p.price >= priceRange[0] && p.price <= priceRange[1]
            return matchesSearch && matchesCategory && matchesPrice
        })

        return [...filtered].sort((a, b) => {
            switch (sortBy) {
                case 'price-asc': return a.price - b.price
                case 'price-desc': return b.price - a.price
                case 'name-asc': return a.name.localeCompare(b.name)
                case 'name-desc': return b.name.localeCompare(a.name)
            }
        })
    }, [products, search, selectedCategories, priceRange, sortBy])

    return (
        <Container maxWidth="xl" sx={{ py: 4 }}>
            {error !== "" && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}

            <Box sx={{ display: 'flex', gap: 3 }}>
                {/* Sidebar filtre */}
                <Box sx={{ width: 220, flexShrink: 0 }}>
                    <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>Filters</Typography>
                    <Divider sx={{ my: 1 }} />

                    <Typography variant="body2" color="text.secondary" sx={{ mb: 0.5 }}>
                        Categories
                    </Typography>
                    <FormGroup>
                        {categories.map((cat) => (
                            <FormControlLabel
                                key={cat.id}
                                control={
                                    <Checkbox
                                        size="small"
                                        checked={selectedCategories.has(cat.id)}
                                        onChange={() => toggleCategory(cat.id)}
                                    />
                                }
                                label={<Typography variant="body2">{cat.name}</Typography>}
                            />
                        ))}
                    </FormGroup>

                    <Divider sx={{ my: 2 }} />

                    <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
                        Price
                    </Typography>
                    <Box sx={{ px: 1 }}>
                        <Slider
                            value={priceRange}
                            min={globalPriceRange[0]}
                            max={globalPriceRange[1]}
                            onChange={(_, val) => setPriceRange(val as [number, number])}
                            valueLabelDisplay="off"
                        />
                        <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                            <Typography variant="caption">{priceRange[0]} RON</Typography>
                            <Typography variant="caption">{priceRange[1]} RON</Typography>
                        </Box>
                    </Box>
                </Box>

                {/* Continut principal */}
                <Box sx={{ flexGrow: 1 }}>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
                        <Typography variant="h4">Shop</Typography>
                        <FormControl size="small" sx={{ width: 200 }}>
                            <InputLabel>Sort by</InputLabel>
                            <Select
                                value={sortBy}
                                label="Sort by"
                                onChange={(e) => setSortBy(e.target.value as SortOption)}
                            >
                                <MenuItem value="price-asc">Price: Low to High</MenuItem>
                                <MenuItem value="price-desc">Price: High to Low</MenuItem>
                                <MenuItem value="name-asc">Name: A to Z</MenuItem>
                                <MenuItem value="name-desc">Name: Z to A</MenuItem>
                            </Select>
                        </FormControl>
                    </Box>

                    <TextField
                        label="Search products"
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                        fullWidth
                        sx={{ mb: 2 }}
                    />

                    {loading ? (
                        <Box sx={{ display: "flex", justifyContent: "center", mt: 4 }}>
                            <CircularProgress />
                        </Box>
                    ) : (
                        <Box
                            sx={{
                                display: 'grid',
                                gap: 2,
                                alignContent: 'start',
                                gridTemplateColumns: 'repeat(auto-fill, minmax(240px, 1fr))',
                            }}
                        >
                            {visibleProducts.map((product) => (
                                <Card key={product.id} sx={{ display: 'flex', flexDirection: 'column' }}>
                                    <CardMedia
                                        component="img"
                                        height="160"
                                        image={product.imageUrl}
                                        alt={product.name}
                                        sx={{ objectFit: 'cover' }}
                                    />
                                    <CardContent sx={{ flexGrow: 1 }}>
                                        <Typography variant="h6">{product.name}</Typography>
                                        <Typography variant="body2" color="textSecondary">
                                            {product.description}
                                        </Typography>
                                        <Typography variant="subtitle1" sx={{ pt: 1 }}>
                                            {product.priceLabel}
                                        </Typography>
                                    </CardContent>
                                    <CardActions>
                                        <Button
                                            fullWidth
                                            variant="contained"
                                            startIcon={<AddShoppingCartIcon />}
                                            onClick={() => addItem(product.id, 1)}
                                        >
                                            Add to Cart
                                        </Button>
                                    </CardActions>
                                </Card>
                            ))}
                            {visibleProducts.length === 0 && (
                                <Typography>No products found.</Typography>
                            )}
                        </Box>
                    )}
                </Box>
            </Box>
        </Container>
    )
}

export default Shop
