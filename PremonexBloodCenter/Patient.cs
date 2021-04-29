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
using System.Drawing.Printing;
using DGVPrinterHelper;


namespace PremonexBloodCenter
{
    public partial class Patient : UserControl
    {
        PrintPreviewDialog pp = new PrintPreviewDialog();
        PrintDocument pd = new PrintDocument();

        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        private static Patient _instance;
        public static Patient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Patient();
                return _instance;
            }
        }
        public Patient()
        {
            InitializeComponent();
            label31.Visible = false;
            panelNewPatient.Visible = false;
            panelEdit.Visible = false;
/*
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
*/
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Patient_Load(object sender, EventArgs e)
        {
            panelNewPatient.Visible = false;
            panelEdit.Visible = false;

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
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteCustomSource = mycollection;

                textBox20.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox20.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox20.AutoCompleteCustomSource = mycollection;

                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {
                string Query = "select Name as 'Name',Email as 'Email',birth as 'Birth Date',Age,Gender,Blood_type as 'Blood Type',Phone as 'Phone',Address as 'Address',Disease as 'Disease' from patient;";
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

        private void button4_Click(object sender, EventArgs e) // search bar for patient
        {
            try
            {
                string Query = "select Name as 'Name',Email as 'Email',birth as 'Birth Date',Age,Gender,Blood_type as 'Blood Type',Phone as 'Phone',Address as 'Address',Disease as 'Disease' from patient where Name = '"+textBox1.Text+"';";
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

            if (textBox1.Text == string.Empty)
            {
                try
                {
                    string Query = "select Name as 'Name',Email as 'Email',birth as 'Birth Date',Age,Gender,Blood_type as 'Blood Type',Phone as 'Phone',Address as 'Address',Disease as 'Disease' from patient;";
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

        private void button13_Click(object sender, EventArgs e) // New patient btn
        {
            panelNewPatient.Visible = true;
            panelEdit.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e) //patient list
        {
            panelNewPatient.Visible = false;
            panelEdit.Visible = false;

            try
            {
                string Query = "select Name as 'Name',Email as 'Email',birth as 'Birth Date',Age,Gender,Blood_type as 'Blood Type',Phone as 'Phone',Address as 'Address',Disease as 'Disease' from patient ;";
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

        private void button3_Click(object sender, EventArgs e) //upload
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(opf.FileName);
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                byte[] img = ms.ToArray();

                string Query = "insert into patient(Name,Email,birth,Age,Gender,Blood_type,Phone,Address,Disease,bd2,batch_no,Date,Ref_by,Image) values('" + t1.Text + "','" + t2.Text + "','" + t3.Text + "','" + t4.Text + "','" + t5.Text + "','" + t6.Text + "','" + t7.Text + "','" + t8.Text + "','" + t9.Text + "','" +t10.Text + "','" + t11.Text + "','" + t12.Text.ToString() + "','" + t13.Text + "',@img);";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();

                MyCommand2.Parameters.Add("@img", MySqlDbType.Blob);
                MyCommand2.Parameters["@img"].Value = img;

                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Save Data");
                t1.Text = string.Empty; t2.Text = string.Empty; t3.Text = string.Empty; t4.Text = string.Empty; t5.Text = null; t6.Text = null; t7.Text = string.Empty; t8.Text = string.Empty; t9.Text = string.Empty; t10.Text = null; t11.Text = string.Empty; t12.Text = null; t13.Text = string.Empty; pictureBox2.Image = null;
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

        private void t1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch) && !Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
            }
        }

        private void t4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void t7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch) && e.KeyChar == 107)
            {

                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e) //print patient list
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Premonex Blood Center";//Header
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Senpara Parbata,Mirpur-10,Dhaka-1216";//Footer
            printer.FooterSpacing = 1;
            //Print landscape mode
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dataGridView1);
            
        }
        
        private void button6_Click(object sender, EventArgs e) //search btn for patient bio
        {
            MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from patient where Name = '" + textBox20.Text + "' OR Phone = '" + textBox20.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                
                label32.Text = dt.Rows[0]["Name"].ToString();
                label33.Text = dt.Rows[0]["Email"].ToString();
                label34.Text = dt.Rows[0]["birth"].ToString();
                label35.Text = dt.Rows[0]["Age"].ToString();
                label36.Text = dt.Rows[0]["Gender"].ToString();
                label37.Text = dt.Rows[0]["Blood_type"].ToString();
                label38.Text = dt.Rows[0]["Phone"].ToString();
                label29.Text = dt.Rows[0]["Address"].ToString();
                label30.Text = dt.Rows[0]["Disease"].ToString();
                label39.Text = dt.Rows[0]["bd2"].ToString();
                label40.Text = dt.Rows[0]["batch_no"].ToString();
                label41.Text = dt.Rows[0]["Date"].ToString();
                label42.Text = dt.Rows[0]["Ref_by"].ToString();

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

        private void button15_Click(object sender, EventArgs e)
        {
            panelNewPatient.Visible = true;
            panelEdit.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e) //print patient bio
        {
            print(this.panel303);
        }

        public void print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panel303 = pnl;
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
            e.Graphics.DrawImage(m, (pagearea.Width ) - (this.panel303.Width ), this.panel303.Location.Y);
        }

        private void panel303_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelEdit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panelNewPatient.Visible = true;
            panelEdit.Visible = true;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                label31.Text = row.Cells["Name"].Value.ToString();
                label32.Text = row.Cells["Name"].Value.ToString();
                label33.Text = row.Cells["Email"].Value.ToString();
                label34.Text = row.Cells["birth date"].Value.ToString();
                label35.Text = row.Cells["Age"].Value.ToString();
                label36.Text = row.Cells["Gender"].Value.ToString();
                label37.Text = row.Cells["Blood type"].Value.ToString();
                label38.Text = row.Cells["Phone"].Value.ToString();
                label29.Text = row.Cells["Address"].Value.ToString();
                label30.Text = row.Cells["Disease"].Value.ToString();
               

            }
            
            MySqlDataAdapter sdaa = new MySqlDataAdapter("select * from Patient where Name = '" + label31.Text + "'", MyConn2);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            try
            {
                label39.Text = dtt.Rows[0]["bd2"].ToString();
                label40.Text = dtt.Rows[0]["batch_no"].ToString();
                label41.Text = dtt.Rows[0]["Date"].ToString();
                label42.Text = dtt.Rows[0]["Ref_by"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MySqlConnection MyConnm = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sdaq = new MySqlDataAdapter("select * from patient where name = '" + label31.Text + "'", MyConnm);
            DataTable dtq = new DataTable();
            sdaq.Fill(dtq);
            try
            {
                byte[] img = (byte[])dtq.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox3.Image = Image.FromStream(ms);
                sdaq.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label32.Text = null; label33.Text = null;label34.Text = null;label35.Text = null;label36.Text = null;label37.Text = null;
            label38.Text = null;label29.Text = null;label30.Text = null;label39.Text = null;label40.Text = null;label41.Text = null;
            label42.Text = null; pictureBox3.Image = null;
        }
    }
}
