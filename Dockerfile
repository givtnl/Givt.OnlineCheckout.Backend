FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
# COPY ./src/Core/Givt.OnlineCheckout.API/Givt.OnlineCheckout.API.csproj ./

# Copy everything else and build
COPY ./ ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "Givt.OnlineCheckout.API.dll"]