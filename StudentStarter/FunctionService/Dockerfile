FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY FunctionService/FunctionService.csproj FunctionService/
RUN dotnet restore FunctionService/FunctionService.csproj
COPY FunctionService/ FunctionService/
RUN dotnet publish FunctionService/FunctionService.csproj -c Release -o /out --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet","FunctionService.dll"]
