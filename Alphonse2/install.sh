#!/usr/bin/env bash

cd "$(dirname "$0")"
script_dir=$(pwd)

#script fail at first error
set -euo pipefail

###################
# Common config
echo "### Common config ###"
common_config_file="alphonse.config"
if [ ! -f "${common_config_file}" ]; then
    cat << EOT >> "${common_config_file}"
#!/usr/bin/env bash
typeset -A alphonse_config # init array

# please check or complete these values
alphonse_config=(
    # basic settings of webapi
    [webapi_listen_port]=6101
    #[webapi_swagger_force]=1   # make the swagger activated

    # admin account created when no one exist
    # please change it or remove this user as soon as possible
    # note : this user will be add again at restart when no admin accout was found - as fallback
    [webapi_admin_name]="root" 
    [webapi_admin_pass]="root"

    # listener account use by the Listener
    # you can leave these info (pass was automaticaly generated)
    [listener_user_name]="__alphonse__"
    [listener_user_pass]="$(uuidgen)"

    # info used for authentication engine (JWT)
    # pass was automaticaly generated
    # you can give sever adresse for issuer and audience - or leave empty
    [webapi_jwt_secret_key]="$(uuidgen)"
    [webapi_jwt_issuer]=""
    [webapi_jwt_audience]=""
    # [webapi_without_authorization]=1     # deactivate authorization (user auth, acces rights, ...) - please don't

    # TODO other to come
)
EOT
    echo "[STOP] The config file ${common_config_file} was created".
    echo "[!!!!] Please open it and check options - then start again $0"
    exit 0
fi
. "${common_config_file}"


###################
# WebApi install
echo "### Alphonse.WebApi ###"
cd "${script_dir}"
./src/Alphonse.WebApi/install.sh


###################
# Listener install
echo "### Alphonse.Listener ###"
cd "${script_dir}"
#./src/Alphonse.Listener/install.sh   #TODO

#TODO other to comme



###################
#adapt configuration with ${common_config_file}
cd "${script_dir}"
. install.webapi.config
#. install.listener.config

file_json_set_value () {
    # $1 : src file
    # $2 : dst file
    # $3 : action
    # $4 : path
    # $5 : value

    node > $2 <<EOT
var data = require('./$1');
switch ('$3') {
    case 'set':
        data.$4 = '$5';
        break;
    case 'del':
        data.$4 = undefined;
        break;
    default:
        break;
}
console.log(JSON.stringify(data, null, 2));
EOT
}

rm -f .tmp.*
config_set_value () {
    # $1 : action
    # $2 : path
    # $3 : value
    file_json_set_value .tmp.$$.json .tmp.$$.json.swap "$1" "$2" "$3"
    mv .tmp.$$.json.swap .tmp.$$.json
}

echo "### Configuration ###"
echo "[STEP] File ${webapi_config[config_path]}"
cp "${webapi_config[config_path]}" .tmp.$$.json
config_set_value 'set' 'Alphonse.ListenPort' "${alphonse_config[webapi_listen_port]}"
if [[ -v "alphonse_config[webapi_swagger_force]" && ${alphonse_config[webapi_swagger_force]} > 0 ]]; then
    config_set_value 'set' 'Alphonse.ForceSwagger' "true"
else
    config_set_value 'del' 'Alphonse.ForceSwagger' "_"
fi

config_set_value 'set' 'Alphonse.FallbackAdminUserName' "${alphonse_config[webapi_admin_name]}"
config_set_value 'set' 'Alphonse.FallbackAdminUserPass' "${alphonse_config[webapi_admin_pass]}"
    
config_set_value 'set' 'Alphonse.AlphonseListenerUserName' "${alphonse_config[listener_user_name]}"
config_set_value 'set' 'Alphonse.AlphonseListenerUserPass' "${alphonse_config[listener_user_pass]}"

config_set_value 'set' 'Alphonse.JwtSecretKey' "${alphonse_config[webapi_jwt_secret_key]}"
config_set_value 'set' 'Alphonse.JwtIssuer'    "${alphonse_config[webapi_jwt_issuer]}"
config_set_value 'set' 'Alphonse.JwtAudience'  "${alphonse_config[webapi_jwt_audience]}"

if [[ -v "alphonse_config[webapi_without_authorization]" && ${alphonse_config[webapi_without_authorization]} > 0 ]]; then
    config_set_value 'set' 'Alphonse.WithoutAuthorization' "true"
else
    config_set_value 'del' 'Alphonse.WithoutAuthorization' "_"
fi

sudo cp .tmp.$$.json "${webapi_config[config_path]}"
rm -f .tmp.$$.json

echo "### Configuration ###"
echo "[STEP] File ${webapi_config[config_path]}"

