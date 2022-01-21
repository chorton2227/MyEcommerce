FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/MyEcommerce.Services.PaymentService.API/MyEcommerce.Services.PaymentService.API.csproj", "MyEcommerce.Services.PaymentService.API/"]
RUN dotnet restore "MyEcommerce.Services.PaymentService.API/MyEcommerce.Services.PaymentService.API.csproj"
COPY . .
WORKDIR "/src/src/MyEcommerce.Services.PaymentService.API"
RUN dotnet build "MyEcommerce.Services.PaymentService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyEcommerce.Services.PaymentService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyEcommerce.Services.PaymentService.API.dll"]