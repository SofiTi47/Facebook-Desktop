using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper;

// $G$ RUL-007 (-40) Late submission (-1 days).
// $G$ RUL-004 (-10) Wrong doc file name. should be named as the solution.
// $G$ RUL-999 (-20) The program is not exception proof.
// $G$ THE-001 (-30) grade 70 on patterns selection / accuracy in implementation / description / document / diagrams (50%) (see comments in document)

namespace BasicFacebookFeatures
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FacebookService.s_UseForamttedToStrings = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
