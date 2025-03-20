# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BeChinhPhucToan_BE.csproj", "./"]
RUN dotnet restore "./BeChinhPhucToan_BE.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet publish "BeChinhPhucToan_BE.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5016
ENTRYPOINT ["dotnet", "BeChinhPhucToan_BE.dll"]
