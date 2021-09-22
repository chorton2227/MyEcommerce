FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt update && apt upgrade
RUN curl -sL https://deb.nodesource.com/setup_16.x |  bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN apt update && apt upgrade
RUN curl -sL https://deb.nodesource.com/setup_16.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["src/MyEcommerce.WebSPA/MyEcommerce.WebSPA.csproj", "MyEcommerce.WebSPA/"]
RUN dotnet restore "MyEcommerce.WebSPA/MyEcommerce.WebSPA.csproj"
COPY . .
WORKDIR "/src/src/MyEcommerce.WebSPA"
RUN dotnet build "MyEcommerce.WebSPA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyEcommerce.WebSPA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyEcommerce.WebSPA.dll"]