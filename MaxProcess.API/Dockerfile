FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MaxProcess.API/MaxProcess.API.csproj", "MaxProcess.API/"]
COPY ["MaxProcess.Application/MaxProcess.Application.csproj", "MaxProcess.Application/"]
COPY ["MaxProcess.Domain/MaxProcess.Domain.csproj", "MaxProcess.Domain/"]
COPY ["MaxProcess.Persistence/MaxProcess.Persistence.csproj", "MaxProcess.Persistence/"]


RUN dotnet restore "MaxProcess.API/MaxProcess.API.csproj"

COPY . .

WORKDIR "/src/MaxProcess.API"

RUN dotnet build "MaxProcess.API.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "MaxProcess.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MaxProcess.API.dll"]
