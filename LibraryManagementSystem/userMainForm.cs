using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FoodTicketingSystem
{
    public partial class userMainForm : Form
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30");

        private Timer inactivityTimer = new Timer();
        private const int InactivityInterval = 3000000;
        private string loggedInUsername;
        private int loggedInStudentId;

        public userMainForm(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            inactivityTimer.Interval = InactivityInterval;
            inactivityTimer.Tick += InactivityTimer_Tick;
            inactivityTimer.Start();
            displayFoodItems();
            GetStudentIdByUsername();
            LoadOrdersForStudent(loggedInStudentId);
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayFoodItems();
        }
        private int foodID = 0;
        int studentId = -1;




        private void GetStudentIdByUsername()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT id FROM studentusers WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", loggedInUsername);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        loggedInStudentId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Student ID not found for the logged in username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void displayFoodItems()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                
                string query = "SELECT id, name, description, price, contains_chicken, contains_peanut, image, date_added FROM FoodItems WHERE 1=1";

               
                if (checkBoxChicken.Checked)
                {
                    query += " AND (contains_chicken IS NULL OR contains_chicken = 0)";
                }

                if (checkBoxPeanut.Checked)
                {
                    query += " AND (contains_peanut IS NULL OR contains_peanut = 0)";
                }

                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);
                dataGridView1.DataSource = table;

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load food items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        public void RefreshOrderTable()
        {
            LoadOrdersForStudent(loggedInStudentId);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                foodID = (int)row.Cells[0].Value;


                string imagePath = row.Cells[6].Value?.ToString();
               

                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    Foodpics.Image = Image.FromFile(imagePath);
                    Foodpics.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    Foodpics.Image = null;
                }
            }
        }
        private int selectedOrderId = 0;
        private void deleteOrderButton_Click(object sender, EventArgs e)
        {
            if (selectedOrderId == 0)
            {
                MessageBox.Show("Please select an order to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this order?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    try
                    {
                        connect.Open();
                        string query = "DELETE FROM Orders WHERE order_id = @orderId";
                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@orderId", selectedOrderId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                selectedOrderId = 0;
                                RefreshOrderTable();
                            }
                            else
                            {
                                MessageBox.Show("Order could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting order: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
       

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow rows = dataGridView2.Rows[e.RowIndex];

               
                    selectedOrderId = (int)rows.Cells[0].Value;

            }
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
            userLoginForm loginForm = new userLoginForm();
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
                userLoginForm lForm = new userLoginForm();
                lForm.Show();
                this.Hide();
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
        public void LoadOrdersForStudent(int studentId)
        {
            string query = @"
        SELECT 
            o.order_id,
            f.name AS FoodName,
            o.quantity,
            o.total_price,
            o.order_date
        FROM Orders o
        INNER JOIN FoodItems f ON o.food_id = f.id
        WHERE o.student_id = @studentId
        ORDER BY o.order_date DESC";

            using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView2.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading student orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (foodID == 0 || string.IsNullOrWhiteSpace(quantityTextBox.Text))
            {
                MessageBox.Show("Please select a food item and enter quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(quantityTextBox.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Quantity must be a positive number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect.Open();

               
                string priceQuery = "SELECT price FROM FoodItems WHERE id = @id";
                decimal price = 0;

                using (SqlCommand priceCmd = new SqlCommand(priceQuery, connect))
                {
                    priceCmd.Parameters.AddWithValue("@id", foodID);
                    object result = priceCmd.ExecuteScalar();

                    if (result != null)
                    {
                        price = Convert.ToDecimal(result);
                    }
                    else
                    {
                        MessageBox.Show("Selected food item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                decimal totalPrice = price * quantity;

                
                string insertQuery = @"INSERT INTO Orders (student_id, food_id, quantity, total_price)
                               VALUES (@studentID, @foodID, @quantity, @totalPrice)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@studentID", loggedInStudentId);
                    cmd.Parameters.AddWithValue("@foodID", foodID);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@totalPrice", totalPrice);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
            MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshOrderTable();
        }

        private void deleteOrder_Click(object sender, EventArgs e)
        {
            if (selectedOrderId == 0)
            {
                MessageBox.Show("Please select an order to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this order?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    try
                    {
                        connect.Open();
                        string query = "DELETE FROM Orders WHERE order_id = @orderId";
                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@orderId", selectedOrderId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                selectedOrderId = 0;
                                RefreshOrderTable();
                            }
                            else
                            {
                                MessageBox.Show("Order could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting order: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void checkBoxChicken_CheckedChanged(object sender, EventArgs e)
        {
            displayFoodItems();
        }

        private void checkBoxPeanut_CheckedChanged(object sender, EventArgs e)
        {
            displayFoodItems();
        }
    }

}


