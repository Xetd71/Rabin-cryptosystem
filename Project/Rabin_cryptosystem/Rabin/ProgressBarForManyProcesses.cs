using System.Collections.Generic;
using System.Windows.Forms;
using RabinLib;

namespace Rabin
{
    class ProgressBarForManyProcesses
    {
        List<ReachProgressBar> processes = new List<ReachProgressBar>();
        ProgressBar mainBar;
        public ProgressBarForManyProcesses(ProgressBar bar) { mainBar = bar; }
        public ProgressBarForManyProcesses(ProgressBar bar, List<ReachProgressBar> listOfNewProcesses)
        {
            processes = listOfNewProcesses;
            mainBar = bar;
        }
        public void AddProcess(ReachProgressBar process)
        {
            processes.Add(process);
        }
        public List<ReachProgressBar> Processes
        {
            get { return processes; }
        }
        public void RemoveProcess(ReachProgressBar process)
        {
            processes.Remove(process);
        }
        public void Apdate(object sender, ProgressInfo e)
        {
            int maximum = 0, value = 0;
            foreach (ReachProgressBar process in processes)
            {
                value += process.value;
                maximum += process.maximum;
            }
            mainBar.Maximum = maximum;
            if (maximum == value)
                mainBar.Value = 0;
            else
                mainBar.Value = value;
        }
    }
}
