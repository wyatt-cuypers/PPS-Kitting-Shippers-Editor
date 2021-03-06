
namespace PPSKittingShippersEditor
{
    partial class JobIDForm
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
            this.components = new System.ComponentModel.Container();
            this.lblJobID = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtJobID = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.bgwMain = new System.ComponentModel.BackgroundWorker();
            this.tmrHalfSec = new System.Windows.Forms.Timer(this.components);
            this.chkUserDefined = new System.Windows.Forms.CheckBox();
            this.lblValid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblJobID
            // 
            this.lblJobID.AutoSize = true;
            this.lblJobID.Location = new System.Drawing.Point(12, 25);
            this.lblJobID.Name = "lblJobID";
            this.lblJobID.Size = new System.Drawing.Size(41, 13);
            this.lblJobID.TabIndex = 0;
            this.lblJobID.Text = "Job ID:";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(12, 62);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(49, 13);
            this.lblQty.TabIndex = 1;
            this.lblQty.Text = "Quantity:";
            // 
            // txtJobID
            // 
            this.txtJobID.Location = new System.Drawing.Point(81, 22);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(230, 20);
            this.txtJobID.TabIndex = 2;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(81, 59);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(230, 20);
            this.txtQty.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(225, 92);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(86, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // bgwMain
            // 
            this.bgwMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // tmrHalfSec
            // 
            this.tmrHalfSec.Enabled = true;
            this.tmrHalfSec.Interval = 500;
            this.tmrHalfSec.Tick += new System.EventHandler(this.tmrHalfSec_Tick);
            // 
            // chkUserDefined
            // 
            this.chkUserDefined.AutoSize = true;
            this.chkUserDefined.Location = new System.Drawing.Point(15, 92);
            this.chkUserDefined.Name = "chkUserDefined";
            this.chkUserDefined.Size = new System.Drawing.Size(144, 17);
            this.chkUserDefined.TabIndex = 5;
            this.chkUserDefined.Text = "Define Quantity Manually";
            this.chkUserDefined.UseVisualStyleBackColor = true;
            // 
            // lblValid
            // 
            this.lblValid.AutoSize = true;
            this.lblValid.Location = new System.Drawing.Point(81, 3);
            this.lblValid.Name = "lblValid";
            this.lblValid.Size = new System.Drawing.Size(0, 13);
            this.lblValid.TabIndex = 6;
            // 
            // JobIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 127);
            this.Controls.Add(this.lblValid);
            this.Controls.Add(this.chkUserDefined);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtJobID);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblJobID);
            this.Name = "JobIDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Job ID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJobID;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnSubmit;
        private System.ComponentModel.BackgroundWorker bgwMain;
        private System.Windows.Forms.Timer tmrHalfSec;
        private System.Windows.Forms.CheckBox chkUserDefined;
        private System.Windows.Forms.Label lblValid;
    }
}