FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY UrlShortner.sln .
COPY ./UrlShortner.csproj .
RUN dotnet restore './UrlShortner.csproj'

COPY . .

RUN dotnet publish './UrlShortner.csproj' -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .


EXPOSE 5022

ENTRYPOINT [ "dotnet", "UrlShortner.dll" ]