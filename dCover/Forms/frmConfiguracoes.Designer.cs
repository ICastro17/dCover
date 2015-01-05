namespace dCover.Forms
{
    partial class frmConfiguracoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguracoes));
            this.lblDirectory = new System.Windows.Forms.Label();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblApplication = new System.Windows.Forms.Label();
            this.btnApplication = new System.Windows.Forms.Button();
            this.txtApplication = new System.Windows.Forms.TextBox();
            this.lblParams = new System.Windows.Forms.Label();
            this.txtParams = new System.Windows.Forms.TextBox();
            this.btnHost = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.chkHost = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblDirectory
            // 
            this.lblDirectory.AutoSize = true;
            this.lblDirectory.Location = new System.Drawing.Point(14, 154);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(72, 13);
            this.lblDirectory.TabIndex = 30;
            this.lblDirectory.Text = "Start directory";
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.Location = new System.Drawing.Point(17, 170);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(250, 20);
            this.txtDirectory.TabIndex = 29;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(214, 207);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 28);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(156, 207);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(52, 28);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblApplication
            // 
            this.lblApplication.AutoSize = true;
            this.lblApplication.Location = new System.Drawing.Point(14, 23);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(59, 13);
            this.lblApplication.TabIndex = 26;
            this.lblApplication.Text = "Application";
            // 
            // btnApplication
            // 
            this.btnApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplication.Location = new System.Drawing.Point(237, 39);
            this.btnApplication.Name = "btnApplication";
            this.btnApplication.Size = new System.Drawing.Size(30, 21);
            this.btnApplication.TabIndex = 25;
            this.btnApplication.Text = "...";
            this.btnApplication.UseVisualStyleBackColor = true;
            // 
            // txtApplication
            // 
            this.txtApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplication.Location = new System.Drawing.Point(17, 39);
            this.txtApplication.Name = "txtApplication";
            this.txtApplication.Size = new System.Drawing.Size(214, 20);
            this.txtApplication.TabIndex = 24;
            // 
            // lblParams
            // 
            this.lblParams.AutoSize = true;
            this.lblParams.Location = new System.Drawing.Point(14, 111);
            this.lblParams.Name = "lblParams";
            this.lblParams.Size = new System.Drawing.Size(110, 13);
            this.lblParams.TabIndex = 17;
            this.lblParams.Text = "Command line params";
            // 
            // txtParams
            // 
            this.txtParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParams.Location = new System.Drawing.Point(17, 127);
            this.txtParams.Name = "txtParams";
            this.txtParams.Size = new System.Drawing.Size(250, 20);
            this.txtParams.TabIndex = 16;
            // 
            // btnHost
            // 
            this.btnHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHost.Location = new System.Drawing.Point(237, 87);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(30, 21);
            this.btnHost.TabIndex = 33;
            this.btnHost.Text = "...";
            this.btnHost.UseVisualStyleBackColor = true;
            // 
            // txtHost
            // 
            this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHost.Location = new System.Drawing.Point(17, 86);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(214, 20);
            this.txtHost.TabIndex = 32;
            // 
            // chkHost
            // 
            this.chkHost.AutoSize = true;
            this.chkHost.Location = new System.Drawing.Point(17, 65);
            this.chkHost.Name = "chkHost";
            this.chkHost.Size = new System.Drawing.Size(102, 17);
            this.chkHost.TabIndex = 31;
            this.chkHost.Text = "Host application";
            this.chkHost.UseVisualStyleBackColor = true;
            // 
            // frmConfiguracoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 241);
            this.Controls.Add(this.btnHost);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.chkHost);
            this.Controls.Add(this.lblDirectory);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblApplication);
            this.Controls.Add(this.btnApplication);
            this.Controls.Add(this.txtApplication);
            this.Controls.Add(this.lblParams);
            this.Controls.Add(this.txtParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfiguracoes";
            this.Text = "Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDirectory;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.Button btnApplication;
        private System.Windows.Forms.TextBox txtApplication;
        private System.Windows.Forms.Label lblParams;
        private System.Windows.Forms.TextBox txtParams;
        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.CheckBox chkHost;
    }
}