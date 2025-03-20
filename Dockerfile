# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy toàn bộ source code vào container
COPY . .

# Khôi phục dependencies
RUN dotnet restore "./BeChinhPhucToan_BE.csproj"

# Build và publish ứng dụng
RUN dotnet publish "BeChinhPhucToan_BE.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy từ build stage
COPY --from=build /app/publish .

# Cấu hình Render để sử dụng đúng cổng
ENV ASPNETCORE_URLS=http://+:${PORT}

# Mở cổng ứng dụng
EXPOSE 5016

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "BeChinhPhucToan_BE.dll"]
