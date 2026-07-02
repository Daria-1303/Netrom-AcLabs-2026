# Autentificare cu JWT și roluri

## De ce JWT și nu sesiuni server-side?

Sesiunile clasice (cookie + sesiune pe server) înseamnă că serverul ține minte cine ești. JWT înseamnă că serverul îți dă un token semnat — tu îl ții, îl trimiți la fiecare request, iar serverul nu trebuie să stocheze nimic în plus.

Avantajul pentru acest proiect: frontendul și backendrul sunt aplicații separate pe porturi diferite. Cu sesiuni ai nevoie de configurare suplimentară pentru cross-origin. Cu JWT, frontendrul pune tokenul în header și gata.

---

## Fluxul complet — de la login până la un request protejat

```
1. POST /api/auth/login { email, password }
      ↓
2. AuthService verifică hash-ul parolei (PBKDF2)
      ↓
3. Generează JWT cu claims: userId, email, role
      ↓
4. Frontendrul primește { accessToken, email, role }
      ↓
5. AuthContext salvează datele în localStorage
      ↓
6. La orice request următor:
   axios interceptor → Authorization: Bearer <token>
      ↓
7. ASP.NET Core JWT middleware validează semnătura + expiry
      ↓
8. [Authorize] / [Authorize(Roles="Admin")] verifică claims
      ↓
9. Controller rulează, extrage userId din token (nu din body)
```

---

## Fluxul rolurilor

```
Register → rol forțat User (hardcodat în AuthService)
         → nu există câmp Role în RegisterDTO

Seed startup → primul admin creat cu credențiale default
             → Program.cs verifică dacă există vreun Admin

Admin poate → PUT /api/users/{id}/role (Authorize Admin only)
           → schimbă rolul oricărui user din DB
           → sau direct în baza de date (coloana Role e string)
```

---

## BACKEND

### DataAccess

---

#### `Entities/Enums/UserRole.cs` — nou

```csharp
public enum UserRole { Admin, User }
```

Enum în loc de string hardcodat din două motive:
- Compilatorul detectează valori invalide
- Consistent cu `PromotionType` și `PromotionReward` deja existente în același folder

---

#### `Entities/User.cs` — nou

```csharp
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.User;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
```

`PasswordHash` nu stochează niciodată parola în clar — stochează rezultatul funcției PBKDF2. Chiar dacă baza de date e compromisă, atacatorul nu poate recupera parola.

`CartItems` ca `ICollection` — fiecare user are propriul coș. Un user nu vede coșul altui user.

---

#### `Entities/CartItem.cs` — modificat

```csharp
public int UserId { get; set; }        // adăugat
public User User { get; set; } = null! // navigation property
```

Înainte: un CartItem exista global, fără proprietar.
Acum: fiecare CartItem aparține unui user specific.

---

#### `Entities/Product.cs` — modificat

```csharp
// înainte
public CartItem? cartItem { get; set; }

// după
public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
```

Relația cu CartItem era **1:1** — un produs putea fi în cel mult un singur coș din tot sistemul. Asta ar fi blocat doi useri diferiți să aibă același produs în coș simultan. Schimbat în **1:many**.

---

#### `Configurations/UserConfiguration.cs` — nou

```csharp
builder.HasIndex(u => u.Email).IsUnique();

builder.Property(u => u.Role)
    .HasConversion(new EnumToStringConverter<UserRole>())
    .HasMaxLength(20);
```

**Index unic pe Email**: previne două conturi cu același email la nivel de bază de date (a doua linie de apărare după verificarea din service).

**Enum → string în DB**: coloana `Role` stochează `"Admin"` / `"User"` ca text, nu ca număr întreg. Asta înseamnă că poți modifica direct în DB (`UPDATE Users SET Role = 'Admin' WHERE Email = '...'`) fără să știi că `0 = Admin` sau `1 = User`.

---

#### `Configurations/CartItemConfiguration.cs` — modificat

```csharp
// înainte
builder.HasOne(ci => ci.Product)
    .WithOne(p => p.cartItem)            // 1:1 global
    .HasForeignKey<CartItem>(ci => ci.ProductId);

// după
builder.HasOne(ci => ci.Product)
    .WithMany(p => p.CartItems)          // 1:many — același produs în coșuri multiple
    .HasForeignKey(ci => ci.ProductId)
    .OnDelete(DeleteBehavior.Cascade);

builder.HasOne(ci => ci.User)
    .WithMany(u => u.CartItems)
    .HasForeignKey(ci => ci.UserId)
    .OnDelete(DeleteBehavior.Cascade);   // dacă userul e șters, coșul dispare cu el
```

