#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 44377

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Matoapp.HttpApi.Host/Matoapp.HttpApi.Host.csproj", "src/Matoapp.HttpApi.Host/"]
COPY ["src/Matoapp.Application/Matoapp.Application.csproj", "src/Matoapp.Application/"]
COPY ["src/Matoapp.Domain/Matoapp.Domain.csproj", "src/Matoapp.Domain/"]
COPY ["src/Matoapp.Domain.Shared/Matoapp.Domain.Shared.csproj", "src/Matoapp.Domain.Shared/"]
COPY ["src/Matoapp.Application.Contracts/Matoapp.Application.Contracts.csproj", "src/Matoapp.Application.Contracts/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.Application/Matoapp.Common.Application.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.Application/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Application.Contracts/Matoapp.Identity.Application.Contracts.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Application.Contracts/"]
COPY ["src/infrastructure/Application.Share/Application.Share.csproj", "src/infrastructure/Application.Share/"]
COPY ["src/infrastructure/Domain.Share/Domain.Share.csproj", "src/infrastructure/Domain.Share/"]
COPY ["src/infrastructure/Matoapp.Infrastructure/Matoapp.Infrastructure.csproj", "src/infrastructure/Matoapp.Infrastructure/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain.Shared/Matoapp.Identity.Domain.Shared.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain.Shared/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.Application.Contracts/Matoapp.Common.Application.Contracts.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.Application.Contracts/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.Domain.Shared/Matoapp.Common.Domain.Shared.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.Domain.Shared/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.Domain/Matoapp.Common.Domain.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.Domain/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.Application/Matoapp.Health.Application.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.Application/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.HttpApi.Client/Matoapp.Identity.HttpApi.Client.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.HttpApi.Client/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.Application.Contracts/Matoapp.Health.Application.Contracts.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.Application.Contracts/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.Domain.Shared/Matoapp.Health.Domain.Shared.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.Domain.Shared/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.Domain/Matoapp.Health.Domain.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.Domain/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Application/Matoapp.Identity.Application.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Application/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain/Matoapp.Identity.Domain.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.Domain/"]
COPY ["src/Matoapp.EntityFrameworkCore/Matoapp.EntityFrameworkCore.csproj", "src/Matoapp.EntityFrameworkCore/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.EntityFrameworkCore/Matoapp.Common.EntityFrameworkCore.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.EntityFrameworkCore/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.EntityFrameworkCore/Matoapp.Health.EntityFrameworkCore.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.EntityFrameworkCore/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.EntityFrameworkCore/Matoapp.Identity.EntityFrameworkCore.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.EntityFrameworkCore/"]
COPY ["src/Matoapp.HttpApi/Matoapp.HttpApi.csproj", "src/Matoapp.HttpApi/"]
COPY ["src/modules/Matoapp.Common/src/Matoapp.Common.HttpApi/Matoapp.Common.HttpApi.csproj", "src/modules/Matoapp.Common/src/Matoapp.Common.HttpApi/"]
COPY ["src/infrastructure/HttpApi.Share/HttpApi.Share.csproj", "src/infrastructure/HttpApi.Share/"]
COPY ["src/modules/Matoapp.Health/src/Matoapp.Health.HttpApi/Matoapp.Health.HttpApi.csproj", "src/modules/Matoapp.Health/src/Matoapp.Health.HttpApi/"]
COPY ["src/modules/Matoapp.Identity/src/Matoapp.Identity.HttpApi/Matoapp.Identity.HttpApi.csproj", "src/modules/Matoapp.Identity/src/Matoapp.Identity.HttpApi/"]
RUN dotnet restore "src/Matoapp.HttpApi.Host/Matoapp.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Matoapp.HttpApi.Host"
RUN dotnet build "Matoapp.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Matoapp.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:44377
ENTRYPOINT ["dotnet", "Matoapp.HttpApi.Host.dll"]