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

Azure länk av applikationen finns här:
**[BankSystem](https://banksystem-asb2bub6e6e5b6fb.swedencentral-01.azurewebsites.net)**  
Direktlänk: `https://banksystem-asb2bub6e6e5b6fb.swedencentral-01.azurewebsites.net`
