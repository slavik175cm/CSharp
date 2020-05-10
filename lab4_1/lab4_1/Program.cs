using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
namespace lab4_1
{
    static class Program
    {

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric metric);

        public enum SystemMetric
        {
            VirtualScreenWidth = 78, // CXVIRTUALSCREEN 0x0000004E 
            VirtualScreenHeight = 79, // CYVIRTUALSCREEN 0x0000004F 
        }

        [STAThread]
        static void Main()
        {
            int desktopWidth = GetSystemMetrics(SystemMetric.VirtualScreenWidth);
            int desktopHeight = GetSystemMetrics(SystemMetric.VirtualScreenHeight);
            TicTacToe newGame = new TicTacToe((desktopWidth - desktopHeight) / 2, 0, desktopHeight);
            MessageBox.Show("Press OK to exit");
        }
    }
}
