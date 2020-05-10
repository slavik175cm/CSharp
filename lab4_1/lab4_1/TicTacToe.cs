using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Timers;
namespace lab4_1
{
    class TicTacToe
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr dc);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point pt);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        public int X { get; set; }
        public int Y {get; set; }
        public int Width{get; set; }
        private System.Timers.Timer timer;
        private List<Point> circles = new List<Point>();
        private List<Point> crosses = new List<Point>();
        private bool drawing = false;
        private bool isFirstPlayerTurn = true;
        private Pen pen = new Pen(Brushes.Red) {Width = 10};

        public TicTacToe(int x, int y, int width)
        {
            X = x;
            Y = y;
            Width = width;
            SetTimer();
        }

        private void SetTimer()
        {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += Draw;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private Point GetCenter(Point point)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (point.X < X + Width * i / 3 || point.Y < Y + Width * j / 3 || point.X > X + Width * (i + 1) / 3 || point.Y > Y + Width * (j + 1) / 3) continue;
                    return new Point(X + Width * i / 3 + Width / 6, Y + Width * j / 3 + Width / 6);
                }
            }
            return new Point(-1, -1);
        }

        private void Draw(Object source, ElapsedEventArgs e)
        {
            IntPtr desktopDC = GetDC(IntPtr.Zero);
            Graphics graphics = Graphics.FromHdc(desktopDC);

            if (GetAsyncKeyState(Keys.LButton) != 0)
            {
                GetCursorPos(out Point cursorPos);
                cursorPos = GetCenter(cursorPos);
                if (cursorPos.X != -1 && !crosses.Contains(cursorPos) && !circles.Contains(cursorPos))
                {
                    if (isFirstPlayerTurn)
                    {
                        crosses.Add(cursorPos);
                    } else
                    {
                        circles.Add(cursorPos);
                    }
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
            }

            if (GetAsyncKeyState(Keys.Escape) != 0)
            {
                circles.Clear();
                crosses.Clear();
            }

            if (drawing) return;
            drawing = true;

            graphics.DrawLine(pen, X, Y + Width / 3, X + Width, Y + Width / 3);
            graphics.DrawLine(pen, X, Y + Width * 2 / 3, X + Width, Y + Width * 2 / 3);
            graphics.DrawLine(pen, X + Width / 3, Y, X + Width / 3, Y + Width);
            graphics.DrawLine(pen, X + Width * 2 / 3, Y, X + Width * 2 / 3, Y + Width);
            for (int i = 0; i < crosses.Count; i++)
            {
                graphics.DrawLine(pen, crosses[i].X - Width / 6 + 20, crosses[i].Y - Width / 6 + 20, crosses[i].X + Width / 6 - 20, crosses[i].Y + Width / 6 - 20);
                graphics.DrawLine(pen, crosses[i].X - Width / 6 + 20, crosses[i].Y + Width / 6 - 20, crosses[i].X + Width / 6 - 20, crosses[i].Y - Width / 6 + 20);
            }
            for (int i = 0; i < circles.Count; i++)
            {
                graphics.DrawEllipse(pen, circles[i].X - Width / 6 + 20, circles[i].Y - Width / 6 + 20, Width / 3 - 40, Width / 3 - 40);
            }

            graphics.Dispose();
            ReleaseDC(desktopDC);
            drawing = false;
        }
        
    }
}
