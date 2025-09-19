#!/bin/bash

# Stop any existing containers
podman stop personalwebsite-app 2>/dev/null
podman rm personalwebsite-app 2>/dev/null

# Build and run the webapp directly on port 6060
echo "Building webapp image..."
podman build -t personalwebsite-app .

echo "Starting webapp directly on port 6060..."
podman run -d \
  --name personalwebsite-app \
  -p 6060:6060 \
  -v app-data:/app/data \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__DefaultConnection=Data Source=/app/data/personalwebsite.db \
  personalwebsite-app

# Wait for the webapp to start
echo "Waiting for webapp to start..."
sleep 5

# Check if the webapp is running
echo "Checking if webapp is running..."
podman exec personalwebsite-app wget --no-verbose --tries=1 --spider http://localhost:6060/health || {
  echo "Webapp failed to start. Check logs with: podman logs personalwebsite-app"
  exit 1
}

echo "Webapp started successfully!"
echo "Access your application at: http://localhost:6060"
echo "Note: This bypasses Caddy reverse proxy and serves directly from ASP.NET Core"