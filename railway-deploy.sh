#!/bin/bash

# Magic Villa - Railway Deployment Script
# Interactive script to deploy to Railway

set -e

echo "🚀 Magic Villa - Railway Deployment"
echo "===================================="
echo ""

# Check Railway CLI
if ! command -v railway &> /dev/null; then
    echo "❌ Railway CLI not found!"
    echo "   Install with: npm install -g @railway/cli"
    exit 1
fi

echo "✅ Railway CLI found: $(railway --version)"
echo ""

# Step 1: Login
echo "📋 Step 1: Login to Railway"
echo "   This will open your browser..."
echo ""
if railway whoami &> /dev/null; then
    echo "✅ Already logged in as: $(railway whoami)"
else
    echo "   Please authenticate in the browser that opens..."
    railway login
    echo ""
fi

# Step 2: Create/Link Project
echo "📋 Step 2: Link to Railway Project"
echo ""
if [ -f ".railway/link.json" ]; then
    echo "✅ Project already linked"
    railway status
else
    echo "   Creating new Railway project..."
    echo ""
    echo "   Option 1: Create project from GitHub repo (recommended)"
    echo "   - Go to https://railway.app/dashboard"
    echo "   - Click 'New Project' → 'Deploy from GitHub repo'"
    echo "   - Select 'magicVilla_API' repository"
    echo "   - Then come back and run: railway link"
    echo ""
    echo "   Option 2: Try linking now (if project exists)"
    echo ""
    read -p "   Create project on Railway dashboard first? (y/n): " create_first
    
    if [ "$create_first" = "y" ] || [ "$create_first" = "Y" ]; then
        echo ""
        echo "   Opening Railway dashboard..."
        railway open || echo "   Visit: https://railway.app/dashboard"
        echo ""
        echo "   After creating the project, press Enter to continue..."
        read -p "   "
    fi
    
    echo ""
    echo "   Linking to Railway project..."
    railway link --project || {
        echo ""
        echo "   ⚠️  Could not link automatically. Please run manually:"
        echo "   railway link"
        echo ""
        echo "   Or create project via web: https://railway.app/dashboard"
        exit 1
    }
    echo ""
fi

# Step 3: Show deployment instructions
echo ""
echo "📋 Step 3: Create Services on Railway"
echo ""
echo "You need to create 3 services manually in Railway dashboard:"
echo ""
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo ""
echo "1️⃣  SQL SERVER DATABASE"
echo "   • Click '+ New' → 'Empty Service'"
echo "   • Name: sqlserver"
echo "   • Go to Settings → Source Image:"
echo "     mcr.microsoft.com/mssql/server:2022-latest"
echo "   • Variables tab, add:"
echo "     ACCEPT_EULA=Y"
echo "     MSSQL_SA_PASSWORD=YourStrong@Passw0rd123!"
echo "     MSSQL_PID=Developer"
echo "   • Networking tab → Add port: 1433"
echo ""
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo ""
echo "2️⃣  API SERVICE"
echo "   • Click '+ New' → 'GitHub Repo'"
echo "   • Select: magicVilla_API repository"
echo "   • Settings → Root Directory: magicVilla_VillaAPI"
echo "   • Build Command: dotnet restore && dotnet publish -c Release -o /app"
echo "   • Start Command: dotnet /app/magicVilla_VillaAPI.dll"
echo "   • Variables tab, add:"
echo "     ASPNETCORE_ENVIRONMENT=Production"
echo "     ASPNETCORE_URLS=http://+:8080"
echo "     ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=Magic_VillaAPI;User Id=SA;Password=YourStrong@Passw0rd123!;TrustServerCertificate=true;MultipleActiveResultSets=true;"
echo "     ApiSetting__Secret=MagicVillaSecretKeyForJWT2024!ChangeThisInProduction"
echo "   • Networking tab → Add port: 8080"
echo "   • Settings → Generate Domain → Copy the URL"
echo ""
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo ""
echo "3️⃣  WEB SERVICE"
echo "   • Click '+ New' → 'GitHub Repo'"
echo "   • Select: magicVilla_API repository"
echo "   • Settings → Root Directory: MagicVilla_Web"
echo "   • Build Command: dotnet restore && dotnet publish -c Release -o /app"
echo "   • Start Command: dotnet /app/MagicVilla_Web.dll"
echo "   • Variables tab, add:"
echo "     ASPNETCORE_ENVIRONMENT=Production"
echo "     ServiceUrls__VillaAPI=https://<your-api-domain>"
echo "     (Replace <your-api-domain> with the API URL from step 2)"
echo "   • Settings → Generate Domain"
echo ""
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
echo ""

# Step 4: Run migrations
echo "📋 Step 4: Run Database Migrations"
echo ""
read -p "Have you created all 3 services? (y/n): " services_ready

if [ "$services_ready" = "y" ] || [ "$services_ready" = "Y" ]; then
    echo ""
    echo "🔄 Running database migrations..."
    echo "   This will create tables in your SQL Server database"
    echo ""
    railway run --service api dotnet ef database update --project magicVilla_VillaAPI || {
        echo ""
        echo "⚠️  Migration failed. Try manually:"
        echo "   railway run --service api dotnet ef database update --project magicVilla_VillaAPI"
    }
else
    echo ""
    echo "⏭️  Skipping migrations. Run after services are created:"
    echo "   railway run --service api dotnet ef database update --project magicVilla_VillaAPI"
fi

echo ""
echo "✅ Deployment setup complete!"
echo ""
echo "📝 Next steps:"
echo "   1. Wait for services to deploy (check Railway dashboard)"
echo "   2. Verify API is accessible: https://your-api-domain.up.railway.app/swagger"
echo "   3. Create admin user via Swagger UI"
echo "   4. Test your web app: https://your-web-domain.up.railway.app"
echo ""
echo "📖 For detailed instructions, see: HOW_TO_DEPLOY.md"
echo ""

