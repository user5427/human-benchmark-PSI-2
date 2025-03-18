FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

EXPOSE 80
EXPOSE 443

COPY *.csproj ./
RUN dotnet restore
COPY . ./

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

USER app

COPY --from=build /app/publish .
COPY certificate.pfx ./

ENV ASPNETCORE_URLS=https://+:443;http://+:80
ENTRYPOINT ["dotnet", "AimReactionAPI.dll"]
