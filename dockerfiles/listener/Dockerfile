#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Listener/IOTHome/IOTHome.csproj", "src/Listener/IOTHome/"]
COPY ["src/Utiliry/EmailClient/EmailClient.csproj", "src/Utiliry/EmailClient/"]
RUN dotnet restore "src/Listener/IOTHome/IOTHome.csproj"
COPY . .
WORKDIR "/src/src/Listener/IOTHome"
RUN dotnet build "IOTHome.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IOTHome.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IOTHome.dll"]