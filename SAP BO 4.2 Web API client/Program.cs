using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAP_BO_4._2_Web_API_client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            WebAPIconnection webapi = new WebAPIconnection("Administrator", "POLOp0l0", "http://vmbi42sp4:6405");
            webapi.Logon();
            DocOperation docop = new DocOperation(webapi);
            SAPDocumentList docList = new SAPDocumentList();
            docop.GetDocumentList("5388", ref docList);
            foreach (SAPDocument doc in docList)
            {
                Console.WriteLine(doc.SI_PATH);
            }
            
            webapi.Logoff();
        }
    }
}
