#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["docker-wpf-exercise-tklecka/docker-wpf-exercise-tklecka.csproj", "docker-wpf-exercise-tklecka/"]
RUN dotnet restore "docker-wpf-exercise-tklecka/docker-wpf-exercise-tklecka.csproj"
COPY . .
WORKDIR "/src/docker-wpf-exercise-tklecka"
RUN dotnet build "docker-wpf-exercise-tklecka.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "docker-wpf-exercise-tklecka.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "docker-wpf-exercise-tklecka.dll"]