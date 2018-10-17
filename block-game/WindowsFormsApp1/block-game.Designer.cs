namespace WindowsFormsApp1
{
    partial class blockUI
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
            this.lblTest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTest
            // 
            this.lblTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTest.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTest.Location = new System.Drawing.Point(0, 0);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(1474, 25);
            this.lblTest.TabIndex = 0;
            this.lblTest.Text = "test";
            // 
            // blockUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1474, 929);
            this.Controls.Add(this.lblTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "blockUI";
            this.Text = "its like tetris but technically not";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTest;
    }
}

