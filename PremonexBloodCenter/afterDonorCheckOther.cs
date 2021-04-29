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
    public partial class afterDonorCheckOther : UserControl
    {
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlConnection MyConn3 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        DataTable dTable;

        private static afterDonorCheckOther _instance;
        public static afterDonorCheckOther Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new afterDonorCheckOther();
                return _instance;
            }
        }
        public afterDonorCheckOther()
        {
            InitializeComponent();
        }

        private void afterDonorCheckOther_Load(object sender, EventArgs e)
        {
            try
            {
                string Query = "select Name from donor;";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (MyReader2.Read())
                {
                    mycollection.Add(MyReader2.GetString(0));
                }
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteCustomSource = mycollection;

                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

            try
            {
                
                string Query = "select * from donor;";
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

        private void button1_Click(object sender, EventArgs e) // search btn
        {
            try
            {
                //Name,Email,Age,Weight,Gender,Blood as 'Blood Type',Last_donate as 'Last donate',Disease,Phone,Address,Image
                string Query = "select * from donor where Blood = '" + textBox1.Text + "' or Name Like '%" + textBox1.Text + "%' or City Like '%" + textBox1.Text + "%'";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            afterDonorCheckOther.Instance.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            donorLogin dl = new donorLogin();
            dl.Show();
        }
    }
}
