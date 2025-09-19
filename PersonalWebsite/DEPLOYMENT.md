# Production Deployment Guide

This guide provides instructions for deploying the Personal Website with CMS to a production environment.

## Prerequisites

- A server with Docker or Podman installed
- Docker Compose or Podman Compose installed
- A domain name (optional, but recommended for production)
- Basic knowledge of server administration and DNS configuration

## Deployment Steps

### 1. Clone the Repository

```bash
git clone https://github.com/RickSteadX/RickSite.git
cd RickSite/PersonalWebsite
```

### 2. Configure for Production

#### Update Caddyfile

Edit the `Caddyfile` to use your domain name:

```
yourdomain.com {
    reverse_proxy webapp:80
    encode gzip zstd
    tls your-email@example.com
    
    # Security headers are already configured
}
```

Replace `yourdomain.com` with your actual domain and `your-email@example.com` with your email address for Let's Encrypt notifications.

#### Environment Variables (Optional)

Create a `.env` file for additional environment variables:

```
# Example .env file
ADMIN_EMAIL=admin@example.com
SMTP_SERVER=smtp.example.com
SMTP_PORT=587
SMTP_USERNAME=user
SMTP_PASSWORD=password
```

### 3. Deploy with Docker/Podman

```bash
# Using Docker
docker-compose up -d

# Using Podman
podman-compose up -d
```

This will:
1. Build the application container
2. Start the application and Caddy web server
3. Set up automatic HTTPS with Let's Encrypt
4. Configure reverse proxy and security headers

### 4. Verify Deployment

1. Check container status:
   ```bash
   docker-compose ps
   # or
   podman-compose ps
   ```

2. View logs:
   ```bash
   docker-compose logs -f
   # or
   podman-compose logs -f
   ```

3. Access your website at `https://yourdomain.com`

### 5. First-Time Setup

1. Access the admin panel at `https://yourdomain.com/login`
2. Log in with the default credentials:
   - Username: `admin`
   - Password: `admin123`
3. **Important**: Change the default password immediately

### 6. Maintenance

#### Updates

To update the application:

```bash
git pull
docker-compose down
docker-compose up -d --build
# or with Podman
podman-compose down
podman-compose up -d --build
```

#### Backups

The SQLite database is stored in a persistent volume. To back it up:

```bash
# For Docker
docker cp personalwebsite-app:/app/data/personalwebsite.db ./backup.db

# For Podman
podman cp personalwebsite-app:/app/data/personalwebsite.db ./backup.db
```

#### Monitoring

Check container health:

```bash
docker inspect --format='{{.State.Health.Status}}' personalwebsite-app
# or
podman inspect --format='{{.State.Health.Status}}' personalwebsite-app
```

## Troubleshooting

### Common Issues

1. **Caddy can't obtain SSL certificate**
   - Ensure your domain's DNS is correctly pointing to your server
   - Check that ports 80 and 443 are open on your firewall

2. **Application container not starting**
   - Check logs: `docker-compose logs webapp`
   - Verify database permissions: The container user needs write access to the data directory

3. **Database connection issues**
   - Verify the connection string in docker-compose.yml
   - Check if the volume is properly mounted

### Getting Help

If you encounter issues not covered in this guide, please:
1. Check the project's GitHub issues
2. Create a new issue with detailed information about your problem

## Security Considerations

- Always change the default admin password
- Keep your server and containers updated
- Consider implementing additional security measures like fail2ban
- Regularly back up your database
- Monitor your server for unusual activity