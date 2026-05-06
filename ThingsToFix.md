# ThingsToFix

Sidst opdateret: 2026-05-06

## Hoej Prioritet

- Verificer at alle loginoplysninger kun ligger i lokal `config\config.json` og aldrig i filer der pushes til GitHub.
- Fortsaet med at flytte hardcodede lokale stier og netvaerksstier ind i config, efterhaanden som de dukker op i flows der ikke er ryddet helt op.
- Programmet antager i praksis 1920x1080 og faste MTG Arena-positioner. Det goer automation skroebelig ved ny oploesning, UI-scale eller Arena-opdateringer.
- Mange `catch { }` skjuler fejl. De boer mindst logge exception og kontekst.
- Lange/infinite loops mangler faelles cancellation token eller kontrolleret stop.
- `Thread.Sleep` styrer meget af flowet. Det boer gradvist erstattes af vent-paa-state, timeout og retry.
- `Form1.cs` er for stor og blander UI, login, questlogik, decklogik, filkopiering og kampautomation.

## Fejlrisici

- `TryJson.InventoryInfo.WildCardRares` bruger `[JsonPropertyName("WildCardUnCommons")]`, hvilket ligner en copy/paste-fejl.
- `ParseLogfile.FindAndParseQuestLine` leder efter inventory med samme `Quest_GetQuests`-tekst som quest-linjen. Det boer verificeres mod rigtige loglinjer.
- `ParseLogfile.RenameLogfiles` kan overskrive/fejle, hvis destinationer allerede findes.
- `CopyLocalPlayerLogFile` bruger `File.Copy(..., false)` og fejler, hvis `LogfilesDump\temp.log` findes.
- Flere steder sletter programmet logfiler efter behandling. Det boer have backup eller en "processed" mappe.
- `MTGArena.CloseMTGArena` draeber kun processen naar `AllowForceKillArena` er true, men der boer senere laves graceful close foerst.
- `ConfigReader` bruges nu fra `Form1`, men flere klasser boer senere faa config injiceret i stedet for at hente `ConfigReader.Current`.
- UI-opdatering fra baggrundstraade er delvist haandteret, men loops kan fortsaette efter vinduer lukkes.

## Oprydning

- Fjern ubrugte `using` statements og doede/kommenterede kodeblokke.
- Giv knapper og handlers tydelige navne i stedet for `button1`, `button2`, `BtnTest_Click_1` osv.
- Saml click helper-metoder, pixel checks og screen states i egne services.
- Lav en mappe til dokumentation: eventuelt `docs\`, hvis der kommer flere filer.
- Tilfoej `.gitignore`, hvis repoet skal i git, saa `bin`, `obj`, screenshots og private config/logfiler ikke blandes med kildekoden.
- Ensret casing paa mapper: `LogfilesDump`, `logfilesdump`, `LogFilesTemp`, `logfilestemp`.
- Goer decklister eksterne, saa de kan aendres uden recompilering.
- Tilfoej validering af inputfelter som loginnummer, antal sider og max quests.

## Testpunkter

- Quest parsing for 1, 2 og 3 quests.
- Farvevalg ved overlappende quests.
- Quest swap-beslutning ved 500/750 reward og "Shift" quests.
- Deck generation for alle single-color og multi-color kombinationer.
- Login queue: hent naeste spiller, gem sidste spiller, indsaet sidste spiller igen.
- Logfil-split og cleanup uden datatab.
- Pixel checks med gemte screenshots, saa de kan testes uden live Arena.

## Sikkerhed Og Drift

- Hold passwords ude af repoet og kun i lokal ignored config.
- Netvaerks-skrivning er gated af `AllowNetworkFileWrites`; gennemgaa senere alle flows for bedre UI-feedback.
- `DryRun` findes i config og er koblet paa vigtige klik/start/kopi flows; udvid senere til alle direkte `MouseOperations`-kald.
- Faelles `DoLeftMouseClick` kan logge koordinater med `DebugClickLogging`; udvid senere med screenstate.
- Tilfoej en synlig noedstop/status i UI udover musens position.
