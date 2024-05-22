using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
namespace ACT1A_CRUD
{
    public partial class Admin : Form
    {
        private MySqlConnection connection;

        public object ID { get; private set; }

        public Admin()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=yesyes;username=root;password=;");
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void loaddata()
        {
            try
            {
                connection.Open();
                string showallrecordquery = "SELECT id, username, name, role FROM table1 ORDER BY id ASC";
                MySqlCommand command = new MySqlCommand(showallrecordquery, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtid.Text = row.Cells["id"].Value.ToString();
                txtname.Text = row.Cells["name"].Value.ToString();
                txtusername.Text = row.Cells["username"].Value.ToString();
                cbrole.Text = row.Cells["role"].Value.ToString();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string search = txtsearch.Text;
                connection.Open();
                string showallrecordquery = "SELECT ID, username, name, role FROM table1 WHERE username LIKE CONCAT('%', @search, '%') OR name LIKE CONCAT('%', @search, '%')  OR role LIKE CONCAT('%', @search, '%')  OR ID LIKE CONCAT('%', @search, '%')";
                MySqlCommand command = new MySqlCommand(showallrecordquery, connection);
                command.Parameters.AddWithValue("@search", search);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            //declare Variable for inputs
            string name = txtname.Text;
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text;
            string role = cbrole.Text;



            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(role))
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
                command.Parameters.AddWithValue("@role", role);


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
            txtusername.Clear();
            txtpassword.Clear();
            txtname.Clear();
            loaddata();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            //declare Variable for inputs
            string name = txtname.Text;
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text;
            string role = cbrole.Text;
            string ID = txtid.Text;




            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("please enter" + "both name, username and password");

            }

            try
            {
                connection.Open();
                string updatequery = "UPDATE table1 SET name = @name, username = @username, password = @password, role = @role WHERE ID = @ID";
                MySqlCommand command = new MySqlCommand(updatequery, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@ID", ID);



                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update Succesfully");
                }
                else
                {
                    MessageBox.Show("failed to update");
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
            txtusername.Clear();
            txtpassword.Clear();
            txtname.Clear();
            loaddata();
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            

        }

        private void btnremove_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete this record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string id = txtid.Text;
                if (string.IsNullOrWhiteSpace(id))
                {
                    MessageBox.Show("No Record found!");
                    return;
                }

                try
                {
                    connection.Open();
                    string deletequery = "DELETE FROM table1 WHERE ID = @ID";
                    MySqlCommand command = new MySqlCommand(deletequery, connection);
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record succesfully Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete record, please try again!");
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
                txtusername.Clear();
                txtpassword.Clear();
                txtname.Clear();    
                txtid.Clear();
                loaddata();
            }
            else
            {
                MessageBox.Show("Deletion cancelled!");
            }
        }
    }
}
