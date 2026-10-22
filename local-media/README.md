# Application Fresh-Summer

## 1. Introduction
Cette application permet de :
- Afficher les fichiers dans un page web situé dans le dossier de référence 'data'
- Permettre la lecture des vidéos dans la page
- Le backedn héberge aussi le front

Les métadonnées de chaque fichier affiché sont conservées dans `.local-media/.data.json`,
dans son dossier parent. Une entrée contient une note de `0` à `5` et une liste de tags,
initialisées respectivement à `0` et `[]`. La note est affichée dans la vue dossier.
Dans le player, la note et les tags peuvent également être consultés et modifiés.

L'API des métadonnées du player expose les opérations suivantes, où `<media_id>` est
l'identifiant renvoyé par `GET /api/files` :
- `GET /api/files/<media_id>/metadata` : lire la note et les tags.
- `PUT /api/files/<media_id>/metadata/rating` avec `{"rating": 0}` à `{"rating": 5}` :
  modifier la note.
- `POST /api/files/<media_id>/metadata/tags` avec `{"tag": "nom"}` : ajouter un tag.
- `DELETE /api/files/<media_id>/metadata/tags` avec `{"tag": "nom"}` : supprimer un tag.

`GET /api/files` accepte également le paramètre optionnel `q` pour rechercher dans les
tags, par exemple `/api/files?path=films&q=ete%20hiver`. La recherche est limitée au
dossier demandé et correspond si un tag contient au moins un mot recherché, sans tenir
compte des accents ni de la casse. Les fichiers sont classés selon le nombre de mots
distincts trouvés ; les dossiers et les fichiers sans correspondance sont masqués
pendant la recherche.

## 2. Structure du projet
- `backend/` : backend Flask.
- `docker-compose.yml` : configuration Docker Compose.
- `README.md` : documentation du projet.

## 3. Configurations Docker
La configuration commune est dans `docker-compose.yml`. Les configurations de
développement et de production sont séparées dans `docker-compose.dev.yml` et
`docker-compose.prod.yml`. La variable `COMPOSE_FILE`, définie dans le fichier
de valeurs du mode ou de l'instance, indique quels fichiers fusionner.

### Mode développement
Le backend tourne avec Flask en mode debug, avec rechargement activé. Le code est
monté depuis `backend/`.

Pour compiler le code avant de lancer ou valider l'application, utiliser :

```bash
docker compose --env-file .env.dev build
```

Copiez le modèle une fois :

```bash
cp .env.dev.example .env.dev
```

Puis lancez le mode dev :

```bash
docker compose --env-file .env.dev up --build
```

Accès local :
- Backend : http://localhost:4200

### Mode production
Le backend tourne avec Gunicorn, sans debug. Chaque instance doit utiliser un
nom de projet Compose unique, un port hôte dédié et son propre chemin de médias.

Copiez le modèle de valeurs pour créer le fichier de l'instance, puis adaptez
`COMPOSE_PROJECT_NAME`, `HOST_PORT` et `MEDIA_PATH` si nécessaire :

```bash
cp .env.prod.example .env.prod.media-a
```

Lancez l'instance :

```bash
docker compose --env-file .env.prod.media-a up --build -d
```

Accès local :
- Backend : http://localhost:5010

Pour déployer une deuxième instance, copiez le modèle dans un nouveau fichier,
puis définissez un nom de projet, un port hôte disponible et un chemin média
différents. Le chemin média indiqué dans `MEDIA_PATH` est monté dans `/srv` du
conteneur en lecture-écriture afin que l'application puisse enregistrer les
miniatures dans le sous-dossier `.local-media` de chaque dossier média. Les
fichiers locaux `.env.dev` et `.env.prod.*` sont
ignorés par Git, à l'exception des modèles `.env.dev.example` et
`.env.prod.example`.

Si un serveur Nginx externe est déjà présent sur la machine, configurez son
`proxy_pass` vers le port hôte correspondant à l'instance (par exemple
`http://127.0.0.1:5010` pour le modèle fourni). Voir `nginx-snippet.txt`.

## 4. Arrêt des services
```bash
docker compose --env-file .env.dev down
docker compose --env-file .env.prod.media-a down
```

## 5. Notes importantes
- Le mode production ne doit pas avoir `debug=True` activé.
- Le backend est configuré via la variable d’environnement `FLASK_DEBUG`.
