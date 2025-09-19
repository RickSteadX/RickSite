# Personal Website with CMS

A technologically advanced personal website with a built-in content management system for dynamic page creation.

## Technology Stack

- **Front-End**: Blazor WebAssembly
- **Back-End**: ASP.NET Core
- **Database**: SQLite
- **Runtime**: Podman
- **Web Server**: Caddy

## Features

- Interactive hero section with particle-based background
- Parallax scrolling effects
- Smooth page transitions
- Dark mode / light mode toggle
- Secure admin panel
- Dynamic page builder
- RESTful API

## Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Podman](https://podman.io/getting-started/installation) or Docker
- [Caddy](https://caddyserver.com/docs/install) (optional for local development)

### Development Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/personal-website.git
   cd personal-website
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run --project PersonalWebsite.Server
   ```

4. Open your browser and navigate to:
   ```
   https://localhost:5001
   ```

### Using Podman/Docker

1. Build and run using docker-compose:
   ```bash
   podman-compose up -d
   ```
   or with Docker:
   ```bash
   docker-compose up -d
   ```

2. Access the website at:
   ```
   http://localhost
   ```

### Admin Access

1. Navigate to the admin login page:
   ```
   http://localhost/login
   ```

2. Use the default credentials:
   - Username: `admin`
   - Password: `admin123`

3. After logging in, you can:
   - Create and manage pages
   - Use the dynamic page builder
   - Adjust site settings

## Project Structure

- **PersonalWebsite.Client**: Blazor WebAssembly frontend
- **PersonalWebsite.Server**: ASP.NET Core backend with API endpoints
- **Database**: SQLite database stored in the Server project

## Advanced Features

### Interactive Hero Section

The website features a dynamic, interactive particle-based background that responds to mouse movement, creating an engaging user experience.

### Dynamic Page Builder

The built-in page builder allows administrators to create entirely new, unique pages and add them to the website's navigation. The backend stores the layout and content structure of these pages, which the Blazor front-end renders dynamically.

### Theme Toggle

Users can switch between dark mode and light mode themes according to their preference.

## Deployment

### Production Deployment

1. Update the Caddyfile with your domain name
2. Ensure your server has Podman/Docker and docker-compose installed
3. Deploy using:
   ```bash
   podman-compose -f docker-compose.yml up -d
   ```

### Custom Domain Setup

1. Update the Caddyfile with your domain:
   ```
   yourdomain.com {
       reverse_proxy webapp:80
       # Other configurations...
   }
   ```

2. Ensure your DNS records point to your server's IP address

## Security Considerations

- The default admin password should be changed immediately after first login
- The application uses secure cookie authentication
- All API endpoints are protected with proper authorization
- HTTPS is enforced in production environments

## License

This project is licensed under the MIT License - see the LICENSE file for details.