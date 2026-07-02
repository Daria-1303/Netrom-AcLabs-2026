# Sesiunea 3 – CRUD complet pentru Products și Promotions

## Ce s-a implementat

Același pattern ca la Categories, extins pentru Products și Promotions, plus îmbunătățiri de design și optimizări de cod.

---

## 1. Strat API

### Fișiere noi

**`src/api/models/ProductModel.ts`**
- `ProductModel` — forma datelor primite de la server (id, name, description, imageUrl, price, categories)
- `ProductInput` — forma datelor trimise la server (fără id, cu `categoryIds: number[]`)

**`src/api/models/PromotionModel.ts`**
- `PromotionModel` și `PromotionInput` — același pattern
- Conține și enum-urile `PromotionType` (Quantity / CartTotal) și `PromotionReward` (FreeItems / PercentDiscount), copiate din backend

**`src/api/clients/ProductApiClient.ts`**
- `ProductsApi.getAll(categoryId?)` — suportă filtru opțional pe categorie via query param
- `ProductsApi.create(data)`, `.update(id, data)`, `.remove(id)`

**`src/api/clients/PromotionApiClient.ts`**
- `PromotionsApi` cu aceleași 4 metode

---

## 2. Tipuri partajate

**`src/components/shared/types/Product.ts`**
- Interfața `Product` — versiunea frontend (description și imageUrl garantat string, nu undefined)
- `toProduct(dto: ProductModel): Product` — mapper care normalizează datele primite

**`src/components/shared/types/Promotion.ts`**
- Interfața `Promotion` + `toPromotion(dto)`
- Re-exportă `PromotionType` și `PromotionReward` cu `export { }` (nu `export type { }` — enum-urile există la runtime, nu doar ca tipuri)

> **De reținut:** `export type` șterge exportul la compilare. Enum-urile au nevoie de `export` simplu.

---

## 3. Componente principale

### `src/components/Products/index.tsx` (înlocuit placeholder-ul)

Structură identică cu `Categories`:
```
Products
├── PageHeader (titlu + buton "Add Product")
├── Alert (eroare la fetch principal)
├── Alert warning (dacă categories nu s-au putut încărca)
├── CircularProgress (loading)
├── TableContainer
│   └── Table cu coloane: Name, Description, Price (RON), Categories (Chips), Actions
├── ProductFormDialog (add/edit)
└── ConfirmDialog (delete)
```

State-uri:
- `products[]`, `categories[]` — date
- `loading`, `error`, `categoriesError` — stări UI
- `formOpen`, `editing` — controlează dialogul de form
- `deleting`, `confirmOpen` — controlează dialogul de confirmare

**Categories se încarcă o singură dată la mount** (în `useEffect`) și se pasează ca prop în `ProductFormDialog` — nu se re-fetch la fiecare deschidere a dialogului.

### `src/components/Products/ProductFormDialog/index.tsx` (nou)

Câmpuri: Name, Description, Price (RON), Image URL, Categories (multi-select cu Checkbox).

Props:
- `product: Product | null` — null înseamnă Add, altfel Edit
- `categories: Category[]` — lista primită din părinte, nu fetch-uită intern
- `onClose()`, `onSaved()`

---

### `src/components/Promotions/index.tsx` (înlocuit placeholder-ul)

Coloane tabel: Name, Type, Threshold, Reward, Value (formatat: `20%` sau `3 items`), Status (Chip verde/gri), Actions.

**Două lookup-uri** încărcate la mount: `CategoriesApi.getAll()` și `ProductsApi.getAll()`. Ambele pasate ca props în `PromotionFormDialog`. Dacă oricare pică → `lookupError = true` → Alert warning vizibil.

### `src/components/Promotions/PromotionFormDialog/index.tsx` (nou)

Câmpuri:
- Name
- Type — Select (Quantity / Cart Total)
- Threshold — label dinamic în funcție de Type ("items" sau "RON")
- Reward — Select (Free Items / Percent Discount)
- Reward Value — label dinamic ("Free items count" sau "Discount %")
- Product — Select dropdown cu produsele din DB (opțional, prima opțiune = None)
- Category — Select dropdown cu categoriile din DB (opțional)
- IsActive — Checkbox

Props: `promotion`, `categories[]`, `products[]`, `onClose`, `onSaved`.

---

## 4. Componente comune

### `src/components/common/EmptyState/index.tsx` (nou)

