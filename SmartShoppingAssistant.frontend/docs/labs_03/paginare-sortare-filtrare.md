# Paginare, Sortare și Filtrare — Server-Side

## De ce server-side și nu client-side?

Varianta client-side înseamnă: fetchuiești **toată** baza de date o singură dată, o ții în memorie, și filtrezi/sortezi/paginezi local în browser.

Problema:
- Sortezi pagina curentă, nu întregul dataset. Dacă ai 200 produse și ești pe pagina 3, sortarea după preț va ordona doar cele 10 produse vizibile, nu toate 200.
- Filtrarea returnează rezultate greșite din același motiv — lucrezi pe un subset.
- Cu 10.000 de înregistrări, clientul primește un răspuns imens la fiecare navigare.

Varianta server-side: la fiecare acțiune (schimbare pagină, sortare, căutare) se trimite un request nou cu parametrii, iar serverul returnează **doar** înregistrările relevante.

---

## Fluxul complet al unui request

```
Browser (useTableState)
  → GET /api/products?page=2&pageSize=10&search=lapte&sortBy=price&sortDir=desc
    → ProductsController.GetAll([FromQuery] QueryParams)
      → ProductService.GetAllAsync(queryParams)
        → IQueryable<Product> pipeline (Where → OrderBy → Skip/Take)
          → SQL generat de EF Core (parametrizat, sigur)
            → PagedResult<ProductGetDTO> { Items, TotalCount, Page, PageSize }
              → JSON response
                → useTableState actualizează items + totalCount
                  → tabel React re-renderizat
```

---

## Backend

### 1. `SmartShoppingAssistant.BusinessLogic/DTOs/Common/QueryParams.cs` — nou

Clasa care captează toți parametrii de query într-un singur obiect:

```csharp
public class QueryParams
{
    private int _page = 1;
    private int _pageSize = 10;

    public int Page     { get => _page;     set => _page     = Math.Max(1, value); }
    public int PageSize { get => _pageSize; set => _pageSize = Math.Max(1, value); }

    public string? Search  { get; set; }
    public string? SortBy  { get; set; }
    public string  SortDir { get; set; } = "asc";
    public int?    CategoryId { get; set; }
}
```

**De ce `Math.Max(1, value)` fără limită superioară?**  
Limita de 50 e pusă în controllere, nu aici. Astfel, apelurile interne (CartService, ShoppingTools) pot cere `PageSize = 1000` fără să fie blocate de DTO.

---

### 2. `SmartShoppingAssistant.BusinessLogic/DTOs/Common/PagedResult.cs` — nou

Generic wrapper pentru orice răspuns paginat:

```csharp
public class PagedResult<T>
{
    public List<T> Items     { get; set; } = new();
    public int TotalCount    { get; set; }
    public int Page          { get; set; }
    public int PageSize      { get; set; }
}
```

Clientul primește atât datele (`Items`) cât și metadata de paginare (`TotalCount`), necesar pentru a calcula numărul total de pagini în UI.

---

### 3. Servicii — pipeline IQueryable

Același pattern în toate 3 servicii. Exemplu complet din `CategoryService`:

```csharp
public async Task<PagedResult<CategoryGetDTO>> GetAllAsync(QueryParams queryParams)
{
    var query = categoryRepository.GetAllAsQueryable();

    // 1. Filtrare (opțional)
    if (!string.IsNullOrWhiteSpace(queryParams.Search))
    {
        var term = queryParams.Search.Trim();
        query = query.Where(c => c.Name.Contains(term) || c.Description.Contains(term));
    }

    // 2. Sortare cu whitelist
    var allowedSort = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "id", "name", "description" };
    var sortBy = allowedSort.Contains(queryParams.SortBy ?? "") ? queryParams.SortBy! : "name";
    var desc = queryParams.SortDir?.ToLower() == "desc";

    query = sortBy.ToLower() switch
    {
        "id"          => desc ? query.OrderByDescending(c => c.Id)          : query.OrderBy(c => c.Id),
        "description" => desc ? query.OrderByDescending(c => c.Description) : query.OrderBy(c => c.Description),
        _             => desc ? query.OrderByDescending(c => c.Name)        : query.OrderBy(c => c.Name),
    };

    // 3. Count înainte de Skip/Take
    var totalCount = await query.CountAsync();

    // 4. Paginare
    var items = await query
        .Skip((queryParams.Page - 1) * queryParams.PageSize)
        .Take(queryParams.PageSize)
        .ToListAsync();

    return new PagedResult<CategoryGetDTO>
    {
        Items      = items.Select(CategoryMapper.ToCategoryGetDTO).ToList(),
        TotalCount = totalCount,
        Page       = queryParams.Page,
        PageSize   = queryParams.PageSize,
    };
}
```

**De ce `IQueryable<T>` și nu `List<T>`?**

