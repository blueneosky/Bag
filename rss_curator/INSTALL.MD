INSTALL
=======

**Tolls needed**
```bash
sudo apt update
sudo apt install -y python3 rsync python3.11-venv
```

**Create user and dirs**
```bash
user="rss-curator"
user_group="$user:$user"
data_dir="/var/lib/rss-curator"
bin_dir="/usr/local/lib/rss-curator"
venv_dir_name="venv"
venv_dir="$bin_dir/$venv_dir_name"
sudo adduser --disabled-password --quiet --system --gecos "rss-curator user" --home $data_dir --group $user
sudo mkdir $bin_dir
sudo chown -R $user_group $bin_dir
```

**Copy Config & Binaries file**
```bash
sudo rsync -cprth --chown=$user_group src/channels.json $data_dir/
sudo rsync -cprth --delete --chown=$user_group --exclude={'*.json','*.service',${venv_dir_name}} src/ $bin_dir/
```

**Setup python venv & install deps**
```bash
sudo -u $user python3 -m venv $venv_dir
sudo -u $user $venv_dir/bin/pip install uvicorn[standard] fastapi[all] lxml requests
```

**[optionnal] Test**
```bash
cd $bin_dir; \
sudo -u $user $venv_dir/bin/python3 main.py --channels /var/lib/rss-curator/channels.json; \
cd -
```

**[optionnal] Setup as service (systemd)**
Create and set service file
```bash
sudo systemctl stop rss-curator.service 2> /dev/null || echo rss-curator.service not yet installed
cat << EOT | sudo -u $user tee $bin_dir/rss-curator.service > /dev/null
[Unit]
Description=RSS Curator service
Wants=network-online.target
After=network-online.target nss-lookup.target

[Service]
User=${user}
WorkingDirectory=${bin_dir}
ExecStart=$venv_dir/bin/python3 main.py \
    --host 127.0.0.1 \
    --port 6110 \
    --channels ${data_dir}/channels.json

[Install]
WantedBy=multi-user.target
EOT
sudo ln -sf $bin_dir/rss-curator.service /etc/systemd/system/rss-curator.service
sudo systemctl daemon-reload
```

Start and check status of the service
```bash
sudo systemctl start rss-curator.service
sudo journalctl -f -u rss-curator.service
```

If everything is ok, activate auto start at boot
```bash
sudo systemctl enable rss-curator.service
```