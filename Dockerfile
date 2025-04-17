# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "./Api/Api.csproj"

RUN dotnet publish "./Api/Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

COPY ./Api/leave.db ./leave.db

EXPOSE 80

ENTRYPOINT ["dotnet", "Api.dll"]
