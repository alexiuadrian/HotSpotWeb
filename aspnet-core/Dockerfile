FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/HotSpotWeb.Web.Host/HotSpotWeb.Web.Host.csproj", "src/HotSpotWeb.Web.Host/"]
COPY ["src/HotSpotWeb.Web.Core/HotSpotWeb.Web.Core.csproj", "src/HotSpotWeb.Web.Core/"]
COPY ["src/HotSpotWeb.Application/HotSpotWeb.Application.csproj", "src/HotSpotWeb.Application/"]
COPY ["src/HotSpotWeb.Core/HotSpotWeb.Core.csproj", "src/HotSpotWeb.Core/"]
COPY ["src/HotSpotWeb.EntityFrameworkCore/HotSpotWeb.EntityFrameworkCore.csproj", "src/HotSpotWeb.EntityFrameworkCore/"]
WORKDIR "/src/src/HotSpotWeb.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/HotSpotWeb.Web.Host", "src/HotSpotWeb.Web.Host"]
COPY ["src/HotSpotWeb.Web.Core", "src/HotSpotWeb.Web.Core"]
COPY ["src/HotSpotWeb.Application", "src/HotSpotWeb.Application"]
COPY ["src/HotSpotWeb.Core", "src/HotSpotWeb.Core"]
COPY ["src/HotSpotWeb.EntityFrameworkCore", "src/HotSpotWeb.EntityFrameworkCore"]
WORKDIR "/src/src/HotSpotWeb.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "HotSpotWeb.Web.Host.dll"]
