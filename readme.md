# BankSystem – Administrationsapp för Bankanställda

## Funktioner

- Razor Pages-app för **bankanställda** (Admins & Cashiers)
- Kopplad till en databas via Entity Framework Core (Database First)
- Bootstrap-tema (Paper Dashboard) implementerat och anpassat
- Statistik på startsida (offentlig):
  - Antal kunder
  - Antal konton
  - Totalsaldo per land
- Sök efter kund via **namn** och **stad** med paginering (50 resultat/sida)
- Sök kund via ID
- Visa **kontobild** med transaktioner
  - AJAX-knapp för att ladda fler transaktioner (20 åt gången)
- **Transaktioner**: insättning, uttag och överföring
- ASP.NET Core Identity med seedade roller (`Admin`, `Cashier`)

## Azure-publicering

Appen var tidigare publicerad på Azure, men både Web appen och databas har nu tagits bort eftersom projektet är gjord i lärosyfte och inte längre aktivt underhålls.