---

#### `SmartShoppingAssistantDbContext.cs` — modificat

```csharp
public DbSet<User> Users { get; set; } = null!;
```

`ApplyConfigurationsFromAssembly` detectează automat `UserConfiguration` — nu trebuie înregistrată manual.

---

#### Migrare EF Core — `AddUsersAndCartPerUser`

Ce face migrarea:
1. `DELETE FROM CartItems` — coșurile globale existente nu au owner, nu pot fi migrat
2. Creează tabelul `Users` cu index unic pe Email
3. Adaugă coloana `UserId INT NOT NULL` în CartItems
4. Adaugă FK `CartItems.UserId → Users.Id`
5. Schimbă relația Product→CartItem din 1:1 în 1:many (drop unique index, create non-unique)

`DELETE FROM CartItems` a fost necesar manual în fișierul de migrare — EF Core a generat adăugarea coloanei cu `DEFAULT 0`, dar nu există niciun user cu `Id = 0`, deci FK constraint ar fi eșuat.

---

#### `Repositories/IUserRepository.cs` — nou

```csharp
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
```

Extinde `IRepository<User>` exact ca `ICartItemRepository` extinde `IRepository<CartItem>`. `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync` vin gratuit din `BaseRepository`.

`GetByEmailAsync` returnează `User?` (nullable) — la login e normal să nu găsești userul, nu e o excepție.

---

#### `Repositories/UserRepository.cs` — nou

```csharp
public async Task<User?> GetByEmailAsync(string email)
{
    return await context.Users
        .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
}
```

Comparație case-insensitive — `admin@SSA.ro` și `admin@ssa.ro` sunt același cont.

---

#### `Repositories/ICartItemRepository.cs` — modificat

Toate metodele primesc acum `userId`:

```csharp
Task<CartItem> GetByIdWithProductAsync(int id, int userId);
Task<List<CartItem>> GetAllWithProductsAsync(int userId);
Task DeleteAllAsync(int userId);
Task<List<CartItem>> GetAllWithProductWithCategoriesAsync(int userId);
```

---

#### `Repositories/CartItemRepository.cs` — modificat

Fiecare query filtrează după `userId`:

```csharp
public async Task<List<CartItem>> GetAllWithProductsAsync(int userId)
{
    return await context.CartItems
        .Where(ci => ci.UserId == userId)    // izolare per user
        .Include(ci => ci.Product)
            .ThenInclude(p => p.Categories)
        .ToListAsync();
}

public async Task<CartItem> GetByIdWithProductAsync(int id, int userId)
{
    var item = await context.CartItems
        .Include(ci => ci.Product)
        .FirstOrDefaultAsync(ci => ci.Id == id && ci.UserId == userId);
    // dacă item nu există SAU aparține altui user → KeyNotFoundException
}
```

`GetByIdWithProductAsync` verifică și `ci.UserId == userId` — un user nu poate accesa/modifica/șterge un item care nu îi aparține, chiar dacă ghicește ID-ul.

---

### BusinessLogic

---

#### `DTOs/Auth/RegisterDTO.cs` — nou

```csharp
public class RegisterDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
```

**Fără câmp `Role`** — un user nu poate alege rolul la înregistrare. Rolul e setat server-side în `AuthService`.

---

#### `DTOs/Auth/LoginDTO.cs` — nou

```csharp
public class LoginDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
```

---

#### `DTOs/Auth/TokenDTO.cs` — nou

```csharp
public class TokenDTO
{
    public string AccessToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}
```

`Role` e inclus în răspuns ca string (`"Admin"` / `"User"`) — frontendrul îl folosește direct pentru UI (afișare butoane, link Users) fără să decodeze manual JWT-ul.

---

#### `DTOs/Auth/UserGetDTO.cs` și `UserRoleUpdateDTO.cs` — noi

```csharp
public class UserGetDTO { public int Id; public string Email; public string Role; }
public class UserRoleUpdateDTO { public string Role; }
```

---

#### `Services/Interfaces/IAuthService.cs` — nou

```csharp
public interface IAuthService
{
    Task RegisterAsync(RegisterDTO dto);
    Task<TokenDTO> LoginAsync(LoginDTO dto);
}
```

---

#### `Services/AuthService.cs` — nou

