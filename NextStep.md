# NextStep

Denne fil er vores kommunikationsfil. Skriv den aktuelle opgave oeverst under "Aktuel opgave", saa bruger jeg den som naeste arbejdsordre.

## Aktuel opgave

```text
Opret AccountQueueService og flyt konto-koe ansvar ud af Form1.cs.

Start med:
- RestoreLastUser
- WhatComputer
- CopyLoginOrderToMaster
- de Form1-steder der henter/skriver naeste spiller

Maal:
- Form1 skal kun kalde service-metoder.
- AccountQueueService skal bruge ConfigReader.
- Master/Slave filstier skal gaa gennem ConfigReader.
- Netvaerks-skrivning skal respektere AllowNetworkFileWrites.
- DryRun skal respekteres ved kopiering/skrivning.
- dotnet build Magic.sln skal lykkes med 0 errors.

Senest afsluttet:
RuntimeLogService er oprettet, og Form1 kalder nu service-metoder for logfil-flowet.
```

## Arbejdsregel

- Hold opgaven konkret og lille nok til at kunne afsluttes i en omgang.
- Skriv gerne hvilken fil eller funktion der skal aendres.
- Skriv hvis jeg kun skal analysere og ikke aendre kode.
- Naar opgaven er loest, kan denne sektion erstattes med naeste opgave.

## Seneste status

- 2026-05-06 16:41:56 +02:00: `RuntimeLogService` oprettet; `Form1.cs` har nu tynde wrappers for logfil-flowet; build lykkes med 0 errors.
- 2026-05-06 16:22:34 +02:00: Plan for opdeling af `Form1.cs` skrevet; naeste implementeringsstep er `RuntimeLogService`.
- 2026-05-06 16:03:30 +02:00: v0.2.0 config/stier/sikkerhed implementeret; build lykkes med 0 errors.
- 2026-05-06 15:49:36 +02:00: Naeste arbejdsopgave sat til v0.2.0 config/stier.
- 2026-05-06: Oprettet `Roadmap.md`, `ThingsToFix.md` og `NextStep.md`.
- Programoverblik: MTG Arena Windows Forms helper til login, quest parsing, quest swap, deck generation/import, kampautomation, screenshots og logfilhaandtering.
