using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FoodTicketingSystem
{
    public partial class Dashboard : UserControl
    {
        SqlConnection connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\FoodD.mdf;Integrated Security=True;Connect Timeout=30");

        public Dashboard()
        {
            InitializeComponent();

           
        }

        public void refreshData()
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            
        }

        
        private void dashboard_AB_Click(object sender, EventArgs e)
        {

        }
    }
}