**Register:**
```csharp
public async Task RegisterAsync(RegisterDTO dto)
{
    var existing = await userRepository.GetByEmailAsync(dto.Email);
    if (existing != null)
        throw new InvalidOperationException("Email already in use.");

    var user = new User { Email = dto.Email, Role = UserRole.User };  // rol forțat
    user.PasswordHash = _hasher.HashPassword(user, dto.Password);      // PBKDF2
    await userRepository.AddAsync(user);
}
```

**Login:**
```csharp
public async Task<TokenDTO> LoginAsync(LoginDTO dto)
{
    const string invalid = "Invalid credentials.";

    var user = await userRepository.GetByEmailAsync(dto.Email);
    if (user == null)
        throw new UnauthorizedAccessException(invalid);   // același mesaj

    var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
    if (result == PasswordVerificationResult.Failed)
        throw new UnauthorizedAccessException(invalid);   // același mesaj

    return GenerateToken(user);
}
```

Același mesaj (`"Invalid credentials."`) indiferent dacă emailul nu există sau parola e greșită. Dacă ai da mesaje diferite, un atacator ar putea afla ce emailuri există în sistem (user enumeration attack).

**Generare JWT:**
```csharp
var claims = new[]
{
    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),   // userId
    new Claim(JwtRegisteredClaimNames.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role.ToString()),              // "Admin"/"User"
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
};

var token = new JwtSecurityToken(
    issuer, audience, claims,
    expires: DateTime.UtcNow.AddMinutes(expiry),
    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
```

`ClaimTypes.Role` e recunoscut automat de `[Authorize(Roles = "Admin")]` — ASP.NET Core știe să caute acest claim specific.

`Jti` (JWT ID) — identificator unic per token, util dacă vrei să implementezi blacklisting mai târziu.

**De ce PBKDF2 și nu SHA256/MD5?**

SHA256 și MD5 sunt rapide — un GPU modern poate calcula miliarde pe secundă. PBKDF2 e deliberat lent prin iterații repetate. Dacă baza de date e compromisă, un atacator nu poate face brute-force rapid.

**Secretul JWT din User Secrets:**
```csharp
var secret = configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("JWT secret is not configured.");
```

Dacă secretul nu e configurat, aplicația nu pornește. Nu există fallback la un secret default hardcodat.

---

#### `Services/Interfaces/IUserService.cs` și `UserService.cs` — noi

```csharp
public async Task UpdateRoleAsync(int id, string role)
{
    if (!Enum.TryParse<UserRole>(role, ignoreCase: true, out var parsedRole))
        throw new ArgumentException($"Invalid role: {role}");

    var user = await userRepository.GetByIdAsync(id);
    user.Role = parsedRole;
    await userRepository.UpdateAsync(user);
}
```

`Enum.TryParse` validează că rolul primit e unul valid (`"Admin"` sau `"User"`) — nu acceptă valori arbitrare.

---

#### `Services/Interfaces/ICartService.cs` — modificat

Toate metodele primesc acum `userId`:

```csharp
Task<CartGetDTO> GetCartAsync(int userId);
Task<CartItemGetDTO> AddItemAsync(CartItemCreateDTO dto, int userId);
Task<CartItemGetDTO> UpdateItemQuantityAsync(int itemId, CartItemUpdateDTO dto, int userId);
Task RemoveItemAsync(int itemId, int userId);
Task ClearCartAsync(int userId);
Task<AnalysisResponse> AnalyzeCartAsync(int userId);
```

---

#### `Services/CartService.cs` — modificat

`userId` e propagat la toate apelurile de repository. La `RemoveItemAsync`:

```csharp
public async Task RemoveItemAsync(int itemId, int userId)
{
    var item = await cartItemRepository.GetByIdWithProductAsync(itemId, userId);
    await cartItemRepository.DeleteAsync(item.Id);
}
```

Se face mai întâi `GetById` cu verificare de ownership — dacă itemul nu aparține userului, se aruncă `KeyNotFoundException` înainte să ajungem la `Delete`. Un user nu poate șterge itemele altui user.

---

### API

---

#### `Program.cs` — rescris

**JWT Middleware:**
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer   = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ClockSkew = TimeSpan.Zero    // tokenul expiră exact la timpul setat, fără toleranță
        };
    });
