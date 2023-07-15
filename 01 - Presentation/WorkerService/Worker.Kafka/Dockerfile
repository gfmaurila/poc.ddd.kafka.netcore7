#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["01 - Presentation/WorkerService/Worker.Kafka/Worker.Kafka.csproj", "01 - Presentation/WorkerService/Worker.Kafka/"]
RUN dotnet restore "01 - Presentation/WorkerService/Worker.Kafka/Worker.Kafka.csproj"
COPY . .
WORKDIR "/src/01 - Presentation/WorkerService/Worker.Kafka"
RUN dotnet build "Worker.Kafka.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Worker.Kafka.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Worker.Kafka.dll"]