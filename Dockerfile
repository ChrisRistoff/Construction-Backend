FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY ["construction/construction.csproj", "construction/"]
RUN dotnet restore "construction/construction.csproj"

COPY construction/ ./construction/
WORKDIR /app/construction
RUN dotnet publish "construction.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/construction/out .

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "construction.dll"]