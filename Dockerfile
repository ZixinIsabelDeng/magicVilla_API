# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["magicVilla_VillaAPI/magicVilla_VillaAPI.csproj", "magicVilla_VillaAPI/"]
COPY ["MagicVilla_Utility/MagicVilla_Utility.csproj", "MagicVilla_Utility/"]
RUN dotnet restore "magicVilla_VillaAPI/magicVilla_VillaAPI.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/magicVilla_VillaAPI"
RUN dotnet build "magicVilla_VillaAPI.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "magicVilla_VillaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "magicVilla_VillaAPI.dll"]

