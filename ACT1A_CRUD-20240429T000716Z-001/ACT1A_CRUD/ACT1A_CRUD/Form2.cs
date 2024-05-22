using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ACT1A_CRUD
{
    public partial class Form2 : Form
    {
        private MySqlConnection connection;
        public Form2()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=yesyes;username=root;password=;");
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Form1 loginpage = new Form1();
            loginpage.Show();
            this.Hide();

        }

        private void btnRegisterAccount_Click(object sender, EventArgs e)
        {
            //declare Variable for inputs
            string name = txtName.Text;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("please enter" + "both name, username and password");

            }

            try
            {
                connection.Open();
                string registerquery = "INSERT INTO table1 (name, username ,password) VALUES (@name, @username, @password)";
                MySqlCommand command = new MySqlCommand(registerquery, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("register Succesfully");
                }
                else
                {
                    MessageBox.Show("failed to register");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
