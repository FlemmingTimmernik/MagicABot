# Workflow

Dette er den faste arbejdsgang for projektet. Formaalet er at holde arbejdet roligt, sporbart og sikkert, isaer fordi projektet styrer login, filer og MTG Arena automation.

## Fast Arbejdsgang

1. Skriv naeste opgave i `NextStep.md`.
2. Laes `NextStep.md`, `Logfil.md`, `Roadmap.md` og `ThingsToFix.md` foer kodning.
3. Lav en afgraenset aendring ad gangen.
4. Byg projektet efter kodeaendringer:

```powershell
dotnet build Magic.sln
```

5. Lav sikkerhedstjek foer commit:

```powershell
rg -n "<private-email-or-password-or-machine-path-pattern>" --glob "!Magic/bin/**" --glob "!Magic/obj/**" --glob "!MouseAndKeyboard/**/bin/**" --glob "!MouseAndKeyboard/**/obj/**"
git status --short --ignored
git check-ignore -v config\config.json
```

6. Opdater `Logfil.md` med timestamp, hvad der er aendret, build-resultat og kendte risici.
7. Commit med en konkret besked.
8. Push til GitHub.
9. Lav versionstag naar aendringen er en stabil milepael.

## Versioner

- Patch-versioner bruges til smaa rettelser og dokumentation.
- Minor-versioner bruges til stabile milepaele som config, quest parsing eller service-opdeling.
- Major-version `v1.0.0` reserveres til en stabil brugsversion.

## Sikkerhedsregler

- Rigtige passwords og logininfo maa kun ligge i lokal `config\config.json`.
- `config\config.json` maa aldrig pushes til GitHub.
- Netvaerkskopiering skal vaere styret af `AllowNetworkFileWrites`.
- Sletning af behandlede logfiler skal vaere styret af `AllowDeleteProcessedLogFiles`.
- Force-kill af MTG Arena skal vaere styret af `AllowForceKillArena`.
- Brug `DryRun` naar et flow skal testes uden klik, process start eller filkopiering.

## Definition Of Done

En opgave er foerst faerdig naar:

- Koden bygger, hvis der er lavet kodeaendringer.
- Sikkerhedstjekket er koert.
- `Logfil.md` er opdateret.
- Relevante docs er opdateret.
- Aendringen er committet og pushed.
