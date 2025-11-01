# magicVilla_API

MagicVilla primarily functions as a platform for retrieving villa information. As an administrator, you have the capabilities to edit, delete, and add villas to the site. Additionally, admins can search for villas using keywords.

The front end of the web application utilizes the Razor View Engine alongside Bootstrap, ensuring a responsive and user-friendly interface.

The backend is powered by C# and ASP.NET, providing a robust and scalable foundation.

The webpage features role-based authentication secured with JWT tokens, and implements application versioning (v1, v2), caching, and pagination to enhance performance and user experience.

## 🌐 Live Deployment

**Want to deploy this project for recruiters to test?**

👉 **See [DEPLOYMENT_QUICK_START.md](./DEPLOYMENT_QUICK_START.md) for fastest deployment options!**

Quick options:

- 🚂 **Railway** (Recommended - Free, Easy) - [Guide](./DEPLOYMENT_QUICK_START.md#-fastest-way-railway-15-minutes)
- 🎨 **Render** (Free tier) - [Full Guide](./DEPLOYMENT_GUIDE.md#-option-2-render-simple-alternative)
- ☁️ **Azure** (Microsoft platform) - [Full Guide](./DEPLOYMENT_GUIDE.md#-option-3-azure-app-service-microsoft-platform)

Deploy in 15 minutes and share a live URL with recruiters!

## Prerequisites

Before running the project, ensure you have the following installed:

- **.NET SDK** (version 6.0 or later) - [Download here](https://dotnet.microsoft.com/download)
- **SQL Server** (Express or higher) - [Download here](https://www.microsoft.com/sql-server/sql-server-downloads)
- **Visual Studio** (recommended) or **Visual Studio Code** with C# extension

## Project Structure

This solution contains two main projects:

1. **magicVilla_VillaAPI** - REST API backend (runs on `https://localhost:7001`)
2. **MagicVilla_Web** - Web frontend application (runs on `https://localhost:7002`)

## Setup Instructions

### 1. Database Configuration

The project uses SQL Server with the following connection string (configured in `magicVilla_VillaAPI/appsettings.json`):

```
Server=localhost\SQLEXPRESS;Database=Magic_VillaAPI;Trusted_Connection=True;TrustServerCertificate=true
```

**If your SQL Server instance has a different name**, update the connection string in:

- `magicVilla_VillaAPI/appsettings.json`
- `magicVilla_VillaAPI/appsettings.Development.json`

### 2. Apply Database Migrations

Navigate to the API project directory and run migrations:

```bash
cd magicVilla_VillaAPI
dotnet ef database update
```

This will create the database and apply all migrations, including seed data for villas.

### 3. Run the API (Backend)

**Option A: Using Visual Studio**

- Open the solution file `magicVilla_VillaAPI.sln`
- Set `magicVilla_VillaAPI` as the startup project
- Press `F5` or click Run

**Option B: Using Command Line**

```bash
cd magicVilla_VillaAPI
dotnet run
```

The API will start on:

- **HTTPS**: `https://localhost:7001`
- **HTTP**: `http://localhost:5065`
- **Swagger UI**: `https://localhost:7001/swagger`

### 4. Run the Web Application (Frontend)

**Option A: Using Visual Studio**

- In Visual Studio, right-click on `MagicVilla_Web` project
- Select "Set as Startup Project"
- Press `F5` or click Run

**Option B: Using Command Line**
Open a **new terminal window** (keep the API running) and run:

```bash
cd MagicVilla_Web
dotnet run
```

The web application will start on:

- **HTTPS**: `https://localhost:7002`
- **HTTP**: `http://localhost:5136`

### 5. Verify Configuration

Ensure that the Web application's API URL matches your API port. Check `MagicVilla_Web/appsettings.json`:

```json
"ServiceUrls": {
  "VillaAPI": "https://localhost:7001"
}
```

If your API runs on a different port, update this URL accordingly.

## Accessing the Application

1. **Web Application**: Navigate to `https://localhost:7002` in your browser
2. **API Swagger Documentation**: Navigate to `https://localhost:7001/swagger` to explore and test the API endpoints

## Troubleshooting

### Database Connection Issues

- Ensure SQL Server is running
- Verify the SQL Server instance name matches your connection string
- Check that the database server allows trusted connections (Windows Authentication)

### Port Conflicts

If ports 7001, 7002, 5065, or 5136 are already in use:

- Update the ports in `launchSettings.json` files
- Update the `ServiceUrls` in the Web project's `appsettings.json` to match

### SSL Certificate Warnings

When first running the application, you may see SSL certificate warnings. This is normal in development. Click "Advanced" → "Proceed to localhost" (or similar option in your browser).

## Development Notes

- The API uses JWT authentication - tokens are required for protected endpoints
- The Web application uses cookie-based authentication and communicates with the API
- Both projects should run simultaneously for full functionality
- Migrations are located in `magicVilla_VillaAPI/Migrations/`
