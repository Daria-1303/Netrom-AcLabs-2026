# Sesiunea 04 — Cart, Shop, Bug Fixes, Protecție rute

Această sesiune a adus 4 lucruri principale:
1. **Bug fixes** la autentificare
2. **Pagina Shop** cu filtre și sortare
3. **Sistemul de Cart** (context, API client, drawer)
4. **Protecția rutelor** pe roluri (admin-only)

---

## 1. Bug fix — Login infinite refresh loop

### Problema

`CartProvider` apelează `cartApi.get()` la mount — adică imediat ce aplicația pornește, înainte ca userul să se fi logat. Dacă tokenul nu există, backend-ul răspunde cu **401**.

Interceptorul din `http.ts` la 401 făcea:
```typescript
if (error.response?.status === 401) {
    localStorage.removeItem(STORAGE_KEY)
    window.location.href = '/login'    // redirect forțat
}
```

Pe pagina `/login`, `CartProvider` era deja montat (e în `App.tsx`, deasupra oricăror rute). La mount apela din nou `cartApi.get()` → 401 → redirect la `/login` → remount → 401 → loop infinit.

### Fix

```typescript
// src/api/base/http.ts
if (error.response?.status === 401) {
    localStorage.removeItem(STORAGE_KEY)
    if (window.location.pathname !== '/login') {   // ← adăugat
        window.location.href = '/login'
    }
}
```

Dacă suntem deja pe `/login`, nu mai facem redirect. Loop-ul se oprește.

---

## 2. Bug fix — Al doilea login eșuează după logout

### Problema

`logout()` în `AuthContext` arăta inițial:

```typescript
function logout() {
    setUser(null)                         // React state async
    window.location.href = '/login'       // redirect imediat
}
```

`setUser(null)` declanșează un re-render React care ulterior rulează `useEffect` care șterge tokenul din `localStorage`. Dar `window.location.href = '/login'` provoacă o reîncărcare completă a paginii **înainte** ca `useEffect` să ruleze. Tokenul rămânea în `localStorage`.

La al doilea login: `CartProvider` pornea și trimitea `cartApi.get()` cu tokenul vechi (invalid/expirat) → 401 → redirect → loop.

### Fix

```typescript
// src/context/AuthContext.tsx
function logout() {
    localStorage.removeItem(STORAGE_KEY)   // ← sincronic, înainte de redirect
    setUser(null)
    window.location.href = '/login'
}
```

`localStorage.removeItem` e sincronic — se execută garantat înainte de redirect.

---

## 3. Bug fix — Cart-ul nu se încărca niciodată

### Problema — CartProvider nu reîncărcă după login

`CartProvider` folosea:
```typescript
useEffect(() => {
    loadCart()
}, [])   // rulează o singură dată la mount
```

Secvența care producea bug-ul:
1. App pornește → `CartProvider` se montează → `loadCart()` → 401 (user nelogat) → `.catch(() => {})` → `cart = null`
2. User se loghează → `user` în AuthContext se schimbă
3. **`CartProvider` nu știe că user-ul s-a logat** — `useEffect([])` nu re-rulează
4. `cart` rămâne `null` pentru toată sesiunea
5. Orice apel `addItem` funcționa pe backend (produsele se adăugau în DB), dar UI-ul nu se actualiza

### Fix

```typescript
// src/context/CartContext/CartProvider.tsx
import { useAuth } from "../AuthContext"

function CartProvider({ children }) {
    const { user } = useAuth()   // ← asculta schimbări de auth
    
    useEffect(() => {
        if (user) {
            loadCart()     // user tocmai s-a logat → încarcă coșul
        } else {
            setCart(null)  // user s-a delogat → golește coșul din UI
        }
    }, [user])   // ← rulează când user se schimbă
}
```

Acum:
- La login → `user` se schimbă din `null` în obiect → coșul se încarcă automat
- La logout → `user` devine `null` → coșul se golește din UI
- La refresh cu token valid → `user` e setat imediat din `localStorage` → coșul se încarcă

---

### Problema — Mismatch între tipurile frontend și backend

`CartModel.ts` (frontend) definea câmpurile cu nume diferite față de ce trimite backend-ul:

| Backend JSON | Frontend `CartModel.ts` (greșit) | Efect |
|---|---|---|
| `itemTypeTotal` (pe CartItem) | `subtotal` | `undefined` |
| `discount` (pe CartGetDTO) | `totalDiscount` | `undefined` |
| lipsă `promotionId` | `promotionId` | `undefined` |

