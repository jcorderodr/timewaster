using System;
using System.Windows.Forms;

namespace WindowsTimeWaster.UI
{
    public partial class FrmWaiter : Form
    {
        public FrmWaiter()
        {
            InitializeComponent();
        }

        private void FrmWaiter_Load(object sender, EventArgs e)
        {
            this.Text = "Processing...";
        }
    }
}
