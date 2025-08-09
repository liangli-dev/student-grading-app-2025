Projet ASP.NET Core pour la gestion des élèves, professeurs, matières et notes.

## ✅ Fonctionnalités principales

### Backend (ASP.NET Core)
- CRUD Élève, Note
- Lister les élèves par classe
- Lister les notes d’un élève - dans la fenêtre de Dialog "Modifier l'élève"
- Lister les professeurs par matière
- Données de démonstration pré-remplies

### Frontend (Vue 3)
- Interface administrateur responsive
- Visualisation des notes avec graphiques
- Switcher des classes
- Tableaux interactifs (tri/filtre/pagination)
- Authentification (optionnelle)

## 🚀 Stack Technique

- **Backend**:
- ASP.NET Core 8
- Entity Framework Core
- SQLite
- REST API (testable avec Postman ou Swagger)

- **Frontend**:
  - Vue 3 (Composition API)
  - Vite (outil de build)

## ▶️ Démarrer le projet

### 📦 Prérequis
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [Node.js 22+](https://nodejs.org/)
- [VS Code (optionnel)](https://code.visualstudio.com/)

### 🏁 Étapes

1. **Cloner le projet** et ouvrir le dossier dans le terminal.
2. **Backend - Restaurer les dépendances et lancer l'application** :
   ```
   dotnet restore
   dotnet run
   ```
3. **Frontend -** :
   ```
   cd ClientApp
   npm install
   npm run dev
   ```

4. **Tester les endpoints** :
   - Ouvrir Swagger : http://localhost:5166/swagger
   - Ou utiliser Postman 
   - Frontend : http://localhost:5173

✅ La base de données `gradesystem.db` sera générée automatiquement dans le dossier racine du projet.

---

## 🧪 Données de démonstration pré-remplies

- 3 professeurs (Claire Durand, Marc Dubois...)
- 3 classe : Cinquième (professeur principal : Claire...)
- 3 matières : Mathématiques, Histoire, Physique
- 3 élèves : Luc Martin etc..
- 6 notes : entre 10 à 20

---

## 📂 Endpoints API disponibles

| Méthode | URL                              | Description                          |
|--------|-----------------------------------|--------------------------------------|
| GET    | `/api/class/{id}`                 | Lister élèves d’une classe           |
| GET    | `/api/teachers`                   | Lister les professeurs d’un cours    |
| GET    | `/api/matieres`                   | Lister les matières                  |
| GET    | `/api/classes `                   | Lister les classes                   |

---

## 💡 Remarques

- Le projet utilise `EnsureCreated()` pour créer la base automatiquement au premier lancement.
- Il n’est pas nécessaire d’exécuter `dotnet ef database update`, sauf si vous souhaitez modifier la structure de la base.
