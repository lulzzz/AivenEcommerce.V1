#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AivenEcommerce.V1.WebApi/AivenEcommerce.V1.WebApi.csproj", "AivenEcommerce.V1.WebApi/"]
COPY ["AivenEcommerce.V1.Infrastructure/AivenEcommerce.V1.Infrastructure.csproj", "AivenEcommerce.V1.Infrastructure/"]
COPY ["AivenEcommerce.V1.Modules.GitHub/AivenEcommerce.V1.Modules.GitHub.csproj", "AivenEcommerce.V1.Modules.GitHub/"]
COPY ["AivenEcommerce.V1.Application/AivenEcommerce.V1.Application.csproj", "AivenEcommerce.V1.Application/"]
COPY ["AivenEcommerce.V1.Modules.ImgBB/AivenEcommerce.V1.Modules.ImgBB.csproj", "AivenEcommerce.V1.Modules.ImgBB/"]
COPY ["AivenEcommerce.V1.Modules.PayPal/AivenEcommerce.V1.Modules.PayPal.csproj", "AivenEcommerce.V1.Modules.PayPal/"]
COPY ["AivenEcommerce.V1.Domain/AivenEcommerce.V1.Domain.csproj", "AivenEcommerce.V1.Domain/"]
COPY ["AivenEcommerce.V1.Domain.Shared/AivenEcommerce.V1.Domain.Shared.csproj", "AivenEcommerce.V1.Domain.Shared/"]
COPY ["AivenEcommerce.V1.Modules.GitHub.DependencyInjection/AivenEcommerce.V1.Modules.GitHub.DependencyInjection.csproj", "AivenEcommerce.V1.Modules.GitHub.DependencyInjection/"]
COPY ["AivenEcommerce.V1.Modules.ImgBB.DependencyInjection/AivenEcommerce.V1.Modules.ImgBB.DependencyInjection.csproj", "AivenEcommerce.V1.Modules.ImgBB.DependencyInjection/"]
COPY ["AivenEcommerce.V1.Modules.PayPal.DependencyInjection/AivenEcommerce.V1.Modules.PayPal.DependencyInjection.csproj", "AivenEcommerce.V1.Modules.PayPal.DependencyInjection/"]
RUN dotnet restore "AivenEcommerce.V1.WebApi/AivenEcommerce.V1.WebApi.csproj"
COPY . .
WORKDIR "/src/AivenEcommerce.V1.WebApi"
RUN dotnet build "AivenEcommerce.V1.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AivenEcommerce.V1.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AivenEcommerce.V1.WebApi.dll 