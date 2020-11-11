using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catalog
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Login_Form fLogin = new Login_Form();
            DialogResult result = fLogin.ShowDialog();
            if (result == DialogResult.OK) 
            {

                Application.Run(new MainForm());
            }
            //unimplemented part
            else if (result == DialogResult.Yes) 
            {
                //attempt at improving the UI
                Application.Run(new FormUser()); 
            }
            else
            {
                Application.Exit();  
            }
        }
    }
}