`IQueryable` reprezintă o interogare care **nu s-a executat încă**. Fiecare `.Where()`, `.OrderBy()`, `.Skip()`, `.Take()` adaugă clauze SQL în memorie. SQL-ul efectiv e trimis la baza de date abia la `CountAsync()` și `ToListAsync()`.

Dacă ai folosi `List<T>` (adică `.ToList()` la început), ai încărca **tot tabelul** în memorie și ai filtra/sorta în C# — ineficient și lent.

**De ce `CountAsync()` înainte de `Skip/Take`?**

`totalCount` trebuie să reflecte câte înregistrări există **după filtrare**, nu câte sunt în total în tabel. Dacă cauți "lapte" și există 5 produse cu acel termen, `totalCount = 5` — UI-ul va afișa că există o singură pagină, nu 20.

**Protecție la SQL Injection pe coloana de sortare:**

Dacă ai permite `sortBy = queryParams.SortBy` direct, un atacator ar putea trimite `sortBy="; DROP TABLE Products--"`. Fix: `HashSet<string>` cu coloanele permise + fallback la `"name"` dacă valoarea nu e în listă. Coloana nu ajunge niciodată ca string în SQL — e mapată la un lambda LINQ (`c => c.Name`), iar EF Core traduce lambdas în SQL parametrizat.

**`ProductService` — fix tip explicit:**

```csharp
// gresit — tip inferit ca IIncludableQueryable<Product, ICollection<Category>>
var query = productRepository.GetAllAsQueryable().Include(p => p.Categories);

// corect — explicit IQueryable<Product>, compatibil cu Where/OrderBy
IQueryable<Product> query = productRepository.GetAllAsQueryable().Include(p => p.Categories);
```

`Include()` returnează `IIncludableQueryable<...>`, care e incompatibil la reassignment cu `IQueryable<Product>` returnat de `Where()` / `OrderBy()`. Tipul explicit forțează compilatorul să trateze variabila ca `IQueryable<Product>` de la început.

---

### 4. Controllere — cap de `PageSize` la granița publică

```csharp
[HttpGet]
public async Task<ActionResult<PagedResult<CategoryGetDTO>>> GetAll([FromQuery] QueryParams queryParams)
{
    if (queryParams.PageSize > 50) queryParams.PageSize = 50;
    var result = await categoryService.GetAllAsync(queryParams);
    return Ok(result);
}
```

Nimeni din afară nu poate cere mai mult de 50 de înregistrări per request. Apelurile interne (CartService cu `PageSize = 1000`) nu trec prin controller, deci nu sunt afectate.

---

### 5. `CartService` și `ShoppingTools` — apeluri interne corectate

Ambele apelau `GetAllAsync()` fără argumente. Acum:

```csharp
// CartService — are nevoie de toate categoriile pentru analiza AI
var categoriesResult = await categoryService.GetAllAsync(new QueryParams { PageSize = 1000 });
var categoryJson = JsonSerializer.Serialize(categoriesResult.Items.Select(...));

// ShoppingTools — are nevoie de toate produsele dintr-o categorie
var result = await productService.GetAllAsync(new QueryParams { CategoryId = categoryId, PageSize = 1000 });
return result.Items;
```

---

## Frontend

### 6. `src/api/models/PagedResult.ts` — nou

Interfețele TypeScript care oglindesc C# exact:

```typescript
export interface PagedResult<T> {
    items: T[];
    totalCount: number;
    page: number;
    pageSize: number;
}

export interface QueryParams {
    page?: number;
    pageSize?: number;
    search?: string;
    sortBy?: string;
    sortDir?: 'asc' | 'desc';
    categoryId?: number;
}
```

---

### 7. `src/hooks/useTableState.ts` — nou

Hook custom care centralizează tot state-ul unui tabel paginat și îl sincronizează cu serverul:

```
State intern:
  items         — înregistrările paginii curente
  totalCount    — total înregistrări (pentru paginare)
  page          — pagina curentă (1-indexed)
  pageSize      — înregistrări per pagină
  search        — valoarea din input (se actualizează la fiecare tastă)
  debouncedSearch — valoarea efectiv trimisă la server (cu întârziere de 300ms)
  sortBy        — coloana de sortare
  sortDir       — 'asc' | 'desc'
  loading       — boolean pentru spinner
  error         — mesaj de eroare
  reloadTick    — incrementat de reload() pentru a forța re-fetch
```

**De ce `debouncedSearch` separat de `search`?**

Fără debounce, fiecare tastă trimite un request la server. La un cuvânt de 5 litere, asta înseamnă 5 requesturi, din care 4 sunt inutile. Soluția: `search` se actualizează imediat (UI responsive), `debouncedSearch` se actualizează cu 300ms întârziere (request real).

```typescript
useEffect(() => {
    const timer = setTimeout(() => {
        setDebouncedSearch(search)
        setPageState(1)  // reset la pagina 1 la fiecare căutare nouă
    }, 300)
    return () => clearTimeout(timer)
}, [search])
```

**De ce `fetchFnRef`?**

