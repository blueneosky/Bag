#!/usr/bin/env bash

#unsure the current cwd
cd "$(dirname "$0")"

#load webapi config
. install.config

#script fail at first error
set -euo pipefail

echo "[Step] root right ..."
sudo echo "acquired"

echo "[Step] dotnet publish ..."
dotnet publish --configuration Release | tee ${webapi_config[publish_log]}
declare alphonse_publish_path="$(grep -e '->.*publish' ${webapi_config[publish_log]} | sed -e 's#^.* -> /#/#' -e 's#/$##')"

echo "[Step] stop existing service ..."
sudo systemctl stop ${webapi_config[service_name]} || echo "Service not found"

echo "[Step] deploy to ${webapi_config[bin_path]} under user ${webapi_config[bin_user]}"
sudo mkdir -p ${webapi_config[bin_path]}
sudo rsync -cprth --delete ${alphonse_publish_path}/ ${webapi_config[bin_path]}
sudo chown -R ${webapi_config[bin_user]}:${webapi_config[bin_user]} ${webapi_config[bin_path]}

echo "[Step] setup config file at ${webapi_config[config_path]}"
sudo mkdir -p $(dirname ${webapi_config[config_path]})
sudo ln -sf ${webapi_config[bin_path]}/appsettings.json ${webapi_config[config_path]}

echo "[Step] setup data path ${webapi_config[data_path]}"
sudo mkdir -p ${webapi_config[data_path]}
sudo chown -R ${webapi_config[bin_user]}:${webapi_config[bin_user]} ${webapi_config[data_path]}

echo "[Step] setup log file ${webapi_config[service_log_path]}"
sudo touch ${webapi_config[service_log_path]}
sudo chown -R root:${webapi_config[bin_user]} ${webapi_config[service_log_path]}
sudo chmod g+rw ${webapi_config[service_log_path]}

echo "[Step] setup service (systemd)"
sudo ln -sf ${webapi_config[service_path]} /etc/systemd/system/
sudo systemctl enable ${webapi_config[service_name]}
sudo systemctl daemon-reload

echo "[Done]"
