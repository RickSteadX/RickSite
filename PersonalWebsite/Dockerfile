# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["PersonalWebsite.Client/PersonalWebsite.Client.csproj", "PersonalWebsite.Client/"]
COPY ["PersonalWebsite.Server/PersonalWebsite.Server.csproj", "PersonalWebsite.Server/"]
COPY ["PersonalWebsite.sln", "."]

# Force NuGet to use specific package sources and clear cache to ensure fresh packages
RUN dotnet nuget locals all --clear
RUN dotnet restore --disable-parallel

# Copy the rest of the source code
COPY . .

# Build and publish the application
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Create non-root user for security
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

# Create directory for SQLite database with proper permissions
RUN mkdir -p /app/data && chown -R appuser:appgroup /app/data

# Copy the published application
COPY --from=build /app/publish .
RUN chown -R appuser:appgroup /app

# Set environment variables
ENV ASPNETCORE_URLS=http://+:6060
ENV ASPNETCORE_ENVIRONMENT=Production

# Switch to non-root user
USER appuser

# Health check
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:6060/health || exit 1

# Expose port 6060
EXPOSE 6060

# Start the application
ENTRYPOINT ["dotnet", "PersonalWebsite.Server.dll"]