```typescript
const fetchFnRef = useRef(fetchFn)
useEffect(() => { fetchFnRef.current = fetchFn })
```

`fetchFn` e o funcție pasată ca prop. React recreează funcții la fiecare render, deci identitatea ei se schimbă la fiecare render. Dacă o includeam în dependency array-ul effect-ului de fetch, am fi primit un loop infinit (effect → render → nouă funcție → effect → ...). `useRef` păstrează referința curentă fără să declanșeze re-renders.

**`toggleSort`:**

```typescript
function toggleSort(col: string) {
    if (col === sortBy) {
        setSortDir(d => d === 'asc' ? 'desc' : 'asc')  // același col → inversează direcția
    } else {
        setSortBy(col)
        setSortDir('asc')  // col nou → reset la asc
    }
    setPageState(1)  // reset la pagina 1 la orice schimbare de sortare
}
```

---

### 8. API Clients — actualizați

Toți trei clienți acceptă acum `QueryParams` și returnează `PagedResult<T>`:

```typescript
getAll: async (params?: QueryParams): Promise<PagedResult<Category>> => {
    const data = await http.get<PagedResult<CategoryModel>>('/categories', params as Record<string, unknown>)
    return { ...data, items: data.items.map(toCategory) }
}
```

**Lookup-uri din formulare** (categoriile în Products, categorii + produse în Promotions) cer explicit `pageSize: 50`:

```typescript
CategoriesApi.getAll({ pageSize: 50 }).then((r) => setCategories(r.items))
```

Fără aceasta, ar fi returnat implicit 10 înregistrări — selectoarele din formulare ar afișa cel mult 10 categorii.

---

### 9. Componente pagini — actualizate

Toate 3 pagini (Categories, Products, Promotions) au primit:

**Search input** cu debounce via hook:
```tsx
<TextField
    value={search}
    onChange={(e) => setSearch(e.target.value)}
    placeholder='Search categories...'
    slotProps={{ input: { startAdornment: <SearchIcon /> } }}
/>
```

**Headere sortabile** via `TableSortLabel`:
```tsx
<TableSortLabel
    active={sortBy === 'name'}
    direction={sortBy === 'name' ? sortDir : 'asc'}
    onClick={() => toggleSort('name')}
>
    Name
</TableSortLabel>
```

`active` determină dacă săgeata e vizibilă. `direction` determină orientarea săgeții.

**Paginare** via `TablePagination`:
```tsx
<TablePagination
    count={totalCount}
    page={page - 1}          // MUI e 0-indexed, backend-ul e 1-indexed
    rowsPerPage={pageSize}
    onPageChange={(_, p) => setPage(p + 1)}
    onRowsPerPageChange={(e) => setPageSize(parseInt(e.target.value, 10))}
/>
```

---

## Rezumat modificări per fișier

### Backend

| Fișier | Tip | Ce s-a schimbat |
|---|---|---|
| `DTOs/Common/QueryParams.cs` | nou | parametrii de query (page, pageSize, search, sortBy, sortDir, categoryId) |
| `DTOs/Common/PagedResult.cs` | nou | wrapper generic pentru răspunsuri paginate |
| `ICategoryService.cs` | modificat | `GetAllAsync(QueryParams) → PagedResult<CategoryGetDTO>` |
| `IProductService.cs` | modificat | același pattern |
| `IPromotionService.cs` | modificat | același pattern |
| `CategoryService.cs` | modificat | IQueryable pipeline cu whitelist sort |
| `ProductService.cs` | modificat | același pipeline + fix tip `IQueryable<Product>` explicit |
| `PromotionService.cs` | modificat | același pipeline |
| `CategoryController.cs` | modificat | `[FromQuery] QueryParams` + cap pageSize 50 |
| `ProductController.cs` | modificat | același pattern |
| `PromotionController.cs` | modificat | același pattern |
| `CartService.cs` | modificat | `GetAllAsync(new QueryParams { PageSize = 1000 })` + `.Items` |
| `ShoppingTools.cs` | modificat | același fix |

### Frontend

| Fișier | Tip | Ce s-a schimbat |
|---|---|---|
| `api/models/PagedResult.ts` | nou | `PagedResult<T>` + `QueryParams` TypeScript |
| `hooks/useTableState.ts` | nou | hook complet cu debounce, sort, pagination, reload |
| `api/clients/CategoryApiClient.ts` | modificat | acceptă `QueryParams`, returnează `PagedResult<Category>` |
| `api/clients/ProductApiClient.ts` | modificat | același pattern |
| `api/clients/PromotionApiClient.ts` | modificat | același pattern |
| `components/Categories/index.tsx` | modificat | `useTableState`, search, `TableSortLabel`, `TablePagination` |
| `components/Products/index.tsx` | modificat | același pattern + lookup cu `pageSize: 50` |
| `components/Promotions/index.tsx` | modificat | același pattern + 2 lookup-uri cu `pageSize: 50` |
