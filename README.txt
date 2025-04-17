#Cloner le projet

git clone https://github.com/AhmedBechirMejri/Api_darblockchain
cd Api_darblockchain

#Initialisation de la base de données

Remarques : Les commandes de cette etape doivent être lancées depuis le Package Manager Console
*Add-Migration CreateBD -Project Data -StartupProject Api
*Update-Database

#Exécuter avec Docker

*Construire l'image Docker

docker build -t api .

docker-compose up -d

*Accéder à l'API

http://localhost:8080/

Projet réalisé par : Ahmed Bechir Mejri

*Exemple de route : http://localhost:8080/api/leaverequests

#Exécuter sans Docker (localement)

*Exécuter le projet API en mode HTTP

*L'API sera disponible par défaut sur : http://localhost:5299

#Architecture du projet & Design Patterns
*Architecture en couches
Le projet est structuré en 4 projets distincts, respectant une architecture propre et maintenable :

*Entity
Contient les classes d’entités représentant les modèles de données de la base.
Intégration des DTOs pour séparer les modèles d’entrée/sortie de ceux de la base.

*Data
Gère l’accès aux données avec Entity Framework Core.

*Services
Contient la logique métier du projet.
Utilisation de ILeaveRequestService et LeaveRequestService pour structurer les traitements.


*Api
Contient les contrôleurs REST (ex : LeaveRequestsController) qui exposent les fonctionnalités via des endpoints. Cette couche consomme les services de la couche "Services".

J'ai utilisé :
Services avec Injection de Dépendance
Le service LeaveRequestService est injecté dans le contrôleur via l’interface ILeaveRequestService.

Cela permet une logique métier centralisée et testable.

Utilisation d’interfaces, conforme à une approche basée sur les principes SOLID.

