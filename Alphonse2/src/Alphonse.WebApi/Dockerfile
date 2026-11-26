ARG DOTNET_VERSION=10.0

#================================================
#Stage 1: Build .Net application
#================================================
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION as dotnet-build

RUN apt-get -y update && apt-get install -y --no-install-recommends \
        jq \
        xmlstarlet \
        && \
    apt-get dist-clean

WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

COPY . ./

# remove logfile target from appsettings.json (not needed for docker container)
RUN cat appsettings.json \
    | jq 'del(.NLog.targets.logfile)' \
    | jq 'del(.NLog.rules[] | select(.writeTo == "logfile"))' \
    | jq '(.NLog.rules[] | select(.logger == "*")).minLevel = "Info"' \
    > appsettings.json.tmp && mv appsettings.json.tmp appsettings.json
RUN xmlstarlet ed  -L -d "/Project/Target/Copy[@SourceFiles='systemd/alphonse.webapi.kestrel.service']" Alphonse.WebApi.csproj

RUN dotnet publish -c Release -o /app/publish


#================================================
#Stage 2: Build the final image
#================================================
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION

WORKDIR /app

COPY --from=dotnet-build /app/publish .
# force the runtime to roll forward to the latest major version of .NET if available
ENV DOTNET_ROLL_FORWARD=Major

EXPOSE 80

ENTRYPOINT ["dotnet", "Alphonse.WebApi.dll"]