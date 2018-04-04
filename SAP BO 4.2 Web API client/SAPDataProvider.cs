using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_BO_4._2_Web_API_client
{
    class SAPDataProvider
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DataSourceId { get; set; }
        public string DataSourceType { get; set; }
        public string DataSourceName { get; set; }
        public string sql { get; set; }

        public SAPDataProvider()
        {
            this.ID = string.Empty;
            this.Name = string.Empty;
            this.DataSourceId = string.Empty;
            this.DataSourceType = string.Empty;
            this.DataSourceName = string.Empty;
            this.sql = string.Empty;
        }
    }
}
