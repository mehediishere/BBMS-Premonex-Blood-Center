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

namespace PremonexBloodCenter
{
    public partial class afterAdminDonor : UserControl
    {
        public MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlDataReader MyReader2;
        public MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=premonexbc");
        public MySqlCommand cmd;
        public DataSet ds = new DataSet();
        DataTable dTable;
        private static afterAdminDonor _instance;
        public static afterAdminDonor Instance
        {
            get
            {
                if ( _instance == null)
                    _instance = new afterAdminDonor();
                return _instance;
            }
        }
        public afterAdminDonor()
        {
            InitializeComponent();

            panelAddDonor.Visible = false;
            panelRemDonor.Visible = false;
            panelUpdateDonor.Visible = false;

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
        }

        private void button1_Click(object sender, EventArgs e) //activity btn
        {
           
        }

        private void afterAdminDonor_Load(object sender, EventArgs e) //after donor button(from afterAdmin page) click 
        {
            textBox1.Visible = true;
            panelAddDonor.Visible = false;
            panelRemDonor.Visible = false;
            panelUpdateDonor.Visible = false;

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

                textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox2.AutoCompleteCustomSource = mycollection;

                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox3.AutoCompleteCustomSource = mycollection;

                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {

                string Query = "select Name,Email,Age,Weight,Gender,Blood as 'Blood Type',Last_donate as 'Last donate',Disease,Phone,Address,Image from donor";
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

        

        private void button3_Click(object sender, EventArgs e) // search btn for donor list
        {
            try
            {

                string Query = "select Name,Email,Age,Weight,Gender,Blood as 'Blood Type',Last_donate as 'Last donate',Disease,Phone,Address,Image from donor where Blood = '" + textBox1.Text + "' or Name Like '%" + textBox1.Text + "%' or Email Like '%" + textBox1.Text + "%'";
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

        private void textBox1_TextChanged(object sender, EventArgs e) //search bar textBox1
        {
            DataView dv = new DataView(dTable);
            dv.RowFilter = string.Format("Name Like '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
        }

        private void button2_Click(object sender, EventArgs e) //donor list btn
        {
            textBox1.Visible = true;
            panelAddDonor.Visible = false;
            panelRemDonor.Visible = false;
            panelUpdateDonor.Visible = false;
            try
            {

                string Query = "select Name,Email,Age,Weight,Gender,Blood as 'Blood Type',Last_donate as 'Last donate',Disease,Phone,Address,Image from donor";
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

        private void button13_Click(object sender, EventArgs e) //add donor btn
        {
            textBox1.Visible = false;
            panelAddDonor.Visible = true;
            panelRemDonor.Visible = false;
            panelUpdateDonor.Visible = false;
        }

        private void button18_Click(object sender, EventArgs e)  //Add Donor -> Save btn
        {
            int i = 1;
            do
            {
                if (txt1.Text == string.Empty)
                {
                    MessageBox.Show("Name required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt2.Text == string.Empty)
                {
                    MessageBox.Show("Email required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt3.Text == string.Empty)
                {
                    MessageBox.Show("Age required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                if (txt4.Text == string.Empty)
                {
                    MessageBox.Show("Weight required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt5.Text == string.Empty)
                {
                    MessageBox.Show("Select Gender", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt6.Text == string.Empty)
                {
                    MessageBox.Show("Phone number required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt7.Text == string.Empty)
                {
                    MessageBox.Show("Current City required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt8.Text == string.Empty)
                {
                    MessageBox.Show("Current Address required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt9.Text == string.Empty)
                {
                    MessageBox.Show("Select your Blood type", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                if (pictureBox2.Image == null)
                {
                    MessageBox.Show("Profile picture required with (130 x 140) size!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                else
                {
                    cmd = new MySqlCommand("select * from donor where Email ='" + txt2.Text + "'", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);
                    int j = ds.Tables[0].Rows.Count;
                    if (j > 0)
                    {
                        MessageBox.Show("Email already in used!");
                        ds.Clear();
                        break;
                    }
                    cmd = new MySqlCommand("select * from donor where Phone ='" + txt8.Text + "'", con);
                    MySqlDataAdapter daa = new MySqlDataAdapter(cmd);
                    daa.Fill(ds);
                    int k = ds.Tables[0].Rows.Count;
                    if (k > 0)
                    {
                        MessageBox.Show("This Mobile number already in used!");
                        ds.Clear();
                        break;
                    }
                    else
                    {
                        try
                        {
                            MemoryStream ms = new MemoryStream();
                            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                            byte[] img = ms.ToArray();

                            string Query = "insert into donor(Name,Email,Age,Weight,Gender,Phone,City,Address,Blood,Last_donate,Disease,Image) values('" + txt1.Text + "','" + txt2.Text + "','" + txt3.Text + "','" + txt4.Text + "','" + txt5.Text + "','" + txt6.Text + "','" + txt7.Text + "','" + txt8.Text + "','" + txt9.Text + "','" + dateTimePicker1.Text.ToString() + "','" + txt10.Text + "',@img);";
                            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                            MySqlDataReader MyReader2;
                            MyConn2.Open();

                            MyCommand2.Parameters.Add("@img", MySqlDbType.Blob);
                            MyCommand2.Parameters["@img"].Value = img;

                            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                            MessageBox.Show("Save Data");
                            txt1.Text = string.Empty; txt2.Text = string.Empty; txt3.Text = string.Empty; txt4.Text = string.Empty; txt5.Text = null; txt6.Text = string.Empty; txt7.Text = string.Empty; txt8.Text = string.Empty; txt9.Text = null; txt10.Text = string.Empty; dateTimePicker1.Text = null; pictureBox2.Image = null;
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
                }

                i--;

            } while (i > 0);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            txt1.Text = string.Empty; txt2.Text = string.Empty; txt3.Text = string.Empty; txt4.Text = string.Empty; txt5.Text = null; txt6.Text = string.Empty; txt7.Text = string.Empty; txt8.Text = string.Empty; txt9.Text = null; txt10.Text = string.Empty; dateTimePicker1.Text = null; pictureBox2.Image = null;
        }

        private void button5_Click(object sender, EventArgs e) // upload pic to add donor
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(opf.FileName);
            }
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch) && !Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
            }
        }

        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void txt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void txt7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void button15_Click(object sender, EventArgs e) //Remove btn
        {
            textBox1.Visible = false;
            panelAddDonor.Visible = true;
            panelRemDonor.Visible = true;
            panelUpdateDonor.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e) // search btn for remove donor
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from donor where Name = '" + textBox2.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {

                text1.Text = dt.Rows[0]["Name"].ToString();
                text2.Text = dt.Rows[0]["Email"].ToString();
                text3.Text = dt.Rows[0]["Age"].ToString();
                text4.Text = dt.Rows[0]["Weight"].ToString();
                text5.Text = dt.Rows[0]["Gender"].ToString();
                text6.Text = dt.Rows[0]["Phone"].ToString();
                text7.Text = dt.Rows[0]["City"].ToString();
                text8.Text = dt.Rows[0]["Address"].ToString();
                text9.Text = dt.Rows[0]["Blood"].ToString();
                text10.Text = dt.Rows[0]["Disease"].ToString();
                dateTimePicker3.Text = dt.Rows[0]["Last_donate"].ToString();

                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox4.Image = Image.FromStream(ms);
                sda.Dispose();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Donor Couldn't Find!!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e) // delete btn
        {
            try
            {

                string Query = "delete from donor where Name ='" + this.text1.Text + "';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Donor Successfully Removed!");
                text1.Text = string.Empty; text2.Text = string.Empty; text3.Text = string.Empty; text4.Text = string.Empty; text5.Text = null; text6.Text = string.Empty; text7.Text = string.Empty; text8.Text = string.Empty; text9.Text = null; text10.Text = string.Empty; dateTimePicker3.Text = null; pictureBox4.Image = null;
                textBox2.Clear();
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

        private void button8_Click(object sender, EventArgs e) // clear btn from remove donor
        {
            txt1.Text = string.Empty; txt2.Text = string.Empty; txt3.Text = string.Empty; txt4.Text = string.Empty; txt5.Text = null; txt6.Text = string.Empty; txt7.Text = string.Empty; txt8.Text = string.Empty; txt9.Text = null; txt10.Text = string.Empty; dateTimePicker1.Text = null; pictureBox2.Image = null;

        }

        private void button6_Click(object sender, EventArgs e) // search for update donor
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("select * from donor where Name = '" + textBox3.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label240.Text = dt.Rows[0]["Name"].ToString();
                texts1.Text = dt.Rows[0]["Name"].ToString();
                texts2.Text = dt.Rows[0]["Email"].ToString();
                texts3.Text = dt.Rows[0]["Age"].ToString();
                texts4.Text = dt.Rows[0]["Weight"].ToString();
                texts5.Text = dt.Rows[0]["Gender"].ToString();
                texts6.Text = dt.Rows[0]["Phone"].ToString();
                texts7.Text = dt.Rows[0]["City"].ToString();
                texts8.Text = dt.Rows[0]["Address"].ToString();
                texts9.Text = dt.Rows[0]["Blood"].ToString();
                texts10.Text = dt.Rows[0]["Disease"].ToString();
                dateTimePicker2.Text = dt.Rows[0]["Last_donate"].ToString();

                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox3.Image = Image.FromStream(ms);
                sda.Dispose();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Donor Couldn't Find or Already removed!!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e) //update btn
        {
            try
            {
                string Query = "UPDATE donor SET Name='" + this.texts1.Text + "',Email='" + this.texts2.Text + "',Age='" + this.texts3.Text + "',Weight='" + this.texts4.Text + "',Gender='" + this.texts5.Text + "',Phone='" + this.texts6.Text + "',City='" + this.texts7.Text + "',Address='" + this.texts8.Text + "',Blood='" + this.texts9.Text + "',Disease='" + this.texts10.Text + "',Last_donate ='" + this.dateTimePicker2.Text.ToString() + "' where Name='" + label240.Text + "';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Successfully Updated!!");
                texts1.Text = string.Empty; texts2.Text = string.Empty; texts3.Text = string.Empty; texts4.Text = string.Empty; texts5.Text = null; texts6.Text = string.Empty; texts7.Text = string.Empty; texts8.Text = string.Empty; texts9.Text = null; texts10.Text = string.Empty; dateTimePicker2.Text = null; pictureBox2.Image = null; label240.Text = string.Empty; pictureBox3.Image = null;
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();//Connection closed here  
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                MessageBox.Show("Donor Couldn't Find or Already removed!!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e) // clear update
        {
            texts1.Text = string.Empty; texts2.Text = string.Empty; texts3.Text = string.Empty; texts4.Text = string.Empty; texts5.Text = null; texts6.Text = string.Empty; texts7.Text = string.Empty; texts8.Text = string.Empty; texts9.Text = null; texts10.Text = string.Empty; dateTimePicker2.Text = null; pictureBox2.Image = null; label240.Text = string.Empty; pictureBox3.Image = null;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            panelAddDonor.Visible = true;
            panelRemDonor.Visible = true;
            panelUpdateDonor.Visible = true;
        }

        private void texts1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch) && !Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
            }
        }

        private void texts3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void texts4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }

        }

        private void texts6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch) && e.KeyChar == 107)
            {
                
                    e.Handled = true;
            }
        }

        private void texts7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void texts6_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) //cell moude double click
        {
            textBox1.Visible = false;
            panelAddDonor.Visible = true;
            panelRemDonor.Visible = true;
            panelUpdateDonor.Visible = true;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                label240.Text = row.Cells["Name"].Value.ToString();
                texts1.Text = row.Cells["Name"].Value.ToString();
                texts2.Text = row.Cells["Email"].Value.ToString();
                texts3.Text = row.Cells["Age"].Value.ToString();
                texts4.Text = row.Cells["Weight"].Value.ToString();
                texts5.Text = row.Cells["Gender"].Value.ToString();
                texts6.Text = row.Cells["Phone"].Value.ToString();
                //texts7.Text = row.Cells["City"].Value.ToString();
                texts8.Text = row.Cells["Address"].Value.ToString();
                texts9.Text = row.Cells["Blood type"].Value.ToString();
                texts10.Text = row.Cells["disease"].Value.ToString();
                dateTimePicker2.Text= row.Cells["last donate"].Value.ToString();


            }
            MySqlConnection MyConnm = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
            MySqlDataAdapter sdaq = new MySqlDataAdapter("select * from donor where name = '" + label240.Text + "'", MyConnm);
            DataTable dtq = new DataTable();
            sdaq.Fill(dtq);
            try
            {
                texts7.Text = dtq.Rows[0]["City"].ToString();
                byte[] img = (byte[])dtq.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox3.Image = Image.FromStream(ms);
                sdaq.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /*
            MySqlDataAdapter sdaa = new MySqlDataAdapter("select * from donor where Email = '" + texts2.Text + "'", MyConn);
            DataTable dtt = new DataTable();
            sdaa.Fill(dtt);
            try
            {
                texts7.Text = dtt.Rows[0]["City"].ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }*/

        }
    }
}
