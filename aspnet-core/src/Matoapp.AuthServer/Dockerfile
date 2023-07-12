#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 44350

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Matoapp.AuthServer/Matoapp.AuthServer.csproj", "src/Matoapp.AuthServer/"]
COPY ["src/Matoapp.EntityFrameworkCore/Matoapp.EntityFrameworkCore.csproj", "src/Matoapp.EntityFrameworkCore/"]
COPY ["src/Matoapp.Domain/Matoapp.Domain.csproj", "src/Matoapp.Domain/"]
COPY ["src/Matoapp.Domain.Shared/Matoapp.Domain.Shared.csproj", "src/Matoapp.Domain.Shared/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.EntityFrameworkCore/Matoapp.Common.EntityFrameworkCore.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.EntityFrameworkCore/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.Domain/Matoapp.Common.Domain.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.Domain/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.Domain.Shared/Matoapp.Common.Domain.Shared.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.Domain.Shared/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.EntityFrameworkCore/Matoapp.Health.EntityFrameworkCore.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.EntityFrameworkCore/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.Domain/Matoapp.Health.Domain.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.Domain/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.HttpApi.Client/Matoapp.Identity.HttpApi.Client.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.HttpApi.Client/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Application.Contracts/Matoapp.Identity.Application.Contracts.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Application.Contracts/"]
COPY ["src/infrastructure/Application.Share/Application.Share.csproj", "src/infrastructure/Application.Share/"]
COPY ["src/infrastructure/Domain.Share/Domain.Share.csproj", "src/infrastructure/Domain.Share/"]
COPY ["src/infrastructure/Matoapp.Infrastructure/Matoapp.Infrastructure.csproj", "src/infrastructure/Matoapp.Infrastructure/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain.Shared/Matoapp.Identity.Domain.Shared.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain.Shared/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.Domain.Shared/Matoapp.Health.Domain.Shared.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.Domain.Shared/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.EntityFrameworkCore/Matoapp.Identity.EntityFrameworkCore.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.EntityFrameworkCore/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain/Matoapp.Identity.Domain.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain/"]
RUN dotnet restore "src/Matoapp.AuthServer/Matoapp.AuthServer.csproj"
COPY . .
WORKDIR "/src/src/Matoapp.AuthServer"
RUN dotnet build "Matoapp.AuthServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Matoapp.AuthServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:44350
ENTRYPOINT ["dotnet", "Matoapp.AuthServer.dll"]