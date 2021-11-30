FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MyEcommerce.Core/MyEcommerce.Core.csproj", "MyEcommerce.Core/"]
COPY ["src/MyEcommerce.Data.EntityFrameworkCore/MyEcommerce.Data.EntityFrameworkCore.csproj", "MyEcommerce.Data.EntityFrameworkCore/"]
COPY ["src/MyEcommerce.Services.OrderService.API/MyEcommerce.Services.OrderService.API.csproj", "MyEcommerce.Services.OrderService.API/"]
COPY ["src/MyEcommerce.Services.OrderService.Application/MyEcommerce.Services.OrderService.Application.csproj", "MyEcommerce.Services.OrderService.Application/"]
COPY ["src/MyEcommerce.Services.OrderService.Data/MyEcommerce.Services.OrderService.Data.csproj", "MyEcommerce.Services.OrderService.Data/"]
COPY ["src/MyEcommerce.Services.OrderService.Domain/MyEcommerce.Services.OrderService.Domain.csproj", "MyEcommerce.Services.OrderService.Domain/"]
RUN dotnet restore "MyEcommerce.Services.OrderService.API/MyEcommerce.Services.OrderService.API.csproj"
COPY . .
WORKDIR "/src/src/MyEcommerce.Services.OrderService.API"
RUN dotnet build "MyEcommerce.Services.OrderService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyEcommerce.Services.OrderService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyEcommerce.Services.OrderService.API.dll"]