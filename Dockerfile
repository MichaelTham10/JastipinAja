FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# penting: bind ke semua IP
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "JastipinAja.Web.dll"]