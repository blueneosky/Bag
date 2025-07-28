Requirement
===========
$> docker


TODO
=====
LOGIN page
split channels Config from Cache & Modif
better class for config and so
in html config -> show by last synchronized, or something like that (need to add some timestamp)


INSTALL NOTES
=============
see INSTALL.MD for instructions


DEV/DEBUG NOTES
=============
When change in docker config (yml) or python requirements
```bash
docker compose up --build --remove-orphans 
```

Or start
```bash
docker compose up
```
