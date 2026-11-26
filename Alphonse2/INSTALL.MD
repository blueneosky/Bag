# INSTALLING (with Podman)

## Déployement

### Setup access to modem device
From your podman account
```bash
sudo usermod -aG dialout $USER
```
Then logout and login again !

### Identify your modem device
Should by an `/dev/ttyUSB0` or `/dev/ttyACM0` or something like that
But prefer using id ref like `/dev/serial/by-id/usb-xxx_Modem_[...]`.

Test the link
```bash
#preparation - build image
podman build --jobs 3 --tag localhost/alphonse-listener:latest --file src/Alphonse.Listener/Dockerfile

#find your device
ls -la /dev/serial/by-id

#adapt '--device' argument with modem device path
podman run --rm -it \
  --device /dev/serial/by-id/usb-Conexant_USB_Modem_[...] \
  --group-add keep-groups \
  localhost/alphonse-listener:latest
```

If success (`podman run` will show the usage of the device), change `device` of `alphonse-listener` section in `compose.yaml`.
You can use 'by-id' name for the left part and the real device name ('ttyXXX') for the right part. Don't remove `:rwm` at the end.


### Create conf & secrets

```bash
mkdir -p ~/.config/alphonse
chmod 700 ~/.config/alphonse
touch ~/.config/alphonse/Alphonse.db
#or copy your db backup
chmod 600 ~/.config/alphonse/*

mkdir -p ~/.local/share/alphonse
tr -d '\n' < /proc/sys/kernel/random/uuid > ~/.local/share/alphonse/listener-user-pass.txt
tr -d '\n' < /proc/sys/kernel/random/uuid > ~/.local/share/alphonse/webapi-jwt-secret-key.txt
chmod 700 ~/.local/share/alphonse
chmod 600 ~/.local/share/alphonse/*
```


## Dev

podman build --tag alphonse-listener:dev --file src/Alphonse.Listener/Dockerfile

podman build --tag alphonse-webapi:dev --file src/Alphonse.WebApi/Dockerfile


# INSTALLING (on host - old way, not maintened)

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
-----------------

Got to `src/Alphonse.Listener`
```bash
cd src/Alphonse.Listener
```

Then build/publish, deploy/install, configure
```bash
./install.sh
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