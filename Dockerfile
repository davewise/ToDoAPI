# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ToDoAPI/*.csproj ./ToDoAPI/
RUN dotnet restore

# copy everything else and build app
COPY ToDoAPI/. ./ToDoAPI/
WORKDIR /source/ToDoAPI
#RUN dotnet publish -c release -o /app --no-restore
RUN dotnet publish -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ToDoAPI.dll"]
