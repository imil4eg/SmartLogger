using SmartLogger.Creators;
using SmartLogger.Loggers;
using System;
using System.Windows.Forms;

namespace SmartLogger.UI.Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ILogger logger = LoggerCreator.CreateLogger(a => a.AddConsole());
            logger.Info("test");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(logger));
        }
    }
}