```

`ClockSkew = TimeSpan.Zero` — implicit ASP.NET Core adaugă 5 minute toleranță la expiry. Setat la zero, tokenul expiră exact când zice el.

**Ordinea middleware contează:**
```csharp
app.UseCors("Frontend");        // 1. CORS
app.UseAuthentication();        // 2. cine ești?
app.UseAuthorization();         // 3. ce ai voie să faci?
app.MapControllers();
```

Dacă `UseAuthentication` e după `UseAuthorization`, tokenul nu e niciodată validat — ai obține mereu 401.

**Seed admin la startup:**
```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SmartShoppingAssistantDbContext>();
    if (!db.Users.Any(u => u.Role == UserRole.Admin))
    {
        var hasher = new PasswordHasher<User>();
        var admin = new User { Email = "admin@ssa.ro", Role = UserRole.Admin };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
        db.Users.Add(admin);
        db.SaveChanges();
    }
}
```

Rulează o singură dată la primul startup. Dacă există deja cel puțin un Admin, nu face nimic. Folosește `PasswordHasher` la runtime (nu în migrare) — singura modalitate de a hashui parola corect fără să precalculezi hash-ul.

**CORS restrâns:**
```csharp
// înainte
corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

// după
policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
```

Doar frontendrul de pe portul 5173 poate face request-uri. Un site extern nu poate apela API-ul în numele unui user autentificat (protecție CSRF).

---

#### `Controllers/AuthController.cs` — nou

```csharp
[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
{
    try { await authService.RegisterAsync(dto); return StatusCode(201); }
    catch (InvalidOperationException ex) { return Conflict(ex.Message); }  // 409
}

[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginDTO dto)
{
    try { return Ok(await authService.LoginAsync(dto)); }
    catch (UnauthorizedAccessException) { return Unauthorized("Invalid credentials."); }
}
```

`Conflict` (409) la register dacă emailul există. `Unauthorized` (401) la login indiferent de motiv.

---

#### `Controllers/UsersController.cs` — nou

```csharp
[Authorize(Roles = "Admin")]   // la nivel de clasă — se aplică tuturor metodelor
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() { ... }

    [HttpPut("{id}/role")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UserRoleUpdateDTO dto) { ... }
}
```

`[Authorize(Roles = "Admin")]` la nivel de clasă înseamnă că orice metodă din controller necesită rolul Admin. Un user normal primește automat 403.

---

#### `Controllers/CartController.cs` — modificat

```csharp
[Authorize]
public class CartController : ControllerBase
{
    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
```

`UserId` e o proprietate calculată din claims-urile JWT validate de middleware. Nu vine din body, nu vine din query string — vine din tokenul semnat. Un user nu poate falsifica un alt `userId`.

`ClaimTypes.NameIdentifier` corespunde claim-ului `sub` setat în `AuthService.GenerateToken`.

---

#### Controllere existente — modificate

Fiecare endpoint a primit atributul corespunzător:

```csharp
[Authorize]                     // GET — orice user autentificat
[Authorize(Roles = "Admin")]    // POST, PUT, DELETE — doar Admin
```

Dacă token-ul lipsește → 401 Unauthorized
Dacă token-ul e valid dar rolul e `User` și endpoint-ul cere `Admin` → 403 Forbidden

---

## FRONTEND

---

#### `context/AuthContext.tsx` — nou

```typescript
interface AuthUser { email: string; role: string; token: string }

export function AuthProvider({ children }) {
    const [user, setUser] = useState<AuthUser | null>(() => {
        const stored = localStorage.getItem(STORAGE_KEY)
        return stored ? JSON.parse(stored) : null   // restaurat la refresh
    })

    function login(data: AuthUser) { setUser(data) }
    function logout() { setUser(null); window.location.href = '/login' }
    function isAdmin() { return user?.role === 'Admin' }
}
```

**`useState` cu inițializare lazy** — citește din `localStorage` o singură dată la mount, nu la fiecare render.

**`logout` cu `window.location.href`** — nu folosim `useNavigate` pentru că `logout` poate fi apelat și din interceptorul axios (care e în afara arborelui React). `window.location.href` funcționează oriunde.

**`isAdmin()`** — comparație cu string `"Admin"`. Rolul vine din tokenul validat de server la login — nu poate fi falsificat de client.

---

#### `api/base/http.ts` — modificat

**Interceptor request — adaugă token:**
```typescript
api.interceptors.request.use((config) => {
    const stored = localStorage.getItem(STORAGE_KEY)
    if (stored) {
        const { token } = JSON.parse(stored)
        if (token) config.headers.Authorization = `Bearer ${token}`
    }
    return config
})
```

Citit direct din `localStorage` (nu din React state) — interceptorul e în afara ciclului React, nu are acces la hooks sau context.

**Interceptor response — logout la 401:**
```typescript
api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem(STORAGE_KEY)
            window.location.href = '/login'
        }
        // ...
    }
)
```

Dacă tokenul expiră sau e invalid, orice request primește 401 și userul e delogat automat. Nu trebuie să verifici manual în fiecare componentă.

---

#### `api/clients/AuthApiClient.ts` — nou

```typescript
export const AuthApi = {
    login:    (email, password) => http.post<TokenDTO>('/auth/login', { email, password }),
    register: (email, password) => http.post<void>('/auth/register', { email, password }),
}
```

---

#### `api/clients/UsersApiClient.ts` — nou

```typescript
export const UsersApi = {
    getAll:     () => http.get<UserDTO[]>('/users'),
    updateRole: (id, role) => http.put<void>(`/users/${id}/role`, { role }),
}
```

---

#### `components/common/ProtectedRoute/index.tsx` — nou

```typescript
function ProtectedRoute({ requiredRole }: { requiredRole?: string }) {
    const { user } = useAuth()

    if (!user) return <Navigate to='/login' replace />
    if (requiredRole && user.role !== requiredRole) return <Navigate to='/' replace />

    return <Outlet />
}
```

`<Outlet />` randează ruta copil dacă condițiile sunt îndeplinite. Folosit în `App.tsx`:

```tsx
<Route element={<ProtectedRoute />}>               // orice user autentificat
    <Route path='/' element={<Home />} />
    <Route path='/categories' element={<Categories />} />
    ...
