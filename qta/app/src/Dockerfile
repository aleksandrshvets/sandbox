ARG NODE_IMAGE=node:8.12

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["QtaElastic/QtaElastic.csproj", "QtaElastic/"]
RUN dotnet restore "QtaElastic/QtaElastic.csproj"
COPY . .
WORKDIR "/src/QtaElastic"

FROM ${NODE_IMAGE} as node-build
WORKDIR /src
COPY QtaElastic/ClientApp .
RUN npm install
RUN npm run build -- --prod

FROM build AS publish
RUN dotnet publish "QtaElastic.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=node-build /src/dist/ClientApp ./ClientApp/dist

ENTRYPOINT ["dotnet", "QtaElastic.dll"]