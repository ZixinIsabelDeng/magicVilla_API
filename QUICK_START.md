# Quick Start Guide - Magic Villa Project

## Prerequisites Check

Before running the project, you need:

1. ✅ **Docker Desktop** - Installed (needs to be running)
2. ❌ **.NET SDK** - Needs to be installed

## Installation Steps

### 1. Install .NET SDK

Run this command in your terminal:
```bash
brew install --cask dotnet-sdk
```

This will require your Mac password.

### 2. Start Docker Desktop

- Open Docker Desktop application
- Wait until it shows "Docker is running" in the menu bar

## Running the Project

Once both prerequisites are met, you can run the project in two ways:

### Option A: Automated Script (Recommended)

Simply run:
```bash
cd /Users/mac/Desktop/magic_Villa/magicVilla_API
./run-project.sh
```

This script will:
- Check prerequisites
- Start SQL Server in Docker
- Run database migrations
- Start the API server
- Start the Web application

### Option B: Manual Steps

1. **Start SQL Server in Docker:**
   ```bash
   cd /Users/mac/Desktop/magic_Villa/magicVilla_API
   ./setup-sqlserver-docker.sh
   ```

2. **Run Database Migrations:**
   ```bash
   cd magicVilla_VillaAPI
   dotnet ef database update
   ```

3. **Start the API (Terminal 1):**
   ```bash
   cd magicVilla_VillaAPI
   dotnet run
   ```
   API will be at: `https://localhost:7001`
   Swagger: `https://localhost:7001/swagger`

4. **Start the Web App (Terminal 2):**
   ```bash
   cd MagicVilla_Web
   dotnet run
   ```
   Web app will be at: `https://localhost:7002`

## Accessing the Applications

- **Web Application**: https://localhost:7002
- **API Swagger UI**: https://localhost:7001/swagger

## Troubleshooting

### Docker Not Running
- Open Docker Desktop and wait for it to fully start
- Check with: `docker ps`

### .NET SDK Not Found
- Install with: `brew install --cask dotnet-sdk`
- Restart your terminal after installation
- Verify with: `dotnet --version`

### SQL Server Connection Issues
- Make sure Docker container is running: `docker ps | grep magicvilla-sqlserver`
- If container stopped, restart it: `docker start magicvilla-sqlserver`
- Check connection string in `magicVilla_VillaAPI/appsettings.json`

