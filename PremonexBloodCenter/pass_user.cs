using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PremonexBloodCenter
{
    public partial class pass_user : UserControl
    {
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");

        private static pass_user _instance;
        public static pass_user Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new pass_user();
                return _instance;
            }
        }
        public pass_user()
        {
            InitializeComponent();
            panel242.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // ok btn
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=premonexbc");

            MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from admin_pass where password='" + textBox1.Text + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                panel1001.Visible = true;
                panel242.Visible = true;
                textBox1.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Incorrect Password !!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pass_user_Load(object sender, EventArgs e)
        {
            panel242.Visible = false;
            panelAdminInfo.Visible = true;
            try
            {
                string Query = "select Email from pass_user ";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (MyReader2.Read())
                {
                    mycollection.Add(MyReader2.GetString(0));
                }
                textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox2.AutoCompleteCustomSource = mycollection;

                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MySqlDataAdapter sda = new MySqlDataAdapter("select * from admin_pass", MyConn2);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                textBox123.Text = dt.Rows[0]["username"].ToString();
                textBox124.Text = dt.Rows[0]["password"].ToString();
                textBox125.Text = dt.Rows[0]["id"].ToString();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }

        private void button32_Click(object sender, EventArgs e) //remove user btn
        {
            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                string Query = "delete from pass_user where Email='" + this.textBox2.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("User Deleted");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";

                string Query = "select * from pass_user";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e) //user btn
        {
            panelAdminInfo.Visible = false;
            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                
                string Query = "select * from pass_user";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e) //admin btn
        {
            panelAdminInfo.Visible = true;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                string Query = "update admin_pass set username='" + this.textBox123.Text + "',password='" + this.textBox124.Text + "',id='" + this.textBox125.Text + "'";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
              
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     
                MessageBox.Show("Updated Data");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button34_Click(object sender, EventArgs e) //done btn
        {
            pass_user.Instance.Visible = false;
            panel1001.Visible = false;

           
        }
    }
}
