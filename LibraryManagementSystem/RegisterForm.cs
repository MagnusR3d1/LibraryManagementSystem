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
using System.Text.RegularExpressions;

namespace LibraryManagementSystem

{

    public partial class RegisterForm : Form

    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\Library.mdf;Integrated Security=True;Connect Timeout=30");

        public RegisterForm()

        {

            InitializeComponent();

        }

        private void signIn_btn_Click(object sender, EventArgs e)

        {

            LoginForm lForm = new LoginForm();

            lForm.Show();

            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)

        {

            Application.Exit();

        }

        private void register_btn_Click(object sender, EventArgs e)

        {

            if (register_email.Text == "" || register_username.Text == "" || register_password.Text == "")

            {

                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            bool isEmailValid = IsValidEmail(register_email.Text);

            bool isUsernameValid = IsValidUsername(register_username.Text);

            bool isPasswordValid = IsValidPassword(register_password.Text);

            if (!isEmailValid && !isUsernameValid && !isPasswordValid)

            {

                MessageBox.Show("Email, Username, and Password are invalid.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            else if (!isEmailValid && !isUsernameValid)

            {

                MessageBox.Show("Email and Username are invalid.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            else if (!isEmailValid && !isPasswordValid)

            {

                MessageBox.Show("Email and Password are invalid.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            else if (!isUsernameValid && !isPasswordValid)

            {

                MessageBox.Show("Username and Password are invalid.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            else if (!isEmailValid)

            {

                MessageBox.Show("Email must end with '@cit.edu'", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            else if (!isUsernameValid)

            {

                MessageBox.Show("Username must be at least 6 characters long and start with a capital letter.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            else if (!isPasswordValid)

            {

                MessageBox.Show("Password must be at least 12 characters long and contain at least one uppercase letter, one lowercase letter, one special character, and one number.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            if (connect.State != ConnectionState.Open)

            {

                try

                {

                    connect.Open();

                    string checkUsername = "SELECT COUNT(*) FROM users WHERE username = @username";

                    using (SqlCommand checkCMD = new SqlCommand(checkUsername, connect))

                    {

                        checkCMD.Parameters.AddWithValue("@username", register_username.Text.Trim());

                        int count = (int)checkCMD.ExecuteScalar();

                        if (count >= 1)

                        {

                            MessageBox.Show(register_username.Text.Trim() + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                        else

                        {

                            DateTime day = DateTime.Today;

                            string insertData = "INSERT INTO users (email, username, password, date_register) VALUES(@email, @username, @password, @date)";

                            using (SqlCommand insertCMD = new SqlCommand(insertData, connect))

                            {

                                insertCMD.Parameters.AddWithValue("@email", register_email.Text.Trim());

                                insertCMD.Parameters.AddWithValue("@username", register_username.Text.Trim());

                                insertCMD.Parameters.AddWithValue("@password", register_password.Text.Trim());

                                insertCMD.Parameters.AddWithValue("@date", day.ToString("yyyy-MM-dd"));

                                insertCMD.ExecuteNonQuery();

                                MessageBox.Show("Register successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoginForm lForm = new LoginForm();

                                lForm.Show();

                                this.Hide();

                            }

                        }

                    }

                }

                catch (Exception ex)

                {

                    MessageBox.Show("Error connecting to Database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                finally

                {

                    connect.Close();

                }

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

            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';

        }

    }

}

