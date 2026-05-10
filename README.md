# personapi-dotnet

Monolithic ASP.NET Core MVC application implementing full CRUD over a `persona_db` schema. Exposes both a REST API (documented with Swagger) and a web UI with Razor views. Built with .NET 10, Entity Framework Core, and MS SQL Server.

---

## Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 10.0 |
| MS SQL Server | 2019 Express or 2022 |
| SQL Server Management Studio (SSMS) | 18+ (optional, for DB setup) |

---

## 1. Database setup

This step involves installing SQL Server, creating the database, and running the DDL script to create all tables. It is a manual process with several steps — follow the detailed instructions in the lab documentation (section **Procedimiento**, steps 3–5), which covers installation, instance configuration, and schema creation.

### (Optional) Load sample data

In case you want to create values using a DML script, here is an example of sample data you can insert in the tables created in the prior step.

```sql
INSERT INTO profesion VALUES (1, 'Ingeniería de Sistemas', 'Carrera de tecnología');
INSERT INTO profesion VALUES (2, 'Medicina', 'Carrera de ciencias de la salud');

INSERT INTO persona VALUES (12345678, 'Juan', 'Pérez', 'M', 30);
INSERT INTO persona VALUES (87654321, 'Ana', 'Gómez', 'F', 25);

INSERT INTO telefono VALUES ('3001234567', 'Claro', 12345678);
INSERT INTO telefono VALUES ('3109876543', 'Movistar', 87654321);

INSERT INTO estudios VALUES (1, 12345678, '2020-06-15', 'Universidad Nacional');
INSERT INTO estudios VALUES (2, 87654321, '2022-11-30', 'Universidad de los Andes');
```

---

## 2. Configure the connection string

Open `personapi-dotnet/appsettings.json` and verify the connection string matches your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=True"
}
```

If your instance has a different name (e.g., just `localhost` without `\SQLEXPRESS`), update `Server=` accordingly.

---

## 3. Run the application

```powershell
cd personapi-dotnet
dotnet run
```

The terminal will show the port the app is listening on, for example:

```
Now listening on: http://localhost:5073
```

Open that URL in your browser.

---

## 4. Web UI (MVC views)

Navigate to any of these URLs to use the visual CRUD interface:

| Entity | URL | Description |
|--------|-----|-------------|
| Personas | `/Persona` | People — CC, name, last name, gender, age |
| Profesiones | `/Profesion` | Professions — ID, name, description |
| Teléfonos | `/Telefono` | Phone numbers — number, carrier, owner CC |
| Estudios | `/Estudio` | Studies (person ↔ profession) — with date and university |

Each page has buttons to **create**, **edit**, **view details**, and **delete** records.

### Example: create a person
1. Go to `http://localhost:{port}/Persona`
2. Click **Nueva Persona**
3. Fill in: CC (numeric ID), Nombre, Apellido, Género (M/F), Edad
4. Click **Guardar** — you are redirected back to the list

> **Important:** Insert professions and people before inserting estudios or teléfonos, since they reference those tables via foreign keys.

---

## 5. REST API + Swagger

Navigate to:

```
http://localhost:{port}/swagger
```

You will see the Swagger UI with all endpoints grouped by entity.

### Available endpoints

| Method | URL | Description |
|--------|-----|-------------|
| `GET` | `/api/PersonaApi` | List all people |
| `GET` | `/api/PersonaApi/{cc}` | Get person by CC |
| `POST` | `/api/PersonaApi` | Create person |
| `PUT` | `/api/PersonaApi/{cc}` | Update person |
| `DELETE` | `/api/PersonaApi/{cc}` | Delete person |
| `GET` | `/api/ProfesionApi` | List all professions |
| `GET` | `/api/ProfesionApi/{id}` | Get profession by ID |
| `POST` | `/api/ProfesionApi` | Create profession |
| `PUT` | `/api/ProfesionApi/{id}` | Update profession |
| `DELETE` | `/api/ProfesionApi/{id}` | Delete profession |
| `GET` | `/api/TelefonoApi` | List all phones |
| `GET` | `/api/TelefonoApi/{num}` | Get phone by number |
| `POST` | `/api/TelefonoApi` | Create phone |
| `PUT` | `/api/TelefonoApi/{num}` | Update phone |
| `DELETE` | `/api/TelefonoApi/{num}` | Delete phone |
| `GET` | `/api/EstudioApi` | List all studies |
| `GET` | `/api/EstudioApi/{idProf}/{ccPer}` | Get study by composite key |
| `POST` | `/api/EstudioApi` | Create study |
| `PUT` | `/api/EstudioApi/{idProf}/{ccPer}` | Update study |
| `DELETE` | `/api/EstudioApi/{idProf}/{ccPer}` | Delete study |

### Example: create a person via Swagger
1. Open `/swagger`
2. Expand **PersonaApi** → `POST /api/PersonaApi`
3. Click **Try it out**
4. Paste the request body:
```json
{
  "cc": 12345678,
  "nombre": "Juan",
  "apellido": "Pérez",
  "genero": "M",
  "edad": 30
}
```
5. Click **Execute** — expect `201 Created`

### Example: create a study (composite key)
```json
POST /api/EstudioApi
{
  "idProf": 1,
  "ccPer": 12345678,
  "fecha": "2020-06-15",
  "univer": "Universidad Nacional"
}
```

### Example using curl
```bash
# List all people
curl http://localhost:{port}/api/PersonaApi

# Get one person
curl http://localhost:{port}/api/PersonaApi/12345678

# Create a profession
curl -X POST http://localhost:{port}/api/ProfesionApi \
  -H "Content-Type: application/json" \
  -d '{"id": 1, "nom": "Ingeniería", "des": "Carrera técnica"}'

# Delete a phone
curl -X DELETE http://localhost:{port}/api/TelefonoApi/3001234567
```

---

## 6. Build

```powershell
dotnet build
```

---

## Project structure

```
personapi-dotnet/
├── Controllers/
│   ├── Api/                  ← REST controllers (ControllerBase)
│   │   ├── PersonaApiController.cs
│   │   ├── ProfesionApiController.cs
│   │   ├── TelefonoApiController.cs
│   │   └── EstudioApiController.cs
│   ├── PersonaController.cs  ← MVC controllers (Controller + Views)
│   ├── ProfesionController.cs
│   ├── TelefonoController.cs
│   └── EstudioController.cs
├── Interfaces/               ← Repository contracts (DAO)
├── Repositories/             ← EF Core implementations
├── Models/
│   └── Entities/             ← Scaffolded EF Core entities + DbContext
├── Views/                    ← Razor views (5 per entity)
└── appsettings.json          ← Connection string
```
