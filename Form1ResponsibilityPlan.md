# Form1 Responsibility Plan

Denne plan beskriver hvordan `Form1.cs` boer deles op uden at lave en stor rewrite. Maalet er at flytte ansvar ud gradvist, saa programmet stadig bygger og virker efter hvert lille skridt.

## Maal

`Form1` skal ende som en UI-skal:

- Knapper og event handlers.
- Opdatering af labels, tekstfelter og checkboxes.
- Start/stop af baggrundsarbejde.
- Kald til services.

Alt der handler om MTG Arena, logfiler, quests, decks, konto-koe og filsystem boer ud i separate klasser.

## Foreslaaede Ansvarsomraader

### UI: `Form1`

Beholdes i `Form1`:

- Button click handlers.
- Lette UI helpers som `WriteTextBox`, `WriteLabel` og `SetCheckBox`.
- Visning af `Monitor`, `PixelTool` og `MTGLogFileReaderFrm`.
- Kald til services og visning af status.

### Sikkerhed Og Drift: `AutomationSafetyService`

Ansvar:

- Stop flag.
- Dry-run check.
- Debug click logging.
- Noedstop/status.
- Faelles regler for om automation maa fortsaette.

Flyt fra `Form1`:

- `stopRequested`
- `ShouldStopAutomation`
- dele af `DoLeftMouseClick` som handler om stop/dry-run/logging

### Input Og Skaerm: `ArenaInputService` og `ScreenDetectionService`

Ansvar:

- Museklik, dobbeltklik og tastaturinput.
- Pixel checks.
- Screenshot/crop helpers.
- Skaermstate-detektion.

Flyt fra `Form1`:

- `DoLeftMouseClick`
- `DoubleClickLeftMouseButton`
- `CheckAreaForColor`
- `WhereAreWe`
- `ClickThrougToMainScreenFromBloomBurrowIntroScreen`

### MTG Arena Process: `ArenaProcessService`

Ansvar:

- Starte MTG Arena.
- Lukke/fokusere MTG Arena.
- Process-sikkerhed omkring force-kill.

Eksisterende kandidat:

- `MTGArena` kan omdoebes/udvides i stedet for at starte fra nul.

### Login Og Konto: `LoginService` og `AccountQueueService`

Ansvar:

- Beregne login email fra konto-nummer.
- Login-flow fra login-skaerm til home.
- Hente naeste konto.
- Gemme sidste konto.
- Master/Slave konto-fil flow.

Flyt fra `Form1`:

- `Login`
- `LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen`
- `LogoutFromHomeScreen`
- `RestoreLastUser`
- `CloseMagicOpenAndLogin`
- dele af `AutoLoginAndPlay` der kun handler om konto-koe

Eksisterende kandidat:

- `Player.GetNextPlayer`, `Player.WriteLastUser` og `Player.TransferLastUserIDToUserlist` kan flyttes ud af `Player` paa sigt.

### Quest: `QuestService` og `QuestDecisionService`

Ansvar:

- Laese quest state fra log.
- Beregne bedste farve.
- Beslutte om en quest skal skiftes.
- Udstille en ren model til UI.

Flyt fra `Form1`:

- `FindWhatQuestToChange`
- `ChangeQuest`
- `CheckQuest`
- `IsQuestDone`
- `ContinueSolvingQuests`
- `ReadLogFileAndSetColorsOnPlayer`
- `SetCheckBoxColors`

Eksisterende kandidat:

- `ParseLogfile` skal senere deles i log-reader og quest-parser.

### Deck: `DeckService`

Ansvar:

- Generere deck tekst.
- Importere deck.
- Vaelge/favorisere deck.
- Senere laese decklister fra filer.

Flyt fra `Form1`:

- `CreateDeck`
- `Create5Decks`
- `PutColorDeckFirst`
- `PutFavouriteOnDeckToPlay`
- `FavouriteFirstDeck`
- `RemoveAllFavouriteDecks`
- `DoubleClickOnFirstDeckAndClickDone`
- `SelectDeck`
- `ClickOnImportDeck`

Eksisterende kandidat:

- `ImportDecks` kan blive ren deck-builder uden UI-klik.

### Match/Game Loop: `GameLoopService`

Ansvar:

- Starte match.
- Spille simple handlinger.
- Concede.
- Starte ny kamp.
- Timeouts.

Flyt fra `Form1`:

- `StartGameLoop`
- `GameLoop`
- `StartFirstMatch`
- `StartNewGame`
- `ConcedeMatch`
- `ClickVictoryOrDefeatScreen`
- `SelectFirstDeck`
- `ClickHomeButton`

### Logfiler Og Runtime-filer: `RuntimeLogService`

Ansvar:

- Kopiere `Player.log`.
- Splitte logfiler pr. spiller.
- Rense logfiler.
- Flytte/slette kun naar config tillader det.
- Sikre mapper.

Flyt fra `Form1`:

- `CopyLogFiles`
- `SplitLogFilesToIndividualLogFiles`
- `ReadAllLogFiles`
- `OnlySaveImportantLines`
- `ReadLogFile`
- `CopyAndParsePlayerFiles`
- `CleanLinesInventoryLines`

Eksisterende kandidat:

- `ParseLogfile.CopyLogfilesAndNumberThem` boer flyttes til log-service.

### Collection: `CollectionService`

Ansvar:

- Automation for collection-decks.

Eksisterende kandidat:

- `CardCollection` er allerede delvist isoleret.

## Foreslaaet Raekkefoelge

1. Dokumenter opdelingen og hold `Form1` stabil.
2. Extract `RuntimeLogService`, fordi fil/log-funktionerne er tydelige og relativt isolerede.
3. Extract `AccountQueueService`, fordi konto-koe og sidste bruger er simpelt og testbart.
4. Extract `QuestDecisionService`, start med pure logic som `FindWhatQuestToChange`.
5. Extract `DeckService`, men lad de faktiske klik blive kaldt via eksisterende helper i foerste omgang.
6. Extract `ArenaInputService`, saa klik/dry-run/debug logging kun findes et sted.
7. Extract `GameLoopService` til sidst, fordi den er mest koblet til alt andet.

## Foerste Implementeringsstep

Start med `RuntimeLogService`.

Hvorfor:

- Det har lavere risiko end game loop.
- Det rammer mange sikkerhedspunkter: kopiering, sletning, mapper og config.
- Det goer `Form1.cs` mindre uden at aendre gameplay-logik.

Foerste konkrete opgave:

```text
Opret RuntimeLogService og flyt CopyLogFiles, SplitLogFilesToIndividualLogFiles, ReadAllLogFiles, OnlySaveImportantLines og ReadLogFile ud af Form1.cs. Form1 skal kun kalde service-metoderne.
```

## Definition Of Done For Foerste Step

- `Form1.cs` mister logfil-metoderne eller har kun tynde wrappers.
- Logfilservice bruger `ConfigReader`.
- Sletning respekterer `AllowDeleteProcessedLogFiles`.
- Kopiering respekterer `DryRun`.
- `dotnet build Magic.sln` lykkes med 0 errors.
- `Logfil.md` opdateres.
