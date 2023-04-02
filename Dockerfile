# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY /*.csproj .
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY /. .
RUN dotnet publish -c Release -o /app --use-current-runtime --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 5001
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TodoAPI.dll"]
