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