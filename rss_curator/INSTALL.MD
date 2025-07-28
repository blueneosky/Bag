INSTALL
=======

**Tools needed**
```bash
sudo apt update
sudo apt install -y docker
```

**Note** :\
Data will be stored in /var/opt/rss-curator \
Check 'NetworkInterfaceBlacklist=...,veth' if connman is used on the system

**Build & install**

Build, test, then start as daemon
```bash
docker compose -f compose.yaml -f compose.prod.yaml build
docker compose -f compose.yaml -f compose.prod.yaml up
docker compose -f compose.yaml -f compose.prod.yaml up -d
```

Or in one line
```bash
docker compose -f compose.yaml -f compose.prod.yaml up --build -d
```
