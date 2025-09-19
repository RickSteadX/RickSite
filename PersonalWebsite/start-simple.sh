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

echo "Webapp started!"
echo "Access your application at: http://localhost:6060"
echo "Note: This bypasses Caddy reverse proxy and serves directly from ASP.NET Core"