
#https://medium.com/@aliyildizoz/understanding-asp-net-core-dockerfile-a523233bb9a4

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base     
USER app                                         
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["agent/agent.csproj", "agent/"]
RUN dotnet restore "./agent/agent.csproj"
COPY agent/ ./agent/
WORKDIR "/src/agent"
RUN dotnet build "./agent.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./agent.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
# ENV SQLSERVER_DOCKER_CONNECTION_STRING=""
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=publish /app/publish .
# Copy Data folder from build stage to final stage
COPY --from=build /src/agent/Data ./Data

# Ensure the app user owns the Data directory and has write permissions
USER root
RUN chown -R app:app /app/Data && chmod -R 755 /app/Data
USER app

ENTRYPOINT ["dotnet", "agent.dll"]