FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

# copy and restore as distinct layers to cache the restore result and prevent unnecessary package restores
COPY ["construction/construction.csproj", "construction/"]
RUN dotnet restore "construction/construction.csproj"

# copy the rest of the files
COPY construction/ ./construction/
WORKDIR /app/construction
RUN dotnet publish "construction.csproj" -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/construction/out .

# set the environment variable and entry point for the application
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "construction.dll"]