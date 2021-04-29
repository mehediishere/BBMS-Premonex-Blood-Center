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
    public partial class bloodStored : UserControl
    {
        public MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public DataSet ds = new DataSet();
        private static bloodStored _instance;
        public static bloodStored Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new bloodStored();
                return _instance;
            }
        }
        public bloodStored()
        {
            InitializeComponent();
            panelBloQuantity.Visible = false;
            panelExpireBag.Visible = false;
            panelUpdateBlood.Visible = false;

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text
            dataGridView1.BackgroundColor = Color.FromArgb(255, 192, 128);  //background colour

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour

            ///////////

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text
            dataGridView2.BackgroundColor = Color.FromArgb(255, 192, 128);  //background colour

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour

            ///////////

            dataGridView4.BorderStyle = BorderStyle.None;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text
            dataGridView4.BackgroundColor = Color.FromArgb(255, 192, 128);  //background colour

            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour
        }

        private void bloodStored_Load(object sender, EventArgs e)
        {
            panelUpdateBlood.Visible = false;
            panelBloQuantity.Visible = false;
            panelTotalBlood.BringToFront();
            try
            {
                string Query = "select batch from bloodstored;";
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
                string Query = "select batch from bloodstored;";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (MyReader2.Read())
                {
                    mycollection.Add(MyReader2.GetString(0));
                }
                

                textBox33.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox33.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox33.AutoCompleteCustomSource = mycollection;
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                
                string Query = " select blood as 'Blood Type', sum(quantity) as 'Quantity' from bloodstored where blood = 'A+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'A-' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'B+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'B-'   union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'O+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'O-' union  select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'AB+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'AB-'; ";
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

        private void button13_Click(object sender, EventArgs e) //blood quantity btn
        {
            panelBloQuantity.Visible = true;
            panelExpireBag.Visible = false;
            panelUpdateBlood.Visible = false;

            string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
            try
            {
                string Query = "select Blood,Quantity,shelf as 'Shelf No.',batch as 'Batch No.',collect as 'Collected',expire as 'Exp. Date' from bloodstored";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView2.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e) //reserved btn
        {
            panelBloQuantity.Visible = false;
            panelExpireBag.Visible = false;
            panelUpdateBlood.Visible = false;
            
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";

                string Query = " select blood as 'Blood Type', sum(quantity) as 'Quantity' from bloodstored where blood = 'A+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'A-' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'B+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'B-'   union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'O+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'O-' union  select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'AB+' union select blood as 'Blood Type', sum(quantity) as 'Quantity'  from bloodstored where blood = 'AB-'; ";
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

        private void button1_Click(object sender, EventArgs e) // add btn
        {
            string MyConnection2 = "server=localhost;user id=root;database=premonexbc";

            try
            {
                string Query = "insert into bloodstored(blood,quantity,shelf,batch,collect,expire) values('" + txt1.Text + "','" + txt2.Text + "','" + this.txt3.Text + "','" + txt4.Text + "','" + dateTimePicker1.Value.ToString("yyyy-M-d") + "','" + dateTimePicker2.Value.ToString("yyyy-M-d") + "');";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                //MessageBox.Show("Save Data");
                txt1.Text = null; txt3.Text = string.Empty; txt2.Text = string.Empty; txt4.Text = string.Empty; dateTimePicker2.Text = string.Empty; dateTimePicker2.Text = string.Empty;
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
                string Query = "select * from bloodstored;";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView2.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e) // search btn from blood quantity
        {
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                //Display query  
                string Query = "select * from bloodstored where blood = '"+textBox1.Text+ "' or batch = '" + textBox1.Text + "' or shelf = '" + textBox1.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();  
                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView2.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.               
                                                   // MyConn2.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Expire search bar btn
        {
            
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                //Display query  
                string Query = "select  blood as 'Blood Type',quantity as 'Quantity',shelf as 'Shelf No.',batch as 'Batch No.',collect as 'Collected',expire as 'Exp. Date' from bloodstored where expire between '" + dateTimePicker3.Value.ToString("yyyy-M-d") + "' and '" + dateTimePicker4.Value.ToString("yyyy-M-d") + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();  
                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView3.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.               
                                                   // MyConn2.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e) // expire blood bag btn
        {
            panelBloQuantity.Visible = true;
            panelExpireBag.Visible = true;
            panelUpdateBlood.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e) //update blood quantity btn
        {
            panelBloQuantity.Visible = true;
            panelExpireBag.Visible = true;
            panelUpdateBlood.Visible = true;

            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                string Query = "select  blood as 'Blood Type',quantity as 'Quantity',shelf as 'Shelf No.',batch as 'Batch No.',collect as 'Collected',expire as 'Exp. Date' from bloodstored; ";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView4.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e) //update search bar
        {
            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from bloodstored where batch = '" + textBox33.Text + "' ", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                metroComboBox2.Text = dt.Rows[0]["blood"].ToString();
                textBox4.Text = dt.Rows[0]["quantity"].ToString();
                textBox6.Text = dt.Rows[0]["shelf"].ToString();
                textBox5.Text = dt.Rows[0]["batch"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void button7_Click(object sender, EventArgs e) // Update btn blood quantity
        {
            try
            {
                MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
                string Query = "update bloodstored set blood='" + metroComboBox2.Text + "',quantity='" + textBox4.Text + "',shelf='" + textBox6.Text + "',batch='" + textBox5.Text + "' where batch = '" + textBox5.Text + "';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn);
                MySqlDataReader MyReader2;
                MyConn.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Successfully Updated");
                metroComboBox2.Text = null; textBox4.Text = string.Empty; textBox6.Text = string.Empty; textBox5.Text = string.Empty;
                while (MyReader2.Read())
                {
                }
                MyConn.Close();//Connection closed here  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                string Query = "select  blood as 'Blood Type',quantity as 'Quantity',shelf as 'Shelf No.',batch as 'Batch No.',collect as 'Collected',expire as 'Exp. Date' from bloodstored; ";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView4.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e) // refresh btn
        {
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
 
                string Query = "select blood,quantity,shelf as 'Shelf No.',batch as 'Batch No.',collect as 'Collected',expire as 'Exp. Date' from bloodstored";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
 
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView2.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e) // delete batch
        {
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                string Query = "delete from bloodstored where batch='" + this.textBox5.Text + "';";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Data Deleted");
                textBox4.Text = string.Empty; textBox5.Text = string.Empty; textBox6.Text = string.Empty; textBox33.Text = string.Empty;metroComboBox2.Text = null;
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
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                string Query = "select  blood as 'Blood Type',quantity as 'Quantity',shelf as 'Shelf No.',batch as 'Batch No.',collect as 'Collected',expire as 'Exp. Date' from bloodstored; ";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView4.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
