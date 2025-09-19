# Deploying on Fedora with Cockpit + Podman

This guide provides instructions for deploying the Personal Website application on Fedora using Cockpit and Podman.

## Prerequisites

- Fedora OS with Cockpit installed
- Podman installed
- Git installed

## Deployment Options

We've provided several scripts to help you deploy the application based on your specific environment and requirements.

### Option 1: Standard Deployment (Port 6060)

This is the standard deployment option that uses port 6060:

```bash
./start-services.sh
```

If you encounter permission issues, try one of the following options.

### Option 2: Using Higher Port (8080)

This option uses port 8080, which doesn't require root privileges:

```bash
./start-with-high-port.sh
```

### Option 3: Using NET_BIND_SERVICE Capability

This option adds the NET_BIND_SERVICE capability to allow binding to privileged ports:

```bash
./start-with-cap-add.sh
```

### Option 4: Using sysctl Settings

This option modifies sysctl settings to allow non-root users to bind to privileged ports:

```bash
./start-with-sysctl.sh
```

### Option 5: Simple Deployment (No Caddy)

This option runs the application directly without using Caddy as a reverse proxy:

```bash
./start-simple.sh
```

## Troubleshooting

### Permission Denied Error

If you encounter a "Permission denied" error when binding to a port, try one of the following solutions:

1. Use a higher port number (Option 2)
2. Add the NET_BIND_SERVICE capability (Option 3)
3. Modify sysctl settings (Option 4)
4. Run as root (not recommended for production)

### Container Already Exists

If you get an error that the container already exists, stop and remove the existing containers:

```bash
podman stop personalwebsite-app personalwebsite-caddy
podman rm personalwebsite-app personalwebsite-caddy
```

### Storage Issues

If you encounter storage issues with Podman, try pruning the system:

```bash
podman system prune -f
```

## Accessing the Application

After successful deployment, you can access the application at:

- Standard deployment: http://localhost:6060
- Higher port deployment: http://localhost:8080

## Admin Access

To access the admin area, use the following credentials:

- Username: admin
- Password: admin123

## Data Persistence

The application data is stored in a volume named `app-data`. This ensures that your data persists even if the container is removed.