</Route>

<Route element={<ProtectedRoute requiredRole='Admin' />}>  // doar Admin
    <Route path='/users' element={<Users />} />
</Route>
```

`replace` la Navigate — înlocuiește intrarea din history în loc să adauge una nouă. Butonul Back nu te duce înapoi la ruta protejată după redirect.

---

#### `components/Auth/LoginPage/index.tsx` — nou

```typescript
async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    try {
        const data = await AuthApi.login(email, password)
        login({ email: data.email, role: data.role, token: data.accessToken })
        navigate('/')
    } catch {
        setError('Invalid credentials.')   // mesaj generic, nu dezvăluie cauza
    }
}
```

Mesajul de eroare e identic indiferent de cauza reală. Consistent cu backendrul.

---

#### `components/Auth/RegisterPage/index.tsx` — nou

Formular Email + Parolă, fără selector de rol. La succes → redirect la `/login`.

---

#### `components/Users/index.tsx` — nou

Accesibil doar Admin (protejat prin `ProtectedRoute`). Tabel cu toți userii, dropdown per rând:

```typescript
async function handleRoleChange(id: number, role: string) {
    await UsersApi.updateRole(id, role)
    setUsers((prev) => prev.map((u) => u.id === id ? { ...u, role } : u))
}
```

Actualizare optimistă a UI-ului local după succes — nu re-fetchuiește toată lista.

---

#### `components/Navbar/index.tsx` — modificat

```typescript
{isAdmin() && <Box component={NavLink} to='/users'>Users</Box>}
<Button onClick={logout}>Logout</Button>
```

Link-ul Users apare doar dacă userul e Admin. Butonul Logout apare doar dacă userul e autentificat.

---

#### Pagini Categories, Products, Promotions — modificate

```typescript
const { isAdmin } = useAuth()

// PageHeader
<PageHeader
    actionLabel={isAdmin() ? 'Add Category' : undefined}
    onAction={isAdmin() ? handleAdd : undefined}
/>

// Coloana Actions în tabel
{isAdmin() && <TableCell align='right'>Actions</TableCell>}

// Butoanele per rând
{isAdmin() && (
    <TableCell align='right'>
        <IconButton onClick={() => handleEdit(item)}>...</IconButton>
        <IconButton onClick={() => handleDeleteClick(item)}>...</IconButton>
    </TableCell>
)}

