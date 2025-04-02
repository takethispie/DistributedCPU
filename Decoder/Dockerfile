FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Decoder.csproj", "Decoder/"]
RUN dotnet restore "Decoder/Decoder.csproj"

COPY . Decoder/.
RUN dotnet publish -c Release -o /app Decoder/Decoder.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Decoder.dll"]