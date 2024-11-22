FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY PaintedPokerLib ./PaintedPokerLib
COPY App/*.csproj ./App/
WORKDIR /app/App
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/App/out .

EXPOSE 5245
ENTRYPOINT ["dotnet", "PaintedPoker.dll"]