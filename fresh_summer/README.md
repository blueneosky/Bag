# Application Fresh-Summer

## 1. Introduction
Cette application permet de :
- Afficher les mesures de stations météo Bluetooth.
- Lire périodiquement ces informations.
- Stocker ces informations.
- Obtenir des prévisions météo.
- Fournir une interface web accessible via téléphone et tablette.
- Enregistrer des alertes en cas d’événements météorologiques.

## 2. Structure du projet
- `backend/` : backend Flask.
- `frontend/` : frontend Angular (PWA à configurer).
- `docker-compose.yml` : configuration Docker Compose.
- `README.md` : documentation du projet.

## 3. Configurations Docker
Le projet utilise un split clair entre le mode développement et le mode production.

### Mode développement
Le backend tourne avec Flask en mode debug, avec rechargement activé.

```bash
docker compose --profile dev up --build
```

Accès local :
- Backend : http://localhost:5000
- Frontend : http://localhost:5001

### Mode production
Le backend tourne avec Gunicorn, sans debug.

```bash
docker compose --profile prod up --build -d
```

Accès local :
- Backend : http://localhost:5000
- Frontend : http://localhost:5001

> Si un serveur Nginx externe est déjà présent sur la machine, le frontend peut aussi être redirigé via `proxy_pass http://localhost:5001;`.

## 4. Arrêt des services
```bash
docker compose --profile dev down
docker compose --profile prod down
```

## 5. Notes importantes
- Le mode production ne doit pas avoir `debug=True` activé.
- Le backend est configuré via la variable d’environnement `FLASK_DEBUG`.
- Le stockage SQLite est conservé dans un volume Docker nommé `db-data` pour ne pas perdre les données entre redémarrages.
- Le port 5001 est utilisé pour le frontend afin d’éviter le conflit avec un Nginx déjà présent sur le système.