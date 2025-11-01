# 🗑️ Files to Delete Before Pushing to GitHub

This guide lists files and folders that should be **deleted** before pushing to GitHub. These are either:
- Build artifacts (automatically generated)
- User-specific/local setup files
- Temporary files
- Files containing sensitive information

## ⚠️ Files/Folders to DELETE:

### 1. Build Output Folders (Already in .gitignore, but delete if already committed)
```bash
# These should already be ignored, but delete them to be safe:
- magicVilla_VillaAPI/bin/
- magicVilla_VillaAPI/obj/
- MagicVilla_Web/bin/
- MagicVilla_Web/obj/
- MagicVilla_Utility/bin/
- MagicVilla_Utility/obj/
```

### 2. Azure Deployment Profiles (Contain sensitive info)
```bash
- magicVilla_VillaAPI/Properties/ServiceDependencies/
- MagicVilla_Web/Properties/ServiceDependencies/
```

### 3. Local Setup Scripts (Not needed in repo)
```bash
- copy-runtime.sh (was for local .NET runtime setup)
- FINAL_SETUP_INSTRUCTIONS.md (local setup only)
- QUICK_START.md (local setup - already have DEPLOYMENT_QUICK_START.md)
- start-apps.sh (redundant - we have start-magic-villa.sh)
```

## ✅ Files to KEEP:

These files should stay in the repository:
- ✅ `run-project.sh` - Useful for running locally
- ✅ `start-magic-villa.sh` - Main startup script
- ✅ `setup-sqlserver-docker.sh` - Docker SQL Server setup (useful)
- ✅ `DEPLOYMENT_GUIDE.md` - Deployment documentation
- ✅ `DEPLOYMENT_QUICK_START.md` - Quick deployment guide
- ✅ `README.md` - Main documentation
- ✅ `docker-compose.yml` - Docker setup
- ✅ `Dockerfile` - Container configuration
- ✅ All source code files
- ✅ `appsettings.json` (but check for secrets!)
- ✅ Migrations folder

## 🔒 Check for Sensitive Data

Before pushing, check these files for secrets/passwords:

1. **appsettings.json** - Should have generic connection strings
2. **ServiceDependencies/** - Contains Azure publish profiles (DELETE)
3. **Any .env files** - Should not exist or be in .gitignore

## 🧹 Quick Cleanup Script

Run this to clean up automatically:

```bash
cd /Users/mac/Desktop/magic_Villa/magicVilla_API

# Delete build folders (safe - they're regenerated)
find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} + 2>/dev/null

# Delete Azure profiles
rm -rf magicVilla_VillaAPI/Properties/ServiceDependencies/
rm -rf MagicVilla_Web/Properties/ServiceDependencies/

# Delete local setup files
rm -f copy-runtime.sh
rm -f FINAL_SETUP_INSTRUCTIONS.md
rm -f QUICK_START.md
rm -f start-apps.sh

echo "✅ Cleanup complete!"
```

## ✅ Final Checklist

Before `git add .` and push:
- [ ] Deleted `bin/` and `obj/` folders
- [ ] Deleted `ServiceDependencies/` folders
- [ ] Deleted local setup scripts listed above
- [ ] Checked `appsettings.json` for hardcoded secrets
- [ ] Verified `.gitignore` is working (run `git status` to check)

## 📝 After Cleanup

1. Verify nothing sensitive is included:
   ```bash
   git status
   ```

2. Review what will be committed:
   ```bash
   git add .
   git status
   ```

3. If everything looks good, commit and push:
   ```bash
   git commit -m "Initial commit - Magic Villa project"
   git push
   ```

