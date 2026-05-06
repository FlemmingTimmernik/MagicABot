# Logfil

## 2026-05-06 16:41:56 +02:00

- Laest `NextStep.md`, `Workflow.md`, `Roadmap.md`, `ThingsToFix.md`, `Logfil.md` og `Form1ResponsibilityPlan.md`.
- Oprettet `Magic\RuntimeLogService.cs`.
- Flyttet ansvar for `CopyLogFiles`, `SplitLogFilesToIndividualLogFiles`, `ReadAllLogFiles`, `OnlySaveImportantLines` og `ReadLogFile` ud af `Form1.cs`.
- `Form1.cs` har nu tynde wrappers, der kalder `RuntimeLogService`.
- `RuntimeLogService` bruger `ConfigReader`, opretter runtime-mapper, respekterer `DryRun` ved kopiering og respekterer `AllowDeleteProcessedLogFiles` ved sletning.
- Opdateret `NextStep.md` med naeste foreslaaede step: `AccountQueueService`.
- Koert `dotnet build Magic.sln`; build lykkedes med 0 errors, men eksisterende warnings er stadig tilbage.

## 2026-05-06 16:22:34 +02:00

- Laest `NextStep.md`, `Workflow.md`, `Roadmap.md`, `ThingsToFix.md` og seneste `Logfil.md`.
- Oprettet `Form1ResponsibilityPlan.md` med plan for hvordan `Form1.cs` boer deles op i services.
- Opdateret `NextStep.md` med foerste konkrete implementeringsstep: opret `RuntimeLogService` og flyt logfil-ansvar ud af `Form1.cs`.
- Build ikke koert, da aendringen kun er dokumentation.

## 2026-05-06 16:12:40 +02:00

- Oprettet `Workflow.md`.
- Workflow fastlagt som: `NextStep` -> kode/docs -> build ved kodeaendringer -> sikkerhedstjek -> `Logfil` -> commit -> push -> evt. version.
- Tilfoejet sikkerhedsregler for lokal config, netvaerkskopiering, logfil-sletning, force-kill og `DryRun`.
- Sikkerhedstjek koert rent; `config\config.json` er fortsat ignored.
- Build ikke koert, da aendringen kun er dokumentation.

## 2026-05-06 16:03:30 +02:00

- Oprettet lokal JSON config: `config\config.json` (ignored af git).
- Oprettet tracked eksempelconfig: `config.example.json`.
- Skiftet config path fra `config\config.txt` til `config\config.json`.
- Flyttet centrale stier ind i `ConfigReader`: `PlayerLogPath`, `SteamPath`, `MasterSharePath`, runtime mapper og login queue filnavne.
- Tilfoejet sikkerhedsflag i config: `DryRun`, `DebugClickLogging`, `AllowForceKillArena`, `AllowNetworkFileWrites` og `AllowDeleteProcessedLogFiles`.
- Gated force-kill af MTGA bag `AllowForceKillArena`.
- Gated netvaerkskopiering bag `AllowNetworkFileWrites`.
- Gated sletning af behandlede logfiler bag `AllowDeleteProcessedLogFiles`.
- Tilfoejet dry-run logging til centrale klik/kopi/start flows.
- Tilfoejet synlig `STOP` knap i UI, som saetter `stopRequested` og stopper den faelles klik-helper.
- Opdateret `Roadmap.md` med senere config-skaermbillede/editor.
- Koert `dotnet build Magic.sln`; build lykkedes med 0 errors, men eksisterende warnings er stadig tilbage.

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
