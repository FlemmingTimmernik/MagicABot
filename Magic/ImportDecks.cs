using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
    internal static class ImportDecks
    {
        public static void CreateDeckFromColors(bool white, bool blue, bool black, bool red, bool green)
        {
            Thread.Sleep(500);
            //Press on Decks
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(322, 42));

            Thread.Sleep(1500);



           

            //ClickOnImportDeck;
            string deck = AssembleDeck(white, blue, black, red, green);
            Thread.Sleep(800);
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(562, 1012));
            Thread.Sleep(800);
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(960, 632));
            Thread.Sleep(800);




            Clipboard.SetText(deck);

            //ClickOnImportDeck;
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(562, 1012));
            Thread.Sleep(800);
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(960, 632));

        }

        public static void CalculateWhatColorToPlay(Player playerPar)
        {
            int? white = 0;
            int? blue = 0;
            int? black = 0;
            int? red = 0;
            int? green = 0;

            for (int i = 0; i < playerPar.QuestCollection.Quests.Count; i++)            
            {
                var current = playerPar.QuestCollection.Quests[i];
                int? value = current.ChestDescription.Quantity;

                if (current.White)
                    white += value;
                if (current.Black)
                    black += value;
                if (current.Blue)
                    blue += value;
                if (current.Red)
                    red += value;
                if (current.Green)
                    green += value;
            }
            playerPar.White = false;
            playerPar.Blue = false;
            playerPar.Black = false;
            playerPar.Red = false;
            playerPar.Green = false;

            if (white >= blue && white >= black && white >= red && white >= green)
                playerPar.White = true;
            else if (blue >= black && blue >= red && blue >= green)
                playerPar.Blue = true;
            else if (black >= red && black >= green)
                playerPar.Black = true;
            else if (red >= green)
                playerPar.Red = true;
            else
                playerPar.Green = true;

        }

        public static string AssembleDeck(bool white, bool blue, bool black, bool red, bool green)
        {
            int numberOfDecks = 0;
            string deck = "About" + Environment.NewLine + "Name B-";

            if (white)
            {
                deck += "White/";
                numberOfDecks++;
            }
            if (blue)
            {
                deck += "Blue/";
                numberOfDecks++;
            }
            if (black)
            {
                deck += "Black/";
                numberOfDecks++;
            }
            if (red)
            {
                deck += "Red/";
                numberOfDecks++;
            }
            if (green)
            {
                deck += "Green/";
                numberOfDecks++;
            }

            deck = deck.Substring(0, deck.Length - 1);

            deck += Environment.NewLine + "Deck" + Environment.NewLine;

            if (white)
                deck += GetWhiteDeck(numberOfDecks);
            if (blue)
                deck += GetBlueDeck(numberOfDecks);
            if (black)
                deck += GetBlackDeck(numberOfDecks);
            if (red)
                deck += GetRedDeck(numberOfDecks);
            if (green)
                deck += GetGreenDeck(numberOfDecks);

            return deck;
        }



        /// <summary>
        /// Get share of a white deck
        /// </summary>
        /// <param name="shareOfDeck">1 give 1/1, 2 give 1/2, 3 give 1/3</param>
        /// <returns></returns>
        public static string GetWhiteDeck(int shareOfDeck)
        {
            #region
            string deck = //triple or more
                "4 Charmed Stray (ANB) 5" + Environment.NewLine +
                "3 Novice Inspector (MKM) 29" + Environment.NewLine +
                "2 Sanctuary Cat (ANB) 17" + Environment.NewLine +
                "2 Fencing Ace (ANB) 7" + Environment.NewLine +
                "1 Hallowed Priest (ANB) 9" + Environment.NewLine +
                "8 Plains (OTJ) 278" + Environment.NewLine;

            if (shareOfDeck <= 2) // dual
                deck +=
                "3 Hallowed Priest (ANB) 9" + Environment.NewLine +
                "1 Impassioned Orator (ANB) 10" + Environment.NewLine +
                "6 Plains (OTJ) 278" + Environment.NewLine;

            if (shareOfDeck == 1) // solo
                deck +=
                "3 Marketwatch Phantom (MKM) 24" + Environment.NewLine +
                "2 Impassioned Orator (ANB) 10" + Environment.NewLine +
                "2 Moorland Inquisitor (ANB) 15" + Environment.NewLine +
                "3 Outlaw Medic (OTJ) 23" + Environment.NewLine +
                "2 Perimeter Enforcer (MKM) 31" + Environment.NewLine +
                "2 Seasoned Consultant (MKM) 33" + Environment.NewLine +
                "2 Shrine Keeper (ANB) 19" + Environment.NewLine +
                //"2 Angel of Vitality (ANB) 1" + Environment.NewLine +
                //"2 Thousand Moons Infantry (LCI) 38" + Environment.NewLine +
                "14 Plains (OTJ) 278" + Environment.NewLine;
            return deck;
            #endregion
        }

        /// <summary>
        /// Get share of a black deck
        /// </summary>
        /// <param name="shareOfDeck">1 give 1/1, 2 give 1/2, 3 give 1/3</param>
        /// <returns></returns>
        private static string GetBlackDeck(int shareOfDeck)
        {
            #region
            string deck =
                "8 Swamp (OTJ) 282" + Environment.NewLine +
                "2 Nezumi Linkbreaker (OTJ) 96" + Environment.NewLine +
                "2 Typhoid Rats (ANB) 63" + Environment.NewLine +
                "2 Barrow Naughty (WOE) 81" + Environment.NewLine +
                "3 Malakir Cullblade (ANB) 51" + Environment.NewLine +
                "3 Mintstrosity (WOE) 100" + Environment.NewLine;

            if (shareOfDeck < 3)
                deck +=
                    "1 Vadmir, New Blood (OTJ) 113" + Environment.NewLine +
                    "2 Vampire Opportunist (ANB) 65" + Environment.NewLine +
                    "3 Savage Gorger (ANB) 58" + Environment.NewLine +
                    "4 Swamp (OTJ) 282" + Environment.NewLine;

            if (shareOfDeck < 2)
                deck +=
                     "25 Swamp (OTJ) 282" + Environment.NewLine +
                     //"1 Writhing Necromass (DMU) 115" + Environment.NewLine +
                     //"1 Writhing Necromass (DMU) 115" + Environment.NewLine +
                     //"" + Environment.NewLine +
                     "2 Screaming Phantom (LCI) 118" + Environment.NewLine +
                     "3 Sweettooth Witch (WOE) 111" + Environment.NewLine;
                     //"2 Scream Puff (WOE) 105" + Environment.NewLine +
                     //"4 Sengir Vampire (ANB) 60" + Environment.NewLine +
                     //"1 Bad Deal (ANB) 45" + Environment.NewLine +
                     //"1 Nightmare (ANB) 54" + Environment.NewLine +
                     //"2 Demon of Loathing (ANB) 48";

            return deck;
            #endregion
        }

        /// <summary>
        /// Get share of a blue deck
        /// </summary>
        /// <param name="shareOfDeck">1 give 1/1, 2 give 1/2, 3 give 1/3</param>
        /// <returns></returns>
        private static string GetBlueDeck(int shareOfDeck)
        {
            #region
            string deck =
                "2 Sworn Guardian (ANB) 35" + Environment.NewLine +
                "2 Razzle-Dazzler (OTJ) 63" + Environment.NewLine +
                "3 Warden of Evos Isle (ANB) 38" + Environment.NewLine +
                "2 Waterwind Scout (LCI) 84" + Environment.NewLine +
                "1 Forensic Gadgeteer (MKM) 57" + Environment.NewLine +
                "10 Island (OTJ) 280" + Environment.NewLine;

            if (shareOfDeck < 3)
                deck +=
                "3 Mocking Sprite (WOE) 62" + Environment.NewLine +
                "3 Cloudkin Seer (ANB) 25" + Environment.NewLine +
                "4 Island (OTJ) 280" + Environment.NewLine;


            if (shareOfDeck < 2)
                deck +=
                // "2 Windstorm Drake (ANB) 42" + Environment.NewLine +
                //"2 Soulblade Djinn (ANB) 34" + Environment.NewLine +
                //"2 Cold Case Cracker (MKM) 46" + Environment.NewLine +
                "3 Waterkin Shaman (ANB) 39" + Environment.NewLine +
                "3 Jaded Analyst (MKM) 62" + Environment.NewLine +
                "1 Archmage's Newt (OTJ) 39" + Environment.NewLine +
                "2 Deduce (MKM) 52" + Environment.NewLine +
                "1 Sleep-Cursed Faerie (WOE) 66" + Environment.NewLine +
                "20 Island (OTJ) 280" + Environment.NewLine;

            return deck;
            #endregion
        }

        public static void CopyBonusDeckToClipBoard()
        {
            Clipboard.SetText(
       "About" + Environment.NewLine +
   "Name thewayisopen" + Environment.NewLine +
   "Deck" + Environment.NewLine +
   "1 Case of the Stashed Skeleton" + Environment.NewLine +
   "1 Extract a Confession" + Environment.NewLine +
   "1 Furtive Courier" + Environment.NewLine +
   "1 Glint Weaver" + Environment.NewLine +
   "1 Granite Witness" + Environment.NewLine +
   "1 Homicide Investigator" + Environment.NewLine +
   "1 Leering Onlooker" + Environment.NewLine +
   "1 Meddling Youths" + Environment.NewLine +
   "1 Novice Inspector" + Environment.NewLine +
   "1 Out Cold" + Environment.NewLine +
   "1 Reckless Detective" + Environment.NewLine +
   "1 The Pride of Hull Clade");
        }

        /// <summary>
        /// Get share of a red deck
        /// </summary>
        /// <param name="shareOfDeck">1 give 1/1, 2 give 1/2, 3 give 1/3</param>
        /// <returns></returns>
        private static string GetRedDeck(int shareOfDeck)
        {
            #region
            string deck =
                    "8 Mountain (OTJ) 284" + Environment.NewLine +
                    "2 Reckless Lackey (OTJ) 140" + Environment.NewLine +
                    "1 Goblin Trashmaster (ANB) 72" + Environment.NewLine +
                    "3 Tin Street Cadet (ANB) 87" + Environment.NewLine +
                    "3 Deadeye Duelist (OTJ) 119" + Environment.NewLine +
                    "2 Goblin Tunneler (ANB) 73" + Environment.NewLine +
                    "1 Magda, the Hoardmaster (OTJ) 133" + Environment.NewLine;

            if (shareOfDeck < 3)
                deck +=
                    "4 Mountain (OTJ) 284" + Environment.NewLine +
                    "4 Nest Robber (ANB) 79" + Environment.NewLine +
                    "2 Burn Bright (ANB) 68" + Environment.NewLine;


            if (shareOfDeck < 2)
                deck +=
                    "24 Mountain (OTJ) 284" + Environment.NewLine +
                    "2 Brazen Blademaster (LCI) 136" + Environment.NewLine +
                    //"2 Goblin Gang Leader (ANB) 70" + Environment.NewLine +
                    //"2 Panicked Altisaur (LCI) 159" + Environment.NewLine +
                    //"" + Environment.NewLine +
                    "2 Mine Raider (OTJ) 135" + Environment.NewLine +
                    "2 Molten Ravager (ANB) 78" + Environment.NewLine;
                    //"2 Plundering Pirate (LCI) 160" + Environment.NewLine;
                    //"2 Raid Bombardment (ANB) 82" + Environment.NewLine +
                    //"2 Ogre Battledriver (ANB) 80" + Environment.NewLine +
                    //"2 Rodeo Pyromancers (OTJ) 143" + Environment.NewLine;


            return deck;
            #endregion
        }
        /// <summary>
        /// Get share of a green deck
        /// </summary>
        /// <param name="shareOfDeck">1 give 1/1, 2 give 1/2, 3 give 1/3</param>
        /// <returns></returns>
        private static string GetGreenDeck(int shareOfDeck)
        {
            #region
            string deck =
                "10 Forest (LTR) 270" + Environment.NewLine +
                "2 Hardbristle Bandit (OTJ) 168" + Environment.NewLine +
                "2 Ankle Biter (OTJ) 153" + Environment.NewLine +
                "3 Bristlepack Sentry (OTJ) 156" + Environment.NewLine +
                "3 Jungle Delver (ANB) 99" + Environment.NewLine;

            if (shareOfDeck < 3)
                deck +=
                    "4 Forest (LTR) 270" + Environment.NewLine +
                    "4 Ilysian Caryatid (ANB) 98" + Environment.NewLine +
                    "2 Rootrider Faun (WOE) 182" + Environment.NewLine;

            if (shareOfDeck < 2)
                deck +=
                    "20 Forest (LTR) 270" + Environment.NewLine +
                    "2 Drover Grizzly (OTJ) 161" + Environment.NewLine +
                    "2 Intrepid Stablemaster (OTJ) 169" + Environment.NewLine +
                    "2 Woodland Mystic (ANB) 109" + Environment.NewLine + // G A
                    "2 Wildwood Patrol (ANB) 108" + Environment.NewLine +
                    //"2 Cactarantula (OTJ) 158" + Environment.NewLine +
                    //"2 Sentinel Spider (ANB) 104" + Environment.NewLine +
                    //"1 Earthshaker Dreadmaw (LCI) 183" + Environment.NewLine +
                    //"1 Prized Unicorn (ANB) 100" + Environment.NewLine +
                    //"1 Hulking Raptor (LCI) 191" + Environment.NewLine +
                    //"2 Rumbling Baloth (ANB) 103" + Environment.NewLine +
                    //"1 Gigantosaurus (ANB) 96" + Environment.NewLine +
                    "2 Giant Beaver (OTJ) 165" + Environment.NewLine;

            return deck;
            #endregion
        }
    }
}
