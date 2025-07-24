# Projet Web Service avec .NET

Bienvenue dans ce projet de web service développé avec .NET. Ce fichier README vous guidera à travers l'installation, l'utilisation et la contribution à ce projet.

## Description

Ce projet est un web service construit avec le framework .NET pour les backend de tout les applications de gestion de la faculté des sciences

## Prérequis

- .NET 8.0 SDK ou version supérieure ([Télécharger ici](https://dotnet.microsoft.com/download))
- Un éditeur de code (Visual Studio, Visual Studio Code, etc.)

## Installation

1. Clonez le dépôt :
   ```bash
   git clone https://github.com/tahinaniaina01/UnivManager.git
   ```
2. Accédez au dossier du projet :
   ```bash
   cd UnivManager
   ```
3. Créé un fichier appsettings.json et copier le contenu ci-dessous

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=<localhost | ip>;Port=5432;Database=univdb;Username=<postgresql user>;Password=<postgresql user password>"
  }
}
```

4. Restaurez les dépendances :
   ```bash
   dotnet restore
   ```

## Lancement du service

Pour lancer le web service en local, exécutez la commande suivante :

```bash
dotnet watch run
```
