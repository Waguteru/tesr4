using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form2 : Form
    {

        DataBasecs data = new DataBasecs();

        private bool clolsed = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (clolsed)
            {
                return;
            }
            else if(CheckLogin(login, password))
            {
                ShowroleForm(login);
            }
        }


        string connectionSt = "Server = localhost; port 5432; DataBase = qwe; Id User = postgres; Password = 123";

        private bool CheckLogin(string login, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionSt))
            {
                string query = $"select COUNT(*) from qq where login_tbl = @login and password = @password";
                using(NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", login);
                    command.Parameters.AddWithValue("password", password);

                    connection.Open();

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        private void ShowroleForm(string login)
        {
            string role = GetRoleUsers(login);

            if(role == "manager")
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Close();
            }
            else if(role == "admin")
            {
                Form2 form2 = new Form2();
                this.Hide();
                form2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("no roles");
            }
            this.Hide();
        }

        private string GetRoleUsers(string login)
        {
            string role = "";
            using(NpgsqlConnection connection = new NpgsqlConnection(connectionSt))
            {
                string query = $"select COUNT(*) from qq where login_tbl = @login and password = @password";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);

                    try
                    {

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if(result != null)
                        {
                           role = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("df");
                        }

                    }catch (Exception ex)
                    {
                        MessageBox.Show($"ошибка при получении роли пользователя: {ex.Message}", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            return role;
        }
    }
}
