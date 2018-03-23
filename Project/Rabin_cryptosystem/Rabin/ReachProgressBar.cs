using System.Windows.Forms;
using RabinLib;

namespace Rabin
{
    class ReachProgressBar
    {
        ProgressBar bar;
        public int value, maximum;
        public ReachProgressBar(ProgressBar myBar)
        {
            bar = myBar;
            value = myBar.Value;
            maximum = myBar.Maximum;
        }
        public ReachProgressBar(int value, int maximum)
        {
            this.value = value;
            this.maximum = maximum;
        }
        public void Reach(object sender, ProgressInfo e)
        {
            maximum = (int)e.maximum;
            value = (int)e.value;
            if (bar == null)
                return;
            bar.Maximum = (int)e.maximum;
            bar.Value = (int)e.value;
            if (bar.Maximum == bar.Value)
                bar.Value = 0;
        }
        public ProgressBar Bar
        {
            get { return bar; }
        }
        public object Tag { get; set;}
    }
}
