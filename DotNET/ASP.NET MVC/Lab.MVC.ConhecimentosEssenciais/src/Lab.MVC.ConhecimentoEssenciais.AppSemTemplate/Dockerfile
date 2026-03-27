# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lab.MVC.AppSemTemplate.csproj", "Lab.MVC.ConhecimentoEssenciais.AppSemTemplate/"]
RUN dotnet restore "Lab.MVC.ConhecimentoEssenciais.AppSemTemplate/Lab.MVC.AppSemTemplate.csproj"
WORKDIR "Lab.MVC.ConhecimentoEssenciais.AppSemTemplate"
COPY . .
RUN dotnet build "Lab.MVC.AppSemTemplate.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN mkdir -p /root/.aspnet/https && chmod 777 /root/.aspnet/https

# Rodando com HTTPs
RUN dotnet dev-certs https -ep /root/.aspnet/https/aspnetapp.pfx -p Teste@123
RUN dotnet dev-certs https --trust

RUN chmod 777 /root/.aspnet/https/aspnetapp.pfx

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Rodando com HTTPs
COPY --from=build /root/.aspnet/https/aspnetapp.pfx /root/.aspnet/https/

RUN dotnet publish "Lab.MVC.AppSemTemplate.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Rodando com HTTPs
COPY --from=publish /app/publish .
COPY --from=publish /root/.aspnet/https/aspnetapp.pfx /root/.aspnet/https/

ENV ASPNETCORE_ENVIRONMENT="Docker"

# o contâiner pode acessar o volume abaixo.
VOLUME /var/data_protection_keys 

# Rodando com HTTPs
ENV ASPNETCORE_URLS="https://+:443;https://+:8080;"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="Teste@123"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/root/.aspnet/https/aspnetapp.pfx"

ENTRYPOINT ["dotnet", "Lab.MVC.AppSemTemplate.dll"]