namespace Rabin
{
    partial class Rabin_Cryptosystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rabin_Cryptosystem));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encrypt2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decrypt2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listFiles = new System.Windows.Forms.ListView();
            this.contextMenuStripFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.encryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItemFileList = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listKeys = new System.Windows.Forms.ListView();
            this.contextMenuStripKeys = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.выбратьКлючToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItemKeys = new System.Windows.Forms.ToolStripMenuItem();
            this.listSelectedKey = new System.Windows.Forms.ListView();
            this.contextMenuStripSelectedKey = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItemSelectedKey = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripFiles.SuspendLayout();
            this.contextMenuStripKeys.SuspendLayout();
            this.contextMenuStripSelectedKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(433, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.KeyToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.addToolStripMenuItem.Text = "Добавить";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openFileToolStripMenuItem.Text = "Открыть Файл";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // KeyToolStripMenuItem
            // 
            this.KeyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genKeyToolStripMenuItem,
            this.addKeyToolStripMenuItem});
            this.KeyToolStripMenuItem.Name = "KeyToolStripMenuItem";
            this.KeyToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.KeyToolStripMenuItem.Text = "Ключ";
            // 
            // genKeyToolStripMenuItem
            // 
            this.genKeyToolStripMenuItem.Name = "genKeyToolStripMenuItem";
            this.genKeyToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.genKeyToolStripMenuItem.Text = "Сгенерировать ключ";
            this.genKeyToolStripMenuItem.Click += new System.EventHandler(this.genKeyToolStripMenuItem_Click);
            // 
            // addKeyToolStripMenuItem
            // 
            this.addKeyToolStripMenuItem.Name = "addKeyToolStripMenuItem";
            this.addKeyToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.addKeyToolStripMenuItem.Text = "Открыть ключ";
            this.addKeyToolStripMenuItem.Click += new System.EventHandler(this.addKeyToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encrypt2ToolStripMenuItem,
            this.decrypt2ToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.actionsToolStripMenuItem.Text = "Действия";
            this.actionsToolStripMenuItem.Click += new System.EventHandler(this.actionsToolStripMenuItem_Click);
            // 
            // encrypt2ToolStripMenuItem
            // 
            this.encrypt2ToolStripMenuItem.Name = "encrypt2ToolStripMenuItem";
            this.encrypt2ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.encrypt2ToolStripMenuItem.Text = "Зашифровать и сохранить";
            this.encrypt2ToolStripMenuItem.Click += new System.EventHandler(this.encryptToolStripMenuItem_Click);
            // 
            // decrypt2ToolStripMenuItem
            // 
            this.decrypt2ToolStripMenuItem.Name = "decrypt2ToolStripMenuItem";
            this.decrypt2ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.decrypt2ToolStripMenuItem.Text = "Расшифровать и сохранить";
            this.decrypt2ToolStripMenuItem.Click += new System.EventHandler(this.decryptToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 27);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 24);
            this.progressBar1.TabIndex = 1;
            // 
            // listFiles
            // 
            this.listFiles.AllowDrop = true;
            this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFiles.ContextMenuStrip = this.contextMenuStripFiles;
            this.listFiles.LargeImageList = this.imageList1;
            this.listFiles.Location = new System.Drawing.Point(12, 75);
            this.listFiles.Name = "listFiles";
            this.listFiles.Scrollable = false;
            this.listFiles.Size = new System.Drawing.Size(305, 315);
            this.listFiles.SmallImageList = this.imageList1;
            this.listFiles.TabIndex = 2;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listFiles_ItemDrag);
            this.listFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listFiles_DragDrop);
            this.listFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listFiles_DragEnter);
            this.listFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFiles_MouseDoubleClick);
            // 
            // contextMenuStripFiles
            // 
            this.contextMenuStripFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptToolStripMenuItem,
            this.decryptToolStripMenuItem,
            this.deleteToolStripMenuItemFileList});
            this.contextMenuStripFiles.Name = "contextMenuStrip1";
            this.contextMenuStripFiles.Size = new System.Drawing.Size(227, 70);
            this.contextMenuStripFiles.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripFiles_Opening);
            // 
            // encryptToolStripMenuItem
            // 
            this.encryptToolStripMenuItem.Name = "encryptToolStripMenuItem";
            this.encryptToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.encryptToolStripMenuItem.Text = "Зашифровать и сохранить";
            this.encryptToolStripMenuItem.Click += new System.EventHandler(this.encryptToolStripMenuItem_Click);
            // 
            // decryptToolStripMenuItem
            // 
            this.decryptToolStripMenuItem.Name = "decryptToolStripMenuItem";
            this.decryptToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.decryptToolStripMenuItem.Text = "Расшифровать и сохранить";
            this.decryptToolStripMenuItem.Click += new System.EventHandler(this.decryptToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItemFileList
            // 
            this.deleteToolStripMenuItemFileList.Name = "deleteToolStripMenuItemFileList";
            this.deleteToolStripMenuItemFileList.Size = new System.Drawing.Size(226, 22);
            this.deleteToolStripMenuItemFileList.Text = "Удалить";
            this.deleteToolStripMenuItemFileList.Click += new System.EventHandler(this.deleteToolStripMenuItemFileList_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listKeys
            // 
            this.listKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listKeys.ContextMenuStrip = this.contextMenuStripKeys;
            this.listKeys.LargeImageList = this.imageList1;
            this.listKeys.Location = new System.Drawing.Point(322, 176);
            this.listKeys.Name = "listKeys";
            this.listKeys.Size = new System.Drawing.Size(100, 214);
            this.listKeys.SmallImageList = this.imageList1;
            this.listKeys.TabIndex = 3;
            this.listKeys.UseCompatibleStateImageBehavior = false;
            this.listKeys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectKeyToolStripMenuItem_Click);
            // 
            // contextMenuStripKeys
            // 
            this.contextMenuStripKeys.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьКлючToolStripMenuItem,
            this.deleteToolStripMenuItemKeys});
            this.contextMenuStripKeys.Name = "contextMenuStripKeys";
            this.contextMenuStripKeys.Size = new System.Drawing.Size(155, 48);
            this.contextMenuStripKeys.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripKeys_Opening);
            // 
            // выбратьКлючToolStripMenuItem
            // 
            this.выбратьКлючToolStripMenuItem.Name = "выбратьКлючToolStripMenuItem";
            this.выбратьКлючToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.выбратьКлючToolStripMenuItem.Text = "Выбрать ключ";
            this.выбратьКлючToolStripMenuItem.Click += new System.EventHandler(this.selectKeyToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItemKeys
            // 
            this.deleteToolStripMenuItemKeys.Name = "deleteToolStripMenuItemKeys";
            this.deleteToolStripMenuItemKeys.Size = new System.Drawing.Size(154, 22);
            this.deleteToolStripMenuItemKeys.Text = "Удалить";
            this.deleteToolStripMenuItemKeys.Click += new System.EventHandler(this.deleteToolStripMenuItemKeys_Click);
            // 
            // listSelectedKey
            // 
            this.listSelectedKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listSelectedKey.ContextMenuStrip = this.contextMenuStripSelectedKey;
            this.listSelectedKey.LargeImageList = this.imageList1;
            this.listSelectedKey.Location = new System.Drawing.Point(323, 75);
            this.listSelectedKey.Name = "listSelectedKey";
            this.listSelectedKey.Size = new System.Drawing.Size(100, 80);
            this.listSelectedKey.SmallImageList = this.imageList1;
            this.listSelectedKey.TabIndex = 4;
            this.listSelectedKey.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenuStripSelectedKey
            // 
            this.contextMenuStripSelectedKey.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItemSelectedKey});
            this.contextMenuStripSelectedKey.Name = "contextMenuStripSelectedKey";
            this.contextMenuStripSelectedKey.Size = new System.Drawing.Size(119, 26);
            this.contextMenuStripSelectedKey.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripSelectedKey_Opening);
            // 
            // deleteToolStripMenuItemSelectedKey
            // 
            this.deleteToolStripMenuItemSelectedKey.Name = "deleteToolStripMenuItemSelectedKey";
            this.deleteToolStripMenuItemSelectedKey.Size = new System.Drawing.Size(118, 22);
            this.deleteToolStripMenuItemSelectedKey.Text = "Удалить";
            this.deleteToolStripMenuItemSelectedKey.Click += new System.EventHandler(this.deleteToolStripMenuItemSelectedKey_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(140, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Файлы";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(319, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Выбранный ключ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(351, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ключи";
            // 
            // Rabin_Cryptosystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 402);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listSelectedKey);
            this.Controls.Add(this.listKeys);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(244, 315);
            this.Name = "Rabin_Cryptosystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rabin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Rabin_Cryptosystem_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Rabin_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripFiles.ResumeLayout(false);
            this.contextMenuStripKeys.ResumeLayout(false);
            this.contextMenuStripSelectedKey.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KeyToolStripMenuItem;
        private System.Windows.Forms.ListView listFiles;
        internal System.Windows.Forms.ImageList imageList1;
        internal System.Windows.Forms.ListView listKeys;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFiles;
        private System.Windows.Forms.ToolStripMenuItem encryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemFileList;
        internal System.Windows.Forms.ListView listSelectedKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripKeys;
        private System.Windows.Forms.ToolStripMenuItem выбратьКлючToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemKeys;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSelectedKey;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItemSelectedKey;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encrypt2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decrypt2ToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}