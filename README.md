<h1>Inventory Management API</h1>

> 🚧 Project Status: In Development

### This project is a simple and scalable Inventory Management API built with .NET 8 and PostgreSQL. It was developed to practice clean architecture, DDD, and unit testing using FluentValidation and Entity Framework Core.

## ⚙️ Features

+ CRUD endpoints for **Products** and **Categories**: `GetAll`, `GetById`, `Add`, `Update`, `Delete`
+ Input data validation using **FluentValidation**
+ Implements **Domain-Driven Design (DDD)** with **Repository** and **Service layers**
+ Uses **Interfaces** and **Dependency Injection** for loose coupling
+ Fully covered by **unit tests** for Services, Repositories, and Controllers
+ Built with **.NET 8**, **Entity Framework Core**, and **PostgreSQL**
+ Migrations used for database schema management

## 🛠 Technologies Used

+ **.NET 8** – backend framework
+ **C#** – programming language
+ **PostgreSQL** – relational database
+ **Entity Framework Core** – ORM and migrations
+ **FluentValidation** – input data validation
+ **xUnit** – unit testing framework

## 🏛 Project Architecture

The project follows **Domain-Driven Design (DDD)** principles with a clear separation of concerns:

```
ESTOQUE-API/
├─ Properties/
│ └─ launchSettings.json
├─ Controllers/
│ ├─ CategoriasController.cs
│ └─ ProdutosController.cs
├─ Data/
│ └─ AppDbContext.cs
├─ Exceptions/
│ ├─ CategoriaException.cs
│ └─ ProdutoException.cs
├─ Migrations/
│ └─ ... (EF Core migration classes)
├─ Models/
│ ├─ Categoria.cs
│ ├─ Produto.cs
│ └─ DTO/
│ ├─ CategoriaDTO.cs
│ └─ ProdutoDTO.cs
├─ Services/
│ ├─ Application/
│ │ ├─ CategoriaService.cs
│ │ └─ ProdutoService.cs
│ └─ Interfaces/
│ ├─ ICategoriaService.cs
│ └─ IProdutoService.cs
├─ Repositories/
│ ├─ EF Core/
│ │ ├─ CategoriaRepository.cs
│ │ └─ ProdutoRepository.cs
│ └─ Interfaces/
│ ├─ ICategoriaRepository.cs
│ └─ IProdutoRepository.cs
├─ Validators/
│ ├─ CategoriaValidator.cs
│ └─ ProdutoValidator.cs
├─ appsettings.json
├─ global.json
├─ Program.cs
├─ Startup.cs
└─ tasks.txt

ESTOQUE-API.TESTS/
├─ CategoriaRepositoryFake.cs
├─ ProdutoRepositoryFake.cs
├─ CategoriaServiceTests.cs
└─ ProdutoServiceTests.cs
```

## 💻 Setup / Installation

1. **Clone the repository**:

```bash
git clone https://github.com/your-username/your-repository.git
cd your-repository
```

2. **Configure the connection string in appsettings.json:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=estoque_db;Username=your_username;Password=your_password"
  }
}
```

3. **Apply migrations and create the database:**

```bash
dotnet ef database update
```

4. **Run the application:**

```bash
dotnet run
```

5. **Test the API using Postman or any HTTP client.**

## 🔬 Running Unit Tests

**To run the unit tests:**
```bash
cd ESTOQUE-API.TESTS
dotnet test
```
