using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Magic
{
    internal static class LookAndClick
    {
        public enum ScreenStates { JUST_STARTED, MAIN_SCREEN, BLOOMBURROW_FIRST_TIME, LOGIN_SCREEN };

        public static bool running = true;

        /// <summary>
        /// Check if the Condition for stop is met
        /// </summary>
        private static void CheckIfIsRunning()
        {
            if (Cursor.Position.Y < 5)
                running = false;
        }

        #region BloomBurrow LoginFirstTime()

        public static bool BloomBurrowLoginFirstTime()
        {
            bool redT = BloomBurrowcheck1();
            bool greenO = BloomBurrowcheck2();
            return redT && greenO;
        }

        /// <summary>
        /// Look for red T on BloomBurrow
        /// </summary>
        /// <returns></returns>
        private static bool BloomBurrowcheck1()
        {
            //Look for the red "T" 
            bool test = CheckAreaWithTolerance(new Point(1530, 840), new Size(3, 7), 255, 25, 0, 5);
            return test;
        }
        /// <summary>
        /// Look for the green "O" 
        /// </summary>
        /// <returns></returns>
        private static bool BloomBurrowcheck2()
        {
            bool test = CheckAreaWithTolerance(new Point(1469, 578), new Size(1, 1), 144, 206, 141, 3);
            return test;
        }
        #endregion

        #region MainScreen

        public static bool MainScreen()
        {
            bool redT = MainScreencheck1();
            // bool greenO = MainScreencheck2();
            return redT;// && greenO;
        }

        /// <summary>
        /// Look for pixel MainScreen in XP Daily Symbol
        /// </summary>
        /// <returns></returns>
        private static bool MainScreencheck1()
        {
            //return CheckAreaWithTolerance(new Point(1255, 852), new Size(1, 1), 255, 197, 0, 2);
            return CheckAreaWithTolerance(new Point(56, 78), new Size(10, 1), 255, 211, 140, 3);
        }
        /// <summary>
        /// Look for pixel MainScreen in XP Daily Symbol
        /// </summary>
        /// <returns></returns>
        private static bool MainScreencheck2()
        {
            return CheckAreaWithTolerance(new Point(1248, 854), new Size(1, 1), 194, 101, 7, 2);
        }
        #endregion

        private static bool CheckAreaWithTolerance(Point upperRightCorner, Size lookingArea, int red, int green, int blue, int tolerance)
        {
            running = true;
            CheckIfIsRunning();

            if (Cursor.Position.Y < 5)
                running = false;

            int howMuchColor = CheckAreaForColor(upperRightCorner, lookingArea, red - tolerance, red + tolerance, green - tolerance, green + tolerance, blue - tolerance, blue + tolerance);
            int pixelsInArea = lookingArea.Width * lookingArea.Height;

            if (howMuchColor == pixelsInArea)
                return true;
            else
                return false;
        }

        public static bool IsFirstDeckFavourite()
        {
            return CheckAreaWithTolerance(new Point(345, 527), new Size(1, 1), 255, 240, 112, 3);
        }

       
        //Look after the playbutton in the lower right corner on the Homescreen, a good indicator that you are logged in
        public static bool WaitForPlayButtonOnHomeScreen(bool clickButton)
        {
            Stopwatch howLongWaitForHomescreenButton = Stopwatch.StartNew();

            running = true;
            //Leder efter playknappen på hovedskærmen
            while (true)
            {
                if (running)
                    Thread.Sleep(500);

                if (howLongWaitForHomescreenButton.ElapsedMilliseconds > 60000)
                    return false;

                if (IsPlayButtonOnHomeScreen())
                {
                    if (clickButton)
                        DoLeftMouseClick(100, 1000, 1722, 1005);
                    return true;
                }
            }            
        }

        public static bool IsMyDecksUnfolded()
        {
            return CheckAreaWithTolerance(new Point(169, 441), new Size(2, 2), 204, 204, 204, 3);
        }

        public static bool IsBotMatchAvailable()
        {
            return CheckAreaWithTolerance(new Point(1677, 764), new Size(1, 3), 176, 177, 177, 3);
        }

        public static bool IsPlayButtonOnHomeScreen()
        {
            if (Cursor.Position.Y < 5)
                running = false;

            int howMuchWhite = CheckAreaForColor(new Point(1722, 1005), new Size(28, 5), 252, 255, 252, 255, 252, 255);

            if (howMuchWhite > 10 && howMuchWhite < 28 * 5 - 20)
                return true;
            else
                return false;
        }



        //Check for the Big Arena text on login Screen
        public static bool LookForLoginScreen()
        {
            return LoginScreenCheck1();

            Point upperRightCornerInTheMiddleOfTheEOfArena = new Point(907, 391);
            Size size = new Size(7, 3);






            running = true;

            if (Cursor.Position.Y < 5)
                running = false;
            int howMuchWhite = CheckAreaForColor(upperRightCornerInTheMiddleOfTheEOfArena, size, 225, 240, 225, 240, 225, 240);

            if (howMuchWhite > 10 && howMuchWhite > size.Width * size.Height - 2)
                return true;
            else
                return false;
        }

        private static bool LoginScreenCheck1()
        {
            return CheckAreaWithTolerance(new Point(691, 393), new Size(5, 5), 210, 48, 32, 4);
        }

        public static bool LoginScreenBannedCards_27082024()
        {
            return CheckAreaWithTolerance(new Point(1286, 325), new Size(1, 1), 159, 150, 147, 4);
        }


        public static void LookForOkayButtonIfAnnouncement()
        {
            Point upperRightCornerInTheMiddleOfTheEOfArena = new Point(948, 923);
            Size size = new Size(3, 10);

            running = true;
            //Looking for Okay Button when Announcement
            while (true)
            {
                if (Cursor.Position.Y < 5)
                    running = false;
                int howMuchWhite = CheckAreaForColor(upperRightCornerInTheMiddleOfTheEOfArena, size, 250, 255, 250, 255, 250, 255);

                if (howMuchWhite > 10 && howMuchWhite > size.Width * size.Height - 2)
                {
                    break;
                }
                else
                {
                    if (running)
                        Thread.Sleep(500);
                    else
                        break;
                }
            }
        }

        private static void DoLeftMouseClick(int waitBefore, int waitAfter, int x = -1, int y = -1)
        {
            Thread.Sleep(waitBefore);
            if (y != -1)
            {
                Cursor.Position = new Point(x, y);
                Thread.Sleep(50);
            }
            MouseOperations.MouseOperations.DoLeftMouseClick();
            Thread.Sleep(waitAfter);
        }


        private static int CheckAreaForColor(Point UpperRightCorner, Size size, int redMin, int redMax, int greenMin, int greenMax, int blueMin, int blueMax)
        {
            int countToleratedColor = 0;
            try
            {

                var croppedPicture = ScreenshotHelper.ScreenshotHelper.GetCroppedScreenShot2(UpperRightCorner, size);
                if (croppedPicture != null)
                    for (int x = 0; x < croppedPicture.Width; x++)
                        for (int y = 0; y < croppedPicture.Height; y++)
                        {
                            var c = croppedPicture.GetPixel(x, y);
                            if (c.R >= redMin && c.R <= redMax && c.G >= greenMin && c.G <= greenMax && c.B >= blueMin && c.B <= blueMax)
                                countToleratedColor++;
                        }
                return countToleratedColor;
            }
            catch
            {
                return 0;
            }
        }

        internal static bool IsMyDecksUnfoldedOnDeckCreation()
        {
            return CheckAreaWithTolerance(new Point(187, 432), new Size(3, 3), 203, 204, 203, 3);
        }

        internal static bool DuskMourneFirstLogin()
        {
            return CheckAreaWithTolerance(new Point(1652, 553), new Size(1, 1), 193, 223, 199, 3);
        }

        internal static bool LeyLineBanned()
        {
            return CheckAreaWithTolerance(new Point(856, 475), new Size(1, 1), 249, 159, 155, 15);
        }

        internal static bool MonstrousRageBanned()
        {
            return CheckAreaWithTolerance(new Point(1086, 324), new Size(2, 2), 249, 168, 143, 6);
        }
    }
}
