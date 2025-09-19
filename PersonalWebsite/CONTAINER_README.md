# Container Deployment Guide

This document provides information about the containerized deployment of the Personal Website with CMS.

## Container Overview

The application is containerized using a multi-stage Docker/Podman build process:

1. **Build Stage**: Uses `mcr.microsoft.com/dotnet/sdk:8.0` to compile and publish the application
2. **Runtime Stage**: Uses lightweight `mcr.microsoft.com/dotnet/aspnet:8.0-alpine` for the runtime environment

## Security Enhancements

The container includes several security best practices:

- Runs as a non-root user (`appuser`)
- Uses Alpine Linux for a minimal attack surface
- Implements proper file permissions
- Includes container health checks
- Removes unnecessary tools and packages

## Deployment Architecture

The deployment uses a two-container architecture:

1. **Web Application Container**: Runs the ASP.NET Core application
2. **Caddy Container**: Provides reverse proxy, HTTPS, and security headers

## Container Configuration

Key configuration aspects:

- **Environment Variables**: Set in docker-compose.yml
- **Persistent Storage**: Uses Docker volumes for database and Caddy data
- **Networking**: Uses a bridge network for container communication
- **Health Checks**: Monitors application health at 30-second intervals

## Production Deployment

For production deployment:

1. Clone the repository from GitHub:
   ```bash
   git clone https://github.com/RickSteadX/RickSite.git
   cd RickSite/PersonalWebsite
   ```

2. Update the Caddyfile with your domain name (see DEPLOYMENT.md for details)

3. Deploy using Docker or Podman:
   ```bash
   # Using Docker
   docker-compose up -d
   
   # Using Podman
   podman-compose up -d
   ```

## Container Maintenance

Regular maintenance tasks:

- **Updates**: Pull the latest code and rebuild containers
- **Backups**: Back up the SQLite database volume
- **Monitoring**: Check container health and logs
- **Security**: Keep base images updated

## Troubleshooting

Common issues and solutions:

1. **Container fails to start**: Check logs with `docker-compose logs webapp`
2. **Database connection issues**: Verify volume permissions
3. **HTTPS certificate problems**: Ensure proper DNS configuration

For more detailed deployment instructions, see the [DEPLOYMENT.md](./DEPLOYMENT.md) file.