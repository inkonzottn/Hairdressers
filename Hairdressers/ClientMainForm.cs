using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hairdressers
{
    public partial class ClientMainForm : Form
    {
        public ClientMainForm()
        {
            InitializeComponent();
        }

        private void tcClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tcAdmin.SelectedTab == tpServices)
            //{
            //    LoadServices();
            //}
            //else if (tcAdmin.SelectedTab == tpClients)
            //{
            //    LoadClients();
            //}
            //else if (tcAdmin.SelectedTab == tpMasters)
            //{
            //    LoadMasters();
            //    InitSpecializations();
            //    InitServices();
            //}
            if (tcClient.SelectedTab == tpExit)
            {
                DialogResult result = MessageBox.Show("Ви дійсно хочете вийти?", "Вихід", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    tcClient.SelectedIndex = 0;
                }
            }
        }
    }
}
