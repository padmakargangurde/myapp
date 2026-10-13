FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY MEP-APIs.sln ./
COPY MEP-APIs/MEP-APIs.csproj MEP-APIs/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

RUN dotnet restore MEP-APIs.sln

COPY . .

RUN dotnet build MEP-APIs/MEP-APIs.csproj \
    -c Release

RUN dotnet publish MEP-APIs/MEP-APIs.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "MEP-APIs.dll"]
