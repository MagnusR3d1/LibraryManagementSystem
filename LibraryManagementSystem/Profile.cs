using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FoodTicketingSystem
{
    public partial class Profile : Form
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30");
        private string originalUsername;
        public Profile(string loggedInUsername)
        {
            InitializeComponent();
            originalUsername = loggedInUsername;
            LoadUserData();
           
            LoadDataIntoDataGridView();
            LoadFoodQuantityChart();

        }
        public void refreshData()
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

        }

        private void LoadUserData()
        {
            try
            {
                connect.Open();
                string query = "SELECT username, email, password, image FROM users WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@username", originalUsername);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            update_username.Text = reader["username"].ToString();
                            update_email.Text = reader["email"].ToString();
                            update_password.Text = reader["password"].ToString();

                            string imagePath = reader["image"].ToString();
                            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                            {
                                addBooks_picture.Image = Image.FromFile(imagePath);
                                addBooks_picture.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                            else
                            {
                                addBooks_picture.Image = null; // Clear if no image found
                            }
                        }
                        else
                        {
                            MessageBox.Show("User data not found.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }



        private void UpdateUserData(string oldUsername, string newEmail, string newUsername, string newPassword)
        {
            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Invalid email format. Email must end with @cit.edu.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidUsername(newUsername))
            {
                MessageBox.Show("Invalid username format. Username must start with a capital letter and be at least 6 characters long.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Invalid password format. Password must be at least 12 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect.Open();
                string updateQuery = "UPDATE users SET email = @newEmail, username = @newUsername, password = @newPassword WHERE username = @oldUsername";
                using (SqlCommand updateCMD = new SqlCommand(updateQuery, connect))
                {
                    updateCMD.Parameters.AddWithValue("@newEmail", newEmail);
                    updateCMD.Parameters.AddWithValue("@newUsername", newUsername);
                    updateCMD.Parameters.AddWithValue("@newPassword", newPassword);
                    updateCMD.Parameters.AddWithValue("@oldUsername", oldUsername);

                    int rowsAffected = updateCMD.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User details updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        originalUsername = newUsername;
                    }
                    else
                    {
                        MessageBox.Show("User not found or update failed.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user data: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }


        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^\s@]+@cit\.edu$");
        }


        private bool IsValidUsername(string username)
        {
            return Regex.IsMatch(username, @"^[A-Z][a-zA-Z0-9]{5,}$");
        }


        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*])(?=.*\d).{12,}$");
        }


        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            update_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        private void register_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void addBooks_picture_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newEmail = update_email.Text.Trim();
            string newUsername = update_username.Text.Trim();
            string newPassword = update_password.Text.Trim();

            if (string.IsNullOrEmpty(newEmail) || string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateUserData(originalUsername, newEmail, newUsername, newPassword); // Use the original username for the update
        }

        private void signIn_btn_Click(object sender, EventArgs e)
        {
            MainForm MForm = new MainForm(originalUsername);
            MForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private String imagePath;
        private void addBooks_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    string newFilePath = Path.Combine(@"C:\Users\user\Desktop\LibraryManagementSystem\Profile_Images\", originalUsername + ".jpg");

                    string directoryPath = Path.GetDirectoryName(newFilePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    
                    File.Copy(imagePath, newFilePath, true);

                    
                    addBooks_picture.Image = Image.FromFile(newFilePath);
                    addBooks_picture.SizeMode = PictureBoxSizeMode.StretchImage; // Adjust size to fit

                    
                    SaveUserProfileImage(originalUsername, newFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SaveUserProfileImage(string username, string newFilePath)
        {
            try
            {
                connect.Open();
                string query = "UPDATE users SET image = @image WHERE username = @username";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@image", newFilePath);
                    cmd.Parameters.AddWithValue("@username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile image updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update profile image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void dashboard_AB_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadDataIntoDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                string query = "SELECT * FROM Orders"; 

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }
        private void LoadFoodQuantityChart()
        {
            
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                string query = @"
                SELECT food_id, SUM(quantity) AS total_quantity
                FROM Orders
                GROUP BY food_id";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                chart1.Series.Clear();
                Series series = new Series("Quantity per Food ID");
                series.ChartType = SeriesChartType.Column; // or .Pie, .Bar, etc.

                foreach (DataRow row in dt.Rows)
                {
                    int foodId = Convert.ToInt32(row["food_id"]);
                    int totalQty = Convert.ToInt32(row["total_quantity"]);
                    series.Points.AddXY(foodId, totalQty);
                }

                chart1.Series.Add(series);
                chart1.ChartAreas[0].AxisX.Title = "Food ID";
                chart1.ChartAreas[0].AxisY.Title = "Total Quantity Ordered";
            }
        }
        private void dataGridView1_AutoSizeColumnsModeChanged(object sender, DataGridViewAutoSizeColumnsModeEventArgs e)
        {
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void register_showPass_CheckedChanged_1(object sender, EventArgs e)
        {
            update_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }
        
        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
