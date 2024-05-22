using MySql.Data.MySqlClient;
namespace ACT1A_CRUD
{
    public partial class Form1 : Form
    {
        //Declare MySqlConnection for handling database connection
        private MySqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=yesyes;username=root;password=;");
        }

        private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("please enter both username and password");

            }

            try
            {
                connection.Open();
                string logininquiry = "SELECT COUNT(*) FROM table1  WHERE username = @username AND password = @password";

                MySqlCommand command = new MySqlCommand(logininquiry, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    Admin adminpage = new Admin();
                    adminpage.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("invalid username and password");
                }
            }

            catch (Exception ex)
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

        private void LinkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 signuppage = new Form2();
            signuppage.Show();
            this.Hide();
        }
    }
}
