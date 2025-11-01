#!/bin/bash

# Magic Villa Startup Script
# This script sets up the environment and starts both API and Web applications

# Use SDK from system location
export PATH="/usr/local/share/dotnet:$PATH"

echo "🏠 Starting Magic Villa Project"
echo "================================"
echo ""

# Verify .NET is available
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET not found in PATH"
    exit 1
fi

# Use full path to ensure we use system dotnet
DOTNET_CMD="/usr/local/share/dotnet/dotnet"

echo "✅ .NET SDK: $($DOTNET_CMD --version)"
echo "✅ .NET Runtimes:"
$DOTNET_CMD --list-runtimes | grep -E "(7\.0|9\.0)" || $DOTNET_CMD --list-runtimes
echo ""

# Check SQL Server
if ! docker ps | grep -q "magicvilla-sqlserver"; then
    echo "⚠️  SQL Server container not running. Starting it..."
    bash setup-sqlserver-docker.sh
    sleep 5
fi

cd "$(dirname "$0")"

# Start API
echo "🚀 Starting API server..."
cd magicVilla_VillaAPI
$DOTNET_CMD run &
API_PID=$!

# Wait for API to start
echo "   Waiting for API to start..."
sleep 10

# Start Web App
echo ""
echo "🌐 Starting Web application..."
cd ../MagicVilla_Web
$DOTNET_CMD run &
WEB_PID=$!

echo ""
echo "✅ Applications are starting!"
echo ""
echo "📍 Access your applications:"
echo "   • API Swagger: https://localhost:7001/swagger"
echo "   • Web App: https://localhost:7002"
echo ""
echo "Press Ctrl+C to stop both applications"
echo ""

# Wait for both processes
wait

