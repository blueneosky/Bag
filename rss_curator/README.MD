Requirement
===========
$> python3 -m pip install fastapi uvicorn[standard]
$> apt install python3-lxml


INSTALL NOTES
=============
Create user :
```bash
$> rss_user="rss-curator"
```

```bash
$> sudo mkdir /usr/local/lib/rss-curator
$> sudo mkdir /usr/local/lib/rss-curator/feeders
$> sudo cp -R *.py static   /usr/local/lib/rss-curator/.
$> sudo cp -R *feeders/*.py /usr/local/lib/rss-curator/feeders/.
$> sudo chown -R root:www-data /usr/local/lib/rss-curator/
$> sudo chmod u+x /usr/local/lib/rss-curator/main.py
```

```bash
$> sudo mkdir /var/lib/rss-curator
$> sudo cp channels.json /var/lib/rss-curator/.
$> sudo chown -R www-data:www-data /var/lib/rss-curator
```

test
```bash
$> cd /usr/local/lib/rss-curator
$> sudo -u www-data ./main.py 
```

DEBUG NOTES
=============
$> uvicorn main:app --reload

