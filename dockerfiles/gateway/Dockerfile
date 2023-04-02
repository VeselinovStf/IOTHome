#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Gateway/LoraGatewayService.API/LoraGatewayService.API.csproj", "src/Gateway/LoraGatewayService.API/"]
COPY ["src/Utiliry/AppLogger/AppLogger.csproj", "src/Utiliry/AppLogger/"]
COPY ["src/Utiliry/MessageQuery/MessageQuery.csproj", "src/Utiliry/MessageQuery/"]
RUN dotnet restore "src/Gateway/LoraGatewayService.API/LoraGatewayService.API.csproj"
COPY . .
WORKDIR "/src/src/Gateway/LoraGatewayService.API"
RUN dotnet build "LoraGatewayService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoraGatewayService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoraGatewayService.API.dll"]