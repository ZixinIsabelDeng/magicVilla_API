# ⚡ Quick Deployment Guide - Magic Villa

## 🚀 Fastest Way: Railway (15 minutes)

### 1. Push to GitHub
```bash
git init
git add .
git commit -m "Magic Villa project"
git branch -M main
git remote add origin <your-github-repo-url>
git push -u origin main
```

### 2. Deploy on Railway

1. Go to [railway.app](https://railway.app) → Sign up with GitHub
2. Click **"New Project"** → **"Deploy from GitHub repo"**
3. Select your repository

### 3. Set up Services

#### **Service 1: SQL Server Database**

- Click **"+ New"** → **"Database"** → **"Add PostgreSQL"** (or use SQL Server)
- For SQL Server, use: **"+ New"** → **"Empty Service"** → Add Dockerfile:

```dockerfile
FROM mcr.microsoft.com/mssql/server:2022-latest
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=YourStrong@Passw0rd123!
ENV MSSQL_PID=Developer
```

- Note the connection string

#### **Service 2: API**

- Click **"+ New"** → **"GitHub Repo"** → Select your repo
- Settings:
  - **Root Directory**: `magicVilla_VillaAPI`
  - **Build Command**: `dotnet restore && dotnet publish -c Release -o /app`
  - **Start Command**: `dotnet /app/magicVilla_VillaAPI.dll`
- **Variables** tab → Add:
  ```
  ASPNETCORE_ENVIRONMENT=Production
  ASPNETCORE_URLS=http://+:8080
  ConnectionStrings__DefaultConnection=<your-sql-connection-string>
  ApiSetting__Secret=GenerateRandomSecretKeyHere123!
  ```
- **Settings** → **Generate Domain** → Copy the URL (e.g., `api-production.up.railway.app`)

#### **Service 3: Web App**

- Click **"+ New"** → **"GitHub Repo"** → Select your repo
- Settings:
  - **Root Directory**: `MagicVilla_Web`
  - **Build Command**: `dotnet restore && dotnet publish -c Release -o /app`
  - **Start Command**: `dotnet /app/MagicVilla_Web.dll`
- **Variables** tab → Add:
  ```
  ASPNETCORE_ENVIRONMENT=Production
  ServiceUrls__VillaAPI=https://<your-api-domain>
  ```
- **Settings** → **Generate Domain**

### 4. Run Migrations

- Open API service → **"Deployments"** tab → Click on deployment → **"View Logs"**
- Or use Railway CLI:
  ```bash
  railway run --service api dotnet ef database update --project magicVilla_VillaAPI
  ```

### 5. Create Admin User

Visit: `https://<your-api-domain>/swagger`

Use the `/api/v1/UsersAuth/register` endpoint:
```json
{
  "userName": "admin",
  "name": "Administrator",
  "password": "admin123",
  "role": "admin"
}
```

### 6. Share with Recruiters

- **Web App**: `https://<your-web-domain>`
- **API Docs**: `https://<your-api-domain>/swagger`
- **Login**: `admin` / `admin123`

---

## 📋 Alternative: Render (Similar Process)

1. Push to GitHub
2. Go to [render.com](https://render.com)
3. Create PostgreSQL database
4. Deploy API service (use `render.yaml` or manual setup)
5. Deploy Web service
6. Run migrations
7. Done!

---

## 🔒 Security Notes

⚠️ **Before sharing publicly:**
- Change default admin password
- Use strong JWT secret
- Enable HTTPS (automatic on Railway/Render)
- Consider adding rate limiting

---

## 💡 Pro Tips

1. **Free Tier Limits**: Railway gives $5/month free, Render has free tier with limits
2. **Auto-Deploy**: Both platforms auto-deploy on git push
3. **Custom Domain**: Add your own domain in settings
4. **Monitoring**: Check logs if something breaks

---

## 🆘 Troubleshooting

**App won't start?**
- Check logs in Railway/Render dashboard
- Verify environment variables
- Check connection string format

**Database connection failed?**
- Ensure SQL Server is accessible
- Check firewall rules
- Verify connection string

**404 errors?**
- Check service URLs are correct
- Verify API is running
- Check CORS settings if needed

---

Good luck! 🚀

