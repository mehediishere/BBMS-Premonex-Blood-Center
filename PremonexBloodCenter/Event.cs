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
    public partial class Event : UserControl
    {
        private static Event _instance;
        public static Event Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Event();
                return _instance;
            }
        }
        public Event()
        {
            InitializeComponent();

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
            //dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text
            dataGridView1.BackgroundColor = Color.FromArgb(255, 249, 249);  //background colour

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour
        }

        private void Event_Load(object sender, EventArgs e)
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
    }
}
