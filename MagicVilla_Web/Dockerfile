# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["MagicVilla_Web/MagicVilla_Web.csproj", "MagicVilla_Web/"]
COPY ["MagicVilla_Utility/MagicVilla_Utility.csproj", "MagicVilla_Utility/"]
RUN dotnet restore "MagicVilla_Web/MagicVilla_Web.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/MagicVilla_Web"
RUN dotnet build "MagicVilla_Web.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "MagicVilla_Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "MagicVilla_Web.dll"]

