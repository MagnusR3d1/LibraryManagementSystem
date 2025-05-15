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
using System.IO;

namespace FoodTicketingSystem
{
    public partial class Addfoods : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\FoodD.mdf;Integrated Security=True;Connect Timeout=30");

        public Addfoods()
        {
            InitializeComponent();
            displayFoodItems();
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

        private void displayFoodItems()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "SELECT id, name, description, price, contains_chicken, contains_peanut, image, date_added FROM FoodItems";
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
                    Foodpics.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addBooks_addBtn_Click(object sender, EventArgs e)
        {
            if (Foodpics.Image == null || foodName.Text == "" || foodDescription.Text == "" || foodPrice.Text == "")
            {
                MessageBox.Show("Please fill all required fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string insertData = "INSERT INTO FoodItems (name, description, price, contains_peanut, contains_chicken, image, date_added) " +
                                        "VALUES (@name, @description, @price, @containsPeanut, @containsChicken, @image, @dateAdded)";

                    string imageFileName = $"{foodName.Text.Trim()}.jpg";
                    string directory = @"C:\\Path\\To\\Your\\Food_Images\\";
                    string path = Path.Combine(directory, imageFileName);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    File.Copy(Foodpics.ImageLocation, path, true);

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@name", foodName.Text.Trim());
                        cmd.Parameters.AddWithValue("@description", foodDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(foodPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@containsPeanut", checkBox2.Checked);
                        cmd.Parameters.AddWithValue("@containsChicken", checkBox1.Checked);
                        cmd.Parameters.AddWithValue("@image", path);
                        cmd.Parameters.AddWithValue("@dateAdded", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }

                    displayFoodItems();
                    clearFields();
                    MessageBox.Show("Food item added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void clearFields()
        {
            foodName.Text = "";
            foodDescription.Text = "";
            foodPrice.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            Foodpics.Image = null;
        }

        private int foodID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex]; 
                foodID = (int)row.Cells[0].Value;
                foodName.Text = row.Cells[1].Value.ToString();
                foodDescription.Text = row.Cells[2].Value.ToString();
                foodPrice.Text = row.Cells[3].Value.ToString();
                checkBox1.Checked = row.Cells[4].Value != DBNull.Value && row.Cells[4].Value is bool && (bool)row.Cells[4].Value;
                checkBox2.Checked = row.Cells[5].Value != DBNull.Value && row.Cells[5].Value is bool && (bool)row.Cells[5].Value;
                string imagePath = row.Cells[6].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    Foodpics.Image = Image.FromFile(imagePath);
                }
                else
                {
                    Foodpics.Image = null;
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void addBooks_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }
        private void addBooks_updateBtn_Click(object sender, EventArgs e)
        {
            if (foodID == 0)
            {
                MessageBox.Show("Please select a food item first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect.Open();

                string updateQuery = "UPDATE FoodItems SET name = @name, description = @description, price = @price, contains_chicken = @containsChicken, contains_peanut = @containsPeanut, image = @image WHERE id = @id";

                string imageFileName = $"{foodName.Text.Trim()}.jpg";
                string directory = @"C:\Path\To\Your\Food_Images\";
                string path = Path.Combine(directory, imageFileName);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.Copy(Foodpics.ImageLocation, path, true);

                using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@id", foodID);
                    cmd.Parameters.AddWithValue("@name", foodName.Text.Trim());
                    cmd.Parameters.AddWithValue("@description", foodDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(foodPrice.Text.Trim()));
                    cmd.Parameters.AddWithValue("@containsChicken", checkBox1.Checked);
                    cmd.Parameters.AddWithValue("@containsPeanut", checkBox2.Checked);
                    cmd.Parameters.AddWithValue("@image", path);

                    cmd.ExecuteNonQuery();
                }

                displayFoodItems();
                clearFields();
                MessageBox.Show("Food item updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

        private void addBooks_deleteBtn_Click(object sender, EventArgs e)
        {
            if (Foodpics.Image == null
                || string.IsNullOrWhiteSpace(foodName.Text)
                || string.IsNullOrWhiteSpace(foodDescription.Text)
                || string.IsNullOrWhiteSpace(foodPrice.Text))
            {
                MessageBox.Show("Please select a food item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (connect.State != ConnectionState.Open)
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to DELETE Food ID: "
                    + foodID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        DateTime today = DateTime.Today;

                        string softDeleteQuery = "UPDATE FoodItems SET date_deleted = @dateDeleted WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(softDeleteQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@dateDeleted", today);
                            cmd.Parameters.AddWithValue("@id", foodID);

                            cmd.ExecuteNonQuery();
                        }

                        displayFoodItems();
                        clearFields();

                        MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
