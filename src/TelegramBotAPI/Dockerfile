# syntax=docker/dockerfile:1

# ===================================================================================
# === Stage 1:Builder container =====================================================
# ===================================================================================
FROM  mcr.microsoft.com/dotnet/sdk:7.0 AS builder

WORKDIR /build

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# ===================================================================================
# === Stage 2: Create a lightweight container =======================================
# ===================================================================================
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80

WORKDIR /app
COPY --from=builder /build/out .

ENTRYPOINT ["dotnet", "TelegramBotAPI.dll"] 