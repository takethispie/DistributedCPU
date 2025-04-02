FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ALU.csproj", "ALU/"]
RUN dotnet restore "ALU/ALU.csproj"

COPY . ALU/.
RUN dotnet publish -c Release -o /app ALU/ALU.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ALU.dll"]