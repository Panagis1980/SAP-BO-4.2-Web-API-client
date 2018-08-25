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
        public DocOperation(WebAPIconnection webAPIconnect)
        {
            this.webAPIconnect = webAPIconnect;
        }

        // Get Document list using CMSquery.. Only applicable for 4.2 and above
        public SAPDocumentList GetDocumentList(string FolderId)
        {
            SAPDocumentList docList;
            string send = string.Empty;
            string recv = string.Empty;

            send = "<attrs xmlns=\"http://www.sap.com/rws/bip\">" +
                "<attr name = \"query\" type = \"string\">" +
                "SELECT TOP 50000 SI_NAME, SI_ID, SI_KIND, SI_UPDATE_TS,  SI_UNIVERSE, SI_DSL_UNIVERSE, " +
                "SI_FHSQL_RELATIONAL_CONNECTION, SI_PARENTID, SI_FILES, SI_PARENT_FOLDER " +
                "from CI_INFOOBJECTS, CI_SYSTEMOBJECTS, CI_APPOBJECTS WHERE si_kind = 'Webi' " +
                "AND SI_INSTANCE = 0 AND SI_ANCESTOR = " + FolderId +
                "</attr ></attrs>";

            try
            {
                webAPIconnect.Send("POST", "/biprws/v1/cmsquery", send, "application/xml", "application/JSON");
                recv = webAPIconnect.responseContent;

                docList = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SAPDocumentList>(recv);
                foreach (SAPDocument item in docList.entries)
                {
                    item.SI_PATH = GetDocumentPath(FolderId, item.SI_ID);
                    item.DataProviderList = GetDocumentDataproviders(item);
                    item.ParameterList = GetDocumentParameters(item.SI_ID);
                    item.ReportList = GetDocumentReports(item.SI_ID);
                }

                return docList;

            }
            catch (Exception e)
            {
                Debug.WriteLine(recv);
                Debug.WriteLine(e.Message);
                Debug.Flush();
                return null;
            }
        }

        //It is better to use the Infostore solution with Serializers for the response XML...
        // Working for 4.1 and above
        public SAPDocumentList GetDocumentListInfostore(string FolderId)
        {
            Debug.WriteLine("In GetDocumentListInfostore  for folder:" + FolderId);
            Debug.Flush();
            SAPDocumentList docList = new SAPDocumentList();
            string send = string.Empty;
            string recv = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            try
            {
                Debug.WriteLine("Try...");
                Debug.Flush();
                webAPIconnect.Send("GET", "/biprws/infostore/" + FolderId + "/children?pageSize=200", send, "application/xml", "application/xml");
                TextReader reader = new StringReader(webAPIconnect.responseContent);
                XmlSerializer serializer = new XmlSerializer(typeof(feed));
                feed deserializedEntries = serializer.Deserialize(reader) as feed;
                if (deserializedEntries.entry == null)
                {
                    Debug.WriteLine("Null deserialized Entries");
                    Debug.Flush();
                }
                else
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
                            docList.entries.Add(GetDocument(id));
                            Debug.WriteLine("Doc ID:" + id);
                            Debug.Flush();
                        }
                        else if (type.Equals("Folder"))
                        {
                            SAPDocumentList docListInner = GetDocumentListInfostore(id);
                            Debug.WriteLine("Folder ID:" + id);
                            Debug.WriteLine("Inner list size:" + docListInner.entries.Count.ToString());
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
        public SAPDocument GetDocument(string DocumentId)
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
                        case "updated":
                            doc.SI_UPDATE_TS = child.InnerText;
                            break;
                        default:
                            break;
                    }
                }
                doc.DataProviderList = GetDocumentDataproviders(doc);
                doc.ParameterList = GetDocumentParameters(doc.SI_ID);
                doc.ReportList = GetDocumentReports(doc.SI_ID);
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

            webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + doc.SI_ID + "/dataproviders", send, "application/xml", "application/xml");
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
                        switch (child.Name)
                        {
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
                            default:
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

        // Not Used
        //public List<SAPDataProvider> GetDataproviders(SAPDocument doc)
        //{

        //    List<SAPDataProvider> DPList = new List<SAPDataProvider>();
        //    string send = string.Empty;
        //    XmlDocument XmlResponse = new XmlDocument();
        //    Debug.WriteLine("/biprws/raylight/v1/documents/" + doc.SI_ID + "/dataproviders");
        //    Debug.Flush();
        //    webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + doc.SI_ID + "/dataproviders", send, "application/xml", "application/xml");
        //    XmlResponse.LoadXml(webAPIconnect.responseContent);
        //    Debug.WriteLine(webAPIconnect.responseContent);
        //    Debug.Flush();
        //    XmlNodeList elemList = XmlResponse.GetElementsByTagName("id");
        //    if (elemList.Count > 0)
        //    {
        //        foreach (XmlNode node in elemList)
        //        {
        //            SAPDataProvider dp = new SAPDataProvider();

        //            string DpId = node.InnerText;
        //            Debug.WriteLine("In GetDataProvider " + DpId + " for Document:" + doc.SI_NAME);
        //            Debug.Flush();
        //            try
        //            {
        //                Debug.WriteLine("/biprws/raylight/v1/documents/" + doc.SI_ID + "/dataproviders/" + DpId);
        //                webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + doc.SI_ID + "/dataproviders/" + DpId, send, "application/xml", "application/xml");
        //                TextReader reader = new StringReader(webAPIconnect.responseContent);
        //                XmlSerializer serializer = new XmlSerializer(typeof(dataprovider));
        //                dataprovider dpSerialized = serializer.Deserialize(reader) as dataprovider;
        //                if (dpSerialized == null)
        //                {
        //                    Debug.WriteLine("Null dataprovider");
        //                    Debug.Flush();
        //                }
        //                else
        //                {
        //                    dp.ID = dpSerialized.id;
        //                    dp.Name = dpSerialized.name;
        //                    dp.DataSourceId = dpSerialized.dataSourceId.ToString();
        //                    dp.DataSourceType = dpSerialized.dataSourceType;
        //                    if (dpSerialized.properties != null)
        //                    {
        //                        foreach (var property in dpSerialized.properties)
        //                        {
        //                            switch (property.key)
        //                            {
        //                                case "sql":
        //                                    dp.sql = property.Value;
        //                                    break;
        //                                default:
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                }

        //                if (dpSerialized.dataSourceType.Equals("fhsql"))
        //                {
        //                    try
        //                    {
        //                        dp.DataSourceName = GetConnectionName(dp.DataSourceId);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Debug.WriteLine("Error in doc name:" + doc.SI_NAME);
        //                        Debug.WriteLine(e.StackTrace);
        //                        Debug.Flush();

        //                    }
        //                }
        //                else
        //                {
        //                    dp.DataSourceName = "No Connection name";
        //                }
        //                DPList.Add(dp);

        //            }
        //            catch (Exception exc)
        //            {
        //                Debug.WriteLine("Error in doc name:" + doc.SI_NAME);
        //                Debug.WriteLine(exc.StackTrace);
        //                Debug.Flush();
        //                dp.DataSourceName = "No Connection name";
        //            }
        //        }
        //    }
        //    return DPList;
        //}


        public List<parametersParameter> GetDocumentParameters(string DocumentId)
        {

            List<parametersParameter> paramList = new List<parametersParameter>();
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();
            Debug.WriteLine("/biprws/raylight/v1/documents/" + DocumentId + "/parameters");
            Debug.Flush();
            try
            {
                webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + DocumentId + "/parameters", send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);
                Debug.WriteLine(webAPIconnect.responseContent);
                Debug.Flush();
                TextReader reader = new StringReader(webAPIconnect.responseContent);
                XmlSerializer serializer = new XmlSerializer(typeof(parameters));
                parameters deserializedEntries = serializer.Deserialize(reader) as parameters;
                if (deserializedEntries.parameter == null)
                {
                    Debug.WriteLine("Null deserialized Entries");
                    Debug.Flush();
                }
                else
                {
                    Debug.WriteLine("Deserialized Entries:" + deserializedEntries.parameter.Length);
                    Debug.Flush();
                    foreach (var entry in deserializedEntries.parameter)
                    {
                        paramList.Add(entry);
                    }
                }
            }
            catch (Exception paramException)
            {
                Debug.WriteLine("Error in Parameter (Prompts) extraction for document: \r\n"
                    + paramException.Message);
                Debug.Flush();
            }
            return paramList;
        }

        public List<reportsReport> GetDocumentReports(string DocumentId)
        {

            List<reportsReport> reportList = new List<reportsReport>();
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();
            Debug.WriteLine("/biprws/raylight/v1/documents/" + DocumentId + "/reports");
            Debug.Flush();
            try
            {
                webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + DocumentId + "/reports", send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);
                Debug.WriteLine(webAPIconnect.responseContent);
                Debug.Flush();
                TextReader reader = new StringReader(webAPIconnect.responseContent);
                XmlSerializer serializer = new XmlSerializer(typeof(reports));
                reports deserializedEntries = serializer.Deserialize(reader) as reports;
                if (deserializedEntries.report == null)
                {
                    Debug.WriteLine("Null deserialized Entries");
                    Debug.Flush();
                }
                else
                {
                    Debug.WriteLine("Deserialized Entries:" + deserializedEntries.report.Length);
                    Debug.Flush();
                    foreach (var entry in deserializedEntries.report)
                    {
                        reportList.Add(entry);
                    }
                }
            }
            catch (Exception paramException)
            {
                Debug.WriteLine("Error in Report (Tabs) extraction for document DocumentId "+ DocumentId + ": \r\n"
                    + paramException.Message);
                Debug.Flush();
            }
            return reportList;
        }

        public void PurgeDataProviders(string DocId, string DpId, Boolean promptsAlso)
        {
            string connName = string.Empty;
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            try
            {
                webAPIconnect.Send("PUT", "/biprws/raylight/v1/documents/" + DocId + "/dataproviders/" + DpId + "?purge=true" +
                    (promptsAlso ? "&purgeOptions=prompts" : ""), send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);

                webAPIconnect.Send("PUT", "/biprws/raylight/v1/documents/" + DocId, send, "application/xml", "application/xml");
                XmlResponse.LoadXml(webAPIconnect.responseContent);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.Flush();
            }
        }

        public string GetFHSQL(string DocId, string DpId)
        {
            string fhsql = string.Empty;
            string send = string.Empty;
            XmlDocument XmlResponse = new XmlDocument();

            webAPIconnect.Send("GET", "/biprws/raylight/v1/documents/" + DocId + "/dataproviders/" + DpId, send, "application/xml", "application/xml");
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
