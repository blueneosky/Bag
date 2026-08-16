# Architecture de l'application météo

## 1. Objectif
Créer une application qui :
- Se connecte à 3 périphériques Bluetooth (station météo).
- Lit périodiquement les informations de ces périphériques.
- Stocke ces informations.
- Obtient périodiquement des prévisions météo.
- Fournit une interface web accessible via téléphone et tablette.
- Permet l'envoi d'alertes en cas d'événements programmer (calculé en fonction des donnée météo).

## 2. Contraintes
- Application à petite échelle.
- Déploiement sur un petit serveur local (mini PC ou Raspberry Pi).
- Volume de données modéré.
- Pas de besoin de rafraîchissement immédiat, mais des alertes sont à prévoir.
- Pas d'application native, mais une PWA est envisagée.
- Dashboard et autres fonctionnalités sont envisagées dans une deuxième phase.

## 3. Choix technologiques

### Backend
- **Langage de programmation :** Python (pour sa simplicité et sa compatibilité avec les bibliothèques Bluetooth). Les types seront le plus possible spécifié
- **Framework :** Flask (pour sa légèreté et sa simplicité de mise en œuvre).
- **Communication Bluetooth :** Utilisation de la bibliothèque `pybluez` pour la communication avec la station météo.
- **API de prévisions météo :** Utilisation de l'API OpenWeatherMap.

### Base de données
- **Type de base de données :** SQLite (pour sa simplicité, sa légèreté, et sa compatibilité avec les petits serveurs locaux).
- **Gestion des données :** Utilisation de `sqlite3` ou `SQLAlchemy` pour interagir avec la base de données.

### Frontend
- **Technologie :** PWA (Progressive Web App) avec Angular (pour sa robustesse et sa compatibilité avec les appareils mobiles).
- **Communication avec le backend :** Utilisation d'appels HTTP (GET, POST, etc.) via `HttpClient`.

### Conteneurisation
- **Outil :** Docker (pour le déploiement sur un petit serveur local).
- **Déploiement :** Utilisation de Docker Compose pour gérer l'ensemble de l'application (backend, frontend, base de données).

## 4. Architecture globale

### 1. Backend
- Gère la communication Bluetooth avec les stations météo.
- Lit périodiquement les données.
- Stocke les données dans la base de données SQLite.
- Communique avec l'API de prévisions météo (ex. OpenWeatherMap).
- Envoie des alertes en cas d'événements météorologiques.

### 2. Frontend (PWA avec Angular)
- Affiche les données météo et les prévisions.
- Permet la configuration de l'application (ex. seuils d'alerte).
- Est accessible via téléphone et tablette.
- Peut être installée comme une application native (PWA).

### 3. Base de données (SQLite)
- Stocke les données lues périodiquement par le backend.
- Peut être optimisée via l'indexation et le partitionnement.

### 4. Conteneurisation (Docker)
- Permet le déploiement de l'application sur un petit serveur local.
- Facilite la gestion des dépendances et la mise à jour de l'application.

## 5. Étapes de développement
1. Créer le backend avec Flask et Python avec sont environnement containérisé
2. Implémenter la communication Bluetooth avec `pybluez`.
3. Configurer la base de données SQLite.
4. Créer une interface web avec Angular (PWA).
5. Intégrer l'API de prévisions météo (ex. OpenWeatherMap).
6. Ajouter des alertes en cas d'événements météorologiques.
7. Déployer l'application via Docker.
8. Tester l'application sur un petit serveur local (ex. Raspberry Pi).
9. Ajouter des fonctionnalités supplémentaires (ex. dashboard) dans une deuxième phase.