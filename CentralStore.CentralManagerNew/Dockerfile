# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy everything else
COPY . .

RUN echo "===== /src contents =====" && ls -R /src

RUN find /src -name "*.csproj"

RUN dotnet restore CentralStore.CentralManagerNew/CentralStore.CentralManagerNew.csproj

# Publish Blazor WASM
RUN dotnet publish CentralStore.CentralManagerNew/CentralStore.CentralManagerNew.csproj \
    -c $BUILD_CONFIGURATION \
    -o /app/publish

# =========================
# RUNTIME STAGE (NGINX)
# =========================
FROM nginx:alpine AS final

# Copy static files
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Copy nginx config (SPA routing)
COPY CentralStore.CentralManagerNew/nginx.conf /etc/nginx/nginx.conf

# Expose port (optional but nice)
EXPOSE 80

# Nginx runs automatically