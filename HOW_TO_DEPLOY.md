# 🚀 How to Deploy Magic Villa - Step by Step Guide

## Quick Overview

This guide will help you deploy Magic Villa to **Railway** (free tier, ~15 minutes).

You'll need to deploy **3 services**:
1. **SQL Server Database** - Stores all data
2. **API Service** - Backend REST API
3. **Web Service** - Frontend web application

---

## Step-by-Step: Railway Deployment

### Step 1: Sign Up for Railway

1. Go to **[railway.app](https://railway.app)**
2. Click **"Start a New Project"** or **"Login"**
3. Sign up with your **GitHub account** (recommended)
4. You'll get $5 free credit monthly

---

### Step 2: Create New Project

1. Click **"New Project"** (top right)
2. Select **"Deploy from GitHub repo"**
3. Find and select: **`magicVilla_API`** repository
4. Click **"Deploy Now"**

---

### Step 3: Set Up SQL Server Database

1. In your project, click **"+ New"** button
2. Select **"Empty Service"**
3. Name it: `sqlserver`
4. Click on the service, then go to **"Settings"** tab
5. Under **"Source Image"**, enter:
   ```
   mcr.microsoft.com/mssql/server:2022-latest
   ```
6. Go to **"Variables"** tab and add these environment variables:
   ```
   ACCEPT_EULA=Y
   MSSQL_SA_PASSWORD=YourStrong@Passw0rd123!
   MSSQL_PID=Developer
   ```
7. Go to **"Networking"** tab
8. Add a port: `1433` (TCP)
9. Note the **internal hostname** (e.g., `sqlserver-production.up.railway.app`) or **service name** `sqlserver`

---

### Step 4: Deploy API Service

1. Click **"+ New"** button again
2. Select **"GitHub Repo"**
3. Select your `magicVilla_API` repository
4. Name it: `api`
5. Go to **"Settings"** tab:
   - **Root Directory**: Set to `magicVilla_VillaAPI`
   - **Build Command**: `dotnet restore && dotnet publish -c Release -o /app`
   - **Start Command**: `dotnet /app/magicVilla_VillaAPI.dll`
6. Go to **"Variables"** tab and add:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ASPNETCORE_URLS=http://+:8080
   ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=Magic_VillaAPI;User Id=SA;Password=YourStrong@Passw0rd123!;TrustServerCertificate=true;MultipleActiveResultSets=true;
   ApiSetting__Secret=MagicVillaSecretKeyForJWT2024!ChangeThisInProduction
   ```
   > **Note**: Replace `sqlserver` with your actual SQL Server service name/hostname if different
7. Go to **"Networking"** tab
8. Add a port: `8080` (HTTP)
9. Click **"Generate Domain"** to get a public URL
10. **Copy the URL** (e.g., `api-production-xxxx.up.railway.app`) - you'll need this for the Web service

---

### Step 5: Deploy Web Service

1. Click **"+ New"** button again
2. Select **"GitHub Repo"**
3. Select your `magicVilla_API` repository
4. Name it: `web`
5. Go to **"Settings"** tab:
   - **Root Directory**: Set to `MagicVilla_Web`
   - **Build Command**: `dotnet restore && dotnet publish -c Release -o /app`
   - **Start Command**: `dotnet /app/MagicVilla_Web.dll`
6. Go to **"Variables"** tab and add:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ServiceUrls__VillaAPI=https://api-production-xxxx.up.railway.app
   ```
   > **Important**: Replace `api-production-xxxx.up.railway.app` with your **actual API domain** from Step 4
7. Go to **"Networking"** tab
8. Click **"Generate Domain"** to get a public URL
9. **Copy this URL** - this is your main website URL!

---

### Step 6: Run Database Migrations

The database needs to be set up with tables. You have two options:

#### Option A: Using Railway CLI (Recommended)

1. Install Railway CLI:
   ```bash
   npm install -g @railway/cli
   ```
   Or using Homebrew:
   ```bash
   brew install railway
   ```

2. Login to Railway:
   ```bash
   railway login
   ```

3. Link to your project:
   ```bash
   railway link
   ```
   Select your project when prompted.

4. Run migrations:
   ```bash
   railway run --service api dotnet ef database update --project magicVilla_VillaAPI
   ```

#### Option B: Using Railway Dashboard

1. Go to your **API service** in Railway
2. Click on **"Deployments"** tab
3. Click on the latest deployment
4. Click **"View Logs"**
5. Open **"Shell"** or **"Console"** tab
6. Run:
   ```bash
   cd magicVilla_VillaAPI
   dotnet ef database update
   ```

---

### Step 7: Create Admin User

1. Open your **API Swagger UI**: `https://your-api-domain.up.railway.app/swagger`
2. Find the **`/api/Users/login`** endpoint (or `/api/Users/register`)
3. Click **"Try it out"**
4. Use this JSON to register:
   ```json
   {
     "userName": "admin",
     "name": "Administrator",
     "password": "admin123",
     "role": "admin"
   }
   ```
5. Click **"Execute"**
6. Note the response - you should get a success message

---

### Step 8: Test Your Deployment

1. **Visit your Web App**: `https://your-web-domain.up.railway.app`
   - You should see the Magic Villa homepage
   - Browse villas, try booking

2. **Check API**: `https://your-api-domain.up.railway.app/swagger`
   - Test endpoints
   - Login with your admin account

3. **Test Booking**: 
   - Navigate to a villa
   - Click "Book Now"
   - Fill out booking form
   - Submit and verify

---

## 🎉 Success! Your App is Live!

### Share These URLs with Recruiters:

- **🌐 Main Website**: `https://your-web-domain.up.railway.app`
- **📚 API Documentation**: `https://your-api-domain.up.railway.app/swagger`
- **👤 Admin Login**: `admin` / `admin123` (change this!)

---

## 🔒 Security Checklist (Before Sharing)

- [ ] Change default admin password
- [ ] Update JWT secret to a stronger value
- [ ] Review and restrict admin endpoints if needed
- [ ] Consider adding rate limiting
- [ ] Enable HTTPS (Railway does this automatically)

---

## 🆘 Troubleshooting

### Database Connection Failed

**Problem**: API can't connect to SQL Server

**Solutions**:
1. Check SQL Server service is running (green status)
2. Verify connection string in API variables matches SQL Server service name
3. Ensure port `1433` is added to SQL Server networking
4. Check SQL Server password in connection string matches the variable

### API Not Responding

**Problem**: API returns 404 or doesn't start

**Solutions**:
1. Check API service logs for errors
2. Verify `Root Directory` is set to `magicVilla_VillaAPI`
3. Check build logs for compilation errors
4. Ensure `ASPNETCORE_URLS` is set to `http://+:8080`

### Web App Can't Connect to API

**Problem**: Web app shows errors when loading data

**Solutions**:
1. Verify `ServiceUrls__VillaAPI` in Web service variables matches API domain
2. Ensure API URL uses `https://` not `http://`
3. Check API is accessible by visiting its Swagger URL
4. Review Web service logs for connection errors

### Migrations Fail

**Problem**: `dotnet ef database update` fails

**Solutions**:
1. Ensure SQL Server is running and accessible
2. Verify connection string is correct
3. Check EF Core tools are installed: `dotnet tool install --global dotnet-ef`
4. Try running migrations from API service shell/console

### Services Won't Deploy

**Problem**: Build fails or deployment errors

**Solutions**:
1. Check build logs for specific errors
2. Verify `.NET SDK 7.0` is supported on Railway (it is)
3. Ensure all NuGet packages are compatible
4. Check if `Root Directory` paths are correct
5. Review Railway status page for platform issues

---

## 💰 Railway Pricing

- **Free Tier**: $5 credit/month (usually enough for this project)
- **Hobby Plan**: $5/month for more resources
- **After free credit**: ~$0.01/hour for basic services

**Tips to save credits**:
- Stop services when not in use
- Use Railway's sleep mode
- Monitor usage in dashboard

---

## 🔄 Updating Your Deployment

When you push changes to GitHub:

1. Railway **automatically detects** the push
2. **Rebuilds** the affected services
3. **Redeploys** automatically

You only need to:
- Push to GitHub: `git push origin master`
- Wait ~2-5 minutes for deployment
- Check Railway dashboard for status

---

## 📚 Alternative Deployment Options

If Railway doesn't work for you, see:
- **[DEPLOYMENT_GUIDE.md](./DEPLOYMENT_GUIDE.md)** - Full guide with Render, Azure options
- **[DEPLOYMENT_QUICK_START.md](./DEPLOYMENT_QUICK_START.md)** - Quick reference

---

## ✅ Deployment Checklist

Before sharing with recruiters:

- [ ] All 3 services deployed (SQL, API, Web)
- [ ] Database migrations run successfully
- [ ] Admin user created
- [ ] Web app loads correctly
- [ ] API Swagger accessible
- [ ] Can create a booking
- [ ] Default password changed
- [ ] URLs documented
- [ ] Tested on mobile/tablet

---

**Need Help?** Check the main [README.md](./README.md) or deployment guides for more details.

**Ready to deploy?** Start with Step 1 above! 🚀

