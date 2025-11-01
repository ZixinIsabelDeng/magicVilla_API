# 🔍 How to Find Railway Service Names

When deploying on Railway, you need to know the **service names** to connect services together.

---

## 📋 Finding Service Names in Railway Dashboard

### Method 1: From the Dashboard (Easiest)

1. **Go to Railway Dashboard**: https://railway.app/dashboard
2. **Select your project**
3. **Look at the service list** on the left side
   - You'll see services like: `sqlserver`, `api`, `web`
   - **The service name is what you see in the list**

### Method 2: From Service Settings

1. **Click on a service** (e.g., SQL Server)
2. **Go to "Settings" tab**
3. **Service name is displayed at the top** or in the "Service Name" field
4. **You can rename it** if needed by editing the name

### Method 3: From Connection String/Environment Variables

When Railway services are in the same project, they can reference each other by:
- **Service name** (what you named the service)
- **Internal hostname** (Railway's internal DNS)

---

## 🎯 Common Service Names

For Magic Villa project, you typically have:

1. **SQL Server Service**
   - Name: `sqlserver` (or whatever you named it)
   - Used in: API connection string
   - Example: `Server=sqlserver,1433;...`

2. **API Service**
   - Name: `api` (or `magicvilla-api`, etc.)
   - Used in: Web app's `ServiceUrls__VillaAPI`
   - Example: `https://api-production-xxxx.up.railway.app`

3. **Web Service**
   - Name: `web` (or `magicvilla-web`, etc.)
   - Usually doesn't need to be referenced by others

---

## 🔗 How Service Names Are Used

### In Connection Strings (API → SQL Server)

**Format:**
```
Server=<sql-server-service-name>,1433;Database=Magic_VillaAPI;...
```

**Example:**
```
Server=sqlserver,1433;Database=Magic_VillaAPI;User Id=SA;Password=YourStrong@Passw0rd123!;TrustServerCertificate=true;
```

**To find:**
1. Go to your **SQL Server service** in Railway
2. Note the **service name** (visible in left sidebar)
3. Use that name in API's connection string

### In Environment Variables (Web → API)

**Format:**
```
ServiceUrls__VillaAPI=https://<api-domain>
```

**Example:**
```
ServiceUrls__VillaAPI=https://api-production-xxxx.up.railway.app
```

**To find:**
1. Go to your **API service** in Railway
2. Go to **Settings** tab
3. Click **"Generate Domain"** (if not already done)
4. **Copy the domain** (e.g., `api-production-xxxx.up.railway.app`)
5. Use that in Web service's `ServiceUrls__VillaAPI` variable

---

## 🖼️ Visual Guide

```
Railway Dashboard
├── Your Project
│   ├── sqlserver          ← Service Name: "sqlserver"
│   │   └── Settings
│   │       └── Service Name: sqlserver
│   │
│   ├── api                ← Service Name: "api"
│   │   └── Settings
│   │       └── Domain: api-production-xxxx.up.railway.app
│   │
│   └── web                ← Service Name: "web"
│       └── Settings
│           └── Domain: web-production-xxxx.up.railway.app
```

---

## ⚙️ Using Railway CLI

You can also check service names via CLI:

```bash
# List all services in linked project
railway status

# Or get detailed info
railway service
```

---

## 💡 Tips

1. **Service names are case-sensitive** in connection strings
2. **Use lowercase** to avoid issues: `sqlserver` not `SqlServer`
3. **Check exact spelling** - must match what Railway shows
4. **If you rename a service**, update all references to it
5. **Internal service names** are used within Railway's network
6. **Public domains** are used for external access (Web → API)

---

## 🆘 Still Can't Find It?

1. **Check the URL** when viewing a service in Railway
   - URL often shows: `railway.app/project/<project-id>/service/<service-name>`
2. **Look in environment variables**
   - Railway sometimes shows service references
3. **Check service logs**
   - Service name might appear in startup logs

---

## ✅ Quick Checklist

- [ ] SQL Server service name: `________` (use in API connection string)
- [ ] API service domain: `________` (use in Web service `ServiceUrls__VillaAPI`)
- [ ] Web service domain: `________` (your public website URL)

---

**Example Setup:**

If your services are named:
- SQL Server: `sqlserver`
- API: `api`  
- Web: `web`

**API Service Variables:**
```
ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=Magic_VillaAPI;...
```

**Web Service Variables:**
```
ServiceUrls__VillaAPI=https://api-production-xxxx.up.railway.app
```

(The `api-production-xxxx.up.railway.app` comes from API service's generated domain, NOT the service name!)

