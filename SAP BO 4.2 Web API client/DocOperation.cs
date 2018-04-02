using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace SAP_BO_4._2_Web_API_client
{
    class DocOperation
    {

        WebAPIconnection webAPIconnect;
        public DocOperation (WebAPIconnection webAPIconnect)
        {
            this.webAPIconnect = webAPIconnect;
        }

        public int GetDocumentList(string FolderId, ref SAPDocumentList docList)
        {
            string send = string.Empty;
            string recv = string.Empty;

            send = "<attrs xmlns=\"http://www.sap.com/rws/bip\">" +
                "<attr name = \"query\" type = \"string\">" +
                "SELECT TOP 50000 SI_NAME, SI_ID, SI_KIND, SI_UNIVERSE, SI_DSL_UNIVERSE, " +
                "SI_FHSQL_RELATIONAL_CONNECTION, SI_PARENTID, SI_FILES, SI_PARENT_FOLDER " +
                "from CI_INFOOBJECTS, CI_SYSTEMOBJECTS, CI_APPOBJECTS WHERE si_kind = 'Webi' " +
                "AND SI_INSTANCE = 0 AND SI_ANCESTOR = " + FolderId +
                "</attr ></attrs>";
          
            try
            {
                webAPIconnect.Send("POST", "/biprws/v1/cmsquery", send,  "application/xml", "application/JSON");
                recv = webAPIconnect.responseContent;

                docList = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SAPDocumentList>(recv);
                foreach (var item in docList.entries)
                {
                    item.SI_PATH = GetDocumentPath(FolderId, item.SI_ID);
                }

                return 0;

            } catch
            {
                return 3;
            }
        }

        public string[] GetFolderDetails(string FolderId)
        {
            string[] FolderDetails = new string[2];
            string send = string.Empty;            
            XmlDocument XmlResponse = new XmlDocument();
            try
            {
                webAPIconnect.Send("GET", "/biprws/v1/folders/"+ FolderId, send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);

                XmlNodeList elemList = XmlResponse.GetElementsByTagName("attr");
                foreach (XmlNode node in elemList)
                {
                    if (node.Attributes["name"].Value.Equals("name"))
                    {
                        FolderDetails[0] = node.InnerText;
                    }
                    else if (node.Attributes["name"].Value.Equals("parentid"))
                    {
                        FolderDetails[1] = node.InnerText;
                    }
                }
                return FolderDetails;
            }
            catch (Exception e)
            {
                webAPIconnect.Logoff();
                FolderDetails[0] = e.StackTrace;
                return FolderDetails;
            }
        }

        public string GetDocumentPath(string ancestorFolder, string DocumentId)
        {
            string path = string.Empty;
            string parentFolder = string.Empty;
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();
            try
            {
                webAPIconnect.Send("GET", "/biprws/v1/documents/" + DocumentId, send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);

                XmlNodeList elemList = XmlResponse.GetElementsByTagName("attr");
                foreach (XmlNode node in elemList)
                {
                    if (node.Attributes["name"].Value.Equals("parentid"))
                    {
                        parentFolder = node.InnerText;
                        break;
                    }
                }

                while (!parentFolder.Equals(ancestorFolder))
                {
                    string[] details = GetFolderDetails(parentFolder);
                    path += details[0] + "/";
                    parentFolder = details[1];
                }
                path = GetFolderDetails(ancestorFolder)[0] + "/" + path;
                return path;
            }
            catch (WebException e)
            {
                return e.StackTrace;
            }
        }
    }
}
