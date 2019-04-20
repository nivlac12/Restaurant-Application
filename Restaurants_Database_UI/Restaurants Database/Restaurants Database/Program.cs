using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurants_Database
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            string shellName = "U:\\CIS 560\\Project\\Restaurant - Application\\Restaurants_Database_UI\\Restaurants Database\\Restaurants Database\\BuildDatabase.ps1";
            System.Diagnostics.Process.Start("C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe ", shellName);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new cDataBaseForm());
        }
    }
}
