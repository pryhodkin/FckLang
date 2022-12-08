#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FckLang.Bot/FckLang.Bot.csproj", "FckLang.Bot/"]
RUN dotnet restore "FckLang.Bot/FckLang.Bot.csproj"
COPY . .
WORKDIR "/src/FckLang.Bot"
RUN dotnet build "FckLang.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FckLang.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FckLang.Bot.dll"]