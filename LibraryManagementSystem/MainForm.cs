using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodTicketingSystem
{
    public partial class MainForm : Form
    {
        private Timer inactivityTimer = new Timer();
        private const int InactivityInterval = 3000000;
        private string loggedInUsername;

        public MainForm(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            inactivityTimer.Interval = InactivityInterval;
            inactivityTimer.Tick += InactivityTimer_Tick;
            inactivityTimer.Start();

        }
        protected override void WndProc(ref Message m)
        {

            const int WM_MOUSEMOVE = 0x0200;
            const int WM_KEYDOWN = 0x0100;
            if (m.Msg == WM_MOUSEMOVE || m.Msg == WM_KEYDOWN)
            {
                ResetInactivityTimer();
            }
            base.WndProc(ref m);
        }
        private void ResetInactivityTimer()
        {
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }
        private void InactivityTimer_Tick(object sender, EventArgs e)
        {

            inactivityTimer.Stop();


            MessageBox.Show("Session timed out due to inactivity. You will be logged out.",
                "Session Timeout", MessageBoxButtons.OK, MessageBoxIcon.Information);


            AutoLogout();
        }
        private void AutoLogout()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                LoginForm lForm = new LoginForm();
                lForm.Show();
                this.Hide();
            }

        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            Dashboard.Visible = true;
            Addfoods.Visible = false;
            FufillOrders.Visible = false;
            MenuItems.Visible = false;

            Dashboard dForm = Dashboard as Dashboard;
            if (dForm != null)
            {
                dForm.refreshData();
            }
        }

        private void addBooks_btn_Click(object sender, EventArgs e)
        {
            Dashboard.Visible = false;
            Addfoods.Visible = true;
            FufillOrders.Visible = false;
            MenuItems.Visible = false;

            Addfoods aForm = Addfoods as Addfoods;
            if (aForm != null)
            {
                aForm.refreshData();
            }
        }

        private void ORDER_btn_Click(object sender, EventArgs e)
        {
            Dashboard.Visible = false;
            Addfoods.Visible = false;
            FufillOrders.Visible = false;
            MenuItems.Visible = true;

            FufillOrders rForm = FufillOrders as FufillOrders;
            if (rForm != null)
            {
                rForm.refreshData();
            }
        }

        private void returnBooks_btn_Click(object sender, EventArgs e)
        {
            Dashboard.Visible = false;
            Addfoods.Visible = false;
            FufillOrders.Visible = true;
            MenuItems.Visible = false;

            MenuItems iForm = MenuItems as MenuItems;
            if (iForm != null)
            {
                iForm.refreshData();
            }
        }

        private void dashboard1_Load(object sender, EventArgs e)
        {

        }

        private void greet_label_Click(object sender, EventArgs e)
        {
            Profile lForm = new Profile(loggedInUsername);
            lForm.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addItems_btn_Click(object sender, EventArgs e)
        {
            Dashboard.Visible = false;
            Addfoods.Visible = true;
            FufillOrders.Visible = false;
            MenuItems.Visible = false;

            Addfoods aForm = Addfoods as Addfoods;
            if (aForm != null)
            {
                aForm.refreshData();
            }
        }
    }

}


