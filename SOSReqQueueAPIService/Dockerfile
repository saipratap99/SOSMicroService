#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SOSReqQueueAPIService/SOSReqQueueAPIService.csproj", "SOSReqQueueAPIService/"]
COPY ["RabbitMQMicroService/RabbitMQMicroService.csproj", "RabbitMQMicroService/"]
RUN dotnet restore "SOSReqQueueAPIService/SOSReqQueueAPIService.csproj"
COPY . .
WORKDIR "/src/SOSReqQueueAPIService"
RUN dotnet build "SOSReqQueueAPIService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SOSReqQueueAPIService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOSReqQueueAPIService.dll"]
