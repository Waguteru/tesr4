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
    public partial class Form1 : Form
    {

        DataBasecs data = new DataBasecs();

        enum RowState
        {
            New,
            Modified,
            Modifiednew,
            Deleted
        }

        int selectRow;

        public Form1()
        {
            InitializeComponent();
            button5.Visible = false;
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_products", "номер товара");
            dataGridView1.Columns.Add("name_product", "название");
            dataGridView1.Columns.Add("photo_products", "фото");
            dataGridView1.Columns.Add("IsNew",string.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;

        }

        public void ReadSingleRow(DataGridView view,IDataRecord record)
        {
            view.Rows.Add(record.GetInt32(0),record.GetString(1),record.GetString(2), RowState.Modified);
        }


        public void RefreshData(DataGridView datav)
        {
            datav.Rows.Clear();

            data.OpenConnection();

            string query = $"select id_products,name_product,photo_products from product_tbl";

            NpgsqlCommand command = new NpgsqlCommand(query,data.GetConnection());

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dataGridView1,reader);
            }
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshData(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string path = row.Cells[2].Value.ToString();
                if (!string.IsNullOrEmpty(path))
                {
                    pictureBox1.ImageLocation = path;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; 
                }
            }

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectRow];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
            }
        }


        public void DeteleRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DeteleRow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data.OpenConnection();

            var id = Convert.ToInt32(textBox1.Text);

            string query = $"delete from product_tbl where id_products = " + id;

            NpgsqlCommand command = new NpgsqlCommand(query,data.GetConnection());

            command.ExecuteNonQuery();

            data.CloseConnection();
        }


        public void EditRow() 
        {
            int selectEdit = dataGridView1.CurrentCell.RowIndex;

            var id = textBox1.Text;
            var name = textBox2.Text;
            var photo = textBox3.Text;  

            if (dataGridView1.Rows[selectEdit].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectEdit].SetValues(id, name, photo);
                dataGridView1.Rows[selectEdit].Cells[3].Value = RowState.Modifiednew;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            data.OpenConnection();

            var id = Convert.ToInt32(textBox1.Text);
            var name = textBox2.Text;
            var photo = textBox3.Text;

            string query = $"UPDATE product_tbl SET name_product = '{name}' where  id_products = " + id;

            NpgsqlCommand command = new NpgsqlCommand(query,data.GetConnection());

            command.ExecuteNonQuery();

            data.CloseConnection();
        }


        public  void Search(DataGridView gridView)
        {
            gridView.Rows.Clear();
           

            string query = $"select ID_products,name_product,photo_products from product_tbl where concat (name_product) like '%" + textBox4.Text + "%'";
       
            NpgsqlCommand command = new NpgsqlCommand(query, data.GetConnection());

            data.OpenConnection();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();
        
            data.CloseConnection();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Название(Возрастание)")
            {
                dataGridView1.Sort(dataGridView1.Columns[0],ListSortDirection.Ascending);
            }
            if(comboBox1.SelectedItem == "Название(Убывание)")
            {
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            }
        }

        private int clicedIndex = -1;



        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           if(e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                clicedIndex = e.RowIndex;

                button5.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(clicedIndex >= 0 && clicedIndex < dataGridView1.Rows.Count)
            {
                string name = dataGridView1.Rows[clicedIndex].Cells[1].Value.ToString();
                string photo = dataGridView1.Rows[clicedIndex].Cells[2].Value.ToString();

                NpgsqlConnection connection = new NpgsqlConnection("Server = localhost; port = 5432; DataBase = zooShop; User Id = postgres; Password = 123");

                Form4 form4 = new Form4(name, photo,connection);

                this.Hide();

                form4.ShowDialog();

                this.Close();
            }
        }
    }
}
