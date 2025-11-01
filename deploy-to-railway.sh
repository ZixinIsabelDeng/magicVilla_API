#!/bin/bash

# Magic Villa - Railway Deployment Script
# This script helps deploy the project to Railway

set -e

echo "🚀 Magic Villa - Railway Deployment Helper"
echo "=========================================="
echo ""

# Check if Railway CLI is installed
if ! command -v railway &> /dev/null; then
    echo "❌ Railway CLI not found!"
    echo ""
    echo "Please install Railway CLI first:"
    echo "  Option 1: brew install railway"
    echo "  Option 2: npm install -g @railway/cli"
    echo ""
    exit 1
fi

echo "✅ Railway CLI found"
echo ""

# Check if user is logged in
if ! railway whoami &> /dev/null; then
    echo "🔐 You need to login to Railway first"
    echo "   This will open a browser window..."
    echo ""
    railway login
    echo ""
fi

echo "✅ Logged in to Railway"
echo ""

# Check if project is linked
if [ ! -f ".railway/link.json" ]; then
    echo "📎 Linking to Railway project..."
    echo "   Select your project from the list, or create a new one"
    echo ""
    railway link
    echo ""
fi

echo "✅ Project linked"
echo ""

echo "📦 Deployment Configuration:"
echo ""
echo "You need to create 3 services on Railway:"
echo ""
echo "1. SQL Server Database"
echo "   - Name: sqlserver"
echo "   - Source Image: mcr.microsoft.com/mssql/server:2022-latest"
echo "   - Variables:"
echo "     ACCEPT_EULA=Y"
echo "     MSSQL_SA_PASSWORD=YourStrong@Passw0rd123!"
echo "     MSSQL_PID=Developer"
echo ""
echo "2. API Service"
echo "   - Root Directory: magicVilla_VillaAPI"
echo "   - Build Command: dotnet restore && dotnet publish -c Release -o /app"
echo "   - Start Command: dotnet /app/magicVilla_VillaAPI.dll"
echo "   - Variables: (see HOW_TO_DEPLOY.md)"
echo ""
echo "3. Web Service"
echo "   - Root Directory: MagicVilla_Web"
echo "   - Build Command: dotnet restore && dotnet publish -c Release -o /app"
echo "   - Start Command: dotnet /app/MagicVilla_Web.dll"
echo "   - Variables: (see HOW_TO_DEPLOY.md)"
echo ""
echo "For detailed step-by-step instructions, see: HOW_TO_DEPLOY.md"
echo ""
echo "Would you like to:"
echo "  [1] Open Railway dashboard in browser"
echo "  [2] Show deployment commands"
echo "  [3] Exit"
echo ""
read -p "Choose option (1-3): " choice

case $choice in
    1)
        echo ""
        echo "🌐 Opening Railway dashboard..."
        railway open
        ;;
    2)
        echo ""
        echo "📋 Deployment Commands:"
        echo ""
        echo "To set environment variables for API service:"
        echo "  railway variables set ASPNETCORE_ENVIRONMENT=Production --service api"
        echo "  railway variables set ASPNETCORE_URLS=http://+:8080 --service api"
        echo ""
        echo "To run migrations:"
        echo "  railway run --service api dotnet ef database update --project magicVilla_VillaAPI"
        echo ""
        ;;
    3)
        echo "👋 Goodbye!"
        exit 0
        ;;
    *)
        echo "Invalid choice"
        exit 1
        ;;
esac

echo ""
echo "✅ Done! Follow HOW_TO_DEPLOY.md for complete instructions."

