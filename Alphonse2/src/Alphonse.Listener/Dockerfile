ARG DOTNET_VERSION=10.0

#================================================
#Stage 1: Build nserial library
#================================================
FROM mcr.microsoft.com/dotnet/runtime:$DOTNET_VERSION as nserial-build

RUN apt-get -y update && apt-get install -y --no-install-recommends \
        build-essential \
        cmake \
        git \
        ca-certificates \
        && \
    update-ca-certificates && \
    apt-get dist-clean

WORKDIR /src
RUN git clone --depth 1 https://github.com/jcurl/serialportstream.git && \
    cd serialportstream/dll/serialunix && \
    ./build.sh

#================================================
#Stage 2: Build .Net application
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
RUN xmlstarlet ed  -L -d "/Project/Target/Copy[@SourceFiles='systemd/alphonse.listener.service']" Alphonse.Listener.csproj

RUN dotnet publish -c Release -o /app/publish


#================================================
#Stage 3: Build the final image
#================================================
FROM mcr.microsoft.com/dotnet/runtime:$DOTNET_VERSION

#testing purpose only, remove later
RUN apt-get update && apt-get install -y --no-install-recommends socat && \
    apt-get dist-clean

WORKDIR /app

COPY --from=nserial-build /src/serialportstream/dll/serialunix/bin ./bin
ENV LD_LIBRARY_PATH=/app/bin/usr/local/lib:$LD_LIBRARY_PATH

COPY --from=dotnet-build /app/publish .
# force the runtime to roll forward to the latest major version of .NET if available
ENV DOTNET_ROLL_FORWARD=Major

ENTRYPOINT ["dotnet", "Alphonse.Listener.dll"]