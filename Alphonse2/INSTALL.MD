INSTALLING
==========

We give some predfined directories.
Do what you want - more a  remainder for me and my config

We suggest:
- `/usr/local/lib/alpsonse` as base path for all products 
- `/var/lib/alphonse` as path for data

Alphonse.WebApi
---------------

We suggest to use a dedicated user `alphonse` for the WebApi
```bash
sudo adduser --disabled-password  --quiet --system \
        --home /var/lib/alphonse --gecos "Alphonse Phone Pick-up" \
        --group alphonse

```

Got to `src/Alphonse.WebApi`
```bash
cd src/Alphonse.WebApi
```

Check/modify configuration file `webapi.config`

Then build/publish, deploy/install, configure
```bash
./install.sh
```

Finaly, start de daemon :
```bash
sudo systemctl start alphonse.webapi.kestrel.service
#get the status
sudo systemctl status  alphonse.webapi.kestrel.service
```

Alphonse.Listener
---------------

Got to `src/Alphonse.Listener`
```bash
cd src/Alphonse.Listener
```

Then build/publish, deploy/install, configure
```bash
declare alphonse_listener_path="/usr/local/lib/alphonse/Alphonse.Listener"  && \
declare alphonse_config_path="/etc/alphonse/listener.appsettings.json"      && \
declare alphonse_service_name="alphonse.listener.service"                   && \
declare alphonse_service_path="${alphonse_listener_path}/systemd/${alphonse_service_name}" && \
declare alphonse_publish_log="/tmp/Alphonse.Listener.dotnet_publish.log"    && \

echo "[Step] root right ..."                                                && \
sudo echo "acquired"                                                        && \

echo "[Step] dotnet publish ..."                                            && \
dotnet publish --configuration Release | tee ${alphonse_publish_log}        && \

declare alphonse_publish_path="$(grep -e '->.*publish' ${alphonse_publish_log} | \sed -e 's#^.* -> /#/#' -e 's#/$##' )"     && \

echo "[Step] stop existing service ..."                                         && \
sudo systemctl stop ${alphonse_service_name} || echo "Service not found"        && \

echo "[Step] deploy to ${alphonse_listener_path} under user root"               && \
sudo mkdir -p ${alphonse_listener_path}                                         && \
sudo rsync -cprth --delete ${alphonse_publish_path}/ ${alphonse_listener_path}  && \

echo "[Step] setup config file at ${alphonse_config_path}"                      && \
sudo mkdir -p $(dirname {alphonse_config_path})                                 && \
sudo ln -sf ${alphonse_listener_path}/appsettings.json ${alphonse_config_path}  && \

echo "[Step] setup service (systemd)"                                           && \
sudo ln -sf ${alphonse_service_path} /etc/systemd/system/                       && \
sudo systemctl enable ${alphonse_service_name}                                  && \
sudo systemctl daemon-reload
```

Please check and update these file if you change path:
- `${alphonse_config_path}`
- `${alphonse_service_path}`

Finaly, start de daemon :
```bash
sudo systemctl start ${alphonse_service_name}
#get the status
sudo systemctl status  ${alphonse_service_name}
```