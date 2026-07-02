# SmartShoppingAssistant

> A full-stack e-commerce application with an AI-powered cart analysis pipeline, built as an internship learning project demonstrating layered .NET architecture and multi-agent orchestration.

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![React 19](https://img.shields.io/badge/React-19-61DAFB?logo=react)
![TypeScript](https://img.shields.io/badge/TypeScript-6-3178C6?logo=typescript)
![EF Core 9](https://img.shields.io/badge/EF_Core-9.0-512BD4)
![OpenAI](https://img.shields.io/badge/OpenAI-GPT--4o-412991?logo=openai)

---

## Visual Proof

> _Add a screenshot or GIF of the shop page, cart drawer, and AI analysis dialog here._

---

## Overview

SmartShoppingAssistant is a client-server web application composed of a .NET 9 REST API and a React 19 SPA. Authenticated users browse a product catalog, add items to a persistent per-user shopping cart, and receive real-time promotion discounts (e.g., "buy 3 jerseys, get the cheapest free"). On demand, a two-step AI agent workflow — built on Microsoft.Agents.AI and OpenAI — inspects the cart, identifies active and near-miss promotional deals, and then suggests up to five additional products the user could add to unlock savings. Admins manage the full catalog (products, categories, promotions) and can promote or demote user roles through a dedicated management UI.

The domain seed data is a World Cup 2026 fan merchandise store (national jerseys, balls, fan accessories, collectibles) and exists solely to make the UI non-trivial during development.

---

## Key Features

- **JWT authentication** — register/login flow; tokens are stored in `localStorage` and automatically attached to every API request via an Axios interceptor. A `401` response redirects the user to `/login`.
- **Role-based access control** — two roles (`User`, `Admin`). Admin routes (product/category/promotion/user management) are protected on both the API (`[Authorize(Roles = "Admin")]`) and the SPA (`<ProtectedRoute requiredRole="Admin">`). An admin account (`admin@ssa.ro`) is seeded automatically on first startup.
- **Per-user persistent cart** — cart items are stored in SQL Server and scoped to the authenticated user. Adding the same product twice merges quantities rather than creating a duplicate row.
- **Rule-based promotion engine** — promotions define a `Type` (`Quantity` or `CartTotal`) with a `Threshold` and a `Reward` (`FreeItems` or `PercentDiscount`). The cart endpoint calculates applicable discounts server-side and returns them as named line items.
- **Promotion progress bar** — the cart drawer shows how many RON remain until the next cart-total threshold is unlocked.
- **AI cart analysis** — `POST /api/cart/analyze` runs a two-agent workflow:
  1. **PromotionCheckerAgent** receives the cart as JSON and calls `GetPromotionsForProduct` (a function tool) for each product to identify active deals and near-miss deals.
  2. **SuggestionComposerAgent** receives the cart, all categories, and the promotion analysis, then calls `GetProductsByCategory` to build a list of up to 5 product suggestions with explanations and estimated savings.
  The result is streamed back and deserialized into a structured `AnalysisResponse`.
- **Accept/Reject suggestions** — users can add AI-suggested items directly to the cart from the analysis dialog.
- **Admin CRUD panels** — paginated, searchable tables for products, categories, promotions, and users, each backed by a dedicated API client and form dialog.
- **Shop filters** — client-side filtering by name, category checkbox, price slider, and sort order (price/name, asc/desc).
- **Swagger UI** — available at `/swagger` in Development mode.
- **Postman collection** — a full collection with Development and Production environments is included at `postman/`.

---

## System Architecture & Data Flow

```
┌─────────────────────────────────────────────────────────────────┐
│  Browser (React 19 SPA — port 5173)                             │
│  AuthContext → localStorage JWT                                  │
│  CartContext → CartProvider (fetches /api/cart on open)          │
│  Axios http client → attaches Bearer token on every request      │
└──────────────────────────┬──────────────────────────────────────┘
                           │ HTTP / JSON
                           ▼
┌─────────────────────────────────────────────────────────────────┐
│  SmartShoppingAssistant.Api  (.NET 9, port 5221)                │
│                                                                  │
│  Controllers                                                     │
│  ├── AuthController      POST /api/auth/register|login          │
│  ├── ProductsController  GET|POST|PUT|DELETE /api/products       │
│  ├── CategoryController  GET|POST|PUT|DELETE /api/categories     │
│  ├── PromotionController GET|POST|PUT|DELETE /api/promotions     │
│  ├── CartController      GET|POST|PUT|DELETE /api/cart/**        │
│  │                       POST /api/cart/analyze  ◄── AI         │
│  └── UsersController     GET|PUT /api/users (Admin only)        │
└──────────────────────────┬──────────────────────────────────────┘
                           │ DI / interfaces
                           ▼
┌─────────────────────────────────────────────────────────────────┐
│  SmartShoppingAssistant.BusinessLogic                            │
│                                                                  │
│  Services: AuthService · CartService · ProductService …         │
│                                                                  │
│  CartService.AnalyzeCartAsync()                                  │
│   1. Serialises cart to JSON                                     │
│   2. Builds PromotionCheckerAgent (tool: GetPromotionsForProduct)│
│   3. Builds SuggestionComposerAgent (tool: GetProductsByCategory)│
│   4. WorkflowBuilder chains them: Checker → Composer            │
│   5. Streams AgentResponseUpdateEvent from SuggestionComposer   │
│   6. Deserialises JSON → AnalysisResponse                        │
│                                                    ▲             │
│                                          Microsoft.Agents.AI     │
│                                          + OpenAI GPT-4o         │
└──────────────────────────┬──────────────────────────────────────┘
                           │ EF Core 9 (Repository pattern)
                           ▼
┌─────────────────────────────────────────────────────────────────┐
│  SmartShoppingAssistant.DataAccess                               │
│                                                                  │
│  Entities: User · Product · Category · Promotion · CartItem      │
│            ProductCategory (many-to-many join)                   │
│  Repositories: BaseRepository<T> · ProductRepository             │
│                PromotionRepository · CartItemRepository           │
│                UserRepository                                    │
│  DbContext: SmartShoppingAssistantDbContext                      │
│  Migrations: 8 applied migrations including seed data           │
└──────────────────────────┬──────────────────────────────────────┘
                           │
                           ▼
                  SQL Server (LocalDB in dev)
                  Database: SmartShoppingAssistant
```

> _A diagram placeholder: replace with a visual architecture diagram or ERD if needed._

---

## Project Structure

```
Netrom-AcLabs-2026/
├── SmartShoppingAssistant/                   # .NET solution root
│   ├── SmartShoppingAssistant.Api/           # ASP.NET Core Web API entry point
│   │   ├── Controllers/                      # HTTP endpoints (6 controllers)
│   │   ├── appsettings.json                  # Connection string, JWT config
│   │   └── Program.cs                        # DI wiring, middleware pipeline
│   ├── SmartShoppingAssistant.BusinessLogic/ # Domain logic, no ASP.NET dependency
│   │   ├── Agents/                           # PromotionCheckerAgent, SuggestionComposerAgent
│   │   ├── DTOs/                             # Request/response shapes (Auth, Cart, Product…)
│   │   ├── Mappers/                          # Entity ↔ DTO conversion
│   │   ├── Models/                           # AI response shapes (AnalysisResponse, PromotionAnalysis)
│   │   ├── Services/                         # Business logic implementations + interfaces
│   │   └── Tools/                            # ShoppingTools (function tools exposed to AI agents)
│   ├── SmartShoppingAssistant.DataAccess/    # EF Core, entities, repositories
│   │   ├── Configurations/                   # Fluent API entity configurations
│   │   ├── Entities/                         # Domain models + enums
│   │   ├── Migrations/                       # EF Core migration history
│   │   ├── Repositories/                     # Repository interfaces + implementations
│   │   └── Seed/                             # Static seed data (categories, products, promotions)
│   ├── SmartShoppingAssistant.Tests/         # xUnit test project (stub — no tests written yet)
│   └── docs/                                 # Internal lab notes, DB schema, endpoint screenshots
│
├── SmartShoppingAssistant.frontend/          # React 19 SPA (Vite)
│   └── src/
│       ├── api/
│       │   ├── base/http.ts                  # Axios instance with JWT interceptor
│       │   ├── clients/                      # Typed API clients per resource
│       │   └── models/                       # API response types
│       ├── components/
│       │   ├── Auth/                         # LoginPage, RegisterPage
│       │   ├── CartDrawer/                   # Slide-in cart + AnalyzeDialog (AI)
│       │   ├── Categories/                   # Admin category management table
│       │   ├── Home/                         # Landing page
│       │   ├── Navbar/                       # Top navigation
│       │   ├── ProductDetail/                # Single product view
│       │   ├── Products/                     # Admin product management table
│       │   ├── Promotions/                   # Admin promotion management table
│       │   ├── Shop/                         # Product grid with filters
│       │   ├── Users/                        # Admin user role management
│       │   └── common/                       # ProtectedRoute, ConfirmDialog, EmptyState…
│       ├── context/
│       │   ├── AuthContext.tsx               # JWT state + localStorage persistence
│       │   ├── CartContext/                  # Cart state, add/update/remove actions
│       │   └── ColorModeContext.tsx          # Light/dark theme toggle
│       └── hooks/useTableState.ts            # Shared pagination + search state for admin tables
│
└── postman/                                  # Postman collection + environments
    ├── SmartShoppingAssistant.postman_collection.json
    └── environments/                         # Development (localhost:5221), Production
```

---

## Tech Stack

### Backend

| Package | Version | Role |
|---|---|---|
| ASP.NET Core | .NET 9 | Web API host, routing, middleware |
| Entity Framework Core | 9.0.5 | ORM with code-first migrations |
| EF Core SqlServer | 9.0.5 | SQL Server / LocalDB provider |
| Microsoft.Agents.AI | 1.3.0 | Multi-agent orchestration framework |
| Microsoft.Agents.AI.OpenAI | 1.3.0 | OpenAI chat client adapter |
| Microsoft.Agents.AI.Workflows | 1.3.0 | Sequential agent workflow builder |
| Microsoft.AspNetCore.Authentication.JwtBearer | 9.0.5 | JWT validation middleware |
| System.IdentityModel.Tokens.Jwt | 8.9.0 | JWT token generation |
| Microsoft.Extensions.Identity.Core | 9.0.5 | `PasswordHasher<T>` for bcrypt-style hashing |
| OpenTelemetry.Api | 1.15.3 | Tracing instrumentation (DataAccess layer) |
| Swashbuckle / AspNetCore.OpenApi | 9.0.5 / 10.1.7 | Swagger UI at `/swagger` |

### Frontend

| Package | Version | Role |
|---|---|---|
| React | 19 | UI rendering |
| React Router DOM | 7 | Client-side routing and protected routes |
| MUI (Material UI) | 9 | Component library (Drawer, Card, DataGrid pattern, etc.) |
| Axios | 1.x | HTTP client with request/response interceptors |
| Vite | 8 | Dev server and build bundler |
| TypeScript | 6 | Static typing |

### Infrastructure

| Tool | Role |
|---|---|
| SQL Server (LocalDB) | Development database |
| EF Core Migrations | Schema management |
| Postman | API testing collection included in repo |

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9)
- SQL Server or SQL Server LocalDB (ships with Visual Studio)
- Node.js ≥ 20 and npm
- An OpenAI API key (used by the AI cart analysis agents)

### 1 — Backend

```bash
cd SmartShoppingAssistant
```

**Configure secrets** — the API requires a JWT signing key and an OpenAI API key that are not stored in `appsettings.json`. Use .NET User Secrets (recommended for dev) or add them to `appsettings.Development.json`:

```bash
dotnet user-secrets set "Jwt:Secret" "your-32-char-minimum-secret-here" \
  --project SmartShoppingAssistant.Api

dotnet user-secrets set "OpenAI:ApiKey" "sk-..." \
  --project SmartShoppingAssistant.Api

# Optional — defaults to gpt-4o if omitted
dotnet user-secrets set "OpenAI:ModelId" "gpt-4o" \
  --project SmartShoppingAssistant.Api
```

The default connection string in `appsettings.json` targets `(localdb)\MSSQLLocalDB`. Adjust it if you use a different SQL Server instance.

**Apply migrations** (creates the database and seeds categories, products, and promotions):

```bash
dotnet ef database update \
  --project SmartShoppingAssistant.DataAccess \
  --startup-project SmartShoppingAssistant.Api
```

**Run the API:**

```bash
dotnet run --project SmartShoppingAssistant.Api
# API available at http://localhost:5221
# Swagger UI at http://localhost:5221/swagger
```

On first startup, an admin account is created automatically if none exists:
- **Email:** `admin@ssa.ro`
- **Password:** `Admin123!`

### 2 — Frontend

```bash
cd SmartShoppingAssistant.frontend
```

**Create a `.env` file** with the API base URL:

```env
VITE_API_URL=http://localhost:5221
```

**Install dependencies and start the dev server:**

```bash
npm install
npm run dev
# SPA available at http://localhost:5173
```

**Build for production:**

```bash
npm run build
# Output in dist/
```

---

## API Reference

All endpoints (except `/api/auth/register` and `/api/auth/login`) require a `Bearer` token in the `Authorization` header. Endpoints marked **Admin** additionally require the `Admin` role claim.

### Authentication

```
POST /api/auth/register    { email, password }          → 201
POST /api/auth/login       { email, password }          → 200 { accessToken, expiresAt, email, role }
```

### Products

```
GET    /api/products?pageNumber=1&pageSize=20&search=...&categoryId=...  → PagedResult<Product>
GET    /api/products/{id}                                                 → Product
POST   /api/products        { name, description, imageUrl, price, categoryIds }  → 201  [Admin]
PUT    /api/products/{id}   { name, description, imageUrl, price, categoryIds }  → 200  [Admin]
DELETE /api/products/{id}                                                 → 204  [Admin]
```

### Categories

```
GET    /api/categories?pageNumber=1&pageSize=20  → PagedResult<Category>
GET    /api/categories/{id}                      → Category
POST   /api/categories   { name, description }   → 201  [Admin]
PUT    /api/categories/{id}                      → 200  [Admin]
DELETE /api/categories/{id}                      → 204  [Admin]
```

### Promotions

```
GET    /api/promotions?pageNumber=1&pageSize=20  → PagedResult<Promotion>
GET    /api/promotions/{id}                      → Promotion
POST   /api/promotions   { name, type, threshold, reward, rewardValue, productId?, categoryId?, isActive }  → 201  [Admin]
PUT    /api/promotions/{id}                      → 200  [Admin]
DELETE /api/promotions/{id}                      → 204  [Admin]
```

**Promotion types:** `0 = Quantity` (triggers on item count), `1 = CartTotal` (triggers on cart subtotal in RON).  
**Promotion rewards:** `0 = FreeItems` (cheapest N items free), `1 = PercentDiscount` (N% off applicable items).

### Cart (requires authentication)

```
GET    /api/cart                           → CartGetDTO (items, subtotal, discount, total, appliedPromotions)
POST   /api/cart/items   { productId, quantity }   → CartItemGetDTO
PUT    /api/cart/items/{itemId}   { quantity }     → CartItemGetDTO
DELETE /api/cart/items/{itemId}                    → 204
DELETE /api/cart                                   → 204
POST   /api/cart/analyze                           → AnalysisResponse (AI)
```

**`POST /api/cart/analyze` response shape:**

```json
{
  "summary": "Adding one more jersey unlocks the 3-for-2 promotion saving you ~85 RON.",
  "suggestions": [
    {
      "productId": 12,
      "name": "România Home Jersey 2026",
      "price": 179.99,
      "quantity": 1,
      "reason": "Adds the third jersey needed to trigger the 3-for-2 deal.",
      "savings": 84.99
    }
  ]
}
```

### Users (Admin only)

```
GET /api/users                            → UserGetDTO[]
PUT /api/users/{id}/role   { role }       → 204
```

---

## Roadmap

The following are visible gaps in the current codebase:

- **Tests** — `SmartShoppingAssistant.Tests` exists but contains only a stub (`UnitTest1` with an empty `[Fact]`). No coverage of services, promotion calculation logic, or AI agent integration.
- **HTTPS in development** — the CORS policy is pinned to `http://localhost:5173`; the HTTPS launch profile (`https://localhost:7139`) is defined but not wired to CORS.
- **No `.env.example`** — the frontend requires `VITE_API_URL` but no example file is committed. Developers must know to create it.
- **No refresh token** — JWTs expire after 60 minutes (configurable via `Jwt:ExpiryMinutes`); there is no token refresh flow.
- **Cart total threshold (DB schema note)** — the DB schema doc states "there is only one cart", which predates the per-user cart introduced in migration `20260531172000_AddUsersAndCartPerUser`. The schema doc is outdated.

---

## License & Contributing

No license file is present in the repository. Treat the code as proprietary unless one is added.

To contribute:
1. Fork the repository and create a feature branch from `main`.
2. Run `dotnet build` and `npm run build` before opening a PR to confirm nothing is broken.
3. If you add a migration, include the generated migration files in the PR.

---

## Contact

- **Author:** [darknick131](https://github.com/darknick131)
- **Issues:** Open an issue in the repository.

---

## Assumptions to Verify

The following claims in this README are inferences rather than things I read directly from the code. Confirm or correct them before publishing:

1. **OpenAI model** — `Program.cs` defaults to `"gpt-4o"` if `OpenAI:ModelId` is not set. Verify that the key you have access to can call `gpt-4o`.
2. **`dotnet ef` startup project** — I inferred the EF CLI command from the project structure. If you run migrations from inside Visual Studio's Package Manager Console, the `--startup-project` flag is not needed; just ensure `SmartShoppingAssistant.DataAccess` is selected as the Default Project.
3. **JWT secret minimum length** — HMAC-SHA256 keys should be at least 256 bits (32 UTF-8 characters). This is a general rule; the code does not enforce a minimum length explicitly.
4. **`ColorModeContext`** — a dark/light mode context exists in the frontend but I did not trace where it is consumed (it is not wired in `App.tsx`). The theme toggle may be incomplete.
