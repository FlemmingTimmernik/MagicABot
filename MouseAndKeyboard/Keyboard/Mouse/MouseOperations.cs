using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;

namespace MouseOperations
{

    public class MouseOperations
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010,
            MOUSEEVENTF_WHEEL = 0x0800
    }
        public static bool END = false;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int X, int Y)
        {
            SetCursorPos(X, Y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static void Move(int x, int y)
        {
            mouse_event(
                       (int)MouseEventFlags.Move, x, y, 0, 0);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseWheelUp(int numberOfClicks)
        {

            for (int i = 0; i < numberOfClicks; i++)
            {
                mouse_event(
                    (int)MouseEventFlags.MOUSEEVENTF_WHEEL, 0, 0, 120, 0);
            }
        }
        public static void MouseWheelDown(int numberOfClicks)
        {

            for (int i = 0; i < numberOfClicks; i++)
            {
                mouse_event(
                    (int)MouseEventFlags.MOUSEEVENTF_WHEEL, 0, 0, -120, 0);
            }
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }

        }

        public static bool DoLeftMouseClick()
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            Thread.Sleep(50);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            return false;
        }
        public static bool DoMiddleMouseClick()
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleDown);
            Thread.Sleep(10);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleUp);
            return false;
        }

        public static void HoldLeftDown()
        {

            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
        }

        public static void HoldLeftUp()
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        public static void HoldRightDown()
        {

            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
        }

        public static void HoldRightUp()
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
        }


        public static bool DoLeftMouseClick(int x, int y)
        {

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(x, y));
            Thread.Sleep(20);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            Thread.Sleep(20);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            //MouseOperations.SetCursorPosition(mp);
            return false;

        }
        public static bool DoLeftMouseClick(int x, int y, int wait)
        {
            Thread.Sleep(wait);
            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(x, y));
            Thread.Sleep(20);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            Thread.Sleep(20);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            Thread.Sleep(wait);
            //MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static bool DoLeftMouseClickHoldMilliseconds(int milliseconds)
        {

            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            Thread.Sleep(20);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            Thread.Sleep(milliseconds);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

            return false;

        }

        public static bool DoLeftMouseClick(Point p)
        {
            return DoLeftMouseClick(p.X, p.Y);
        }

        public static bool DoLeftMouseClick(MousePoint mpPar)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(mpPar.X, mpPar.Y));
            Thread.Sleep(100);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            //MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static bool HoldLeftMouseButton()
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;



            Thread.Sleep(200);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);


            return false;

        }
        public static bool ReleaseLeftMouseButton()
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();

            Thread.Sleep(200);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);



            return false;

        }
        public static bool DoLeftDragMouseClick(int x1, int y1,int x2, int y2)
        {
            return DoLeftDragMouseClick(new MousePoint(x1, y1), new MousePoint(x2, y2));
        }
        public static bool DoLeftDragMouseClick(MousePoint start, MousePoint end)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            Thread.Sleep(50);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(start.X, start.Y));
            Thread.Sleep(50);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(end.X, end.Y));
            Thread.Sleep(150);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            Thread.Sleep(150);
            MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static bool DoRightDragMouseClick(int x1, int y1, int x2, int y2)
        {
            return DoRightDragMouseClick(new MousePoint(x1, y1), new MousePoint(x2, y2));
        }        

        public static bool DoRightDragMouseClick(MousePoint start, MousePoint end)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            Thread.Sleep(10);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(start.X, start.Y));
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
            Thread.Sleep(10);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(end.X, end.Y));
            Thread.Sleep(10);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            Thread.Sleep(10);
            MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static bool DoLeftDragMouseClick(Point start, Point end)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(start.X, start.Y));
            Thread.Sleep(200);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(end.X, end.Y));
            Thread.Sleep(200);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static bool DoRightMouseClick()
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            Thread.Sleep(20);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            
            return false;

        }


        public static bool DoRightMouseClick(MousePoint mpPar)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;
            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            Thread.Sleep(30);
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(mpPar.X, mpPar.Y));
            //Thread.Sleep(1000);
            Thread.Sleep(30);
            mouse_event(MOUSEEVENTF_RIGHTUP, mpPar.X, mpPar.Y, 0, 0);
            Thread.Sleep(30);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            Thread.Sleep(30);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
            Thread.Sleep(30);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            Thread.Sleep(30);
            MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static List<MousePoint> CreatePointsBetween(MousePoint mp1, MousePoint mp2,int steps = 5)
        {
            List<MousePoint> mpBetween = new List<MousePoint>();
            int startX = mp1.X;
            int endX = mp2.X;
            int startY = mp1.Y;
            int endY = mp2.Y;

            int distanceBetweenX = endX - startX;
            int distanceBetweenY = endY - startY;

            for (int i = 0; i < steps; i++)
            {
                MousePoint newPoint = new MousePoint(startX + (i * (distanceBetweenX / steps)), startY + (i * (distanceBetweenY / steps)));
                mpBetween.Add(newPoint);
            }
            return mpBetween;
        }

        public static bool DoLeftDragMouseClick(List<MouseOperations.MousePoint> connectedPoints, int millisecondsBetween)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.SetCursorPosition(connectedPoints[0]);
            WaitForNextAction(300);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            for (int i = 0; i < connectedPoints.Count; i++)
            {
                if (i % 5 == 0)
                    WaitForNextAction(millisecondsBetween * 4);
                WaitForNextAction(millisecondsBetween);
                MouseOperations.SetCursorPosition(connectedPoints[i]);

            }
            Thread.Sleep(millisecondsBetween * 2);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            Thread.Sleep(millisecondsBetween);
            MouseOperations.SetCursorPosition(mp);
            return false;

        }

        public static bool WaitForNextAction(int milliseconds)
        {
            int loops = milliseconds / 200;

            for (int i = 0; i < loops; i++)
            {
                Thread.Sleep(200);
                if (MouseOperations.IsMouseInUpperLeftCorner())
                {
                    MouseOperations.END = true;
                    return false;
                }
            }
            Thread.Sleep(milliseconds % 200);
            return true;
        }

        public static bool Waiting(double milliseconds)
        {
            DateTime starttime = DateTime.Now.AddMilliseconds(milliseconds);
            while (DateTime.Now.Ticks < starttime.Ticks)
            {
                if (IsMouseInUpperLeftCorner())
                    return false;
                Thread.Sleep(100);
            }
            return true;
        }

        public static bool IsMouseInUpperLeftCorner()
        {
            //if (MouseOperations.GetCursorPosition().Y < 3)
            //    if (MouseOperations.GetCursorPosition().X < 3)
            if (MouseOperations.GetCursorPosition().Y < 5)
                //if (MouseOperations.GetCursorPosition().X > 180)
                return true;
            return false;
        }



        internal static void ShowPositions(List<MouseOperations.MousePoint> mousePoints, int p)
        {
            for (int i = 0; i < mousePoints.Count; i++)
            {
                MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(mousePoints[i].X, mousePoints[i].Y));
                WaitForNextAction(p);

            }
        }

        internal static bool HoldLeftMouseClick(int x, int y)
        {
            if (MouseOperations.IsMouseInUpperLeftCorner())
                return true;

            // MouseOperations.MousePoint mp = MouseOperations.GetCursorPosition();
            MouseOperations.SetCursorPosition(new MouseOperations.MousePoint(x, y));
            //Thread.Sleep(1000);
            //MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            //MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            // MouseOperations.SetCursorPosition(mp);
            return false;
        }

        internal static bool ReleaseLeftMouseClick()
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            return false;
        }

    }
}