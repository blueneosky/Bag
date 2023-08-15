#!/usr/bin/env bash

#unsure the current cwd
cd "$(dirname "$0")"

#load listener config
. install.config

#script fail at first error
set -euo pipefail

echo "[Step] root right ..."
sudo echo "acquired"

echo "[Step] modules install ..."
npm install

echo "[Step] ng build ..."
npm run ng -- build --base-href ${front_config[base_href]}

user_group="${front_config[webrsx_user]}:${front_config[webrsx_user]}"
echo "[Step] deploy to ${front_config[bin_path]} under user $user_group"
sudo mkdir -p ${front_config[bin_path]}
sudo rsync -cprth --delete --chown=$user_group dist/ ${front_config[bin_path]}
echo
echo "/!\\ don't forget to reference this site with your favorite web engine"
echo

echo "[Done]"
