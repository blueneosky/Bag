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

Then build/publish, deploy/install, configure
```bash
declare alphonse_user="alphonse"                                            && \
declare alphonse_webapp_path="/usr/local/lib/alphonse/Alphonse.WebApi"      && \
declare alphonse_data_path="/var/lib/alphonse"                              && \
declare alphonse_config_path="/etc/alphonse/webapi.appsettings.json"        && \
declare alphonse_service_name="alphonse.webapi.kestrel.service"             && \
declare alphonse_service_path="${alphonse_webapp_path}/systemd/${alphonse_service_name}" && \
declare alphonse_service_log_path="/var/log/Alphonse.WebApi.log"            && \
declare alphonse_publish_log="/tmp/Alphonse.WebApp.dotnet_publish.log"      && \

echo "[Step] dotnet publish ..."                                            && \
dotnet publish --configuration Release | tee ${alphonse_publish_log}        && \

declare alphonse_publish_path="$(grep -e '->.*publish' ${alphonse_publish_log} | \sed -e 's#^.* -> /#/#' -e 's#/$##' )"     && \

echo "[Step] stop existing service ..."                                         && \
sudo systemctl stop alphonse.webapi.kestrel.service || echo "Service not found" && \

echo "[Step] deploy to ${alphonse_webapp_path} under user ${alphonse_user}"     && \
sudo mkdir -p ${alphonse_webapp_path}                                           && \
sudo rsync -cprth --delete ${alphonse_publish_path}/ ${alphonse_webapp_path}    && \
sudo chown -R ${alphonse_user}:${alphonse_user} ${alphonse_webapp_path}         && \

echo "[Step] setup config file at ${alphonse_config_path}"                      && \
sudo mkdir -p $(dirname alphonse_config_path)                                   && \
sudo ln -sf ${alphonse_webapp_path}/appsettings.json ${alphonse_config_path}    && \

echo "[Step] setup data path ${alphonse_data_path}"                             && \
sudo mkdir -p ${alphonse_data_path}                                             && \
sudo chown -R ${alphonse_user}:${alphonse_user} ${alphonse_data_path}           && \

echo "[Step] setup log file ${alphonse_service_log_path}"                       && \
sudo touch ${alphonse_service_log_path}                                         && \
sudo chown -R root:${alphonse_user} ${alphonse_service_log_path}                && \
sudo chmod g+rw ${alphonse_service_log_path}                                    && \

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