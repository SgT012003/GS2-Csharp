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

## Initialization & Docker Support

### Prerequisites
1. **.NET SDK (8.0+)** installed.
2. **Docker Desktop** (For fast local testing without installing MySQL).

### Running via Docker (Development & Testing)
We have configured a `docker-compose.yml` for you to run the entire system instantly without local databases.
1. Open a terminal at the root directory where `docker-compose.yml` is located.
2. Run:
   ```bash
   docker-compose up --build -d
   ```
3. The system will start, automatically run the database migrations, and **Seed** the initial data (Default Origins and Administrator Account).

### Starting Locally with Aspire
If you prefer running locally without Docker:
1. Ensure your MySQL is running and update the `appsettings.json` Connection String inside `ProjetoGS.ApiService`.
2. Navigate to the AppHost folder:
   ```bash
   cd GlobalSolutionSpace/GlobalSolutionSpace.AppHost
   dotnet run
   ```
3. The **.NET Aspire Dashboard** will open. Click the `webfrontend` link.

---

## Authentication & Default Credentials

The system employs a strict Authentication loop. Only logged-in users can access the **Dashboard** and CRUD operations.
To test the system immediately, the Seeder provides a default admin account:
- **Email:** `admin@novaeconomia.space`
- **Password:** `Admin@123`

---

## Architecture & Visual Identity (Antigravity UI)

### Backend (ProjetoGS.ApiService)
- Exposes routes for `Tecnologias`, `Origens`, and `Usuarios`.
- `Origens` is fully decoupled from free-text, enforcing a relational database structure (e.g., ISS, Apollo, Hubble).

### Frontend (ProjetoGS.Web)
- **Landing Page vs Dashboard:** The entry point (`/`) is a premium Landing Page. The statistical graphs and data are securely gated behind the `/Home/Dashboard` route, requiring authentication.
- **"Antigravity" Aesthetic:** We implemented a high-end UI design system focusing on:
  - **Glassmorphism:** Frosted glass effect for cards and panels.
  - **Dark Space Theme:** Deep Indigo and Cyan radial gradients.
  - **Micro-Animations:** Fluid CSS fade-up animations and hover scaling for high-contrast tables.
  - **ApexCharts Integration:** Data is now visualized interactively through animated Donut Charts replacing standard progress bars.