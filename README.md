# CV Pentru Profesori

A web application where teachers and academics build a CV online, edit it section by section, and export it to PDF. Full-stack project with an ASP.NET Core API and a Vue 3 single-page frontend.

## Features

- **Online CV editor:** basic info, academic profiles (ORCID, Google Scholar, Web of Science, ResearchGate), and sections for work experience, education, skills, languages, publications, certificates, projects, awards, interests, references and volunteering.
- **Bilingual interface:** switch the editor between English and Romanian.
- **Import publications from Crossref:** pull an author's publications automatically by name. Results are filtered to the actual person (matching first and last name, ignoring diacritics) so publications by other people with the same surname are left out, and titles are cleaned of HTML markup.
- **Custom sections:** an admin can define extra sections and fields beyond the built-in ones.
- **Admin panel:** control which sections are active and in what order.
- **Save and reload:** log in to store a CV and open it again later.
- **Export to PDF:** generate the finished CV as a PDF in the browser.

## Tech stack

**Backend**
- ASP.NET Core Web API (.NET 6, C#)
- Entity Framework Core 6 with SQL Server (LocalDB)
- Swagger / OpenAPI for the API docs
- Crossref REST API for publication import

**Frontend**
- Vue 3 with Vue Router
- Vite
- axios for API calls
- jsPDF for PDF export

## Project structure

```
CVPentruProfesori/
├── Backend/     ASP.NET Core API (controllers, EF Core models, migrations)
└── Frontend/    Vue 3 single-page app (views, components, router)
```

## Running it locally

**Prerequisites:** .NET 6 SDK, Node.js, and SQL Server LocalDB.

### Backend

```bash
cd Backend/Backend
dotnet run
```

The API starts on `https://localhost:7234` (Swagger UI at `/swagger`). On first run it creates the database and seeds the default CV sections automatically.

### Frontend

```bash
cd Frontend
npm install
npm run dev
```

The app starts on `http://localhost:5173`. The backend already allows this origin via CORS.

## Notes

- The database uses SQL Server LocalDB with Windows authentication, so no credentials are stored in the project.
- Publication import depends on the public Crossref API and works best when the professor's name is filled in.
