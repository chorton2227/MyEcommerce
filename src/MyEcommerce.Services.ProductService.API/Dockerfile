FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MyEcommerce.Core/MyEcommerce.Core.csproj", "MyEcommerce.Core/"]
COPY ["src/MyEcommerce.Data.EntityFrameworkCore/MyEcommerce.Data.EntityFrameworkCore.csproj", "MyEcommerce.Data.EntityFrameworkCore/"]
COPY ["src/MyEcommerce.Services.ProductService.API/MyEcommerce.Services.ProductService.API.csproj", "MyEcommerce.Services.ProductService.API/"]
COPY ["src/MyEcommerce.Services.ProductService.Application/MyEcommerce.Services.ProductService.Application.csproj", "MyEcommerce.Services.ProductService.Application/"]
COPY ["src/MyEcommerce.Services.ProductService.Data/MyEcommerce.Services.ProductService.Data.csproj", "MyEcommerce.Services.ProductService.Data/"]
COPY ["src/MyEcommerce.Services.ProductService.Domain/MyEcommerce.Services.ProductService.Domain.csproj", "MyEcommerce.Services.ProductService.Domain/"]
RUN dotnet restore "MyEcommerce.Services.ProductService.API/MyEcommerce.Services.ProductService.API.csproj"
COPY . .
WORKDIR "/src/src/MyEcommerce.Services.ProductService.API"
RUN dotnet build "MyEcommerce.Services.ProductService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyEcommerce.Services.ProductService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyEcommerce.Services.ProductService.API.dll"]