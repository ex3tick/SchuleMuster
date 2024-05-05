
# WebApp - Benutzeranmeldung und Registrierung mit Cookies und SQL

## Beschreibung
Diese WebApp ist ein ASP.NET Core 8.0 Projekt, das sich auf das Arbeiten mit Cookies, SQL-Abfragen und dem MVC-Pattern konzentriert. Es zeigt, wie Benutzerdaten über eine SQL-Datenbank verwaltet und Benutzersitzungen mithilfe von Cookies in einer Webanwendung gehandhabt werden. Das Projekt dient dazu, praktische Erfahrungen in diesen Bereichen zu sammeln.

## Voraussetzungen
Um dieses Projekt zu verwenden, benötigen Sie:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) – Das Software Development Kit für die Entwicklung und das Ausführen von .NET Anwendungen.
- Ein geeigneter Code-Editor, wie [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) oder [VS Code](https://code.visualstudio.com/).

## Projektstruktur
Das Projekt besteht aus mehreren Hauptkomponenten, die die MVC-Architektur widerspiegeln:

- **Models**: Definieren die Datenstrukturen und sind verantwortlich für die Datenübertragung zwischen der Datenbank und der Anwendung.
- **Views**: Stellen die Benutzeroberfläche bereit und zeigen Daten an, die vom Controller geliefert werden.
- **Controllers**: Verarbeiten Benutzereingaben, interagieren mit den Models und bestimmen die angezeigten Views.
- **Data Access Layer**: Verwaltet die direkten SQL-Abfragen und Datenbankinteraktionen.

## Hauptfunktionen
- **Benutzerregistrierung und -anmeldung**: Nutzer können ein Konto erstellen und sich anmelden. Dabei werden ihre Sitzungsdaten mithilfe von Cookies gespeichert.
- **Session Management**: Demonstriert, wie Sitzungsinformationen sicher in Cookies gespeichert und abgerufen werden können.

## Nutzung
Das Projekt kann direkt in Visual Studio oder einem anderen IDE geöffnet und gestartet werden. Besuchen Sie die Startseite unter `http://localhost:5287`, um die Anwendung zu nutzen und die Funktionen der Benutzerregistrierung und -anmeldung zu testen.
