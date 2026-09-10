## NexaWorks

Projet de conception et de mise en place d'une base de données de traçabilité pour NexaWorks, permettant de suivre les tickets remontés sur ses différents produits, versions et systèmes d'exploitation.

## Modèle entité-association

```mermaid
erDiagram
    PRODUCT ||--o{ VERSION_PRODUCT : has
    SYSTEM_OS ||--o{ VERSION_OS : has
    VERSION_PRODUCT ||--o{ VERSION_OS : has
    VERSION_OS ||--o{ TICKET : has
    STATUS_TICKET ||--o{ TICKET : has
    TICKET ||--o| RESOLUTION : has

    PRODUCT {
        int Id PK
        string NameProduct
        bool IsDeleted
    }

    VERSION_PRODUCT {
        int Id PK
        string RefVersion
        int ProductId FK
        bool IsDeleted
    }

    SYSTEM_OS {
        int Id PK
        string NameSystem
        bool IsDeleted
    }

    VERSION_OS {
        int Id PK
        int VersionProductId FK
        int SystemOsId FK
        bool IsDeleted
    }

    STATUS_TICKET {
        int Id PK
        string Title
    }

    TICKET {
        int Id PK
        date CreationDate
        string Description
        int StatusTicketId FK
        int VersionOsId FK
        bool IsDeleted
    }

    RESOLUTION {
        int Id PK
        date ResolutionDate
        string Description
        int TicketId FK
    }
```

## Installation
Cloner le dépôt
Ouvrir la solution dans Visual Studio
Dans la Console du Gestionnaire de package, appliquer les migrations :
Update-Database

La base NexaWorksDb est créée et automatiquement peuplée avec un jeu de données de test (25 tickets répartis sur les 4 produits).

## Documentation des requêtes

Le détail des 20 requêtes demandées (paramètres, résultats attendus et obtenus) ainsi que leurs équivalents en procédures stockées SQL sont fournis dans un document de documentation séparé, transmis en complément de ce dépôt.
