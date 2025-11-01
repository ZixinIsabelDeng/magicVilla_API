# 🚀 Magic Villa - Deployment Guide

This guide provides multiple options to deploy the Magic Villa project so recruiters can easily access and test it.

## 📋 Prerequisites

- GitHub account (for code hosting)
- One of the following deployment platforms:
  - **Railway** (Recommended - Free tier, easiest)
  - **Render** (Free tier)
  - **Azure** (Free tier available)
  - **Fly.io** (Free tier)

---

## 🎯 Option 1: Railway (Recommended - Easiest)

Railway is the easiest option with a generous free tier.

### Steps:

1. **Push to GitHub**

   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   git remote add origin <your-github-repo-url>
   git push -u origin main
   ```

2. **Deploy to Railway**

   - Go to [railway.app](https://railway.app)
   - Sign up with GitHub
   - Click "New Project"
   - Select "Deploy from GitHub repo"
   - Choose your repository
   - Add **3 services**:

     **Service 1: SQL Server**

     - Name: `sqlserver`
     - Use Docker image: `mcr.microsoft.com/mssql/server:2022-latest`
     - Environment Variables:
       - `ACCEPT_EULA=Y`
       - `MSSQL_SA_PASSWORD=YourStrong@Passw0rd123!`
       - `MSSQL_PID=Developer`
     - Add Port: `1433` (internal)

     **Service 2: API**

     - Name: `api`
     - Root Directory: `magicVilla_VillaAPI`
     - Build Command: `dotnet restore && dotnet publish -c Release -o /app`
     - Start Command: `dotnet /app/magicVilla_VillaAPI.dll`
     - Environment Variables:
       - `ASPNETCORE_ENVIRONMENT=Production`
       - `ConnectionStrings__DefaultConnection=Server=sqlserver:1433;Database=Magic_VillaAPI;User Id=SA;Password=YourStrong@Passw0rd123!;TrustServerCertificate=true;`
       - `ApiSetting__Secret=YOUR_JWT_SECRET_KEY_HERE`
     - Add Port: `8080` (internal)
     - Generate Public Domain

     **Service 3: Web App**

     - Name: `web`
     - Root Directory: `MagicVilla_Web`
     - Build Command: `dotnet restore && dotnet publish -c Release -o /app`
     - Start Command: `dotnet /app/MagicVilla_Web.dll`
     - Environment Variables:
       - `ASPNETCORE_ENVIRONMENT=Production`
       - `ServiceUrls__VillaAPI=<your-api-service-url>`
     - Generate Public Domain

3. **Run Migrations**

   - In Railway dashboard, go to API service
   - Open the shell/console
   - Run:
     ```bash
     dotnet ef database update --project magicVilla_VillaAPI
     ```

4. **Access Your App**
   - Use the public URLs provided by Railway
   - API: `https://your-api-service.up.railway.app`
   - Web: `https://your-web-service.up.railway.app`

---

## 🎯 Option 2: Render (Simple Alternative)

### Steps:

1. **Push to GitHub** (same as Railway)

2. **Create PostgreSQL Database**

   - Go to [render.com](https://render.com)
   - Create a new PostgreSQL database
   - Note the connection string

3. **Update Connection String**

   - For PostgreSQL, you'll need to:
     - Install `Npgsql.EntityFrameworkCore.PostgreSQL` package
     - Update `Program.cs` to use PostgreSQL
     - Or use SQL Server on Render

4. **Deploy API**

   - New > Web Service
   - Connect GitHub repo
   - Root Directory: `magicVilla_VillaAPI`
   - Build: `dotnet publish -c Release -o publish`
   - Start: `dotnet publish/magicVilla_VillaAPI.dll`
   - Environment Variables: (same as Railway)

5. **Deploy Web App**
   - New > Web Service
   - Connect GitHub repo
   - Root Directory: `MagicVilla_Web`
   - Build: `dotnet publish -c Release -o publish`
   - Start: `dotnet publish/MagicVilla_Web.dll`

---

## 🎯 Option 3: Azure App Service (Microsoft Platform)

### Steps:

1. **Install Azure CLI**

   ```bash
   # macOS
   brew install azure-cli
   ```

2. **Login to Azure**

   ```bash
   az login
   ```

3. **Create Resource Group**

   ```bash
   az group create --name MagicVillaRG --location eastus
   ```

4. **Create SQL Database**

   ```bash
   az sql server create --name magicvilla-sql --resource-group MagicVillaRG \
     --location eastus --admin-user azureadmin --admin-password YourPassword123!

   az sql db create --resource-group MagicVillaRG --server magicvilla-sql \
     --name Magic_VillaAPI --service-objective Basic
   ```

5. **Deploy API**

   ```bash
   cd magicVilla_VillaAPI
   az webapp up --name magicvilla-api --resource-group MagicVillaRG \
     --runtime "DOTNET|7.0" --location eastus
   ```

6. **Deploy Web App**

   ```bash
   cd ../MagicVilla_Web
   az webapp up --name magicvilla-web --resource-group MagicVillaRG \
     --runtime "DOTNET|7.0" --location eastus
   ```

7. **Configure Connection Strings**
   - Go to Azure Portal
   - API App > Configuration > Connection strings
   - Add: `DefaultConnection` with your SQL connection string

---

## 🎯 Option 4: Docker Compose (Self-Hosted)

If you have a VPS or server:

```bash
# Clone repository
git clone <your-repo>
cd magic_Villa/magicVilla_API

# Start everything
docker-compose up -d

# Run migrations
docker exec -it magicvilla-api dotnet ef database update
```

---

## 🔧 Quick Fixes for Production

### Update Connection String for Production

Create `appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<your-production-connection-string>"
  },
  "ApiSetting": {
    "Secret": "<generate-a-strong-secret-key>"
  }
}
```

### Generate JWT Secret

```bash
# Generate a random secret (Linux/Mac)
openssl rand -base64 32
```

---

## 📝 Pre-Deployment Checklist

- [ ] Update JWT secret in production settings
- [ ] Update connection string for production database
- [ ] Update Web app's API URL to production API URL
- [ ] Run database migrations
- [ ] Test all endpoints
- [ ] Create admin user (use registration endpoint)
- [ ] Verify images load correctly

---

## 🌐 Quick Deploy Script (Railway)

Create a `railway.json`:

```json
{
  "$schema": "https://railway.app/railway.schema.json",
  "build": {
    "builder": "NIXPACKS"
  },
  "deploy": {
    "startCommand": "dotnet magicVilla_VillaAPI.dll",
    "restartPolicyType": "ON_FAILURE",
    "restartPolicyMaxRetries": 10
  }
}
```

---

## 📞 Support

For deployment issues, check:

- Application logs in your deployment platform
- Database connectivity
- Environment variables are set correctly
- Ports are properly exposed

---

## 🎉 Post-Deployment

After deployment, share these with recruiters:

- **Web Application URL**: `https://your-web-app-url.com`
- **API Swagger URL**: `https://your-api-url.com/swagger`
- **Demo Credentials**:
  - Username: `admin`
  - Password: `admin123`

Good luck with your deployment! 🚀
