FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["RegisterFile.csproj", "RegisterFile/"]
RUN dotnet restore "RegisterFile/RegisterFile.csproj"

COPY . RegisterFile/.
RUN dotnet publish -c Release -o /app RegisterFile/RegisterFile.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "RegisterFile.dll"]