// FormDialog — deschis doar dacă Admin
{isAdmin() && formOpen && <CategoryFormDialog ... />}
```

**Important:** ascunderea butoanelor în UI e doar UX, nu securitate. Dacă un user ar apela direct `POST /api/categories`, ar primi 403 de la server. Securitatea reală e la nivel de API.

---

#### `App.tsx` — modificat

```typescript
function App() {
    return (
        <AuthProvider>
            <Box className='app'>
                <Navbar />
                <Routes>
                    <Route path='/login' element={<LoginPage />} />
                    <Route path='/register' element={<RegisterPage />} />

                    <Route element={<ProtectedRoute />}>
                        <Route path='/' element={<Home />} />
                        <Route path='/categories' element={<Categories />} />
                        <Route path='/products' element={<Products />} />
                        <Route path='/promotions' element={<Promotions />} />
                    </Route>

                    <Route element={<ProtectedRoute requiredRole='Admin' />}>
                        <Route path='/users' element={<Users />} />
                    </Route>

                    <Route path='*' element={<NotFound />} />
                </Routes>
            </Box>
        </AuthProvider>
    )
}
```

`AuthProvider` învelește toată aplicația — orice componentă din arbore poate apela `useAuth()`.

---

## Rezumat fișiere modificate/create

### Backend

| Fișier | Tip | Rol |
|---|---|---|
| `Entities/Enums/UserRole.cs` | nou | enum Admin/User |
| `Entities/User.cs` | nou | entitate user cu hash parolă și rol |
| `Entities/CartItem.cs` | modificat | adăugat UserId + navigation User |
| `Entities/Product.cs` | modificat | cartItem → CartItems (1:many) |
| `Configurations/UserConfiguration.cs` | nou | unique index email, enum→string |
| `Configurations/CartItemConfiguration.cs` | modificat | WithOne→WithMany, FK UserId |
| `SmartShoppingAssistantDbContext.cs` | modificat | DbSet<User> |
| Migrare EF Core | nou | tabel Users + coloana UserId în CartItems |
| `Repositories/IUserRepository.cs` | nou | GetByEmailAsync |
| `Repositories/UserRepository.cs` | nou | implementare |
| `Repositories/ICartItemRepository.cs` | modificat | userId pe toate metodele |
| `Repositories/CartItemRepository.cs` | modificat | filtrare per userId, ownership check |
| `DTOs/Auth/*.cs` | nou (5 fișiere) | RegisterDTO, LoginDTO, TokenDTO, UserGetDTO, UserRoleUpdateDTO |
| `Services/Interfaces/IAuthService.cs` | nou | Register, Login |
| `Services/Interfaces/IUserService.cs` | nou | GetAll, UpdateRole |
| `Services/Interfaces/ICartService.cs` | modificat | userId pe toate metodele |
| `Services/AuthService.cs` | nou | PBKDF2 + JWT generation |
| `Services/UserService.cs` | nou | gestiune useri |
| `Services/CartService.cs` | modificat | userId propagat la repository |
| `Mappers/CartItemMapper.cs` | modificat | ToEntity acceptă userId |
| `Program.cs` | modificat | JWT middleware, CORS restrâns, seed admin |
| `Controllers/AuthController.cs` | nou | /api/auth/register + /api/auth/login |
| `Controllers/UsersController.cs` | nou | /api/users (Admin only) |
| `Controllers/CartController.cs` | modificat | [Authorize], userId din token |
| `Controllers/CategoryController.cs` | modificat | [Authorize] / [Authorize(Roles="Admin")] |
| `Controllers/ProductController.cs` | modificat | idem |
| `Controllers/PromotionController.cs` | modificat | idem |

### Frontend

| Fișier | Tip | Rol |
|---|---|---|
| `context/AuthContext.tsx` | nou | state autentificare, login, logout, isAdmin |
| `api/base/http.ts` | modificat | interceptor Bearer token + logout la 401 |
| `api/clients/AuthApiClient.ts` | nou | login, register |
| `api/clients/UsersApiClient.ts` | nou | getAll, updateRole |
| `components/common/ProtectedRoute/index.tsx` | nou | redirect dacă neautentificat sau rol insuficient |
| `components/Auth/LoginPage/index.tsx` | nou | formular login |
| `components/Auth/RegisterPage/index.tsx` | nou | formular register |
| `components/Users/index.tsx` | nou | gestiune useri (Admin only) |
| `components/Navbar/index.tsx` | modificat | link Users (Admin), buton Logout |
| `components/common/PageHeader/index.tsx` | modificat | actionLabel și onAction opționale |
| `components/Categories/index.tsx` | modificat | butoane condiționate pe isAdmin() |
| `components/Products/index.tsx` | modificat | idem |
| `components/Promotions/index.tsx` | modificat | idem |
| `App.tsx` | modificat | AuthProvider, rute protejate, login/register |

---

## Credențiale default

| Câmp | Valoare |
|---|---|
| Email | admin@ssa.ro |
| Parolă | Admin123! |
| Rol | Admin |

Creat automat la primul startup dacă nu există niciun Admin în DB.
