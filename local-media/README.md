# Application Fresh-Summer

## 1. Introduction
Cette application permet de :
- Afficher les fichiers dans un page web situé dans le dossier de référence 'data'
- Permettre la lecture des vidéos dans la page
- Le backedn héberge aussi le front

## 2. Structure du projet
- `backend/` : backend Flask.
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
- Backend : http://localhost:5010

### Mode production
Le backend tourne avec Gunicorn, sans debug.

```bash
docker compose --profile prod up --build -d
```

Accès local :
- Backend : http://localhost:5010

> Si un serveur Nginx externe est déjà présent sur la machine, le frontend peut aussi être redirigé via `proxy_pass http://localhost:5010;` (go check nginx-snippet.txt)

## 4. Arrêt des services
```bash
docker compose --profile dev down
docker compose --profile prod down
```

## 5. Notes importantes
- Le mode production ne doit pas avoir `debug=True` activé.
- Le backend est configuré via la variable d’environnement `FLASK_DEBUG`.
