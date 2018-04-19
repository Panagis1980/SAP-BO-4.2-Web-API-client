using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAP_BO_4._2_Web_API_client
{
    class CSVExport
    {
        private SAPDocumentList DocList { get; set; }

        public CSVExport(SAPDocumentList docList)
        {
            DocList = docList;
        }

        public CSVExport()
        {
        }

        public void ExportReportList(string Filename)
        {

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(Filename);
            char Delim = ';';

            sw.WriteLine("ReportID" + Delim + "Report Name" + Delim + "Path");

                foreach (SAPDocument doc in DocList)
                {
                    sw.WriteLine(doc.SI_ID + Delim + doc.SI_NAME  + Delim + doc.SI_PATH);                        
                }

                //Close the file
                sw.Close();
        }

    }
}
