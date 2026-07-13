# =========================
# 1. Base runtime image
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# penting: bind ke semua IP
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

# =========================
# 2. Build image
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj & restore dependencies
COPY ["JastipinAja.Web.csproj", "./"]
RUN dotnet restore "JastipinAja.Web.csproj"

# Copy all files & build
COPY . .
RUN dotnet build "JastipinAja.Web.csproj" -c Release -o /app/build

# =========================
# 3. Publish
# =========================
FROM build AS publish
RUN dotnet publish "JastipinAja.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# 4. Final image
# =========================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "JastipinAja.Web.dll"]