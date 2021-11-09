FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MyEcommerce.Core/MyEcommerce.Core.csproj", "MyEcommerce.Core/"]
COPY ["src/MyEcommerce.Data.EntityFrameworkCore/MyEcommerce.Data.EntityFrameworkCore.csproj", "MyEcommerce.Data.EntityFrameworkCore/"]
COPY ["src/MyEcommerce.Services.IdentityService.API/MyEcommerce.Services.IdentityService.API.csproj", "MyEcommerce.Services.IdentityService.API/"]
RUN dotnet restore "MyEcommerce.Services.IdentityService.API/MyEcommerce.Services.IdentityService.API.csproj"
COPY . .
WORKDIR "/src/src/MyEcommerce.Services.IdentityService.API"
RUN dotnet build "MyEcommerce.Services.IdentityService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyEcommerce.Services.IdentityService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyEcommerce.Services.IdentityService.API.dll"]