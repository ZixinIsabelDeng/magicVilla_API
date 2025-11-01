#!/bin/bash

echo "🧹 Cleaning up files before Git push..."
echo ""

# Delete build output folders
echo "Deleting build folders (bin/, obj/)..."
find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} + 2>/dev/null
echo "✅ Build folders deleted"

# Delete Azure deployment profiles (contain sensitive info)
echo "Deleting Azure ServiceDependencies..."
rm -rf magicVilla_VillaAPI/Properties/ServiceDependencies/ 2>/dev/null
rm -rf MagicVilla_Web/Properties/ServiceDependencies/ 2>/dev/null
echo "✅ Azure profiles deleted"

# Delete only redundant/one-time local setup files
echo "Deleting redundant local setup files..."
rm -f copy-runtime.sh  # One-time .NET runtime setup (not needed after first setup)
rm -f FINAL_SETUP_INSTRUCTIONS.md  # Temporary setup guide (superseded by DEPLOYMENT_QUICK_START.md)
rm -f start-apps.sh  # Redundant (we have start-magic-villa.sh which is better)
# NOTE: Keeping QUICK_START.md - it's useful for recruiters setting up locally
echo "✅ Redundant files deleted"

echo ""
echo "✅ Cleanup complete!"
echo ""
echo "📋 Files KEPT (useful for deployment & recruiters):"
echo "   ✅ run-project.sh - Local running script"
echo "   ✅ start-magic-villa.sh - Main startup script"
echo "   ✅ setup-sqlserver-docker.sh - Docker SQL setup"
echo "   ✅ QUICK_START.md - Local setup guide"
echo "   ✅ All deployment guides and Docker configs"
echo ""
echo "🔒 Security: All passwords in repo are for local dev only"
echo "   Production deployment uses environment variables"
echo ""
echo "Next steps:"
echo "  1. Review changes: git status"
echo "  2. Add files: git add ."
echo "  3. Commit: git commit -m 'Magic Villa project - ready for deployment'"
echo "  4. Push: git push"
