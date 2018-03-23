using System;
using System.Windows.Forms;
using RabinLib;
using System.IO;
using System.Threading.Tasks;

namespace Rabin
{
    public partial class GenKey : Form
    {
        Rabin_Cryptosystem baseForm;
        ProgressBarForManyProcesses mainBar;
        public GenKey(Rabin_Cryptosystem form)
        {
            mainBar = form.mainBar;
            baseForm = form;
            InitializeComponent();
        }
        private async void GenKeyButton_Click(object sender, EventArgs e)
        {
            int bitLength;
            if (mainBar.Processes.Count != 0)
            {
                DialogResult res = MessageBox.Show(
                    "Генерация ключа трудозатратный процесс\r\nРекомендуется подождать пока завершатся предыдущие процессы и сгенерировать ключ позже",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
                return;
            }
            if (!int.TryParse(bitLenghtTextBox.Text, out bitLength))
            {
                MessageBox.Show("Неверно задано число\r\nПовторите ввод", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bitLenghtTextBox.Text = null;
                return;
            }
            if (bitLength < 128)
            {
                MessageBox.Show("Число слишком мало\r\nВозьмите число большее 128", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bitLenghtTextBox.Text = null;
                return;
            }
            if (bitLength > 4000)
            {
                DialogResult res = MessageBox.Show(
                    "Число слишком велико, генерация ключа может занять много времени\r\nРекомендуется взять число меньшее 4000\r\nХотите продолжить?",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.No)
                {
                    bitLenghtTextBox.Text = null;
                    return;
                }
            }
            RabinLib.Rabin rab = null;
            this.Visible = false;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = baseForm.lastDirectory;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Progress<ProgressInfo> progress = new Progress<ProgressInfo>();
                    ReachProgressBar keyBar = new ReachProgressBar(0, 0);
                    mainBar.AddProcess(keyBar);
                    progress.ProgressChanged += keyBar.Reach;
                    progress.ProgressChanged += mainBar.Apdate;
                    await Task.Run(() => { rab = new RabinLib.Rabin(bitLength, progress); });
                    rab.KeyToFile(sfd.FileName);
                    foreach (string key in Directory.GetFiles(sfd.FileName))
                        baseForm.AddFileItemToListView(key, baseForm.listKeys, baseForm.imageList1);
                    if (sfd.FileNames.Length != 0)
                        baseForm.lastDirectory = Path.GetDirectoryName(sfd.FileNames[0]);
                    mainBar.RemoveProcess(keyBar);
                }
            }
            this.Close();
        }
        private void bitLenghtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GenKeyButton_Click(sender, e);
            }
        }

        private void bitLenghtTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}