Component reutilizabil pentru starea de tabel gol. Afișează un icon `InboxIcon` și un mesaj centrat. Folosit în toate 3 tabele (Categories, Products, Promotions).

```tsx
<EmptyState message="No products yet." />
```

Motivul pentru care există ca component separat: logica de "tabel gol" e identică în 3 locuri — un singur fișier e mai ușor de modificat decât 3.

### `src/components/common/PageHeader/index.tsx` (actualizat)

Înainte: `Box` plat fără layout.  
După: flex `space-between`, titlu `variant="h4"`, buton cu icon `+` și stil pill din temă.

---

## 5. Design system — `src/theme.ts` (rescris)

Cele mai importante modificări față de versiunea anterioară:

| Ce | Înainte | După |
|---|---|---|
| Butoane | `borderRadius: 10`, fontWeight 600 | `borderRadius: 9999` (pill), fontWeight 420 |
| AppBar | boxShadow, `#2C2C1F` | borderBottom, `#1C1C14` (canvas-night), `borderRadius: 0` |
| Card/Paper shadow | `0 2px 12px rgba(...)` simplu | Level-3 stacked shadow din design system (4 umbre suprapuse) |
| Table headers | default MUI | uppercase, 12px, `#787868`, letterSpacing 0.72px (eyebrow-cap) |
| Typography h1-h3 | fontWeight 700 | fontWeight 300 (display signature din design system) |
| Chip | default | pill shape, eyebrow-cap sizing |
| Dialog | default | borderRadius 12, Level-4 shadow |

> **De reținut:** `AppBar` extinde `Paper` în MUI, deci primea `borderRadius: 12` din override-ul global pe Paper. Fix: `borderRadius: 0` explicit pe `MuiAppBar`.

---

## 6. Navbar — `src/components/Navbar/index.tsx` (actualizat)

Înainte: `Button` component pentru fiecare link de navigare → apăreau ca butoane cu background.  
După: `Box component={NavLink}` cu stilizare manuală — text links cu pill hover/active, fără chrome de buton.

Active state: text `#C8C000` (lime) + background `rgba(200,192,0,0.10)`.

---

## 7. NotFound — `src/components/NotFound/index.tsx`

Fix mic: `fontWeight: 700 → 300` pe "404". Display type la weight 300 e semnătura editorială a design system-ului — la 700 se pierde stilul.

---

## 8. Optimizare: prop drilling pentru lookup data

**Problema:** `ProductFormDialog` și `PromotionFormDialog` apelau `CategoriesApi.getAll()` / `ProductsApi.getAll()` la fiecare deschidere a dialogului.

**Fix:** fetch-urile s-au mutat în componentele părinte (`Products`, `Promotions`), care oricum se montează o singură dată per navigare. Datele se pasează ca props în dialogs.

```
Înainte: dialog se deschide → fetch → date
După:    pagina se montează → fetch → date stocate în state → pasate în dialog
```

---

## Structura finală `src/`

```
src/
├── theme.ts
├── App.tsx
├── main.tsx
├── api/
│   ├── base/http.ts
│   ├── models/
│   │   ├── CategoryModel.ts
│   │   ├── ProductModel.ts          ← nou
│   │   └── PromotionModel.ts        ← nou
│   └── clients/
│       ├── CategoryApiClient.ts
│       ├── ProductApiClient.ts      ← nou
│       └── PromotionApiClient.ts    ← nou
└── components/
    ├── Navbar/index.tsx             ← actualizat
    ├── Home/index.tsx
    ├── NotFound/index.tsx           ← fix fontWeight
    ├── common/
    │   ├── PageHeader/index.tsx     ← actualizat
    │   ├── ConfirmDialog/index.tsx
    │   └── EmptyState/index.tsx     ← nou
    ├── shared/types/
    │   ├── Category.ts
    │   ├── Product.ts               ← nou
    │   └── Promotion.ts             ← nou
    ├── Categories/
    │   ├── index.tsx                ← actualizat (EmptyState, quotes)
    │   └── CategoryFormDialog/index.tsx
    ├── Products/
    │   ├── index.tsx                ← nou (înlocuit placeholder)
    │   └── ProductFormDialog/index.tsx  ← nou
    └── Promotions/
        ├── index.tsx                ← nou (înlocuit placeholder)
        └── PromotionFormDialog/index.tsx  ← nou
```