Când `toCartModel()` apela `money(item.subtotal)`:
```typescript
function money(value: number): string {
    return `${value.toFixed(2)} RON`   // undefined.toFixed(2) → TypeError!
}
```

Eroarea era prinsă de `.catch(() => {})` din `loadCart()` și înghițită silențios. `cart` rămânea `null`.

### Fix

```typescript
// src/api/models/CartModel.ts — reflectă exact câmpurile din backend
export interface CartItem {
    id: number
    productId: number
    productName: string
    unitPrice: number
    quantity: number
    itemTypeTotal: number   // ← era: subtotal
}

export interface CartModel {
    items: CartItem[]
    subtotal: number
    appliedPromotions: AppliedPromotion[]
    discount: number        // ← era: totalDiscount
    total: number
}

// AppliedPromotion — promotionId eliminat (nu există în backend DTO)
export interface AppliedPromotion {
    promotionName: string
    discount: number
}
```

```typescript
// src/components/shared/types/Cart.ts — mapping corectat
export function toCartModel(dto: CartModel): Cart {
    return {
        items: dto.items.map((item) => ({
            ...
            subtotal: item.itemTypeTotal,         // ← citit din câmpul corect
            subtotalLabel: money(item.itemTypeTotal),
        })),
        totalDiscount: dto.discount,              // ← câmpul corect
        totalDiscountLabel: money(dto.discount),
        appliedPromotions: dto.appliedPromotions.map((p, index) => ({
            promotionId: index,                   // ← fallback pentru React key
            promotionName: p.promotionName,
            discount: p.discount,
            discountLabel: money(p.discount),
        })),
        ...
    }
}
```

---

## 4. Sistemul de Cart — arhitectură

### Fișiere noi

| Fișier | Rol |
|---|---|
| `src/api/models/CartModel.ts` | Tipuri care reflectă JSON-ul de la backend |
| `src/api/clients/CartApiClient.ts` | `get`, `addItem`, `updateItem`, `removeItem` |
| `src/components/shared/types/Cart.ts` | Tipuri UI (`Cart`, `CartItem`) + `toCartModel()` |
| `src/context/CartContext/cart-context.ts` | Interfața contextului + `useCart()` hook |
| `src/context/CartContext/CartProvider.tsx` | Provider — state, funcții, legătură cu auth |
| `src/components/CartDrawer/index.tsx` | Drawer lateral cu conținut coș |

### Fluxul de date — adaugă produs în coș

```
[Shop Page]
  User click "Add to Cart"
        ↓
  addItem(productId, 1)     ← din useCart()
        ↓
[CartProvider]
  cartApi.addItem({ productId, quantity: 1 })
        ↓
[CartApiClient]
  POST http://localhost:5221/api/cart/items
  { productId: 5, quantity: 1 }
  Header: Authorization: Bearer <token>
        ↓
[Backend — CartController]
  UserId extras din JWT token (nu din body)
  cartService.AddItemAsync(dto, userId)
        ↓
[Backend — CartService.AddItemAsync]
  Caută item existent cu același productId și userId
  ├── dacă există → UPDATE quantity += 1
  └── dacă nu există → INSERT nou CartItem
        ↓
[CartProvider — după await]
  loadCart()  →  GET /api/cart
        ↓
[CartContext state]
  setCart(nouldCart)  →  React re-renderizează
        ↓
[Navbar]
  badge = cart.itemCount  (suma tuturor quantity-urilor)
```

### Fluxul de date — încărcare coș la login

```
[AuthContext]
  login(userData) → setUser(userData)
        ↓  (user state se schimbă)
[CartProvider — useEffect([user])]
  user !== null → loadCart()
        ↓
[CartApiClient]
  GET /api/cart
        ↓
[Backend]
  SELECT CartItems WHERE UserId = <userId>
  aplicare promoții → calculare total
        ↓
[toCartModel(dto)]
  mapare DTO → Cart
  itemCount = items.reduce(sum + quantity, 0)
        ↓
[CartContext]
  setCart(cart)
        ↓
[Navbar badge, CartDrawer]
  re-render cu datele reale
```

---

## 5. Pagina Shop — filtre și sortare

### Fișier nou: `src/components/Shop/index.tsx`

Shop-ul are un sidebar fix de 220px și o zonă principală cu grid.

### Filtre disponibile

