using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Assembler
{
    static class MainEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplachScreen());//new Main()
        }
    }
}
