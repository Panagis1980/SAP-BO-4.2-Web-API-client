using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SAP_BO_4._2_Web_API_client
{
    class DocOperation
    {

        WebAPIconnection webAPIconnect;
        public DocOperation (WebAPIconnection webAPIconnect)
        {
            this.webAPIconnect = webAPIconnect;
        }

        public SAPDocumentList GetDocumentList(string FolderId)
        {
            SAPDocumentList docList;
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
                foreach (SAPDocument item in docList.entries)
                {
                    item.SI_PATH = GetDocumentPath(FolderId, item.SI_ID);
                    item.DataProviderList = GetDocumentDataproviders(item);
                }

                return docList;

            } catch (Exception e)
            {
                Debug.WriteLine(recv);
                Debug.WriteLine(e.Message);
                Debug.Flush();
                return null;
            }
        }

        public SAPDocumentList GetDocumentListInfostore(string FolderId)
        {
            SAPDocumentList docList = new SAPDocumentList();
            string send = string.Empty;
            string recv = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();
          
            try
            {
                Debug.WriteLine("In Document Fetch from infostore...");
                Debug.Flush();
                webAPIconnect.Send("GET", "/biprws/infostore/"+ FolderId + "/children", send, "application/xml", "application/xml");              
                TextReader reader = new StringReader(webAPIconnect.responseContent);                
                XmlSerializer serializer = new XmlSerializer(typeof(feed));
                feed deserializedEntries = serializer.Deserialize(reader) as feed;
                if (deserializedEntries.entry == null)
                {
                    Debug.WriteLine("Null deserialized Entries");
                    Debug.Flush();
                } else
                {
                    Debug.WriteLine("Deserialized Entries:" + deserializedEntries.entry.Length);
                    Debug.Flush();
                    foreach (var entry in deserializedEntries.entry)
                    {
                        SAPDocument doc = new SAPDocument();
                        string cuid = string.Empty;
                        string name = string.Empty;
                        string description = string.Empty;
                        string id = string.Empty;
                        string type = string.Empty;

                        foreach (var attr in entry.content.attrs)
                        {
                            switch (attr.name)
                            {
                                case "type":
                                    type = attr.Value;
                                    break;
                                case "name":
                                    name = attr.Value;
                                    break;
                                case "description":
                                    description = attr.Value;
                                    break;
                                case "id":
                                    id = attr.Value;
                                    break;
                                case "cuid":
                                    cuid = attr.Value;
                                    break;
                                default:
                                    break;
                            }

                        }

                        if (type.Equals("Webi"))
                        {
                            docList.entries.Add(GetDocumentInfo(id));
                            Debug.WriteLine("Doc ID");
                            Debug.Flush();
                        }
                        else if (type.Equals("Folder"))
                        {
                            SAPDocumentList docListInner = GetDocumentListInfostore(id);
                            Debug.WriteLine("Folder ID");
                            Debug.Flush();
                            foreach (SAPDocument docInner in docListInner)
                            {
                                docList.entries.Add(docInner);
                            }

                        }
                        
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error in document fetch from Infostore:" + e.StackTrace);
                Debug.WriteLine("Response:" + webAPIconnect.responseContent);
                Debug.WriteLine(e.Message);
                Debug.Flush();
            }
            return docList;
        }

        public string[] GetFolderDetails(string FolderId)
        {
            string[] FolderDetails = new string[2];
            string send = string.Empty;            
            XmlDocument XmlResponse = new XmlDocument();

            try
            {
                webAPIconnect.Send("GET", "/biprws/v1/folders/" + FolderId, send, "application/xml", "application/xml");
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
                Debug.WriteLine(e.Message);
                Debug.Flush();
                return null;
            }

        }
        public SAPDocument GetDocumentInfo(string DocumentId)
        {
            SAPDocument doc = new SAPDocument();
            string connName = string.Empty;
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            try
            {
                webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + DocumentId, send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);

                XmlNode document = XmlResponse.GetElementsByTagName("document").Item(0);
                XmlNodeList properties = document.ChildNodes;
                foreach (XmlNode child in properties)
                {
                    switch (child.Name)
                    {
                        case "id":
                            doc.SI_ID = child.InnerText;
                            break;
                        case "name":
                            doc.SI_NAME = child.InnerText;
                            break;
                        case "path":
                            doc.SI_PATH = child.InnerText;
                            break;
                        default:
                            break;
                    }
                }
                doc.DataProviderList = GetDocumentDataproviders(doc);
                return doc;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.Flush();
                return null;
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.Flush();
                return null;
            }

        }

        public List<SAPDataProvider> GetDocumentDataproviders(SAPDocument doc)
        {

            List<SAPDataProvider> DPList = new List<SAPDataProvider>();
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + doc.SI_ID +"/dataproviders", send, "application/xml", "application/xml");
            XmlResponse.LoadXml(webAPIconnect.responseContent);

            XmlNodeList elemList = XmlResponse.GetElementsByTagName("dataprovider");
            if (elemList.Count > 0)
            {
                foreach (XmlNode node in elemList)
                {
                    SAPDataProvider dp = new SAPDataProvider();
                    XmlNodeList childList = node.ChildNodes;
                    foreach (XmlNode child in childList)
                    {
                        switch (child.Name) {
                            case "id":
                                dp.ID = child.InnerText;
                                break;
                            case "name":
                                dp.Name = child.InnerText;
                                break;
                            case "dataSourceId":
                                dp.DataSourceId = child.InnerText;
                                break;
                            case "dataSourceType":
                                dp.DataSourceType = child.InnerText;
                                break;
                            default :                                    
                                break;
                        }
                    }
                    if (!dp.DataSourceType.Equals("unv") && !dp.DataSourceType.Equals("unx"))
                    {
                        if (dp.DataSourceType.Equals("fhsql"))
                        {
                            dp.sql = GetFHSQL(doc.SI_ID, dp.ID);
                        }                                
                        dp.DataSourceName = GetConnectionName(dp.DataSourceId);
                    }
                                                   
                    DPList.Add(dp);
                }
            }
            return DPList;
        }

        public string GetFHSQL(string DocId, string DpId)
        {
            string fhsql = string.Empty;
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + DocId + "/dataproviders/"+DpId, send, "application/xml", "application/xml");
            XmlResponse.LoadXml(webAPIconnect.responseContent);

            XmlNodeList properties = XmlResponse.GetElementsByTagName("property");
            foreach (XmlNode child in properties)
            {
                if (child.Attributes["key"].Value.Equals("sql"))
                {
                    fhsql = child.InnerText;
                    break;
                }
            }
            return fhsql;

        }

        public string GetConnectionName(string ConnId)
        {
            string connName = string.Empty;
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            webAPIconnect.Send("GET", "/biprws/raylight/v1/connections/" + ConnId, send, "application/xml", "application/xml");
            XmlResponse.LoadXml(webAPIconnect.responseContent);

            XmlNode connection = XmlResponse.GetElementsByTagName("connection").Item(0);
            XmlNodeList properties = connection.ChildNodes;
            foreach (XmlNode child in properties)
            {
                if (child.Name.Equals("name"))
                {
                    connName = child.InnerText;
                    break;
                }
            }
            return connName;

        }
    }
}
