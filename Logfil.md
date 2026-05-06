# Logfil

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
