# 📦 Magazyn API (MVP)

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

To repozytorium zawiera **minimalną wersję produkcyjną (MVP)** API służącego do zarządzania dokumentami przyjęcia towarów w magazynie, kontrahentami oraz pozycjami towarowymi. Zostało ono zaprojektowane w architekturze warstwowej z podziałem na: `Domain`, `Application`, `Infrastructure`.

## 🏗 Technologie

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SQLite)
- Clean Architecture (Application, Domain, Infrastructure)
- Dependency Injection (MS DI)

## 🔗 Dokumentacja i testowanie API

Kompletny interfejs REST API znajduje się i jest dostępny do testowania na:

🌐 [https://qubity.azurewebsites.net/swagger](https://qubity.azurewebsites.net/swagger)

---

## 📌 Główne funkcjonalności

### 🔹 Towary

- `GET /api/towary` – lista towarów
- `POST /api/towary` – dodaj nowy towar
- `PUT /api/towary/{id}` – aktualizuj
- `DELETE /api/towary/{id}` – usuń

### 🔹 Kontrahenci

- `GET /api/kontrahenci`
- `POST /api/kontrahenci`
- `PUT /api/kontrahenci/{id}`
- `DELETE /api/kontrahenci/{id}`

### 🔹 Dokumenty przyjęcia

- `GET /api/dokumenty`
- `GET /api/dokumenty/{id}`
- `POST /api/dokumenty` – tworzy dokument z jedną pozycją (towarem)
- `PUT /api/dokumenty/{id}` – edycja nagłówka
- `DELETE /api/dokumenty/{id}`

### 🔹 Pozycje dokumentu

- `GET /api/pozycje`
- `POST /api/pozycje` – dodaj pozycję do istniejącego dokumentu
- `PUT /api/pozycje/{id}` – zmień ilość
- `DELETE /api/pozycje/{id}`

---

## 🧪 Testowanie lokalne (opcjonalnie)

1. Wymagania:

   - .NET 8 SDK
   - Visual Studio / Rider / VS Code

2. Uruchomienie:

```bash
dotnet restore
dotnet build
dotnet run --project Your.Api.Project
```

3. Otwórz `https://localhost:<PORT>/swagger` w przeglądarce

---

## 📁 Struktura folderów

```
├── Domain/
│   └── Entities/           // Encje: Towar, Kontrahent, DokumentPrzyjecia, PozycjaDokumentu
├── Application/
│   ├── DTOs/               // DTO do komunikacji z API
│   ├── Interfaces/         // Interfejsy serwisów
│   └── Services/           // Logika aplikacyjna
├── Infrastructure/
│   ├── Repository/         // Repozytoria EF
│   ├── Services/           // Implementacje interfejsów aplikacji
│   └── ServiceExtensions/  // Rejestracja DI
```

---

## ⚠️ Uwaga

> Projekt jest w wersji **MVP**, co oznacza, że:
>
> - logika domenowa jest uproszczona
> - obsługa wyjątków i walidacja są minimalne
> - interfejs może ulec zmianie

---

This project is licensed under the GNU General Public License v3.0 – see the LICENSE file for details.
