#!/bin/bash

# Script to set up SQL Server in Docker for macOS

echo "Setting up SQL Server in Docker..."

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker Desktop and try again."
    exit 1
fi

# Check if container already exists
if docker ps -a | grep -q "magicvilla-sqlserver"; then
    echo "📦 SQL Server container already exists. Starting it..."
    docker start magicvilla-sqlserver
else
    echo "🚀 Creating and starting SQL Server container..."
    docker run -e "ACCEPT_EULA=Y" \
        -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
        -p 1433:1433 \
        --name magicvilla-sqlserver \
        --hostname magicvilla-sqlserver \
        -d \
        mcr.microsoft.com/mssql/server:2022-latest
    
    echo "⏳ Waiting for SQL Server to start (this may take 30-60 seconds)..."
    sleep 30
    
    # Wait for SQL Server to be ready
    for i in {1..30}; do
        if docker exec magicvilla-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "YourStrong@Passw0rd" -Q "SELECT 1" &> /dev/null; then
            echo "✅ SQL Server is ready!"
            break
        fi
        echo "   Waiting... ($i/30)"
        sleep 2
    done
fi

echo ""
echo "✅ SQL Server is running!"
echo "   Server: localhost,1433"
echo "   Username: SA"
echo "   Password: YourStrong@Passw0rd"
echo ""
echo "Connection string for appsettings.json:"
echo "Server=localhost,1433;Database=Magic_VillaAPI;User Id=SA;Password=YourStrong@Passw0rd;TrustServerCertificate=true;"

