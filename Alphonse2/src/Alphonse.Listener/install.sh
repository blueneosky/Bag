#!/usr/bin/env bash

#unsure the current cwd
cd "$(dirname "$0")"

#load listener config
. install.config

#script fail at first error
set -euo pipefail

echo "[Step] root right ..."
sudo echo "acquired"

echo "[Step] dotnet publish ..."
dotnet publish --configuration Release | tee ${listener_config[publish_log]}
declare alphonse_publish_path="$(grep -e '->.*publish' ${listener_config[publish_log]} | sed -e 's#^.* -> /#/#' -e 's#/$##')"

echo "[Step] stop existing service ..."
sudo systemctl stop ${listener_config[service_name]} || echo "Service not found"

echo "[Step] deploy to ${listener_config[bin_path]} under user root"
sudo mkdir -p ${listener_config[bin_path]}
sudo rsync -cprth --delete ${alphonse_publish_path}/ ${listener_config[bin_path]}

echo "[Step] setup config file at ${listener_config[config_path]}"
sudo mkdir -p $(dirname {listener_config[config_path]})
sudo ln -sf ${listener_config[bin_path]}/appsettings.json ${listener_config[config_path]}

echo "[Step] setup service (systemd)"
sudo ln -sf ${listener_config[service_path]} /etc/systemd/system/
sudo systemctl enable ${listener_config[service_name]}
sudo systemctl daemon-reload

echo "[Done]"
