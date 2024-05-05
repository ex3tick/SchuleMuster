
# Dokumentation für das WebApp-Projekt

## Übersicht
Diese Dokumentation beschreibt die WebApp, ein ASP.NET Core 8.0 Projekt, das sich auf Benutzerregistrierung und -anmeldung, das Arbeiten mit Cookies, und SQL-Abfragen innerhalb des MVC-Patterns konzentriert. Die Anwendung demonstriert die Verwendung von MVC-Architektur zur Handhabung von Benutzerdaten und Sitzungsinformationen.

## Projektstruktur
Das Projekt ist in mehrere Hauptteile gegliedert, die den MVC-Architekturrichtlinien folgen:

### Models
- **Person**: Repräsentiert die Benutzerdaten.
- **LoginTransferObjekt**: Dient als Datenübertragungsobjekt für Login-Operationen.

### Views
- **Index.cshtml**: Hauptseite, die nach der Anmeldung angezeigt wird.
- **Login.cshtml**: Anmeldeseite für Benutzer.
- **Register.cshtml**: Registrierungsseite für neue Benutzer.
- **Privacy.cshtml**: Datenschutzseite, zugänglich für angemeldete Benutzer.
- **Everyone.cshtml**: Eine allgemein zugängliche Seite.

### Controllers
- **HomeController**: Verwaltet die Hauptinteraktionen wie Anmeldung, Registrierung und das Anzeigen der Startseite.

### Data Access Layer
- **UserDAL**: Interagiert direkt mit der SQL-Datenbank zur Handhabung von Benutzerdaten.
- **IAccessable**: Schnittstelle für den Datenzugriff, implementiert von `UserDAL`.

## Hauptfunktionen
- **Benutzeranmeldung und -registrierung**: Nutzer können ein Konto erstellen und sich anmelden.
- **Session Management mit Cookies**: Die Anwendung verwendet Cookies, um Benutzersitzungen zu verwalten und den Zugriff auf bestimmte Seiten zu steuern.

## Sicherheit
- Die Anwendung verwendet sichere Methoden zur Handhabung von Benutzerdaten und Sessions. SQL-Abfragen werden durch die DAL-Schicht abstrahiert, um SQL-Injection zu verhindern.

## Entwicklungs- und Betriebsumgebung
- Entwickelt mit .NET Core 8.0.
- Erfordert eine SQL-Datenbank für das Backend.

## Verwendung
- Das Projekt kann in Visual Studio geöffnet und ausgeführt werden. Es ist darauf ausgelegt, auf einem lokalen Server für Entwicklungs- und Testzwecke zu laufen.
