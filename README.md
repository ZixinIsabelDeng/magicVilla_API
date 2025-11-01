# 🏖️ Magic Villa - Vacation Rental Management System

[![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-7.0-512BD4?style=flat&logo=asp.net-core)](https://dotnet.microsoft.com/apps/aspnet)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat&logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)

A full-stack vacation rental management system built with ASP.NET Core, featuring villa listings, booking management, and comprehensive admin capabilities. This project demonstrates modern web development practices including RESTful API design, JWT authentication, API versioning, caching, and pagination.

## 📋 Table of Contents

- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Prerequisites](#-prerequisites)
- [Installation & Setup](#-installation--setup)
- [Running the Project](#-running-the-project)
- [API Documentation](#-api-documentation)
- [Project Structure](#-project-structure)
- [Key Features Details](#-key-features-details)
- [Authentication](#-authentication)
- [Deployment](#-deployment)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

### 🏠 Villa Management
- **Browse Villas**: View available vacation rentals with detailed information
- **Search & Filter**: Search villas by name, filter by occupancy
- **Admin CRUD Operations**: Create, read, update, and delete villa listings
- **Image Management**: Display villa images with responsive design
- **Pagination**: Efficient data loading with configurable page sizes

### 📅 Booking System
- **Reservation Management**: Create and manage villa bookings
- **Availability Checking**: Automatic availability validation
- **Cost Calculation**: Automatic price calculation based on dates and occupancy
- **Booking Status**: Track booking status (Pending, Confirmed, Cancelled)
- **User Bookings**: View personal booking history

### 🔐 Authentication & Authorization
- **JWT Authentication**: Secure token-based authentication for API
- **Cookie Authentication**: Session management for web application
- **Role-Based Access**: Admin and user role separation
- **Protected Endpoints**: Secure API endpoints with authorization

### 🌐 API Features
- **RESTful API**: Clean, REST-compliant API design
- **API Versioning**: Support for multiple API versions (v1, v2)
- **Response Caching**: Performance optimization with caching strategies
- **Swagger Documentation**: Interactive API documentation
- **Pagination Support**: Efficient data retrieval for large datasets

### 🎨 Frontend Features
- **Responsive Design**: Bootstrap-powered responsive UI
- **Razor Views**: Server-side rendering with Razor View Engine
- **AJAX Integration**: Dynamic content loading without page refresh
- **Form Validation**: Client and server-side validation
- **User-Friendly Interface**: Intuitive navigation and UX

## 🛠️ Tech Stack

### Backend
- **ASP.NET Core 7.0** - Web framework
- **C# 12** - Programming language
- **Entity Framework Core 7.0** - ORM for database operations
- **SQL Server** - Relational database
- **AutoMapper** - Object-to-object mapping
- **JWT Bearer** - Token-based authentication
- **Swagger/OpenAPI** - API documentation

### Frontend
- **ASP.NET Core MVC** - MVC framework
- **Razor View Engine** - Server-side templating
- **Bootstrap 5** - CSS framework for responsive design
- **jQuery** - JavaScript library for DOM manipulation
- **AJAX** - Asynchronous HTTP requests

### Tools & Libraries
- **Docker** - Containerization for SQL Server (macOS)
- **Git** - Version control
- **Swagger UI** - Interactive API testing

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     Magic Villa System                       │
├─────────────────────────────────────────────────────────────┤
│                                                               │
│  ┌──────────────────┐         ┌──────────────────┐          │
│  │  MagicVilla_Web  │────────▶│ magicVilla_API   │          │
│  │  (Frontend MVC)  │  HTTP   │  (REST API)      │          │
│  │                  │◀────────│                  │          │
│  └──────────────────┘         └────────┬─────────┘          │
│         │                               │                    │
│         │                               │                    │
│    ┌────▼────┐                    ┌────▼────┐               │
│    │ Browser │                    │Repository│               │
│    │ (Razor) │                    │  Pattern │               │
│    └─────────┘                    └────┬────┘               │
│                                        │                     │
│                                   ┌────▼────┐               │
│                                   │   EF    │               │
│                                   │   Core  │               │
│                                   └────┬────┘               │
│                                        │                     │
│                                   ┌────▼────┐               │
│                                   │SQL Server│              │
│                                   │ Database │              │
│                                   └──────────┘              │
│                                                               │
└─────────────────────────────────────────────────────────────┘
```

### Architecture Patterns
- **Repository Pattern**: Abstraction layer for data access
- **Dependency Injection**: Loose coupling and testability
- **DTO Pattern**: Data transfer objects for API communication
- **MVC Pattern**: Separation of concerns in web application

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- **.NET SDK 7.0 or later** - [Download here](https://dotnet.microsoft.com/download/dotnet/7.0)
- **SQL Server 2022** or **SQL Server Express** - [Download here](https://www.microsoft.com/sql-server/sql-server-downloads)
  - *Alternative for macOS*: Use SQL Server in Docker (see setup instructions below)
- **Docker Desktop** (optional, for macOS users) - [Download here](https://www.docker.com/products/docker-desktop)
- **Visual Studio 2022** or **Visual Studio Code** with C# extension
- **Git** - For cloning the repository

## 🚀 Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/ZixinIsabelDeng/magicVilla_API.git
cd magicVilla_API
```

### 2. Database Setup

#### Option A: Windows (SQL Server Express)

Update the connection string in `magicVilla_VillaAPI/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Magic_VillaAPI;Trusted_Connection=True;TrustServerCertificate=true"
}
```

#### Option B: macOS/Linux (Docker)

1. Start SQL Server in Docker:
   ```bash
   ./setup-sqlserver-docker.sh
   ```

   Or manually:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
      -p 1433:1433 --name magicvilla-sqlserver \
      -d mcr.microsoft.com/mssql/server:2022-latest
   ```

2. The connection string is already configured for Docker in `appsettings.json`

### 3. Apply Database Migrations

Navigate to the API project and run migrations:

```bash
cd magicVilla_VillaAPI
dotnet ef database update
```

This will:
- Create the `Magic_VillaAPI` database
- Create all necessary tables (Villas, VillaNumbers, Bookings, Users)
- Seed initial data with sample villas

### 4. Configure API Secret (Optional but Recommended)

Update the JWT secret in `magicVilla_VillaAPI/appsettings.json`:

```json
"ApiSetting": {
  "Secret": "YOUR_SUPER_SECRET_KEY_HERE_AT_LEAST_32_CHARACTERS"
}
```

## ▶️ Running the Project

### Quick Start (Automated)

Use the provided startup script:

```bash
./start-magic-villa.sh
```

This script will:
- Start SQL Server (if using Docker)
- Run database migrations
- Start the API server
- Start the Web application

### Manual Start

#### Step 1: Start the API (Backend)

**Terminal 1:**
```bash
cd magicVilla_VillaAPI
dotnet run
```

The API will be available at:
- **HTTPS**: `https://localhost:7001`
- **HTTP**: `http://localhost:5065`
- **Swagger UI**: `https://localhost:7001/swagger`

#### Step 2: Start the Web Application (Frontend)

**Terminal 2:**
```bash
cd MagicVilla_Web
dotnet run
```

The web application will be available at:
- **HTTPS**: `https://localhost:7002`
- **HTTP**: `http://localhost:5136`

### Verify Configuration

Ensure the Web app can communicate with the API. Check `MagicVilla_Web/appsettings.json`:

```json
"ServiceUrls": {
  "VillaAPI": "http://localhost:5065"
}
```

Update this URL if your API runs on a different port.

## 📚 API Documentation

### Accessing Swagger UI

Once the API is running, navigate to:
```
https://localhost:7001/swagger
```

### API Endpoints

#### Villa API (`/api/v1/VillaAPI`)
- `GET /api/v1/VillaAPI` - Get all villas (with pagination, filtering, search)
- `GET /api/v1/VillaAPI/{id}` - Get villa by ID
- `POST /api/v1/VillaAPI` - Create new villa (Admin only)
- `PUT /api/v1/VillaAPI/{id}` - Update villa (Admin only)
- `PATCH /api/v1/VillaAPI/{id}` - Partial update (Admin only)
- `DELETE /api/v1/VillaAPI/{id}` - Delete villa (Admin only)

#### Villa Number API (`/api/v1/VillaNumberAPI`)
- `GET /api/v1/VillaNumberAPI` - Get all villa numbers
- `GET /api/v1/VillaNumberAPI/{id}` - Get villa number by ID
- `POST /api/v1/VillaNumberAPI` - Create new villa number
- `PUT /api/v1/VillaNumberAPI/{id}` - Update villa number
- `DELETE /api/v1/VillaNumberAPI/{id}` - Delete villa number

#### Booking API (`/api/v1/BookingAPI`)
- `GET /api/v1/BookingAPI` - Get all bookings (with optional user filter)
- `GET /api/v1/BookingAPI/{id}` - Get booking by ID
- `POST /api/v1/BookingAPI` - Create new booking
- `PUT /api/v1/BookingAPI/{id}` - Update booking
- `DELETE /api/v1/BookingAPI/{id}` - Delete booking

#### Authentication (`/api/Users`)
- `POST /api/Users/login` - User login
- `POST /api/Users/register` - User registration

### API Versioning

The API supports versioning:
- **v1**: Current stable version (`/api/v1/...`)
- **v2**: Future version (`/api/v2/...`)

### Authentication

Most endpoints require authentication. Include the JWT token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## 📁 Project Structure

```
magicVilla_API/
│
├── magicVilla_VillaAPI/          # REST API Backend
│   ├── Controllers/              # API Controllers
│   │   ├── UsersController.cs   # Authentication endpoints
│   │   ├── v1/                  # API Version 1
│   │   │   ├── ValuesController.cs      # Villa API
│   │   │   ├── VillaNumberAPIController.cs
│   │   │   └── BookingAPIController.cs
│   │   └── v2/                  # API Version 2
│   ├── Data/
│   │   └── ApplicationDbContext.cs  # EF Core DbContext
│   ├── Models/                   # Domain Models
│   │   ├── Villa.cs
│   │   ├── VillaNumber.cs
│   │   ├── Booking.cs
│   │   └── Dto/                 # Data Transfer Objects
│   ├── Repository/              # Repository Pattern Implementation
│   │   ├── IRepository/         # Repository Interfaces
│   │   └── *.cs                 # Repository Implementations
│   ├── Migrations/              # EF Core Migrations
│   ├── Program.cs               # Application Entry Point
│   └── appsettings.json         # Configuration
│
├── MagicVilla_Web/              # Web Application Frontend
│   ├── Controllers/             # MVC Controllers
│   │   ├── HomeController.cs
│   │   ├── VillaController.cs
│   │   ├── VillaNumberController.cs
│   │   ├── BookingController.cs
│   │   └── AuthController.cs
│   ├── Views/                   # Razor Views
│   │   ├── Home/
│   │   ├── Villa/
│   │   ├── VillaNumber/
│   │   ├── Booking/
│   │   └── Auth/
│   ├── Services/                # Service Layer
│   │   ├── IServices/           # Service Interfaces
│   │   └── *.cs                 # Service Implementations
│   ├── Models/                  # View Models and DTOs
│   ├── wwwroot/                 # Static Files (CSS, JS, Images)
│   └── Program.cs               # Application Entry Point
│
├── MagicVilla_Utility/          # Shared Utilities
│   └── SD.cs                    # Static Details (Roles, etc.)
│
├── Dockerfile                   # Docker configuration for API
├── docker-compose.yml           # Docker Compose setup
├── start-magic-villa.sh         # Startup script
├── setup-sqlserver-docker.sh    # SQL Server Docker setup
└── README.md                    # This file
```

## 🔑 Key Features Details

### Pagination
Villa listings support pagination with configurable page size:
```
GET /api/v1/VillaAPI?PageNumber=1&pageSize=5
```

### Response Caching
API responses are cached for 30 seconds (configurable) to improve performance:
```csharp
[ResponseCache(CacheProfileName = "Default30")]
```

### Search & Filtering
- **Search**: Filter villas by name using the `search` query parameter
- **Filter**: Filter by occupancy using `filterOccupancy` query parameter

### Automatic Cost Calculation
Bookings automatically calculate total cost based on:
- Villa price per night
- Number of nights
- Number of guests

### Availability Validation
The booking system checks for:
- Date conflicts with existing bookings
- Minimum stay requirements
- Maximum occupancy limits

## 🔐 Authentication

### Default Admin Credentials

After running migrations, you can register a new user or use seed data. The application uses JWT tokens for API authentication.

### Registration Flow
1. Navigate to `/Auth/Register`
2. Create an account
3. Login at `/Auth/Login`
4. Admin role can be assigned manually in the database or during registration

### JWT Token Usage

Once authenticated, the web application stores the JWT token in a cookie. For API testing:
1. Login via `/api/Users/login`
2. Copy the token from the response
3. Use it in Swagger UI: Click "Authorize" → Enter `Bearer <token>`

## 🚀 Deployment

This project can be deployed to various cloud platforms. See the deployment guides for detailed instructions:

### Quick Deployment (15 minutes)
👉 **[DEPLOYMENT_QUICK_START.md](./DEPLOYMENT_QUICK_START.md)** - Fastest way to deploy

### Full Deployment Guide
👉 **[DEPLOYMENT_GUIDE.md](./DEPLOYMENT_GUIDE.md)** - Comprehensive guide for all platforms

### Supported Platforms
- 🚂 **Railway** (Recommended - Free tier, easiest setup)
- 🎨 **Render** (Free tier available)
- ☁️ **Azure App Service** (Microsoft platform)
- 🐳 **Docker** (Any container platform)

### Docker Deployment

The project includes Dockerfiles for both API and Web applications:

```bash
# Build and run with Docker Compose
docker-compose up -d
```

See `docker-compose.yml` for configuration details.

## 📸 Screenshots

*Add screenshots of your application here*

### Villa Listing Page
- Browse available villas with images and details

### Booking Interface
- User-friendly booking form with date selection
- Automatic price calculation
- Booking confirmation page

### Admin Dashboard
- Manage villas, villa numbers, and bookings
- User management interface

*Note: Screenshots can be added to showcase the UI/UX*

## 🤝 Contributing

Contributions are welcome! If you'd like to contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow C# coding conventions
- Write meaningful commit messages
- Add comments for complex logic
- Update documentation for new features

## 🐛 Troubleshooting

### Database Connection Issues
- **Windows**: Ensure SQL Server is running and instance name matches connection string
- **macOS/Docker**: Verify Docker container is running: `docker ps | grep magicvilla-sqlserver`
- Check firewall settings if using remote database

### Port Conflicts
If ports 7001, 7002, 5065, or 5136 are in use:
- Update ports in `launchSettings.json` files
- Update `ServiceUrls` in `MagicVilla_Web/appsettings.json`

### SSL Certificate Warnings
In development, you may see SSL certificate warnings. This is normal:
- Click "Advanced" → "Proceed to localhost" (Chrome)
- Or "Advanced" → "Accept the Risk" (Firefox)

### Migration Errors
If migrations fail:
```bash
# Remove existing database and recreate
dotnet ef database drop
dotnet ef database update
```

### API Connection Issues
- Verify API is running and accessible
- Check `ServiceUrls:VillaAPI` in `MagicVilla_Web/appsettings.json`
- Ensure both HTTP and HTTPS endpoints are configured


## 👤 Author

**Zixin Isabel Deng**

- GitHub: [@ZixinIsabelDeng](https://github.com/ZixinIsabelDeng)
- Project Link: [https://github.com/ZixinIsabelDeng/magicVilla_API](https://github.com/ZixinIsabelDeng/magicVilla_API)

## 🙏 Acknowledgments

- ASP.NET Core documentation
- Entity Framework Core team
- Bootstrap team for the UI framework
- All contributors and testers

---

**⭐ If you find this project helpful, please give it a star!**
