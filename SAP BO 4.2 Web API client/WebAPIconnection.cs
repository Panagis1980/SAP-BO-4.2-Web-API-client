using System.IO;
using System.Net;
using System.Xml;

namespace SAP_BO_4._2_Web_API_client
{
    class WebAPIconnection
    {
        private string userName;
        private string password;
        public HttpWebRequest request { get; private set; }
        public HttpWebResponse response { get; private set; }
        public string responseContentType { get; private set; }
        public string responseContent { get; private set; }
        public string logonToken { get; private set; }

        public string URI;
        public static string nameSpace = "http://www.sap.com/rws/bip";

        public WebAPIconnection(string userName, string password, string URI)
        {
            this.userName = userName;
            this.password = password;
            this.logonToken = string.Empty;
            this.URI = URI;
        }

        public WebAPIconnection()
        {
            this.logonToken = string.Empty;
        }

        #region Logon
        /* uses userName and password to retrieve the logonToken
         * 
         * On success: returns 0
         * On failure: returns a positive int
         */
        public int Logon()
        {
            if (logonToken != "")
            {
                // already has a logon token. Need to explicitly logoff before proceeding.
                return 1;
            }

            XmlDocument docGET;
            XmlDocument docPOST;
            XmlNamespaceManager nsmgrGET, nsmgrPOST;
            XmlNodeList nodeList;
            int result;

            docGET = new XmlDocument();
            docPOST = new XmlDocument();

            // send request for logon form
            result = CreateWebRequest(null, docGET, "GET", "/biprws/logon/long");
            if (result != 0)
            {
                return 2;
            }
            // docGET is the response xml from the server
            // need to fill the fields in now and send it back

            // create namespace manager (this is necessary in order to use XmlDocument)
            nsmgrGET = new XmlNamespaceManager(docGET.NameTable);
            nsmgrGET.AddNamespace("rest", nameSpace);

            // search for the userName node
            nodeList = docGET.SelectNodes("//rest:attr[@name='userName']", nsmgrGET);
            if (nodeList.Count != 1)
            {
                // there is either no userName attribute or multiple. Either way, return error
                return 3;
            }
            // write into the attribute (i.e. filling out the xml)
            nodeList.Item(0).InnerText = userName;

            // repeat for password attribute
            nodeList = docGET.SelectNodes("//rest:attr[@name='password']", nsmgrGET);
            if (nodeList.Count != 1)
            {
                return 3;
            }
            nodeList.Item(0).InnerText = password;

            // POST the document that was just filled out to the server
            // in order to retrieve the logon token
            result = CreateWebRequest(docGET, docPOST, "POST", "/biprws/logon/long");

            // a separate namespace manager keeps things cleaner
            nsmgrPOST = new XmlNamespaceManager(docPOST.NameTable);
            nsmgrPOST.AddNamespace("rest", nameSpace);

            // extract logon token from the server's response
            nodeList = docPOST.SelectNodes("//rest:attr[@name='logonToken']", nsmgrPOST);
            if (nodeList.Count != 1 || nodeList.Item(0).InnerText == "")
            {
                // there should be exactly one non-empty result
                return 4;
            }

            // concatenate with double quotes and store as member
            logonToken = "\"" + nodeList.Item(0).InnerText + "\"";

            return 0; // success
        }

        /* sends a request to logoff from the server
         * also releases logonToken
         * 
         * it is safe to call Logon() again after calling using this method
         */
        public int Logoff()
        {
            XmlDocument empty = new XmlDocument();
            XmlDocument empty2 = new XmlDocument();
            int result = CreateWebRequest(empty, empty2, "POST", "/biprws/logoff");
            if (result == 0)
            {
                this.logonToken = "";
            }
            return result;
        }

        public bool isLoggedOn()
        {
            return (this.logonToken != "");
        }

        #endregion

        #region CreateWebRequest
        /* sends an http request and 
         * retrieves the response from the server at (this.URI + URIExtension)
         * supports GET, PUT, and POST methods
         * 
         * send: [optional] the xml document that will be sent as the body
         *       used in POST and PUT methods only
         * recv: the xml document to store the server's response in
         *       this parameter WILL be overwritten
         * method: "GET", "PUT", or "POST"
         * URIExtension: the URI after the default string. E.g. the "biprws/logon/long" of a log on URI
         * 
         * On success: returns 0
         * On failure: returns a positive int
         */
        public int CreateWebRequest(XmlDocument send, XmlDocument recv, string method, string URIExtension)
        {

            reset();
            if (method != "GET" && method != "PUT" && method != "POST")
            {
                return 1; // unsupported method
            }

            request = (HttpWebRequest)WebRequest.Create(URI + URIExtension);
            request.Method = method;
            request.ContentType = "application/xml";
            request.Accept = "application/xml";
            if (logonToken != "")
            {
                request.Headers["X-SAP-LogonToken"] = logonToken;
            }

            // if the method is post or put, the body must be prepared
            if (method == "POST" || method == "PUT")
            {
                // turn the send xml into a stream and put it in request
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(send.OuterXml);
                Stream requestStream;
                try
                {
                    requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                catch
                {
                    return 2;
                }
            }

            // send the request and retrieve the response
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // logging off has no response stream and an attempt to read it will fail
                    if (URIExtension != "/biprws/logoff")
                    {
                        recv.Load(response.GetResponseStream());
                    }
                }
                else
                {
                    return 3;
                }
            }
            catch
            {
                return 4;
            }

            return 0;
        }
        #endregion

        #region migratedFunctions
        public int Send(string method, string URIExtension, string send, string contentType, string accept )
        {
            reset();
            if (method != "GET" && method != "PUT" && method != "POST")
            {
                return 1; // unsupported method
            }

            request = (HttpWebRequest)WebRequest.Create(URI + URIExtension);
            request.Method = method;
            request.ContentType = contentType;
            request.Accept = accept;
            if (logonToken != "")
            {
                request.Headers["X-SAP-LogonToken"] = logonToken;
            }

            // if the method is post or put, the body must be prepared
            if (method == "POST" || method == "PUT")
            {
                // turn the send xml into a stream and put it in request
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(send);
                Stream requestStream;
                try
                {
                    requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                catch
                {
                    return 2;
                }
            }

            // send the request and retrieve the response
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //System.Windows.Forms.MessageBox.Show(response.ContentType);
                    // logging off has no response stream and an attempt to read it will fail
                    if (URIExtension != "/biprws/logoff")                    
                    {
                        //recv.Load(response.GetResponseStream());
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                            responseContent = reader.ReadToEnd();
                        }
                        responseContentType = response.ContentType;
                    }
                }
                else
                {
                    return 3;
                }
            }
            catch
            {
                return 4;
            }

            return 0;
        }

        private void reset()
        {
            this.request = null;
            this.response = null;
            this.responseContent = null;
            this.responseContentType = null;
        }

        #endregion
    }
}
