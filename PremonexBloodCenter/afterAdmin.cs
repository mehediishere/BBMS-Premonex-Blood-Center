using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PremonexBloodCenter
{
    public partial class afterAdmin : Form
    {
        public MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlDataReader MyReader2;
        public afterAdmin()
        {
            InitializeComponent();
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text
            //dataGridView1.BackgroundColor = Color.FromArgb(255, 192, 128);  //background colour

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour
        }

        private void afterAdmin_Load(object sender, EventArgs e) 
        {
            try
            {
                string Query = "select * from messagebox;";
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);

                labelCount.Text = dTable.Rows.Count.ToString(); 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                string Query = "select Date,Email,phone,Blood,Message from messagebox";
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

        

        private void button16_Click(object sender, EventArgs e) //panelSmsPop to hide
        {
            dataGridView1.Visible = true;
            panelSmsPop.Visible = false;
            button1.Visible = true;
        }

        



        private void btnDonor_Click(object sender, EventArgs e) // Donor button call afterAdminDonor UserControl
        {
            pass_user.Instance.Visible = false;
            //panelMessage.Visible = false;
            adminEvent.Instance.Visible = false;
            panelSmsPop.Visible = false;
            afterAdminDonor.Instance.Visible = true;
            bloodStored.Instance.Visible = false;
            Patient.Instance.Visible = false;
            DonorAndPatient.Instance.Visible = false;

            if (!panelBack.Controls.Contains(afterAdminDonor.Instance))
            {
                panelBack.Controls.Add(afterAdminDonor.Instance);
                afterAdminDonor.Instance.Dock = DockStyle.Fill;
                afterAdminDonor.Instance.BringToFront();

            }
            else
                afterAdminDonor.Instance.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e) //logout btn
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void btnmessage_Click(object sender, EventArgs e)
        {
            pass_user.Instance.Visible = false;
            afterAdminDonor.Instance.Visible = false;
            bloodStored.Instance.Visible = false;
            Patient.Instance.Visible = false;
            DonorAndPatient.Instance.Visible = false;
            adminEvent.Instance.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e) //blood stored
        {
            pass_user.Instance.Visible = false;
            bloodStored.Instance.Visible = true;
            Patient.Instance.Visible = false;
            DonorAndPatient.Instance.Visible = false;
            adminEvent.Instance.Visible = false;

            if (!panelBack.Controls.Contains(bloodStored.Instance))
            {
                panelBack.Controls.Add(bloodStored.Instance);
                bloodStored.Instance.Dock = DockStyle.Fill;
                bloodStored.Instance.BringToFront();

            }
            else
                bloodStored.Instance.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e) // Patient btn
        {
            pass_user.Instance.Visible = false;
            adminEvent.Instance.Visible = false;
            Patient.Instance.Visible = true;
            DonorAndPatient.Instance.Visible = false;
            if (!panelBack.Controls.Contains(Patient.Instance))
            {
                panelBack.Controls.Add(Patient.Instance);
                Patient.Instance.Dock = DockStyle.Fill;
                Patient.Instance.BringToFront();

            }
            else
                Patient.Instance.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e) //Donor & Patient appoinment
        {
            pass_user.Instance.Visible = false;
            adminEvent.Instance.Visible = false;
            DonorAndPatient.Instance.Visible = true;
            if (!panelBack.Controls.Contains(DonorAndPatient.Instance))
            {
                panelBack.Controls.Add(DonorAndPatient.Instance);
                DonorAndPatient.Instance.Dock = DockStyle.Fill;
                DonorAndPatient.Instance.BringToFront();

            }
            else
                DonorAndPatient.Instance.BringToFront();
        }

        

        private void button6_Click(object sender, EventArgs e) // camp btn
        {
            pass_user.Instance.Visible = false;
            adminEvent.Instance.Visible = true;
            if (!panelBack.Controls.Contains(adminEvent.Instance))
            {
                panelBack.Controls.Add(adminEvent.Instance);
                adminEvent.Instance.Dock = DockStyle.Fill;
                adminEvent.Instance.BringToFront();

            }
            else
                adminEvent.Instance.BringToFront();
        }

        private void button18_Click(object sender, EventArgs e) // consent btn
        {
            
            pass_user.Instance.Visible = true;
            if (!panelBack.Controls.Contains(pass_user.Instance))
            {
                panelBack.Controls.Add(pass_user.Instance);
                pass_user.Instance.Dock = DockStyle.Fill;
                pass_user.Instance.BringToFront();

            }
            else
                pass_user.Instance.BringToFront();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Visible = false;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                labelEm.Text = row.Cells["Email"].Value.ToString();
                labelPn.Text = row.Cells["Phone"].Value.ToString();
              //labelW.Text = row.Cells["Within"].Value.ToString();
                //labelMs.Text = row.Cells["Message"].Value.ToString();
                labelBt.Text = row.Cells["Blood"].Value.ToString();
                label3.Text = row.Cells["Date"].Value.ToString();
                

            }

            panelSmsPop.Visible = true;
            dataGridView1.Visible = false;

            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from messagebox where Email = '" + labelEm.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                labelW.Text = dt.Rows[0]["Within"].ToString();
                labelMs.Text = dt.Rows[0]["Message"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void button1_Click(object sender, EventArgs e) //remove btn
        {
            string rowIndex1 = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
            string rowIndex2 = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
            string rowIndex3 = dataGridView1.CurrentRow.Cells["Blood"].Value.ToString();
            string rowIndex4 = dataGridView1.CurrentRow.Cells["Message"].Value.ToString();
            //dataGridView1.Rows.RemoveAt(rowIndex);
            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                string Query = "delete from messagebox where Email='" + rowIndex1 + "' AND Phone='" + rowIndex2.ToString() + "' AND Blood='" + rowIndex3 + "'AND Message='" + rowIndex4 + "' ";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Data Deleted");
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
                string Query = "select Date,Email,phone,Blood,Message from messagebox";
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
