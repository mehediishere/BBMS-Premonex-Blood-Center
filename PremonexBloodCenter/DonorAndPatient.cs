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
using System.IO;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace PremonexBloodCenter
{
    public partial class DonorAndPatient : UserControl
    {
        PrintPreviewDialog pp = new PrintPreviewDialog();
        PrintDocument pd = new PrintDocument();
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");

        private static DonorAndPatient _instance;
        public static DonorAndPatient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DonorAndPatient();
                return _instance;
            }
        }
        public DonorAndPatient()
        {
            InitializeComponent();
            button6.Visible = false;
            dataGridView3.Visible = false;
            labeldon.Visible = false;
            labelpas.Visible = false;

/*
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(128, 212, 255);  //row colour
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(53, 222, 157);  //selected row
            //dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White; //selected row text  */
            dataGridView1.BackgroundColor = Color.White;  //background colour

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour

            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204); //header background colour
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 51, 0); //header text colour
        }

        private void DonorAndPatient_Load(object sender, EventArgs e)
        {
            button6.Visible = true;
            panel9.Visible = true;

            try
            {
                string Query = "select image1 as 'Donor Image',name1 as 'Donor Name',blood1 as 'Donor Blood Type', phone1 as 'Donor Phone number',address1 as 'Donor Address',image2 as 'Patient Image',name2 as 'Patient Name',blood2 as 'Patient Blood Type', phone2 as 'Patient Phone number',address2 as 'Patient Address' from appoinment;";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();  
                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.               
                                                   // MyConn2.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
                string Query = "select Name from patient;";
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
        }

        private void button1_Click(object sender, EventArgs e) // donor search
        {
            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from donor where Name = '" + textBox1.Text + "' OR Phone = '" + textBox1.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {

                label32.Text = dt.Rows[0]["Name"].ToString();
                label33.Text = dt.Rows[0]["Blood"].ToString();
                label34.Text = dt.Rows[0]["Phone"].ToString();
                label2.Text = dt.Rows[0]["Address"].ToString();
               

                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox2.Image = Image.FromStream(ms);
                sda.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Donor Couldn't Find!!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) //patient search
        {
            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from patient where Name = '" + textBox2.Text + "' OR Phone = '" + textBox2.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {

                label7.Text = dt.Rows[0]["Name"].ToString();
                label8.Text = dt.Rows[0]["blood_type"].ToString();
                label9.Text = dt.Rows[0]["Phone"].ToString();
                label10.Text = dt.Rows[0]["Address"].ToString();


                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox3.Image = Image.FromStream(ms);
                sda.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Donor Couldn't Find!!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) //save btn
        {
            try
            {

                MemoryStream ms = new MemoryStream();
                pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                byte[] img = ms.ToArray();

                MemoryStream mss = new MemoryStream();
                pictureBox3.Image.Save(mss, pictureBox3.Image.RawFormat);
                byte[] img2 = mss.ToArray();

                string Query = "insert into appoinment(name1,blood1,phone1,address1,name2,blood2,phone2,address2,image1,image2) values('" + this.label32.Text + "','" + this.label33.Text + "','" + this.label34.Text + "','" + this.label2.Text + "','" + this.label7.Text + "','" + this.label8.Text + "','" + this.label9.Text + "','" + this.label10.Text + "',@img,@img2);"; 
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();

                MyCommand2.Parameters.Add("@img", MySqlDbType.Blob);
                MyCommand2.Parameters["@img"].Value = img;

                MyCommand2.Parameters.Add("@img2", MySqlDbType.Blob);
                MyCommand2.Parameters["@img2"].Value = img2;

                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
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
        }

        private void button5_Click(object sender, EventArgs e) //ptint appoinment form
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Premonex Blood Center";//Header
            print(this.panel1);

        }
        public void print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panel1 = pnl;
            getprintarea(pnl);
            pp.Document = pd;
            pd.PrintPage += new PrintPageEventHandler(pdpt);
            pp.ShowDialog();
        }

        Bitmap m;
        public void getprintarea(Panel pnl)
        {
            m = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(m, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        public void pdpt(object Sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(m, (pagearea.Width) - (this.panel1.Width), this.panel1.Location.Y);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button15_Click(object sender, EventArgs e) // Form btn
        {
            dataGridView3.Visible = false;
            button6.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            panel9.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e) // Appoinment btn
        {
            labelpas.Visible = false;
            labeldon.Visible = false;
            labelapp.Visible = true;

            panel9.Visible = true;
            dataGridView3.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            button6.Visible = true;
            try
            {
                string Query = "select image1 as 'Donor Image',name1 as 'Donor Name',blood1 as 'Donor Blood Type', phone1 as 'Donor Phone number',address1 as 'Donor Address',image2 as 'Patient Image',name2 as 'Patient Name',blood2 as 'Patient Blood Type', phone2 as 'Patient Phone number',address2 as 'Patient Address' from appoinment;";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();  
                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.               
                                                   // MyConn2.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e) // Eligible donor btn
        {
            labelpas.Visible = false;
            labeldon.Visible = true;
            labelapp.Visible = false;

            panel9.Visible = true;
            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
            button6.Visible = false;
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                
                string Query = " SELECT * FROM donor WHERE DATEDIFF(Now(),Last_donate)>= 90";
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                label32.Text = row.Cells["donor name"].Value.ToString();
                label33.Text = row.Cells["donor blood type"].Value.ToString();
                label34.Text = row.Cells["donor phone number"].Value.ToString();
                label2.Text = row.Cells["donor address"].Value.ToString();

                label7.Text = row.Cells["patient name"].Value.ToString();
                label8.Text = row.Cells["patient blood type"].Value.ToString();
                label9.Text = row.Cells["patient phone number"].Value.ToString();
                label10.Text = row.Cells["patient address"].Value.ToString();

            }
            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from donor where name = '" + label32.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox2.Image = Image.FromStream(ms);
                sda.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            MySqlConnection MyConnm = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sdaa = new MySqlDataAdapter("select * from patient where name = '" + label7.Text + "'", MyConnm);
            DataTable dta = new DataTable();
            sdaa.Fill(dta);
            try
            {
                byte[] img = (byte[])dta.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox3.Image = Image.FromStream(ms);
                sdaa.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            dataGridView1.Visible = false;
            panel9.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string rowIndex1 = dataGridView1.CurrentRow.Cells["Donor Name"].Value.ToString();
            string rowIndex2 = dataGridView1.CurrentRow.Cells["Donor Blood Type"].Value.ToString();
            string rowIndex3 = dataGridView1.CurrentRow.Cells["Donor Phone number"].Value.ToString();
            string rowIndex4 = dataGridView1.CurrentRow.Cells["Donor Address"].Value.ToString();

            string rowIndex5 = dataGridView1.CurrentRow.Cells["Patient Name"].Value.ToString();
            string rowIndex6 = dataGridView1.CurrentRow.Cells["Patient Blood Type"].Value.ToString();
            string rowIndex7 = dataGridView1.CurrentRow.Cells["Patient Phone number"].Value.ToString();
            string rowIndex8 = dataGridView1.CurrentRow.Cells["Patient Address"].Value.ToString();
            //dataGridView1.Rows.RemoveAt(rowIndex);
            try
            {
                string MyConnection2 = "server = localhost; user id = root; database=premonexbc";
                string Query = "delete from appoinment where name1='" + rowIndex1 + "' AND blood1='" + rowIndex2.ToString() + "' AND phone1='" + rowIndex3 + "'AND address1='" + rowIndex4 + "' AND name2='" + rowIndex5 + "' AND blood2='" + rowIndex6.ToString() + "' AND phone2='" + rowIndex7 + "'AND address2='" + rowIndex8 + "' ";
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
                string Query = "select image1 as 'Donor Image',name1 as 'Donor Name',blood1 as 'Donor Blood Type', phone1 as 'Donor Phone number',address1 as 'Donor Address',image2 as 'Patient Image',name2 as 'Patient Name',blood2 as 'Patient Blood Type', phone2 as 'Patient Phone number',address2 as 'Patient Address' from appoinment;";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                //  MyConn2.Open();  
                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.               
                                                   // MyConn2.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            labelpas.Visible = true;
            labeldon.Visible = false;
            labelapp.Visible = false;

            panel9.Visible = true;
            dataGridView3.Visible = true;
            dataGridView3.BringToFront();
            dataGridView2.Visible = true;
            button6.Visible = false;

            try
            {
                string Query = "select Name as 'Name',Email as 'Email',birth as 'Birth Date',Age,Gender,Blood_type as 'Blood Type',Phone as 'Phone',Address as 'Address',Disease as 'Disease',Image from patient ;";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView3.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) // dgv2
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                label32.Text = row.Cells["name"].Value.ToString();
                label33.Text = row.Cells["blood"].Value.ToString();
                label34.Text = row.Cells["phone"].Value.ToString();
                label2.Text = row.Cells["address"].Value.ToString();

            }
            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from donor where name = '" + label32.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox2.Image = Image.FromStream(ms);
                sda.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            dataGridView3.Visible = false;
            button6.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            panel9.Visible = false;
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) //dvg3
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];

                label7.Text = row.Cells["name"].Value.ToString();
                label8.Text = row.Cells["blood type"].Value.ToString();
                label9.Text = row.Cells["phone"].Value.ToString();
                label10.Text = row.Cells["address"].Value.ToString();

            }
            MySqlConnection MyConnm = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sdaa = new MySqlDataAdapter("select * from patient where name = '" + label7.Text + "'", MyConnm);
            DataTable dta = new DataTable();
            sdaa.Fill(dta);
            try
            {
                byte[] img = (byte[])dta.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox3.Image = Image.FromStream(ms);
                sdaa.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            dataGridView3.Visible = false;
            button6.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            panel9.Visible = false;
        }
    }

}
