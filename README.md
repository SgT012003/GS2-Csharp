# The New Space Economy - Global Solution

### Group

| Name | RM |
|:----:|:----:|
| Matheus Zottis | 94119 |
| Victor Didoff | 552965 |
| Vinicius Silva | 553240 |

---

Welcome to the official repository of **The New Space Economy** project. This system is a modern, distributed web platform focused on cataloging, monitoring, and analyzing innovations and technologies inherited from space exploration that impact society (Health, Agriculture, Consumption, etc.).

## Requirements Documentation

The project was built meeting strict architectural requirements aiming at scalability and security:
- **Orchestrated Microservices**: Uses **.NET Aspire** as the central orchestrator for telemetry, logs, and lifecycle.
- **Total Decoupling**: The MVC Front-End (`ProjetoGS.Web`) has no connection to the database and consumes data asynchronously through `HttpClient` injection and Service Discovery.
- **Relational Database**: Implemented using MySQL on the host machine and accessed via **Entity Framework Core 9**, strictly using the Repository Pattern and Dependency Injection.
- **Security (Cryptography)**: Passwords are never stored in plain text. The **BCrypt.Net-Next** library is used for secure hashing.
- **Access Control (Claims)**: Uses Cookie-based Authentication. Only users with the `Administrator` Claim/Role can perform critical actions such as create, edit, or delete.

---

## Structural Declaration

The solution is divided into the following modules under the `GlobalSolutionSpace/` folder:

- **`GlobalSolutionSpace.AppHost`**
  - The heart of orchestration. Defines the application's dependency graph. Runs and monitors the `apiservice` and `webfrontend` services.
- **`GlobalSolutionSpace.ServiceDefaults`**
  - Contains standardized configurations for resilience, metrics, Health Checks, and OpenTelemetry for the entire solution.
- **`ProjetoGS.ApiService`**
  - Isolated Backend. Hosts the business logic, database Models (`Tecnologia`, `CategoriaImpacto`, `Usuario`), Entity Framework Core Repositories, and RESTful API Controllers.
- **`ProjetoGS.Web`**
  - Front-End using **ASP.NET Core MVC**. Responsively designed interface with **Bootstrap 5**, focused on User Experience (UX), featuring an interactive Dashboard consuming the API.

---

## Installation and Execution Procedure (Initialization)

### Prerequisites
1. **.NET SDK (8.0, 9.0, or 10.0)** installed. (The project currently uses .NET 10).
2. **MySQL** server running locally (Default port 3306).
3. Visual Studio 2022, VS Code, or Rider.

### Local Initialization Steps

1. **Database Configuration**
   - Access `ProjetoGS.ApiService/appsettings.json` and verify the `DefaultConnection` Connection String.
   - By default, it will look for: `Server=localhost;Database=gs_db;Uid=root;Pwd=root;`. Modify according to your local MySQL user/password.

2. **Applying the Migration**
   - To create the schema in your local database, open a terminal at the root of `GlobalSolutionSpace/ProjetoGS.ApiService` and run:
     ```bash
     dotnet ef database update
     ```

3. **Starting the Solution with Aspire**
   - Instead of starting the API and Web separately, you will always start the orchestrator.
   - Navigate to the AppHost folder:
     ```bash
     cd GlobalSolutionSpace/GlobalSolutionSpace.AppHost
     dotnet run
     ```
   - The console will display a URL for the **.NET Aspire Dashboard**. Open it in the browser to see the application map and access real-time logs.

---

## Deployment Guide (Publishing)

To deploy on traditional IIS servers or cloud (Azure App Service, AWS, etc.):

1. **Production Build Generation**
   Run the publish command on the individual projects:
   ```bash
   dotnet publish ProjetoGS.ApiService -c Release -o ./publish/api
   dotnet publish ProjetoGS.Web -c Release -o ./publish/web
   ```
2. **Hosting**
   - For IIS, create two separate *Application Pools*, point each site to its respective `/publish` folder.
   - Modify the `appsettings.json` of `ProjetoGS.Web` (production version) so that the API URL stops using the internal Service Discovery (`http://apiservice`) and points to the actual public URL of your API (e.g., `https://api.novaeconomia.com.br`).

---

## API Endpoints Examples

The `ApiService` exposes routes for the Technologies entity. The endpoints use standard JSON format.

### 1. Create a Technology (POST)
**Endpoint:** `POST /api/tecnologias`
```json
{
  "nome": "CMOS Image Sensors",
  "descricao": "Miniaturized technology inherited from telescopes, now in cell phones.",
  "origemEspacial": "NASA planetary exploration missions",
  "categoriaImpactoId": 1
}
```

### 2. Fetch Technologies (GET)
**Endpoint:** `GET /api/tecnologias`
**Response:**
```json
[
  {
    "id": 1,
    "nome": "CMOS Image Sensors",
    "descricao": "Miniaturized technology...",
    "origemEspacial": "NASA planetary exploration missions",
    "dataCadastro": "2026-06-02T15:30:00Z",
    "categoriaImpactoId": 1,
    "categoriaImpacto": null
  }
]
```

### 3. Statistics Endpoint / Dashboard
**Endpoint:** `GET /api/tecnologias/stats`
This custom endpoint aggregates data using LINQ to feed the Dashboard in real-time.
**Response:**
```json
{
  "totalTecnologias": 150,
  "porSetor": [
    { "setor": "Health", "quantidade": 50 },
    { "setor": "Agriculture", "quantidade": 75 },
    { "setor": "Consumption", "quantidade": 25 }
  ],
  "ultimasCadastradas": [
    {
      "id": 150,
      "nome": "Water Purifiers",
      "origemEspacial": "Apollo 11",
      "dataCadastro": "2026-06-02T16:00:00Z"
    }
  ]
}
```

---

## First Access Example (Front-End MVC)

1. When you initialize the project and click the `webfrontend` endpoint via the Aspire Dashboard, you will be taken to the **Home Page (Dashboard)**.
2. The system will immediately make an asynchronous call to the `/stats` endpoint of the API, displaying:
   - A Main Card with the absolute sum.
   - Bootstrap 5 style Progress Bars calculating the mathematical percentage of each sector's size against the total.
   - The responsive table (grid) below containing the Top 5 most recent registrations.
3. **Administrative Access**: 
   - Some routes (`/Tecnologias/Create`, `/Tecnologias/Delete`) are strictly closed. If the user tries to access the Registration screen without a valid Cookie with the `Administrator` Claim, they will be automatically redirected to the MVC Login screen.