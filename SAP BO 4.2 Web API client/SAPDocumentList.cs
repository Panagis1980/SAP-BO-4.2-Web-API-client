using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_BO_4._2_Web_API_client
{
    class SAPDocumentList : IEnumerable
    {
        public SAPDocumentList()
        {
            this.entries = new List<SAPDocument>();
        }

        public List<SAPDocument> entries { get; set; }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)entries).GetEnumerator();
        }
    }
}
