using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RabinLib;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace Rabin
{
    public partial class Rabin_Cryptosystem : Form
    {
        public Rabin_Cryptosystem(string[] args)
        {
            InitializeComponent();
            mainBar = new ProgressBarForManyProcesses(progressBar1);
            imageList1.Images.Add(".publicKey", Properties.Resources.PublicKey);
            imageList1.Images.Add(".privateKey", Properties.Resources.PrivateKey);
            imageList1.Images.Add(".encrypted", Properties.Resources.Encrypted);
            foreach (string fileName in args)
            {
                string extention;
                try
                {
                    extention = Path.GetExtension(fileName);
                    switch (extention)
                    {
                        case (".publicKey"):
                        case (".privateKey"):
                            AddFileItemToListView(fileName, listKeys, imageList1);
                            break;
                        default:
                            AddFileItemToListView(fileName, listFiles, imageList1);
                            break;
                    }
                }
                catch { }
            }
        }
        RabinLib.Rabin rab;
        internal string lastDirectory = String.Empty;
        List<ListViewItem> encryptingItems = new List<ListViewItem>();
        List<ListViewItem> decryptingItems = new List<ListViewItem>();
        internal ProgressBarForManyProcesses mainBar;
        /// <summary>
        /// Позволяет пользователю выбрать файлы и добавить их в listFiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = lastDirectory;
                ofd.Title = "Открыть файл";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                    foreach (string file in ofd.FileNames)
                    {
                        if (listFiles.Items.Count == 128)
                        {
                            MessageBox.Show("Извините, но нельзя добавить больше 128 файлов", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        AddFileItemToListView(file, listFiles, imageList1);
                    }
                if (ofd.FileNames.Length != 0)
                    lastDirectory = Path.GetDirectoryName(ofd.FileNames[0]);
            }
        }
        /// <summary>
        /// Позволяет пользователю выбрать ключи и добавить их в listKeys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ключи (*.publicKey; *.privateKey)|*.publicKey; *.privateKey";
                ofd.Title = "Выбор ключа";
                ofd.Multiselect = true;
                ofd.InitialDirectory = lastDirectory;
                if (ofd.ShowDialog() == DialogResult.OK)
                    foreach (string file in ofd.FileNames)
                        AddFileItemToListView(file, listKeys, imageList1);
                if (ofd.FileNames.Length != 0)
                    lastDirectory = Path.GetDirectoryName(ofd.FileNames[0]);
            }
        }
        internal void AddFileItemToListView(string fileName, ListView listToAdd, ImageList imageList)
        {
            int pictureIndex = AddNewFileImageItemToImageListAndReturnsPictureIndex(fileName, imageList);
            ListViewItem newItem = CreateNewListViewItem(fileName, pictureIndex);
            AddItemToListIfNotExist(newItem, listToAdd);
        }
        private int AddNewFileImageItemToImageListAndReturnsPictureIndex(string fileName, ImageList imageList)
        {
            string extentionFile = Path.GetExtension(fileName);
            if (!imageList.Images.Keys.Contains(extentionFile))
                imageList.Images.Add(extentionFile, Icon.ExtractAssociatedIcon(fileName));
            return imageList.Images.Keys.IndexOf(extentionFile);
        }
        private ListViewItem CreateNewListViewItem(string fileName, int pictureIndex)
        {
            ListViewItem newItem = new ListViewItem();
            newItem.Text = Path.GetFileName(fileName);
            newItem.ImageIndex = pictureIndex;
            newItem.Tag = fileName;
            return newItem;
        }
        private void AddItemToListIfNotExist(ListViewItem newItem, ListView listToAdd)
        {
            foreach (ListViewItem item in listToAdd.Items)
            {
                if (newItem.Tag.Equals(item.Tag))
                {
                    MessageBox.Show("Файл уже добавлен", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            listToAdd.Items.Add(newItem);
        }
        private ProgressBar CreateProgressBar(ListViewItem item, ListView listToAdd)
        {
            ProgressBar pb = new ProgressBar();
            pb.Maximum = (int)(new FileInfo((string)item.Tag)).Length;
            pb.Name = (string)item.Tag;
            pb.SetBounds(item.Position.X - 21 + 5, item.Position.Y + item.ImageList.ImageSize.Height + 20, item.Bounds.Width - 10, 8);
            listToAdd.Controls.Add(pb);
            Update();
            return pb;
        }
        private void contextMenuStripFiles_Opening(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < contextMenuStripFiles.Items.Count; i++)
                contextMenuStripFiles.Items[i].Enabled = false;
            if (listFiles.SelectedItems.Count != 0)
            {
                bool isUsing = true;
                if (listFiles.SelectedItems.Count == 1)
                {
                    isUsing = false;
                    foreach (var item in encryptingItems)
                    {
                        if (item.Tag == listFiles.SelectedItems[0].Tag)
                        {
                            isUsing = true;
                            break;
                        }
                    }
                    if (!isUsing)
                    {
                        foreach (var item in decryptingItems)
                        {
                            if (item.Tag == listFiles.SelectedItems[0].Tag)
                            {
                                isUsing = true;
                                break;
                            }
                        }
                    }
                }
                if (!isUsing) {
                    for (int i = 0; i < contextMenuStripFiles.Items.Count; i++)
                        contextMenuStripFiles.Items[i].Enabled = true;
                }
                else {
                    for (int i = 0; i < contextMenuStripFiles.Items.Count - 1; i++)
                        contextMenuStripFiles.Items[i].Enabled = true;
                }
            }
        }
        private void genKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenKey gk = new GenKey(this);
            gk.Show();
        }
        private void contextMenuStripKeys_Opening(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < contextMenuStripKeys.Items.Count; i++)
                contextMenuStripKeys.Items[i].Enabled = false;
            if (listKeys.SelectedItems.Count != 0)
            {
                for (int i = 0; i < contextMenuStripKeys.Items.Count; i++)
                    contextMenuStripKeys.Items[i].Enabled = true;
            }
        }

        private void selectKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(listKeys.SelectedItems[0].Tag.ToString()))
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listKeys.Items.Remove(listKeys.SelectedItems[0]);
                return;
            }
            ListViewItem newKey = (ListViewItem)listKeys.SelectedItems[0].Clone();
            listSelectedKey.Items.Clear();
            try
            {
                rab = new RabinLib.Rabin(newKey.Tag.ToString());
                listSelectedKey.Items.Add(newKey);
                rab.Tag = newKey.Text;
            }
            catch { MessageBox.Show("Ошибка в чтении ключа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void contextMenuStripSelectedKey_Opening(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < contextMenuStripSelectedKey.Items.Count; i++)
                contextMenuStripSelectedKey.Items[i].Enabled = false;
            if (listSelectedKey.SelectedItems.Count == 1)
                for (int i = 0; i < contextMenuStripSelectedKey.Items.Count; i++)
                    contextMenuStripSelectedKey.Items[i].Enabled = true;
        }
        private void deleteToolStripMenuItemSelectedKey_Click(object sender, EventArgs e)
        {
            rab = null;
            listSelectedKey.Items.Clear();
        }

        private async void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rab == null) {
                MessageBox.Show("Не выбран открытый ключ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (Path.GetExtension(listSelectedKey.Items[0].Tag.ToString()).CompareTo(".publicKey") != 0) {
                MessageBox.Show("Не тот ключ!\r\nВыберите открытый ключ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            string folder = BrouseFolder();
            if (string.IsNullOrEmpty(folder))
                return;
            foreach (ListViewItem item in listFiles.SelectedItems)
            {
                if (!IsExistInEncryptItems(item))
                {
                    item.SubItems[0].Tag = rab;
                    item.SubItems[0].Name = folder;
                    encryptingItems.Add(item);
                    ProgressBar pb = CreateProgressBar(item, listFiles);
                    ReachProgressBar filesBar = new ReachProgressBar(pb);
                    filesBar.Tag = pb.Name;
                    mainBar.AddProcess(filesBar);
                }
                else
                {
                    MessageBox.Show($"Файл с именем: {item.Name}\r\nуже добавлен в очередь", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            if (encryptingItems.Count != listFiles.SelectedItems.Count)
                return;
            var progress = new Progress<ProgressInfo>();
            progress.ProgressChanged += mainBar.Apdate;
            while(encryptingItems.Count > 0)
            { 
                string encryptPath = encryptingItems[0].SubItems[0].Name + "\\" + encryptingItems[0].Text + ".encrypted";
                ReachProgressBar filesBar = mainBar.Processes.OfType<ReachProgressBar>().FirstOrDefault(q => (string)q.Tag == (string)encryptingItems[0].Tag);
                progress.ProgressChanged += filesBar.Reach;
                try { await ((RabinLib.Rabin)encryptingItems[0].SubItems[0].Tag).EncryptAsync(encryptingItems[0].Tag.ToString(), encryptPath, progress); }
                catch(Exception ex)
                {
                    MessageBox.Show("Невозможно зашифровать файл " + encryptingItems[0].Text + "\n\r" +
                        ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mainBar.RemoveProcess(filesBar);
                    progress.ProgressChanged -= filesBar.Reach;
                    listFiles.Controls.Remove(listFiles.Controls.OfType<ProgressBar>().FirstOrDefault(q => q.Name == (string)encryptingItems[0].Tag));
                    encryptingItems.Remove(encryptingItems[0]);
                    continue;
                }
                progress.ProgressChanged -= filesBar.Reach;
                listFiles.Controls.Remove(listFiles.Controls.OfType<ProgressBar>().FirstOrDefault(q => q.Name == (string)encryptingItems[0].Tag));
                ListFilesRemoveItem(encryptingItems[0]);
                encryptingItems.Remove(encryptingItems[0]);
                AddFileItemToListView(encryptPath, listFiles, imageList1);
            }
            mainBar.Apdate(this, null);
            if (encryptingItems.Count == 0 && decryptingItems.Count == 0)
                mainBar = new ProgressBarForManyProcesses(progressBar1, new List<ReachProgressBar>());
        }
        private bool IsExistInEncryptItems(ListViewItem item)
        {
            foreach (ListViewItem encItem in encryptingItems)
            {
                if (((string)encItem.Tag).Equals((string)item.Tag))
                    return true;
            }
            return false;
        }
        private string BrouseFolder()
        {
            string folder = string.Empty;
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = lastDirectory;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    folder = fbd.SelectedPath;
                    lastDirectory = Path.GetDirectoryName(folder);
                }
            }
            return folder;
        }
        private void listFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listFiles.SelectedItems.Count != 1)
                return;
            if (!File.Exists(listFiles.SelectedItems[0].Tag.ToString()))
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ListFilesRemoveItem(listFiles.SelectedItems[0]);
                return;
            }
            try
            {
                Process.Start(listFiles.SelectedItems[0].Tag.ToString());
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void  decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rab == null) {
                MessageBox.Show("Не выбран закрытый ключ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (Path.GetExtension(listSelectedKey.Items[0].Tag.ToString()).CompareTo(".privateKey") != 0) {
                MessageBox.Show("Не тот ключ!\r\nВыберите закрытый ключ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            string folder = BrouseFolder();
            if (string.IsNullOrEmpty(folder))
                return;
            foreach (ListViewItem item in listFiles.SelectedItems)
            {
                if (!IsExistInDecryptItems(item))
                {
                    item.SubItems[0].Tag = rab;
                    item.SubItems[0].Name = folder;
                    decryptingItems.Add(item);
                    ProgressBar pb = CreateProgressBar(item, listFiles);
                    ReachProgressBar filesBar = new ReachProgressBar(pb);
                    filesBar.Tag = pb.Name;
                    mainBar.AddProcess(filesBar);
                }
                else
                {
                    MessageBox.Show($"Файл с именем: {item.Text}\r\nуже добавлен в очередь", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            if (decryptingItems.Count != listFiles.SelectedItems.Count)
                return;
            var progress = new Progress<ProgressInfo>();
            progress.ProgressChanged += mainBar.Apdate;
            while (decryptingItems.Count > 0)
            {
                string decryptPath = decryptingItems[0].SubItems[0].Name + "\\" + Path.GetFileNameWithoutExtension(decryptingItems[0].Text);
                ReachProgressBar filesBar = mainBar.Processes.OfType<ReachProgressBar>().FirstOrDefault(q => (string)q.Tag == (string)decryptingItems[0].Tag);
                if (Path.GetExtension(decryptingItems[0].Tag.ToString()).CompareTo(".encrypted") != 0)
                {
                    MessageBox.Show("Невозможно расшифровать файл " + decryptingItems[0].Text + "\n\rФайл не содержит расширение \".encrypted\"", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listFiles.Controls.Remove(listFiles.Controls.OfType<ProgressBar>().FirstOrDefault(q => q.Name == (string)decryptingItems[0].Tag));
                    decryptingItems.Remove(decryptingItems[0]);
                    mainBar.RemoveProcess(filesBar);
                    continue;
                }
                progress.ProgressChanged += filesBar.Reach;
                try { await ((RabinLib.Rabin)decryptingItems[0].SubItems[0].Tag).DecryptAsync(decryptingItems[0].Tag.ToString(), decryptPath, progress); }
                catch
                {
                    MessageBox.Show(
                        $"{decryptingItems[0].Text} Невозможно раскодировать с использованием ключа {((RabinLib.Rabin)decryptingItems[0].SubItems[0].Tag).Tag.ToString()}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progress.ProgressChanged -= filesBar.Reach;
                    listFiles.Controls.Remove(listFiles.Controls.OfType<ProgressBar>().FirstOrDefault(q => q.Name == (string)decryptingItems[0].Tag));
                    decryptingItems.Remove(decryptingItems[0]);
                    mainBar.RemoveProcess(filesBar);
                    continue;
                }
                progress.ProgressChanged -= filesBar.Reach;
                listFiles.Controls.Remove(listFiles.Controls.OfType<ProgressBar>().FirstOrDefault(q => q.Name == (string)decryptingItems[0].Tag));
                ListFilesRemoveItem(decryptingItems[0]);
                decryptingItems.Remove(decryptingItems[0]);
                AddFileItemToListView(decryptPath, listFiles, imageList1);
            }
            mainBar.Apdate(this, null);
            if (encryptingItems.Count == 0 && decryptingItems.Count == 0)
                mainBar = new ProgressBarForManyProcesses(progressBar1, new List<ReachProgressBar>());
        }
        private bool IsExistInDecryptItems(ListViewItem item)
        {
            foreach (ListViewItem decItem in decryptingItems)
            {
                if (((string)decItem.Tag).Equals((string)item.Tag))
                    return true;
            }
            return false;
        }
        private void deleteToolStripMenuItemFileList_Click(object sender, EventArgs e)
        {
            for (int i = listFiles.Items.Count - 1; i >= 0; i--)
            {
                if (listFiles.Items[i].Selected)
                    ListFilesRemoveItem(i);
            }
        }

        private void deleteToolStripMenuItemKeys_Click(object sender, EventArgs e)
        {
            for (int i = listKeys.Items.Count - 1; i >= 0; i--)
            {
                if (listKeys.Items[i].Selected)
                    listKeys.Items[i].Remove();
            }
        }
        private void ListFilesRemoveItem(ListViewItem item)
        {
            listFiles.Items.Remove(item);
            ProgressBarFindItem(listFiles);

        }
        private void ListFilesRemoveItem(int i)
        {
            listFiles.Items[i].Remove();
            ProgressBarFindItem(listFiles);

        }
        private void ProgressBarFindItem(ListView list)
        {
            foreach (Control control in list.Controls)
            {
                ProgressBar bar = control as ProgressBar;
                if (bar == null)
                    continue;
                ListViewItem item = list.Items.OfType<ListViewItem>().FirstOrDefault(q => (string)q.Tag == bar.Name);
                if (item != null)
                    bar.SetBounds(item.Position.X - 21 + 5, item.Position.Y + item.ImageList.ImageSize.Height + 20, item.Bounds.Width - 10, 8);
            }
        }
        private void listFiles_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listFiles.DoDragDrop(listFiles.SelectedItems, DragDropEffects.Move);
        }

        private void listFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0)
                return;
            ListViewItem dragItem = null;
            for (int i = 0; i < listFiles.Items.Count; i++)
            {
                Point pt = listFiles.PointToClient(new Point(e.X, e.Y));
                int x = listFiles.Items[i].Bounds.Right;
                int y = listFiles.Items[i].Bounds.Bottom;
                if (pt.X <= x && pt.Y <= y)
                {
                    dragItem = listFiles.Items[i];
                    break;
                }
            }
            if (dragItem == null)
                dragItem = listFiles.Items[listFiles.Items.Count - 1];
            ListViewItem[] selectedItems = new ListViewItem[listFiles.SelectedItems.Count];
            for (int i = 0; i < selectedItems.Length; i++)
                selectedItems[i] = listFiles.SelectedItems[i];
            if (dragItem.Index < selectedItems[0].Index)
            {
                int k = selectedItems[0].Index;
                int j = dragItem.Index;
                for (int i = dragItem.Index; i < k; i++)
                {
                    ListViewItem insertItem = (ListViewItem)listFiles.Items[j].Clone();
                    listFiles.Items.Add(insertItem);
                    ListFilesRemoveItem(j);
                }
            }
            else
            if (dragItem.Index > selectedItems[0].Index)
            {
                for (int i = 0; i < selectedItems.Length; i++)
                {
                    ListViewItem insertItem = (ListViewItem)selectedItems[i].Clone();
                    listFiles.Items.Add(insertItem);
                    ListFilesRemoveItem(selectedItems[i]);
                }
            }
            ProgressBarFindItem(listFiles);
        }

        private void Rabin_Cryptosystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainBar.Processes.Count != 0)
            {
                DialogResult res = MessageBox.Show("Некоторые процессы не завершены\n\rВсе равно закрыть приложение?",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void actionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0)
            {
                encrypt2ToolStripMenuItem.Enabled = false;
                decrypt2ToolStripMenuItem.Enabled = false;
            }
            else
            {
                encrypt2ToolStripMenuItem.Enabled = true;
                decrypt2ToolStripMenuItem.Enabled = true;
            }
        }

        private void Rabin_SizeChanged(object sender, EventArgs e)
        {
            ProgressBarFindItem(listFiles);
        }
    }
}
