FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["dfreeze-api.csproj", "./"]
RUN dotnet restore "./dfreeze-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dfreeze-api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "dfreeze-api.csproj" -c Release -o /app

FROM base AS final
ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dfreeze-api.dll"]
