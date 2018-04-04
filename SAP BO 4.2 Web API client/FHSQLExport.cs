using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_BO_4._2_Web_API_client
{
    class FHSQLExport
    {
        private SAPDocumentList DocList { get; set; }

        public FHSQLExport(SAPDocumentList docList)
        {
            DocList = docList;
        }

        public FHSQLExport()
        {
        }

        public void ExportFHSQL(string Filename)
        {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(Filename);

                sw.WriteLine("ReportID");
                sw.WriteLine("*****************");
                sw.WriteLine("DataProviderID");
                sw.WriteLine("*****************");
                sw.WriteLine("DataProviderName");
                sw.WriteLine("*****************");
                sw.WriteLine("ConnectionName");
                sw.WriteLine("*****************");
                sw.WriteLine("ConnectionType");
                sw.WriteLine("*****************");
                sw.WriteLine("FHSQL");
                sw.WriteLine("#################");

                foreach (SAPDocument doc in DocList)
                {
                    //Write a line of text
                    foreach (SAPDataProvider dp in doc.DataProviderList)
                    {
                        sw.WriteLine(doc.SI_ID);
                        sw.WriteLine("*****************");
                        sw.WriteLine(dp.ID);
                        sw.WriteLine("*****************");
                        sw.WriteLine(dp.Name);
                        sw.WriteLine("*****************");
                        sw.WriteLine(dp.DataSourceName);
                        sw.WriteLine("*****************");
                        sw.WriteLine(dp.DataSourceType);
                        sw.WriteLine("*****************");
                        sw.WriteLine(dp.sql);
                        sw.WriteLine("#################");
                    }
                }

                //Close the file
                sw.Close();

        }

    }
}
