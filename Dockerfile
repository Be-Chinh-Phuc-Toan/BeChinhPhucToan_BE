# Use official .NET 8.0 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BeChinhPhucToan_BE.csproj", "./"]
RUN dotnet restore "./BeChinhPhucToan_BE.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BeChinhPhucToan_BE.csproj" -c Release -o /app/build

# Publish application
FROM build AS publish
RUN dotnet publish "BeChinhPhucToan_BE.csproj" -c Release -o /app/publish

# Create final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeChinhPhucToan_BE.dll"]
