# 🔧 Railway Deployment Fix Guide

## Issue: Healthcheck Failed

The build succeeded but the app isn't starting. Here's how to fix it:

### Root Causes:
1. **Database not migrated** - App crashes on startup trying to connect to empty database
2. **Port configuration** - Railway uses dynamic PORT, but we might need to adjust
3. **SQL Server connection** - Connection string might not match service name

---

## 🔧 Fix Steps:

### Step 1: Check API Service Logs

In Railway dashboard:
1. Go to your **API service**
2. Click **"Deployments"** tab
3. Click on the latest deployment
4. Check **"Logs"** tab

**Look for errors like:**
- `Cannot open database "Magic_VillaAPI"`
- `Connection refused`
- `A network-related or instance-specific error`

---

### Step 2: Verify Environment Variables

In Railway API service → **Variables** tab, ensure you have:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:${PORT}
PORT=${PORT}
ConnectionStrings__DefaultConnection=Server=<sqlserver-service-name>,1433;Database=Magic_VillaAPI;User Id=SA;Password=YourStrong@Passw0rd123!;TrustServerCertificate=true;MultipleActiveResultSets=true;
ApiSetting__Secret=MagicVillaSecretKeyForJWT2024!ChangeThisInProduction
```

**Important:**
- Replace `<sqlserver-service-name>` with your actual SQL Server service name
- Find it in Railway: SQL Server service → Settings → it's usually `sqlserver` or similar
- Railway provides `PORT` automatically, but we can set it explicitly

---

### Step 3: Run Database Migrations (CRITICAL!)

The app needs the database tables created first:

**Option A: Via Railway CLI**

```bash
# Make sure you're in the project directory
cd /Users/mac/Desktop/magic_Villa/magicVilla_API

# Link to project (if not already)
railway link

# Run migrations on the API service
railway run --service <your-api-service-name> dotnet ef database update --project magicVilla_VillaAPI
```

**Option B: Via Railway Dashboard**

1. Go to API service → **Deployments** tab
2. Click on latest deployment
3. Click **"Shell"** or **"Console"** tab
4. Run:
   ```bash
   cd magicVilla_VillaAPI
   dotnet ef database update
   ```

**Option C: Manual SQL Script**

If migrations fail, you can run them locally and copy the schema:

```bash
# Locally, generate SQL script
cd magicVilla_VillaAPI
dotnet ef migrations script -o migrations.sql

# Then execute migrations.sql in Railway SQL Server
```

---

### Step 4: Fix Port Configuration

**In Railway API service → Variables:**

Add or update:
```
PORT=8080
ASPNETCORE_URLS=http://+:8080
```

Or use Railway's dynamic PORT:
```
ASPNETCORE_URLS=http://+:${PORT}
```

**In Railway API service → Networking:**
- Make sure port `8080` is exposed
- Or check what port Railway assigned and update accordingly

---

### Step 5: Verify SQL Server is Running

1. Go to **SQL Server service** in Railway
2. Check it's **running** (green status)
3. Note the **service name** (visible in settings)
4. Update connection string in API service variables to match

**Connection String Format:**
```
Server=<service-name>,1433;Database=Magic_VillaAPI;User Id=SA;Password=<your-password>;TrustServerCertificate=true;MultipleActiveResultSets=true;
```

---

### Step 6: Update Healthcheck (Optional)

If healthcheck still fails after app starts:

In Railway API service → **Settings**:
- **Healthcheck Path**: `/swagger` or `/` or remove healthcheck temporarily
- **Healthcheck Port**: `8080` or leave blank (uses default)

---

## 🐛 Common Errors & Fixes

### Error: "Cannot open database"
**Fix:** Run migrations (Step 3 above)

### Error: "Connection refused" or "Server not found"
**Fix:** 
- Check SQL Server service name matches connection string
- Verify SQL Server is running
- Check port `1433` is exposed in SQL Server networking

### Error: "Invalid login credentials"
**Fix:** 
- Verify password in connection string matches SQL Server `MSSQL_SA_PASSWORD`
- Password must match exactly (case-sensitive)

### Error: App starts but healthcheck fails
**Fix:**
- Check if app is listening on correct port
- Verify healthcheck path is correct (`/swagger` should work)
- Check logs for startup errors

---

## ✅ Quick Checklist

- [ ] SQL Server service is running
- [ ] API service has correct connection string (with correct service name)
- [ ] Database migrations have been run
- [ ] API service has PORT environment variable set
- [ ] API service has `ApiSetting__Secret` set
- [ ] Networking port is configured (8080)
- [ ] Checked API service logs for errors

---

## 📞 Still Not Working?

1. **Check logs carefully** - Most errors are in the logs
2. **Start with SQL Server** - Make sure database is accessible first
3. **Run migrations** - Database must have tables before app starts
4. **Test connection string** - Verify you can connect from API to SQL Server

---

## 🚀 After Fixing

Once the API service is healthy:
1. Copy the API domain URL
2. Update Web service `ServiceUrls__VillaAPI` variable
3. Redeploy Web service
4. Test both services

