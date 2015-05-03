using SHDocVw;

namespace UtilX
{
    interface IExplorerTerminator
    {
        void Run(int threshold);
    }

    public class ExplorerTerminator : IExplorerTerminator
    {
        public void Run(int threshold)
        {
            ShellWindows shellWindows = new SHDocVw.ShellWindows();
            
            threshold = AppSettings.Default.ActiveExplorerThreshold;

            if (shellWindows.Count > threshold)
            {
                int needToKill = shellWindows.Count - threshold;
                int killed = 0;

                foreach (InternetExplorer ie in shellWindows)
                {
                    if (needToKill == killed)
                    {
                        break;
                    }
                    ie.Quit();
                    killed++;
                }
            }
        }
    }
}
