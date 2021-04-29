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
    public partial class adminEvent : UserControl
    {
        private static adminEvent _instance;
        public static adminEvent Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new adminEvent();
                return _instance;
            }
        }
        public adminEvent()
        {
            InitializeComponent();

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
           //dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text
            dataGridView1.BackgroundColor = Color.FromArgb(255, 192, 128);  //background colour

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour
        }

        private void button1_Click(object sender, EventArgs e) // save event
        {
            try
            {
                 
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";

                string Query = "insert into event(Date,Time,Location,Organizer,OrganizerContactNo) values('" + this.dateTimePicker1.Text.ToString() + "','" + this.maskedTextBox2.Text + "','" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.maskedTextBox3.Text + "');";

                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);

                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();  
                MessageBox.Show("Save Data");
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
                string Query = "select * from event";
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

        private void adminEvent_Load(object sender, EventArgs e)
        {
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                string Query = "select * from event";
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

        private void button2_Click(object sender, EventArgs e)
        {
            string rowIndex1 = dataGridView1.CurrentRow.Cells["Date"].Value.ToString();
            string rowIndex2 = dataGridView1.CurrentRow.Cells["Time"].Value.ToString();
            string rowIndex3 = dataGridView1.CurrentRow.Cells["Location"].Value.ToString();
            string rowIndex4 = dataGridView1.CurrentRow.Cells["Organizer"].Value.ToString();
            string rowIndex5 = dataGridView1.CurrentRow.Cells["OrganizerContactNo"].Value.ToString();
           
            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                string Query = "delete from event where Time='" + rowIndex2 + "' AND Location='" + rowIndex3.ToString() + "' AND Organizer='" + rowIndex4 + "'AND OrganizerContactNo='" + rowIndex5 + "' ";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Event Deleted");
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
                string Query = "select * from event";
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
    }
}
