using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SAP_BO_4._2_Web_API_client
{
    public partial class MainForm : Form
    {

        private WebAPIconnection webAPIconnect = new WebAPIconnection();

        private DocOperation docOperation;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TxtFilename.Text = TxtFolderId.Text + "_Webi_List.xlsx";
        }

        private void BtnLogon_Click(object sender, EventArgs e)
        {
            if (TxtUsername.Text == "" || TxtPassword.Text == "")
            {
                TraceBox.Text += "Please enter a username and password.\r\n";
                return;
            }

            if (webAPIconnect != null && webAPIconnect.isLoggedOn())
            {
                TraceBox.Text += "Please log off before logging on again.\r\n";             
                return;
            }

            TraceBox.Text += "Logging on...  \r\n";
            webAPIconnect = new WebAPIconnection(TxtUsername.Text, TxtPassword.Text, TxtURI.Text);
            int result = webAPIconnect.Logon();
            if (result != 0)
            {
                TraceBox.Text += "Logon failed. Error: " + result.ToString() + "\r\n";
            }
            else
            {
                TraceBox.Text += "Logon successful.\r\n";

                this.docOperation = new DocOperation(webAPIconnect);

            }
        }

        private void BtnLogoff_Click(object sender, EventArgs e)
        {
            if (!webAPIconnect.isLoggedOn())
            {
                TraceBox.Text += "Not currently logged on, thus unable to log off.\r\n";
                return;
            }

            TraceBox.Text += "Logging off...   \r\n";

            int result = webAPIconnect.Logoff();

            if (result != 0)
            {
                TraceBox.Text += "Logoff failed. Error: " + result.ToString() + "\r\n";
            }
            else
            {
                TraceBox.Text += "Logoff successful.\r\n";
            }
        }

        private void BtnFetchParam_Click(object sender, EventArgs e)
        {
            if (webAPIconnect == null || !webAPIconnect.isLoggedOn())
            {
                TraceBox.Text += "Not logged on. Could not fetch document parameters.\r\n";
                return;
            }
            TraceBox.Text = "";
            TraceBox.Text += "Attempting HTTP Request...\r\n";

            XmlDocument send = new XmlDocument();
            XmlDocument recv = new XmlDocument();
            XmlNamespaceManager nsmgrRecv = new XmlNamespaceManager(recv.NameTable);
            nsmgrRecv.AddNamespace("rest", "");

            try
            {
                if (TxtReqXMLBody == null)
                {
                    send.InnerXml = TxtReqXMLBody.Text;
                }
                webAPIconnect.CreateWebRequest(send, recv, LovHttpMethod.Text, TxtRequest.Text);
                TraceBox.Text += send.OuterXml.ToString() + "\r\n";
                TraceBox.Text += "==================================\r\n";
                TraceBox.Text += recv.OuterXml.ToString() + "\r\n";
                TraceBox.Text += "==================================\r\n";
                TraceBox.Text += recv.InnerText + "\r\n";
                TraceBox.Text += "==================================\r\n";
                TraceBox.Text += recv.InnerXml + "\r\n";
                TraceBox.Text += "==================================\r\n";
                TraceBox.Text += "Request succesfully completed...\r\n";
                TraceBox.Text += "==================================\r\n";
            } catch
            {
                TraceBox.Text += "Error in HTTP Request...\r\n";
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Filter = "Excel 2007 Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TxtFilename.Text = saveFileDialog1.FileName;
            }
        }

        private void BtnClearTrace_Click(object sender, EventArgs e)
        {
            TraceBox.Text = "";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (webAPIconnect == null || !webAPIconnect.isLoggedOn())
            {
                TraceBox.Text += "Not logged on. Could not fetch document parameters.\r\n";
                return;
            }

            if (!(!TxtFolderId.Text.Equals(string.Empty) ^ !TxtDocId.Text.Equals(string.Empty)))
            {
                TraceBox.Text += "Please choose either Document or Folder for export\r\n";
                return;
            }

            TraceBox.Text += "Attempting HTTP Request...\r\n";

            try
            {
                SAPDocumentList DocList = docOperation.GetDocumentList(TxtFolderId.Text);
                ExcelExport xlsx = new ExcelExport(DocList);
                FHSQLExport fhsql = new FHSQLExport(DocList);
                xlsx.GenerateExcel(TxtFilename.Text);
                fhsql.ExportFHSQL(TxtFilename.Text.Replace("xlsx", "fhsql"));
                TraceBox.Text += "Closing HTTP Request...\r\n";
            }
            catch
            {
                TraceBox.Text += "Error in HTTP Request...\r\n";
                TraceBox.Text += "Cannot get Document or Folder ...\r\n";
            }
        }

        private void TxtFolderId_TextChanged(object sender, EventArgs e)
        {
            //if (webAPIconnect == null || !webAPIconnect.isLoggedOn())
            //{
            //    TraceBox.Text += "Not logged on. Could not fetch folder parameters.\r\n";
            //    return;
            //}
            //string[] folderDetails = docOperation.GetFolderDetails(TxtFolderId.Text);
            //if (folderDetails[0] != null)
            //{
                TxtFilename.Text = TxtFolderId.Text + "_Webi_List.xlsx";
            //}
        }
    }
}
