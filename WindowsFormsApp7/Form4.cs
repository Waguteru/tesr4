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
    public partial class Form4 : Form
    {
        private string photo;
        private string name;
        private NpgsqlConnection connection;

        private Random random = new Random();

        public Form4(string name, string photo, Npgsql.NpgsqlConnection connection)
        {
            InitializeComponent();


            this.name = name;
            this.photo = photo;
          
            conn = connection;

            richTextBox1.Text = $"название товара: {this.name}\n\n" +
                                $"photo: {this.photo}\n\n" +
                                $"DATE: {DateTime.Now:dd/MM/yyyy}" +
                                $"number: {DataOrders()}";
        }

        private NpgsqlConnection conn;

        private string GenerateCodOrders()
        {
            return random.Next(100,999).ToString();
        }

        private string DataOrders()
        {
            return DateTime.Now.ToString("yyyMMddHHmmssfff");
        }


        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = $"название товара: {this.name}\n\n" +
                                $"photo: {this.photo}\n\n" +
                                $"place: {comboBox1.SelectedItem}\n\n" +
                                $"cod orders: {GenerateCodOrders()}\n\n"+
                                 $"DATE: {DateTime.Now:dd/MM/yyyy} \n\n"+
                                   $"number: {DataOrders()}";
        }
    }
}
