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
    public partial class Form3 : Form
    {

        DataBasecs data = new DataBasecs();

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.OpenConnection();

            var login = textBox1.Text;
            var password =textBox2.Text;

            string query = $"update roles_tbl set password_roles = '{password}' where login_roles =  '{login}'";

            NpgsqlCommand command = new NpgsqlCommand(query,data.GetConnection());

            command.ExecuteNonQuery();

            data.CloseConnection();
        }
    }
}
