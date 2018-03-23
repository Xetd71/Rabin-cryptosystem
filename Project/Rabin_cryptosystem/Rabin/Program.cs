using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Rabin
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Rabin_Cryptosystem(args));
        }
    }
}
