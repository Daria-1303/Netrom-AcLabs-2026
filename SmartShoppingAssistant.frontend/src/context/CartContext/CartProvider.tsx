import { useEffect, useState, type ReactNode } from "react"
import type { Cart } from "../../components/shared/types/Cart"
import { cartApi } from "../../api/clients/CartApiClient"
import { CartContext } from "./cart-context"
import { useAuth } from "../AuthContext"

function CartProvider({ children }: { children: ReactNode }) {
    const { user } = useAuth()
    const [cart, setCart] = useState<Cart | null>(null)
    const [open, setOpen] = useState(false)

    const loadCart = () => {
        cartApi.get().then(setCart).catch(() => {})
    }

    async function addItem(productId: number, quantity: number) {
        await cartApi.addItem({ productId, quantity })
        loadCart()
    }

    async function updateQuantity(itemId: number, quantity: number) {
        await cartApi.updateItem(itemId, { quantity })
        loadCart()
    }

    async function removeProduct(itemId: number) {
        await cartApi.removeItem(itemId)
        loadCart()
    }

    useEffect(() => {
        if (user) {
            loadCart()
        } else {
            setCart(null)
        }
    }, [user])

    return (
        <CartContext.Provider
            value={{
                cart,
                open,
                openCart: () => setOpen(true),
                closeCart: () => setOpen(false),
                addItem,
                updateQuantity,
                removeProduct,
            }}
        >
            {children}
        </CartContext.Provider>
    )
}

export default CartProvider