| Filtru | Tip | Comportament |
|---|---|---|
| Categorii | Checkboxuri | un produs apare dacă are cel puțin una din categoriile selectate |
| Interval preț | Slider MUI | `min` și `max` se calculează dinamic din produsele încărcate |
| Căutare text | TextField | filtrare case-insensitive pe `product.name` |
| Sortare | Select dropdown | 4 opțiuni: preț crescător/descrescător, nume A-Z/Z-A |

### Cum se calculează produsele afișate

Toate filtrele se aplică împreună cu `useMemo` — recalculat doar când se schimbă produsele sau un filtru:

```typescript
const visibleProducts = useMemo(() => {
    const filtered = products.filter((p) => {
        const matchesSearch = p.name
            .toLocaleLowerCase()
            .includes(search.trim().toLocaleLowerCase())

        const matchesCategory =
            selectedCategories.size === 0 ||   // dacă nicio categorie selectată → toate
            p.categories.some((c) => selectedCategories.has(c.id))

        const matchesPrice =
            p.price >= priceRange[0] && p.price <= priceRange[1]

        return matchesSearch && matchesCategory && matchesPrice
    })

    return [...filtered].sort((a, b) => {
        switch (sortBy) {
            case 'price-asc':  return a.price - b.price
            case 'price-desc': return b.price - a.price
            case 'name-asc':   return a.name.localeCompare(b.name)
            case 'name-desc':  return b.name.localeCompare(a.name)
        }
    })
}, [products, search, selectedCategories, priceRange, sortBy])
```

**De ce `useMemo`?** Filtrarea și sortarea sunt operații care iterează toate produsele. Fără `useMemo`, s-ar executa la fiecare keystroke din orice input al paginii. Cu `useMemo`, React o re-execută doar când se schimbă efectiv o dependință.

### De ce `ProductsApi.getAll({ pageSize: 100 })` și nu API simplu?

Versiunea anterioară a API-ului era paginată (din sesiunea 2). Există două clienți:
- `ProductsApi` (cu `P` mare) — returnează `{ items, totalCount, ... }` — paginat
- `productsApi` (cu `p` mic) — returnează array simplu

Shop-ul folosește versiunea paginată cu `pageSize: 100` pentru a lua toate produsele client-side și a face filtrarea local (fără request la server pentru fiecare filtru).

---

## 6. Protecția rutelor pe roluri

### Înainte (după sesiunea 3)

```
/categories   — orice user autentificat
/products     — orice user autentificat
/promotions   — orice user autentificat
/users        — Admin only
```

Butonele Add/Edit/Delete din pagini erau ascunse pentru useri non-admin (cu `isAdmin()`), dar rutele erau accesibile. Un user putea naviga direct la `/products` și vedea lista completă.

### După (sesiunea 4)

```
/           — orice user autentificat
/shop       — orice user autentificat
/categories — Admin only
/products   — Admin only
/promotions — Admin only
/users      — Admin only
```

```typescript
// src/App.tsx
<Route element={<ProtectedRoute />}>
    <Route path='/' element={<Home />} />
    <Route path='/shop' element={<Shop />} />
</Route>

<Route element={<ProtectedRoute requiredRole='Admin' />}>
    <Route path='/categories' element={<Categories />} />
    <Route path='/products' element={<Products />} />
    <Route path='/promotions' element={<Promotions />} />
    <Route path='/users' element={<Users />} />
</Route>
```

Un user care încearcă să acceseze `/categories` direct din browser este redirecționat la `/` de `ProtectedRoute`.

---

## 7. Navbar — schimbări

### Înainte

- Toate linkurile vizibile pentru orice user autentificat
- Linkul Users vizibil doar pentru Admin
- Pe paginile neautentificate (login/register) apărea tot navbar-ul cu linkuri

### După

```typescript
// Structura finală
const userNavLinks   = [Home, Shop]
const adminNavLinks  = [Categories, Products, Promotions, Users]

// Navbar randează:
// - logo mereu
// - dacă user !== null → userNavLinks + (isAdmin ? adminNavLinks : nimic)
// - dacă user !== null → coș badge + buton Logout
// - dacă user === null → doar logo
```

```
Neautentificat:  [Logo]
User normal:     [Logo] [Home] [Shop]  🛒  [Logout]
Admin:           [Logo] [Home] [Shop] [Categories] [Products] [Promotions] [Users]  🛒  [Logout]
```

**De ce ascunde tot navbar-ul pentru neautentificați?**

Pe paginile de login și register nu există context de navigare util. Afișarea unui navbar parțial (doar logo) e mai curată și evită confuzia — nu există link-uri la care userul să nu aibă acces oricum.

