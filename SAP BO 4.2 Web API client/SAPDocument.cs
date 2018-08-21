using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_BO_4._2_Web_API_client
{
    class SAPDocument
    {        
            public string SI_ID { get; set; }
            public string SI_NAME { get; set; }
            public string SI_PARENTID { get; set; }
            public string SI_KIND { get; set; }
            public string SI_PARENT_FOLDER { get; set; }
            public string SI_PATH { get; set; }
            public string SI_UPDATE_TS { get; set; }
            public List<SAPDataProvider> DataProviderList { get; set; }
    }
}
