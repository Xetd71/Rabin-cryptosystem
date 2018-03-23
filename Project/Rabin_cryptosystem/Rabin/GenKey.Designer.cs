namespace Rabin
{
    partial class GenKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenKey));
            this.bitLenghtLabel = new System.Windows.Forms.Label();
            this.bitLenghtTextBox = new System.Windows.Forms.TextBox();
            this.GenKeyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bitLenghtLabel
            // 
            this.bitLenghtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bitLenghtLabel.Location = new System.Drawing.Point(21, 19);
            this.bitLenghtLabel.Name = "bitLenghtLabel";
            this.bitLenghtLabel.Size = new System.Drawing.Size(128, 40);
            this.bitLenghtLabel.TabIndex = 0;
            this.bitLenghtLabel.Text = "Битовая длина открытого ключа";
            // 
            // bitLenghtTextBox
            // 
            this.bitLenghtTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bitLenghtTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bitLenghtTextBox.Location = new System.Drawing.Point(177, 24);
            this.bitLenghtTextBox.Name = "bitLenghtTextBox";
            this.bitLenghtTextBox.Size = new System.Drawing.Size(116, 23);
            this.bitLenghtTextBox.TabIndex = 1;
            this.bitLenghtTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bitLenghtTextBox_KeyDown);
            this.bitLenghtTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bitLenghtTextBox_KeyPress);
            // 
            // GenKeyButton
            // 
            this.GenKeyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GenKeyButton.Location = new System.Drawing.Point(52, 62);
            this.GenKeyButton.Name = "GenKeyButton";
            this.GenKeyButton.Size = new System.Drawing.Size(215, 27);
            this.GenKeyButton.TabIndex = 2;
            this.GenKeyButton.Text = "Сгенерировать и сохранить ключ";
            this.GenKeyButton.UseVisualStyleBackColor = true;
            this.GenKeyButton.Click += new System.EventHandler(this.GenKeyButton_Click);
            // 
            // GenKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 97);
            this.Controls.Add(this.GenKeyButton);
            this.Controls.Add(this.bitLenghtTextBox);
            this.Controls.Add(this.bitLenghtLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenKey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label bitLenghtLabel;
        private System.Windows.Forms.TextBox bitLenghtTextBox;
        private System.Windows.Forms.Button GenKeyButton;
    }
}