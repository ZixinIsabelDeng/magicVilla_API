#!/bin/bash

# Script to run the Magic Villa project

cd "$(dirname "$0")"

echo "🏠 Magic Villa Project Startup"
echo "================================"
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK is not installed."
    echo "   Please install it with: brew install --cask dotnet-sdk"
    echo "   Then restart your terminal and run this script again."
    exit 1
fi

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running."
    echo "   Please start Docker Desktop and try again."
    exit 1
fi

# Check if SQL Server container is running
if ! docker ps | grep -q "magicvilla-sqlserver"; then
    echo "⚠️  SQL Server container is not running."
    echo "   Running setup script..."
    bash setup-sqlserver-docker.sh
    if [ $? -ne 0 ]; then
        echo "❌ Failed to start SQL Server. Please check the setup script."
        exit 1
    fi
fi

echo ""
echo "📦 Step 1: Running database migrations..."
cd magicVilla_VillaAPI
dotnet ef database update
if [ $? -ne 0 ]; then
    echo "❌ Database migration failed. Please check the error above."
    exit 1
fi

echo ""
echo "🚀 Step 2: Starting API server..."
echo "   API will be available at: https://localhost:7001"
echo "   Swagger UI: https://localhost:7001/swagger"
echo ""
echo "   (Press Ctrl+C to stop the API, then the web app will start in a new terminal)"
echo ""

# Start API in background
dotnet run &
API_PID=$!

# Wait a bit for API to start
sleep 5

echo ""
echo "🌐 Step 3: Starting Web application..."
cd ../MagicVilla_Web
echo "   Web app will be available at: https://localhost:7002"
echo ""

# Start Web app
dotnet run

# Cleanup on exit
trap "kill $API_PID 2>/dev/null" EXIT

