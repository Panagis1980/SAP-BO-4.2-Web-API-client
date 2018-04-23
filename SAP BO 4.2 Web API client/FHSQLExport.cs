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

                sw.Write("ReportID");
                sw.Write("*****************");
                sw.Write("DataProviderID");
                sw.Write("*****************");
                sw.Write("DataProviderName");
                sw.Write("*****************");
                sw.Write("ConnectionName");
                sw.Write("*****************");
                sw.Write("ConnectionType");
                sw.Write("*****************");
                sw.Write("FHSQL");
                sw.Write("#################");

                foreach (SAPDocument doc in DocList)
                {
                    //Write a line of text
                    foreach (SAPDataProvider dp in doc.DataProviderList)
                    {
                        sw.Write(doc.SI_ID);
                        sw.Write("*****************");
                        sw.Write(dp.ID);
                        sw.Write("*****************");
                        sw.Write(dp.Name);
                        sw.Write("*****************");
                        sw.Write(dp.DataSourceName);
                        sw.Write("*****************");
                        sw.Write(dp.DataSourceType);
                        sw.Write("*****************");
                        sw.Write(dp.sql);
                        sw.Write("#################");
                    }
                }

                //Close the file
                sw.Close();

        }

    }
}
