FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /app

COPY src/Users.Shared ./src/Users.Shared
COPY src/Users.Api ./src/Users.Api
COPY src/Users.Web ./src/Users.Web

RUN dotnet restore ./src/Users.Web/Users.Web.csproj
#RUN dotnet publish ./Users.Web/Users.Web.csproj -c Release -o out --self-contained -r linux-x64 
RUN dotnet publish ./src/Users.Web/Users.Web.csproj -c Release -o out 

COPY tests/Users.Api.Tests ./tests/Users.Api.Tests
COPY tests/Users.Web.Tests ./tests/Users.Web.Tests
RUN dotnet restore ./tests/Users.Api.Tests/Users.Api.Tests.csproj
RUN dotnet restore ./tests/Users.Web.Tests/Users.Web.Tests.csproj
RUN dotnet test ./tests/Users.Api.Tests/Users.Api.Tests.csproj
RUN dotnet test ./tests/Users.Web.Tests/Users.Web.Tests.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS release-env

#RUN apt-get update \
#    && apt-get install -y --no-install-recommends libgdiplus libc6-dev
WORKDIR /app

COPY --from=build-env ./app/src/Users.Web/out .

ARG MYSQL_CONN="server=0.0.0.0;uid=garrard;pwd=password;database=WebApiDB"
ENV ASPNETCORE_URLS="http://*:5000" \
    MYSQL_CONN=$MYSQL_CONN

ENTRYPOINT ["dotnet","Users.Web.dll"]
EXPOSE 5000 
