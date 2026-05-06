using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseOperations;

namespace Magic
{
    internal class CardCollection
    {

        public CardCollection() { }

        public void AddCards(int numberOfPages)
        {
            numberOfPages = Math.Min(numberOfPages, 11);
            numberOfPages = Math.Max(numberOfPages, 1);

            for (int i = 0; i < numberOfPages; i++)
            {
                for (int y = 0; y < 3; y++)
                    for (int x = 0; x < 7; x++)
                    {
                        MouseOperations.MouseOperations.DoLeftMouseClick(130 + x * 206, 330 + y * 296);
                        Thread.Sleep(50);
                    }

                //Next cardPage
                if (i < numberOfPages-1)
                {
                    MouseOperations.MouseOperations.DoLeftMouseClick(1470, 633);
                    Thread.Sleep(500);
                }

            }
        }

        private void DoLeftMouseClick(int sleepBefore, int sleepAfter, int x, int y)
        {
            Thread.Sleep(sleepBefore);
            MouseOperations.MouseOperations.DoLeftMouseClick(x, y);
            Thread.Sleep(sleepAfter);
        }

        public void MakeFullCollection()
        {
          

            for (int q = 2; q <= 4; q++)
            {
                int counterFor11FullPages = 1;
                bool finishWithCurrentCollection = false;
                while (!finishWithCurrentCollection)
                {
                    //ClickMyDeck
                    DoLeftMouseClick(400, 2500, 210, 420);

                    //Reset arrow (Can be white from last time)
                    for (int x = 0; x < 14; x++)
                    {
                        Cursor.Position = new Point(1430 + x * 5,700);
                        Thread.Sleep(100);
                    }


                    //SelectFormat
                    DoLeftMouseClick(200, 300, 1700, 134);

                    //ScrollDown
                    Cursor.Position = new Point(1600, 310);
                    MouseOperations.MouseOperations.MouseWheelDown(50);
                    //Select DirectGame
                    DoLeftMouseClick(500, 300, 1600, 310);
                    //Click On Name
                    DoLeftMouseClick(200, 300, 1750, 133);
                    //Write Name (Collection1-1)
                    string nameOfCurrentDeck = "Collection " + q.ToString() + "-" + counterFor11FullPages;
                    SendKeys.Send(nameOfCurrentDeck);
                    SendKeys.Flush();
                    
                    //clickOn Search
                    DoLeftMouseClick(300, 200, 100, 137);
                    //Write q:x where x is the qauntity of the cards for the collection
                    SendKeys.Send("q:" + q.ToString());
                    SendKeys.Flush();
                    //Select Land
                    DoLeftMouseClick(300, 200, 626, 137);
                    //Deselect Autoassign
                    DoLeftMouseClick(300, 200, 133, 331);
                    //Remove select Land
                    DoLeftMouseClick(300, 500, 626, 137);
                    //Start Selecting Until 11 or missing arrow
                    //If untill 11 then collection1-x will be made where x is the next, if x is higher than 0 go x times 11 pages forward
                    for (int i = 0; i < (counterFor11FullPages-1)*11; i++)
                    {
                        //Click next page
                        DoLeftMouseClick(200, 500, 1470, 635);

                    }

                    int pageCounter = 0;

                    while (pageCounter < 11)
                    {
                        SelectFullPage();

                        bool isArrowVisible = IsArrowVisible();

                        if (isArrowVisible)
                        {
                            //Click Next page                            
                            DoLeftMouseClick(200, 500, 1470, 635);
                            pageCounter++;
                        }
                        else
                            break;
                    }
                    //Click Done
                    DoLeftMouseClick(5000, 1000, 1730, 1010);
                    DoLeftMouseClick(200, 5000, 1100, 667);

                    if (pageCounter == 11)
                    {
                        counterFor11FullPages++;
                    }
                    else
                        finishWithCurrentCollection = true;

                }

                //Repeat for q2, q3 and q4
            }         

            
        }

        private static bool IsArrowVisible()
        {
            Color c = ScreenshotHelper.ScreenshotHelper.GetSinglePixelFromScreen(new Point(1470, 635));
            return c.R < 10 && c.B < 10 && c.G < 10;
        }

        private void SelectFullPage()
        {
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 7; x++)
                {
                    MouseOperations.MouseOperations.DoLeftMouseClick(130 + x * 206, 330 + y * 296);
                    Thread.Sleep(150);
                    if (Cursor.Position.Y < 5)
                        Thread.Sleep(10000);
                }
        }
    }
}
