namespace Discovery
{
    partial class FormMain
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
            this.PanelControls = new System.Windows.Forms.Panel();
            this.ButtonGo = new System.Windows.Forms.Button();
            this.TextBoxAddress = new System.Windows.Forms.TextBox();
            this.WebBrowserResponse = new System.Windows.Forms.WebBrowser();
            this.WebBrowserSpecify = new System.Windows.Forms.WebBrowser();
            this.SplitContainerBrowsers = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SplitContainerBrowsers2 = new System.Windows.Forms.SplitContainer();
            this.WebBrowserHTML = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxResource = new System.Windows.Forms.TextBox();
            this.TextBoxResponseRaw = new System.Windows.Forms.TextBox();
            this.PanelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerBrowsers)).BeginInit();
            this.SplitContainerBrowsers.Panel1.SuspendLayout();
            this.SplitContainerBrowsers.Panel2.SuspendLayout();
            this.SplitContainerBrowsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerBrowsers2)).BeginInit();
            this.SplitContainerBrowsers2.Panel1.SuspendLayout();
            this.SplitContainerBrowsers2.Panel2.SuspendLayout();
            this.SplitContainerBrowsers2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelControls
            // 
            this.PanelControls.Controls.Add(this.TextBoxResource);
            this.PanelControls.Controls.Add(this.ButtonGo);
            this.PanelControls.Controls.Add(this.TextBoxAddress);
            this.PanelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelControls.Location = new System.Drawing.Point(5, 5);
            this.PanelControls.Name = "PanelControls";
            this.PanelControls.Size = new System.Drawing.Size(1047, 42);
            this.PanelControls.TabIndex = 0;
            // 
            // ButtonGo
            // 
            this.ButtonGo.Location = new System.Drawing.Point(679, 8);
            this.ButtonGo.Name = "ButtonGo";
            this.ButtonGo.Size = new System.Drawing.Size(75, 23);
            this.ButtonGo.TabIndex = 1;
            this.ButtonGo.Text = "Discover";
            this.ButtonGo.UseVisualStyleBackColor = true;
            this.ButtonGo.Click += new System.EventHandler(this.ButtonGo_Click);
            // 
            // TextBoxAddress
            // 
            this.TextBoxAddress.Location = new System.Drawing.Point(7, 10);
            this.TextBoxAddress.Name = "TextBoxAddress";
            this.TextBoxAddress.Size = new System.Drawing.Size(666, 20);
            this.TextBoxAddress.TabIndex = 0;
            this.TextBoxAddress.Text = "http://haltalk.herokuapp.com/";
            this.TextBoxAddress.Enter += new System.EventHandler(this.TextBoxAddress_Enter);
            this.TextBoxAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxAddress_KeyDown);
            // 
            // WebBrowserResponse
            // 
            this.WebBrowserResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserResponse.Location = new System.Drawing.Point(5, 283);
            this.WebBrowserResponse.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserResponse.Name = "WebBrowserResponse";
            this.WebBrowserResponse.Size = new System.Drawing.Size(331, 216);
            this.WebBrowserResponse.TabIndex = 1;
            this.WebBrowserResponse.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserResponse_DocumentCompleted);
            // 
            // WebBrowserSpecify
            // 
            this.WebBrowserSpecify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserSpecify.Location = new System.Drawing.Point(5, 28);
            this.WebBrowserSpecify.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserSpecify.Name = "WebBrowserSpecify";
            this.WebBrowserSpecify.Size = new System.Drawing.Size(310, 471);
            this.WebBrowserSpecify.TabIndex = 2;
            this.WebBrowserSpecify.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserSpecify_DocumentCompleted);
            // 
            // SplitContainerBrowsers
            // 
            this.SplitContainerBrowsers.BackColor = System.Drawing.SystemColors.Control;
            this.SplitContainerBrowsers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SplitContainerBrowsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerBrowsers.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerBrowsers.Name = "SplitContainerBrowsers";
            // 
            // SplitContainerBrowsers.Panel1
            // 
            this.SplitContainerBrowsers.Panel1.Controls.Add(this.WebBrowserResponse);
            this.SplitContainerBrowsers.Panel1.Controls.Add(this.TextBoxResponseRaw);
            this.SplitContainerBrowsers.Panel1.Controls.Add(this.label1);
            this.SplitContainerBrowsers.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // SplitContainerBrowsers.Panel2
            // 
            this.SplitContainerBrowsers.Panel2.Controls.Add(this.WebBrowserSpecify);
            this.SplitContainerBrowsers.Panel2.Controls.Add(this.label2);
            this.SplitContainerBrowsers.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.SplitContainerBrowsers.Size = new System.Drawing.Size(673, 508);
            this.SplitContainerBrowsers.SplitterDistance = 345;
            this.SplitContainerBrowsers.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Response";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(310, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Specify";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SplitContainerBrowsers2
            // 
            this.SplitContainerBrowsers2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SplitContainerBrowsers2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerBrowsers2.Location = new System.Drawing.Point(5, 47);
            this.SplitContainerBrowsers2.Name = "SplitContainerBrowsers2";
            // 
            // SplitContainerBrowsers2.Panel1
            // 
            this.SplitContainerBrowsers2.Panel1.Controls.Add(this.SplitContainerBrowsers);
            // 
            // SplitContainerBrowsers2.Panel2
            // 
            this.SplitContainerBrowsers2.Panel2.Controls.Add(this.WebBrowserHTML);
            this.SplitContainerBrowsers2.Panel2.Controls.Add(this.label3);
            this.SplitContainerBrowsers2.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.SplitContainerBrowsers2.Size = new System.Drawing.Size(1047, 508);
            this.SplitContainerBrowsers2.SplitterDistance = 673;
            this.SplitContainerBrowsers2.TabIndex = 4;
            // 
            // WebBrowserHTML
            // 
            this.WebBrowserHTML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserHTML.Location = new System.Drawing.Point(5, 28);
            this.WebBrowserHTML.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserHTML.Name = "WebBrowserHTML";
            this.WebBrowserHTML.Size = new System.Drawing.Size(356, 471);
            this.WebBrowserHTML.TabIndex = 0;
            this.WebBrowserHTML.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserHTML_DocumentCompleted);
            this.WebBrowserHTML.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserHTML_Navigating);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(356, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Presentation";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxResource
            // 
            this.TextBoxResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxResource.Location = new System.Drawing.Point(760, 10);
            this.TextBoxResource.Name = "TextBoxResource";
            this.TextBoxResource.Size = new System.Drawing.Size(280, 20);
            this.TextBoxResource.TabIndex = 2;
            // 
            // TextBoxResponseRaw
            // 
            this.TextBoxResponseRaw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxResponseRaw.Dock = System.Windows.Forms.DockStyle.Top;
            this.TextBoxResponseRaw.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxResponseRaw.Location = new System.Drawing.Point(5, 28);
            this.TextBoxResponseRaw.MaxLength = 0;
            this.TextBoxResponseRaw.Multiline = true;
            this.TextBoxResponseRaw.Name = "TextBoxResponseRaw";
            this.TextBoxResponseRaw.ReadOnly = true;
            this.TextBoxResponseRaw.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxResponseRaw.Size = new System.Drawing.Size(331, 255);
            this.TextBoxResponseRaw.TabIndex = 3;
            this.TextBoxResponseRaw.WordWrap = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 560);
            this.Controls.Add(this.SplitContainerBrowsers2);
            this.Controls.Add(this.PanelControls);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Discovery - Human Client API Browser";
            this.PanelControls.ResumeLayout(false);
            this.PanelControls.PerformLayout();
            this.SplitContainerBrowsers.Panel1.ResumeLayout(false);
            this.SplitContainerBrowsers.Panel1.PerformLayout();
            this.SplitContainerBrowsers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerBrowsers)).EndInit();
            this.SplitContainerBrowsers.ResumeLayout(false);
            this.SplitContainerBrowsers2.Panel1.ResumeLayout(false);
            this.SplitContainerBrowsers2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerBrowsers2)).EndInit();
            this.SplitContainerBrowsers2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelControls;
        private System.Windows.Forms.Button ButtonGo;
        private System.Windows.Forms.TextBox TextBoxAddress;
        private System.Windows.Forms.WebBrowser WebBrowserResponse;
        private System.Windows.Forms.WebBrowser WebBrowserSpecify;
        private System.Windows.Forms.SplitContainer SplitContainerBrowsers;
        private System.Windows.Forms.SplitContainer SplitContainerBrowsers2;
        private System.Windows.Forms.WebBrowser WebBrowserHTML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxResource;
        private System.Windows.Forms.TextBox TextBoxResponseRaw;
    }
}

