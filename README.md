# BurgerDinner API - Arquitetura DDD com .NET 10

##  Visão Geral

API RESTful para gerenciamento de um sistema de Burger Dinner implementada com **Domain-Driven Design (DDD)** e **.NET 10**.

## Arquitetura

A aplicação segue os princípios do DDD com separação clara de responsabilidades em 4 camadas principais:

```
BurgerDinner/
├── Domain/           # Camada de Domínio (Core Business)
├── Application/      # Camada de Aplicação (Use Cases)
├── Infrastructure/   # Camada de Infraestrutura (Data Access)
└── API/             # Camada de Apresentação (Controllers)
```

## Estrutura de Pastas

### Domain Layer (Domínio)
Contém o coração do negócio, regras de negócio e entidades principais.

```
Domain/
├── Entities/         # Entidades de domínio
│   ├── Burger.cs          # Hambúrguer com lógica de negócio
│   ├── Ingredient.cs      # Ingredientes com controle de estoque
│   ├── Order.cs          # Pedidos com status e itens
│   ├── OrderItem.cs      # Itens do pedido
│   └── BurgerIngredient.cs # Relacionamento N:N
├── Enums/           # Enumerações de domínio
│   └── OrderStatus.cs     # Status do pedido
└── Interfaces/      # Interfaces de repositórios
    ├── IBurgerRepository.cs
    ├── IIngredientRepository.cs
    └── IOrderRepository.cs
```

**Principais características:**
- **Entidades ricas**: Contém lógica de negócio (ex: `burger.UpdatePrice()`)
- **Invariantes protegidas**: Validações dentro das entidades
- **Agregados**: `Order` como raiz de agregado com `OrderItem`
- **Value Objects**: Conceitos como `Price`, `Email` (futuro)

###  Application Layer (Aplicação)
Orquestra casos de uso e coordena objetos de domínio.

```
Application/
├── DTOs/            # Data Transfer Objects
│   ├── BurgerDto.cs       # DTO para resposta
│   ├── CreateBurgerDto.cs # DTO para criação
│   ├── UpdateBurgerDto.cs # DTO para atualização
│   ├── IngredientDto.cs
│   └── OrderDto.cs
├── Services/        # Services de aplicação
│   ├── IBurgerService.cs
│   └── BurgerService.cs
└── Interfaces/      # Interfaces de services
```

**Responsabilidades:**
- **Orquestração**: Coordena múltiplas entidades
- **Transformação**: Converte entre Entidades ↔ DTOs
- **Casos de uso**: Implementa fluxos de negócio complexos

###  Infrastructure Layer (Infraestrutura)
Implementa interfaces de domínio e detalhes técnicos.

```
Infrastructure/
└── Repositories/    # Implementação concreta
    ├── BurgerRepository.cs    # Em memória (mock)
    ├── IngredientRepository.cs
    └── OrderRepository.cs
```

**Características atuais:**
- **Repositórios em memória**: Para desenvolvimento/testes
- **Future EF Core**: Para persistência real
- **External services**: Integrações futuras

### API Layer (Apresentação)
Expõe endpoints HTTP e lida com comunicação externa.

```
API/
└── Controllers/
    └── BurgersController.cs  # RESTful endpoints
```

**Endpoints implementados:**
- `GET /api/burgers` - Listar todos
- `GET /api/burgers/available` - Disponíveis
- `GET /api/burgers/{id}` - Por ID
- `POST /api/burgers` - Criar
- `PUT /api/burgers/{id}` - Atualizar
- `DELETE /api/burgers/{id}` - Remover

## Fluxo de Dados

```
HTTP Request → Controller → Service → Repository → Domain Entity
                ↓           ↓          ↓           ↓
            DTO     →  DTO  →  Entity  →  Business Logic
                ↑           ↑          ↑           ↑
Response ← Controller ← Service ← Repository ← Domain Entity
```

## Princípios DDD Aplicados

### 1. **Ubiquitous Language**
- Entidades refletem linguagem do negócio: `Burger`, `Ingredient`, `Order`
- Métodos expressam intenções: `AddIngredient()`, `UpdateStatus()`

### 2. **Bounded Context**
- Contexto claro: "Gestão de Burger Dinner"
- Entidades coesas com responsabilidades bem definidas

### 3. **Aggregates**
- **Order** como aggregate root: controla `OrderItem`
- **Burger** como aggregate root: controla `BurgerIngredient`

### 4. **Repository Pattern**
- Abstração do acesso a dados
- Interfaces no domínio, implementação na infraestrutura

### 5. **Dependency Inversion**
- Domain não depende de Infrastructure
- Injeção de dependência via DI container

## Tecnologias

- **.NET 10** - Framework mais recente
- **ASP.NET Core** - API RESTful
- **OpenAPI** - Documentação automática
- **Injeção de Dependência** - DI Container nativo

## Exemplo de Uso

### Criar um Hambúrguer
```bash
POST /api/burgers
{
  "name": "Classic Burger",
  "description": "Traditional beef burger",
  "price": 12.50,
  "ingredientIds": []
}
```

### Resposta
```json
{
  "id": "74ff9d19-218c-4508-9ed3-32bdf7aa13f4",
  "name": "Classic Burger",
  "description": "Traditional beef burger",
  "price": 12.50,
  "isAvailable": true,
  "createdAt": "2026-01-13T18:34:46.8839921Z",
  "updatedAt": null,
  "ingredients": []
}
```

## Próximos Passos

### Imediatos
1. **Entity Framework Core**
   - Configurar DbContext
   - Migrations para SQL Server/PostgreSQL
   - Substituir repositórios em memória

2. **Controllers Adicionais**
   - `IngredientsController`
   - `OrdersController`

3. **Validações**
   - FluentValidation
   - Custom error handling

### Futuros
1. **Autenticação & Autorização**
   - JWT Tokens
   - Role-based access

2. **Event-Driven Architecture**
   - Domain Events
   - Message Queue (RabbitMQ/Kafka)

3. **Testing**
   - Unit tests (xUnit)
   - Integration tests
   - API tests (WebApplicationFactory)

## Como Executar

```bash
# Restaurar pacotes
dotnet restore

# Compilar
dotnet build

# Executar
dotnet run --urls=http://localhost:5000
```

### Acessar
- **API**: http://localhost:5000
- **OpenAPI**: http://localhost:5000/openapi/v1.json

## Convenções

- **Naming**: PascalCase para classes, camelCase para propriedades
- **Async**: Todos os métodos de I/O são assíncronos
- **Immutability**: Propriedades com setters privados onde possível
- **Validation**: Regras de negócio nas entidades
- **Error Handling**: Exceções específicas para cada caso

---

**Arquitetura escalável, manutenível e alinhada com as melhores práticas DDD!**
# BurgerDinnerApi
