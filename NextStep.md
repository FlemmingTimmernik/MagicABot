# NextStep

Denne fil er vores kommunikationsfil. Skriv den aktuelle opgave oeverst under "Aktuel opgave", saa bruger jeg den som naeste arbejdsordre.

## Aktuel opgave

```text
Opret RuntimeLogService og flyt logfil-ansvar ud af Form1.cs.

Start med:
- CopyLogFiles
- SplitLogFilesToIndividualLogFiles
- ReadAllLogFiles
- OnlySaveImportantLines
- ReadLogFile

Maal:
- Form1 skal kun kalde service-metoder.
- RuntimeLogService skal bruge ConfigReader.
- Sletning skal respektere AllowDeleteProcessedLogFiles.
- Kopiering skal respektere DryRun.
- dotnet build Magic.sln skal lykkes med 0 errors.

Senest afsluttet:
Plan for opdeling af ansvar i Form1.cs er skrevet i Form1ResponsibilityPlan.md.
```

## Arbejdsregel

- Hold opgaven konkret og lille nok til at kunne afsluttes i en omgang.
- Skriv gerne hvilken fil eller funktion der skal aendres.
- Skriv hvis jeg kun skal analysere og ikke aendre kode.
- Naar opgaven er loest, kan denne sektion erstattes med naeste opgave.

## Seneste status

- 2026-05-06 16:22:34 +02:00: Plan for opdeling af `Form1.cs` skrevet; naeste implementeringsstep er `RuntimeLogService`.
- 2026-05-06 16:03:30 +02:00: v0.2.0 config/stier/sikkerhed implementeret; build lykkes med 0 errors.
- 2026-05-06 15:49:36 +02:00: Naeste arbejdsopgave sat til v0.2.0 config/stier.
- 2026-05-06: Oprettet `Roadmap.md`, `ThingsToFix.md` og `NextStep.md`.
- Programoverblik: MTG Arena Windows Forms helper til login, quest parsing, quest swap, deck generation/import, kampautomation, screenshots og logfilhaandtering.
