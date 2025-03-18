# Sử dụng .NET 9 Preview (cập nhật mới nhất từ Microsoft)
FROM mcr.microsoft.com/dotnet/nightly:9.0-preview AS build

WORKDIR /src
COPY ["BeChinhPhucToan_BE.csproj", "./"]
RUN dotnet restore "./BeChinhPhucToan_BE.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet publish "BeChinhPhucToan_BE.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/nightly:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BeChinhPhucToan_BE.dll"]
