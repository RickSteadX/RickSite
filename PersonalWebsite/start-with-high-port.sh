#!/bin/bash

# Stop any existing containers
podman stop personalwebsite-app 2>/dev/null
podman rm personalwebsite-app 2>/dev/null

# Build the webapp image
echo "Building webapp image..."
podman build -t personalwebsite-app .

# Create network if it doesn't exist
podman network create app-network 2>/dev/null || true

# Start the webapp container with a higher port number
echo "Starting webapp container..."
podman run -d \
  --name personalwebsite-app \
  --network app-network \
  -p 8080:6060 \
  -v app-data:/app/data \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e "ConnectionStrings__DefaultConnection=Data Source=/app/data/personalwebsite.db" \
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
echo "Access your application at: http://localhost:8080"
echo "Note: This uses a higher port number (8080) which doesn't require root privileges"