---

## 8. CartDrawer — componenta

`CartDrawer` e un `Drawer` MUI ancorat la dreapta, deschis/închis prin `CartContext.open`.

### Ce afișează

```
┌─────────────────────────────────┐
│ Your Cart                    ✕  │
├─────────────────────────────────┤
│ Pepsi 500ml            🗑️       │
│ 5.49 RON each                   │
│  ─  3  +              16.47 RON │
├─────────────────────────────────┤
│ Sprite 500ml           🗑️       │
│ ...                             │
├─────────────────────────────────┤
│                                 │
│ Subtotal           106.90 RON   │
│ Total               80.88 RON   │
└─────────────────────────────────┘
```

Diferența Subtotal - Total = reducerile din promoțiile active aplicate de backend.

### Funcțiile expuse prin CartContext

```typescript
interface CartContextValue {
    cart: Cart | null
    open: boolean
    openCart: () => void      // Navbar cart icon → setOpen(true)
    closeCart: () => void     // butonul ✕ din drawer → setOpen(false)
    addItem: (productId: number, quantity: number) => Promise<void>
    updateQuantity: (itemId: number, quantity: number) => Promise<void>
    removeProduct: (itemId: number) => Promise<void>
}
```

Fiecare operație modificatoare (`addItem`, `updateQuantity`, `removeProduct`) apelează backend-ul și după `await` apelează `loadCart()` pentru a sincroniza starea locală cu ce e în DB.

---

## 9. Fix minor — textAlign în MUI Typography

```typescript
// Înainte (generează warning în consolă):
<Typography textAlign='center'>...</Typography>

// După:
<Typography sx={{ textAlign: 'center' }}>...</Typography>
```

MUI Typography în unele versiuni nu acceptă `textAlign` ca prop direct — trebuie transmis prin `sx`. Warning-ul era vizibil în consola browserului.

---

## Rezumat fișiere noi în această sesiune

| Fișier | Rol |
|---|---|
| `src/api/models/CartModel.ts` | Tipuri DTO exact ca backend (itemTypeTotal, discount) |
| `src/api/clients/CartApiClient.ts` | GET /cart, POST /cart/items, PUT/DELETE /cart/items/{id} |
| `src/components/shared/types/Cart.ts` | Tipuri UI + `toCartModel()` mapping |
| `src/context/CartContext/cart-context.ts` | `CartContext` + `useCart()` hook |
| `src/context/CartContext/CartProvider.tsx` | State management coș, legătură cu AuthContext |
| `src/components/CartDrawer/index.tsx` | UI drawer coș (MUI Drawer) |
| `src/components/Shop/index.tsx` | Pagina shop cu filtre, sortare, grid produse |

## Rezumat fișiere modificate

| Fișier | Ce s-a schimbat |
|---|---|
| `src/App.tsx` | CartProvider, CartDrawer, /shop, admin-only rute |
| `src/api/base/http.ts` | Guard `/login` în interceptor 401 |
| `src/context/AuthContext.tsx` | `localStorage.removeItem` sincronic în logout |
| `src/components/Navbar/index.tsx` | Cart icon + badge, admin links, hide when logged out |
| `src/components/Auth/LoginPage/index.tsx` | `textAlign` → `sx={{ textAlign }}` |
| `src/components/Auth/RegisterPage/index.tsx` | același fix |
| `src/components/Products/ProductFormDialog/index.tsx` | Preview imagine la URL completat |
| `src/components/Promotions/PromotionFormDialog/index.tsx` | Checkbox → Switch pentru câmpul Active |

---

## Diagrama finală a structurii aplicației

```
App
├── AuthProvider                    (token, user, login, logout, isAdmin)
│   └── CartProvider                (cart state, legat de user din AuthContext)
│       ├── Navbar                  (links condiționate pe user și isAdmin)
│       ├── Routes
│       │   ├── /login              (public)
│       │   ├── /register           (public)
│       │   ├── ProtectedRoute      (redirect /login dacă neautentificat)
│       │   │   ├── /              (Home)
│       │   │   └── /shop          (Shop — filtre, sortare, add to cart)
│       │   ├── ProtectedRoute[Admin]  (redirect / dacă nu e Admin)
│       │   │   ├── /categories
│       │   │   ├── /products
│       │   │   ├── /promotions
│       │   │   └── /users
│       │   └── * → NotFound
│       └── CartDrawer              (drawer global, deschis prin CartContext)
```
