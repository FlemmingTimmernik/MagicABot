# Roadmap

Sidst opdateret: 2026-05-06

## Programoverblik

Programmet er en Windows Forms helper til MTG Arena. Det kombinerer logfil-laesning, screenshots, pixelgenkendelse, muse/keyboard-automation og deck-import for at automatisere gentagne Arena-opgaver.

Det kan i dag:

- Starte, lukke og fokusere MTG Arena via Steam og Windows-processer.
- Logge ind paa flere konti ud fra nummererede MTGA-konti.
- Laese MTG Arena `Player.log` og udtraekke spiller, quests, inventory og quest-progress.
- Vurdere hvilken farve der bedst loeser de aktive quests.
- Skifte quests, naar en quest er lav vaerdi eller markeret som en quest der boer skiftes.
- Generere simple decks for white, blue, black, red og green og importere dem via clipboard.
- Vaelge/favorisere et deck baseret paa quest-farver.
- Starte bot-matches og spille simple loops ved at finde knapper, kort og states via pixels.
- Concede/starte ny kamp ud fra timeout og quest-status.
- Tage screenshots foer/efter quest-skift og kopiere/splitte/rydde logfiler.
- Koordinere "Master" og "Slave" PC via en konfigureret delt mappe.
- Vise simple monitor-vinduer for quest/logstatus.
- Bruge Pixel Tool og ScreenshotHelper til at finde skaermkoordinater og farver.
- Lave collection-decks ved at klikke kort igennem i deck builderen.

## Kommende Funktionalitet

### 1. Stabil drift

- Saml alle timeouts, skaermoploesninger, kontointervaller, stier og netvaerksstier i `config\config.json`.
- Tilfoej en faelles stop/cancel-mekanisme til lange loops, saa programmet kan stoppes rent uden at flytte musen til toppen af skaermen.
- Lav status/log i UI for "hvad laver botten nu", sidste fejl og nuvaerende konto.
- Sikr at noedvendige mapper oprettes automatisk: `LogFiles`, `LogfilesDump`, `cleanLogfiles`, `magicLogfile`, `config`, screenshotmapper.
- Lav senere et skaermbillede i programmet til at se og rette `config\config.json` uden at redigere filen manuelt.

### 2. Robust MTG Arena navigation

- Erstat flere faste koordinater med navngivne skaerme og pixel-signaturer.
- Lav support for flere oploesninger eller en kalibreringsfil til koordinater.
- Goer login-flowet mere tolerant overfor nye popups, announcements og ban-meddelelser.
- Tilfoej retry-regler for failed login, sort skaerm, disconnect og Arena update-skaerme.

### 3. Quest- og decklogik

- Saml quest parsing et sted, saa `ParseLogfile` og `TryJson` ikke har to versioner af samme farvelogik.
- Goer quest-vurdering synlig i UI: quest navn, progress, reward, farver, "swap/keep" beslutning.
- Goer deck-opskrifter data-drevne, fx `Decks\B-White.txt`, i stedet for hardcodede strings i C#.
- Tilfoej bedre deckvalg for quests der kan klares af flere farver.

### 4. Account management

- Lav en lille konto-/queue-visning til `NextPlayer1.txt`, `loginOrder.txt` og sidste bruger.
- Tilfoej pause/resume for konto-koersel.
- Gem resultat pr. konto: login OK, quests foer/efter, deck valgt, antal kampe, fejl.
- Goer Master/Slave-samarbejdet tydeligt og konfigurerbart.

### 5. Tests og vedligehold

- Tilfoej unit tests for quest parsing, farvevalg, deck generation og login queue.
- Flyt automation, questlogik, logfilservice og UI ud i separate klasser.
- Tilfoej en "dry run" mode, hvor programmet kan beregne beslutninger uden at klikke i Arena.
- Tilfoej README med opsaetning, mappekrav og sikker brug.

## Mulig Prioritering

1. Goer config og mapper stabile.
2. Fjern hardcodede credentials/stier fra kode.
3. Saml quest parsing og skriv tests for den.
4. Lav bedre statusvindue og stopknap.
5. Kalibrer pixel/koordinat-systemet.
