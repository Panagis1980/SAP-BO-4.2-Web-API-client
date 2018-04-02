namespace SAP_BO_4._2_Web_API_client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtURI = new System.Windows.Forms.TextBox();
            this.LblCMSURI = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.BtnLogon = new System.Windows.Forms.Button();
            this.BtnLogoff = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnClearTrace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TraceBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LovHttpMethod = new System.Windows.Forms.ComboBox();
            this.BtnFetchParam = new System.Windows.Forms.Button();
            this.LblRequest = new System.Windows.Forms.Label();
            this.TxtRequest = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.LblFilename = new System.Windows.Forms.Label();
            this.TxtFilename = new System.Windows.Forms.TextBox();
            this.LblFolderId = new System.Windows.Forms.Label();
            this.TxtFolderId = new System.Windows.Forms.TextBox();
            this.LblDocId = new System.Windows.Forms.Label();
            this.TxtDocId = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtReqXMLBody = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtURI
            // 
            this.TxtURI.Location = new System.Drawing.Point(77, 23);
            this.TxtURI.Name = "TxtURI";
            this.TxtURI.Size = new System.Drawing.Size(211, 20);
            this.TxtURI.TabIndex = 0;
            this.TxtURI.Text = "http://vmbi42sp4:6405";
            // 
            // LblCMSURI
            // 
            this.LblCMSURI.AutoSize = true;
            this.LblCMSURI.Location = new System.Drawing.Point(19, 26);
            this.LblCMSURI.Name = "LblCMSURI";
            this.LblCMSURI.Size = new System.Drawing.Size(52, 13);
            this.LblCMSURI.TabIndex = 1;
            this.LblCMSURI.Text = "CSM URI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username";
            // 
            // TxtUsername
            // 
            this.TxtUsername.Location = new System.Drawing.Point(77, 48);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(119, 20);
            this.TxtUsername.TabIndex = 2;
            this.TxtUsername.Text = "Administrator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(77, 72);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(119, 20);
            this.TxtPassword.TabIndex = 4;
            this.TxtPassword.Text = "POLOpolo";
            this.TxtPassword.UseSystemPasswordChar = true;
            // 
            // BtnLogon
            // 
            this.BtnLogon.Location = new System.Drawing.Point(203, 47);
            this.BtnLogon.Name = "BtnLogon";
            this.BtnLogon.Size = new System.Drawing.Size(85, 23);
            this.BtnLogon.TabIndex = 6;
            this.BtnLogon.Text = "Log On";
            this.BtnLogon.UseVisualStyleBackColor = true;
            this.BtnLogon.Click += new System.EventHandler(this.BtnLogon_Click);
            // 
            // BtnLogoff
            // 
            this.BtnLogoff.Location = new System.Drawing.Point(203, 71);
            this.BtnLogoff.Name = "BtnLogoff";
            this.BtnLogoff.Size = new System.Drawing.Size(85, 23);
            this.BtnLogoff.TabIndex = 7;
            this.BtnLogoff.Text = "Log Off";
            this.BtnLogoff.UseVisualStyleBackColor = true;
            this.BtnLogoff.Click += new System.EventHandler(this.BtnLogoff_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnClearTrace);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TraceBox);
            this.groupBox1.Controls.Add(this.LblCMSURI);
            this.groupBox1.Controls.Add(this.BtnLogoff);
            this.groupBox1.Controls.Add(this.TxtURI);
            this.groupBox1.Controls.Add(this.BtnLogon);
            this.groupBox1.Controls.Add(this.TxtUsername);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtPassword);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 432);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // BtnClearTrace
            // 
            this.BtnClearTrace.Location = new System.Drawing.Point(203, 95);
            this.BtnClearTrace.Name = "BtnClearTrace";
            this.BtnClearTrace.Size = new System.Drawing.Size(85, 23);
            this.BtnClearTrace.TabIndex = 10;
            this.BtnClearTrace.Text = "Clear Trace";
            this.BtnClearTrace.UseVisualStyleBackColor = true;
            this.BtnClearTrace.Click += new System.EventHandler(this.BtnClearTrace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Trace:";
            // 
            // TraceBox
            // 
            this.TraceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TraceBox.Location = new System.Drawing.Point(22, 132);
            this.TraceBox.Multiline = true;
            this.TraceBox.Name = "TraceBox";
            this.TraceBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TraceBox.Size = new System.Drawing.Size(266, 281);
            this.TraceBox.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.TxtReqXMLBody);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.LovHttpMethod);
            this.groupBox2.Controls.Add(this.BtnFetchParam);
            this.groupBox2.Controls.Add(this.LblRequest);
            this.groupBox2.Controls.Add(this.TxtRequest);
            this.groupBox2.Location = new System.Drawing.Point(330, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 153);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Method:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LovHttpMethod
            // 
            this.LovHttpMethod.FormattingEnabled = true;
            this.LovHttpMethod.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT"});
            this.LovHttpMethod.Location = new System.Drawing.Point(84, 126);
            this.LovHttpMethod.Name = "LovHttpMethod";
            this.LovHttpMethod.Size = new System.Drawing.Size(121, 21);
            this.LovHttpMethod.TabIndex = 0;
            // 
            // BtnFetchParam
            // 
            this.BtnFetchParam.Location = new System.Drawing.Point(319, 125);
            this.BtnFetchParam.Name = "BtnFetchParam";
            this.BtnFetchParam.Size = new System.Drawing.Size(75, 21);
            this.BtnFetchParam.TabIndex = 3;
            this.BtnFetchParam.Text = "Send Rq";
            this.BtnFetchParam.UseVisualStyleBackColor = true;
            this.BtnFetchParam.Click += new System.EventHandler(this.BtnFetchParam_Click);
            // 
            // LblRequest
            // 
            this.LblRequest.AutoSize = true;
            this.LblRequest.Location = new System.Drawing.Point(29, 27);
            this.LblRequest.Name = "LblRequest";
            this.LblRequest.Size = new System.Drawing.Size(50, 13);
            this.LblRequest.TabIndex = 2;
            this.LblRequest.Text = "Request:";
            this.LblRequest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtRequest
            // 
            this.TxtRequest.Location = new System.Drawing.Point(84, 23);
            this.TxtRequest.Name = "TxtRequest";
            this.TxtRequest.Size = new System.Drawing.Size(309, 20);
            this.TxtRequest.TabIndex = 1;
            this.TxtRequest.Text = "/biprws/raylight/v1/";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.BtnBrowse);
            this.groupBox3.Controls.Add(this.BtnExport);
            this.groupBox3.Controls.Add(this.LblFilename);
            this.groupBox3.Controls.Add(this.TxtFilename);
            this.groupBox3.Controls.Add(this.LblFolderId);
            this.groupBox3.Controls.Add(this.TxtFolderId);
            this.groupBox3.Controls.Add(this.LblDocId);
            this.groupBox3.Controls.Add(this.TxtDocId);
            this.groupBox3.Location = new System.Drawing.Point(330, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 273);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Document Tools";
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(318, 82);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(75, 23);
            this.BtnBrowse.TabIndex = 13;
            this.BtnBrowse.Text = "Browse...";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(319, 31);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(75, 21);
            this.BtnExport.TabIndex = 6;
            this.BtnExport.Text = "Export";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // LblFilename
            // 
            this.LblFilename.AutoSize = true;
            this.LblFilename.Location = new System.Drawing.Point(27, 87);
            this.LblFilename.Name = "LblFilename";
            this.LblFilename.Size = new System.Drawing.Size(52, 13);
            this.LblFilename.TabIndex = 12;
            this.LblFilename.Text = "Filename:";
            this.LblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtFilename
            // 
            this.TxtFilename.Location = new System.Drawing.Point(85, 83);
            this.TxtFilename.Name = "TxtFilename";
            this.TxtFilename.Size = new System.Drawing.Size(227, 20);
            this.TxtFilename.TabIndex = 11;
            // 
            // LblFolderId
            // 
            this.LblFolderId.AutoSize = true;
            this.LblFolderId.Location = new System.Drawing.Point(26, 61);
            this.LblFolderId.Name = "LblFolderId";
            this.LblFolderId.Size = new System.Drawing.Size(53, 13);
            this.LblFolderId.TabIndex = 8;
            this.LblFolderId.Text = "Folder ID:";
            this.LblFolderId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtFolderId
            // 
            this.TxtFolderId.Location = new System.Drawing.Point(85, 57);
            this.TxtFolderId.Name = "TxtFolderId";
            this.TxtFolderId.Size = new System.Drawing.Size(121, 20);
            this.TxtFolderId.TabIndex = 7;
            this.TxtFolderId.Text = "5388";
            // 
            // LblDocId
            // 
            this.LblDocId.AutoSize = true;
            this.LblDocId.Location = new System.Drawing.Point(6, 35);
            this.LblDocId.Name = "LblDocId";
            this.LblDocId.Size = new System.Drawing.Size(73, 13);
            this.LblDocId.TabIndex = 6;
            this.LblDocId.Text = "Document ID:";
            this.LblDocId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtDocId
            // 
            this.TxtDocId.Location = new System.Drawing.Point(85, 31);
            this.TxtDocId.Name = "TxtDocId";
            this.TxtDocId.Size = new System.Drawing.Size(121, 20);
            this.TxtDocId.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "XML:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtReqXMLBody
            // 
            this.TxtReqXMLBody.Location = new System.Drawing.Point(84, 48);
            this.TxtReqXMLBody.Multiline = true;
            this.TxtReqXMLBody.Name = "TxtReqXMLBody";
            this.TxtReqXMLBody.Size = new System.Drawing.Size(309, 72);
            this.TxtReqXMLBody.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 456);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "SAP Business Objects Web API 4.2 client";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtURI;
        private System.Windows.Forms.Label LblCMSURI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Button BtnLogon;
        private System.Windows.Forms.Button BtnLogoff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TraceBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnFetchParam;
        private System.Windows.Forms.Label LblRequest;
        private System.Windows.Forms.TextBox TxtRequest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox LovHttpMethod;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LblFolderId;
        private System.Windows.Forms.TextBox TxtFolderId;
        private System.Windows.Forms.Label LblDocId;
        private System.Windows.Forms.TextBox TxtDocId;
        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Label LblFilename;
        private System.Windows.Forms.TextBox TxtFilename;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button BtnClearTrace;
        private System.Windows.Forms.TextBox TxtReqXMLBody;
        private System.Windows.Forms.Label label5;
    }
}

