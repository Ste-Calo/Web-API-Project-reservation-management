🍽️ API RESTful - Gestione Prenotazioni Ristorante

📌 Descrizione
Questa Web API RESTful, sviluppata con **ASP.NET Core**, è stata progettata per la gestione completa delle prenotazioni in un contesto di ristorazione. 

🛠️ Tecnologie Utilizzate
* **Framework:** .NET 10 (ASP.NET Core)
* **Linguaggio:** C#
* **Database:** SQLite (scelto per facilità di portabilità e setup "zero-config" in locale)
* **ORM:** Entity Framework Core (Code-First)
* **Documentazione:** Swagger UI / OpenAPI
* **Versioning:** Git / GitHub

🚀 Funzionalità e Punti di Forza
* **Architettura Clean:** Implementazione del pattern Repository/Service per separare la logica di accesso ai dati dalla logica di business.
* **Dependency Injection:** Utilizzo nativo del container di .NET per una gestione efficiente del ciclo di vita dei servizi.
* **Operazioni CRUD:** Gestione completa delle entità (Prenotazioni, Tavoli, Clienti).
* **Data Persistence:** Gestione delle migrazioni tramite EF Core per assicurare la coerenza dello schema del database.
* **Testing semplificato:** Integrazione di Swagger UI per testare gli endpoint direttamente dal browser.

## 💻 Come avviare il progetto in locale

### Prerequisiti
* **.NET SDK 10:** scaricabile dal sito ufficiale Microsoft.
* **Database:** non serve SQL Server, il progetto usa SQLite (file locale autogenerato).

### Installazione e Avvio
* **Download:** clona il repository lanciando nel terminale `git clone https://github.com/Ste-Calo/Web-API-Project-reservation-management.git` e poi entra nella cartella digitando `cd Web-API-Project-reservation-management`.
* **Dipendenze:** lancia `dotnet restore` per scaricare automaticamente i pacchetti necessari.
* **Database:** lancia `dotnet ef database update` per creare il database e applicare le migrazioni.
* **Avvio:** lancia `dotnet run` per far partire l'applicazione.
* **Test:** apri il browser all'indirizzo `https://localhost:XXXX/swagger/index.html` per visualizzare e usare l'interfaccia API. (Le XXXX fanno riferimento al codice nel link subito dopo la voce "Now listening on:" all'interno del terminale)
