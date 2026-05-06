# Logfil

## 2026-05-06 15:49:36 +02:00

- Opdateret `NextStep.md`.
- Naeste arbejdsopgave er sat til `v0.2.0`: flyt hardcodede lokale stier og netvaerksstier ud af koden og ind i `ConfigReader`/config.
- Foerste fokusomraader: `Player.log` path, Steam path, Master/Slave share path og runtime mapper.

## 2026-05-06 15:43:47 +02:00

- Besluttet at oprette foerste baseline-version.
- Version: `v0.1.0`.
- Betydning: Foerste GitHub-version hvor projektet bygger, dokumentationsfiler er oprettet, `.gitignore` er paa plads, og loginoplysninger er flyttet ud af `Form1.cs`.
- Naeste naturlige version: `v0.2.0` naar config/stier er samlet bedre og flere lokale hardcodede stier er flyttet ud.

## 2026-05-06 15:42:27 +02:00

- Lavet foerste lokale commit: `Initial commit`.
- Pushet `main` til GitHub remote `origin`.
- GitHub repo: `https://github.com/FlemmingTimmernik/MagicABot`.

## 2026-05-06 15:41:55 +02:00

- Initialiseret git i projektmappen.
- Sat branch-navn til `main`.
- Sat GitHub remote `origin` til `https://github.com/FlemmingTimmernik/MagicABot.git`.
- Kontrolleret at `.gitignore` ignorerer `.vs`, `bin`, `obj` og user-filer.
- Kontrolleret `.vscode`-filer; de bruger relative `${workspaceFolder}`-stier.

## 2026-05-06 15:40:35 +02:00

- Oprettet `Roadmap.md` med overblik over programmets nuvaerende funktioner og kommende funktionalitet.
- Oprettet `ThingsToFix.md` med konkrete oprydningspunkter, fejlrisici, testpunkter og drift/sikkerhed.
- Oprettet `NextStep.md` som kommunikationsfil til naeste opgave.
- Oprettet `.gitignore` til Visual Studio/.NET build-output, logs, screenshots, private config-filer og lokale runtime-filer.
- Flyttet hardcodede loginoplysninger ud af `Form1.cs` og over i `ConfigReader`.
- Tilfoejet loginfelter til config: `LoginEmailAccountZero`, `LoginEmailTemplate` og `LoginPassword`.
- Tilfoejet `config.example.txt` som eksempel paa lokal config uden rigtige credentials.
- Koert `dotnet build Magic.sln`; build lykkedes med 0 errors, men der er stadig warnings som skal ryddes op senere.
