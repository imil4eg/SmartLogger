using SmartLogger.Creators;
using SmartLogger.Enums;
using SmartLogger.Loggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLogger.UI.Test
{
    public partial class Form1 : Form
    {
        private readonly ILogger _logger;

        public Form1(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _logger.Info("test");
        }
    }
}
