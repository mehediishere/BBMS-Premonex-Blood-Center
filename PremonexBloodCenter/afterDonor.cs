using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PremonexBloodCenter
{
    public partial class afterDonor : Form
    {
        public MySqlConnection MyConn = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public MySqlConnection MyConn2 = new MySqlConnection("server = localhost; user id = root; database=premonexbc");
        public afterDonor(string name)
        {
            InitializeComponent();
            label101.Text = name;
            panelProfileSettings.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e) // Check other donor btn
        {
            Event.Instance.Visible = false;
            afterDonorCheckOther.Instance.Visible = true;

            if (!panelBack2.Controls.Contains(afterDonorCheckOther.Instance))
            {
                panelBack2.Controls.Add(afterDonorCheckOther.Instance);
                afterDonorCheckOther.Instance.Dock = DockStyle.Fill;
                afterDonorCheckOther.Instance.BringToFront();

            }
            else
               afterDonorCheckOther.Instance.BringToFront();
        }

        private void button11_Click(object sender, EventArgs e) // Blood Request btn
        {
            Event.Instance.Visible = false;
            panelProfileSettings.Visible = true;
            panelProfile.Visible = true;
            panelBloodRequest.Visible = true;
            
        }

        private void button10_Click(object sender, EventArgs e) // profile btn
        {
            Event.Instance.Visible = false;
            panelProfileSettings.Visible = true;
            panelProfile.Visible = true;
            panelBloodRequest.Visible = false;
            MySqlDataAdapter sda = new MySqlDataAdapter("select Name,Age,Weight,Gender,Blood,Last_donate,Phone,Address,Disease from donor where Email ='" + label101.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label33.Text = dt.Rows[0]["Name"].ToString();
                label34.Text = dt.Rows[0]["Age"].ToString();
                label35.Text = dt.Rows[0]["Weight"].ToString();
                label36.Text = dt.Rows[0]["Gender"].ToString();
                label37.Text = dt.Rows[0]["Blood"].ToString();
                label38.Text = dt.Rows[0]["Last_Donate"].ToString();
                label39.Text = dt.Rows[0]["Phone"].ToString();
                label40.Text = dt.Rows[0]["Address"].ToString();
                label41.Text = dt.Rows[0]["Disease"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Donor Couldn't Find!!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e) // account settings btn
        {
            panelSSS.Visible = false;
        }

        private void panelProfile_Paint(object sender, PaintEventArgs e)
        {

        }

        private void afterDonor_Load(object sender, EventArgs e)
        {

            try
            {
                string Query = "select Email from donor where Email = '"+label101.Text+"';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (MyReader2.Read())
                {
                    mycollection.Add(MyReader2.GetString(0));
                }
                textBox12.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox12.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox12.AutoCompleteCustomSource = mycollection;

                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {
                string Query2 = "select Phone from donor where Email = '" + label101.Text + "';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query2, MyConn2);
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


            MySqlDataAdapter sda = new MySqlDataAdapter("select Name,Age,Weight,Gender,Blood,Last_donate,Phone,Address,Disease,Image from donor where Email ='" + label101.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label33.Text = dt.Rows[0]["Name"].ToString();
                label34.Text = dt.Rows[0]["Age"].ToString();
                label35.Text = dt.Rows[0]["Weight"].ToString();
                label36.Text = dt.Rows[0]["Gender"].ToString();
                label37.Text = dt.Rows[0]["Blood"].ToString();
                label38.Text = dt.Rows[0]["Last_Donate"].ToString();
                label39.Text = dt.Rows[0]["Phone"].ToString();
                label40.Text = dt.Rows[0]["Address"].ToString();
                label41.Text = dt.Rows[0]["Disease"].ToString();

                byte[] img = (byte[])dt.Rows[0]["Image"];
                MemoryStream ms = new MemoryStream(img);
                pictureBox4.Image = Image.FromStream(ms);
                sda.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void button30_Click(object sender, EventArgs e) // Settings btn
        {
            panelProfile.Visible = false;

            MySqlDataAdapter sda = new MySqlDataAdapter("select Name,Age,Weight,Gender,Blood,Last_donate,Phone,Address,Disease from donor where Email ='" + label101.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label14.Text = dt.Rows[0]["Name"].ToString();
                label15.Text = dt.Rows[0]["Age"].ToString();
                label16.Text = dt.Rows[0]["Weight"].ToString();
                label17.Text = dt.Rows[0]["Gender"].ToString();
                label18.Text = dt.Rows[0]["Blood"].ToString();
                label20.Text = dt.Rows[0]["Last_Donate"].ToString();
                label19.Text = dt.Rows[0]["Phone"].ToString();
                label21.Text = dt.Rows[0]["Address"].ToString();
                label22.Text = dt.Rows[0]["Disease"].ToString();

                textBox2.Text = dt.Rows[0]["Name"].ToString();
                textBox3.Text = dt.Rows[0]["Age"].ToString();
                textBox4.Text = dt.Rows[0]["Weight"].ToString();
                textBox5.Text = dt.Rows[0]["Gender"].ToString();
                comboBox1.Text = dt.Rows[0]["Blood"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["Last_Donate"].ToString());
                textBox7.Text = dt.Rows[0]["Phone"].ToString();
                textBox8.Text = dt.Rows[0]["Address"].ToString();
                textBox9.Text = dt.Rows[0]["Disease"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button17_Click(object sender, EventArgs e) // edit name
        {
            textBox2.Visible = true;
            button17.Visible = false;
        }

        private void button18_Click(object sender, EventArgs e) // edit age
        {
            textBox3.Visible = true;
            button18.Visible = false;
        }

        private void button19_Click(object sender, EventArgs e) // edit weight
        {
            textBox4.Visible = true;
            button19.Visible = false;
        }

        private void button20_Click(object sender, EventArgs e) // edit gender
        {
            textBox5.Visible = true;
            button20.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e) // e blood
        {
            comboBox1.Visible = true;
            button21.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = true;
            button22.Visible = false;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox7.Visible = true;
            button23.Visible = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox8.Visible = true;
            button24.Visible = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox9.Visible = true;
            button25.Visible = false;
        }

        private void button32_Click(object sender, EventArgs e) //save btn
        {
            try
            {
                string Query22 = "UPDATE donor SET Name='" + this.textBox2.Text + "',Age='" + this.textBox3.Text + "',Weight='" + this.textBox4.Text + "',Gender='" + this.textBox5.Text + "',Blood='" + this.comboBox1.Text + "',Last_donate='" + this.dateTimePicker1.Value.ToString("yyyy-M-d") + "',Phone='" + this.textBox7.Text + "',Address='" + this.textBox8.Text + "',Disease='" + this.textBox9.Text + "' where Name = '" + this.label14.Text + "'";
                MySqlCommand MyCommand2 = new MySqlCommand(Query22, MyConn);
                MySqlDataReader MyReader2;
                MyConn.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                //MessageBox.Show("Save Successfully!");

                textBox2.Visible = false; textBox3.Visible = false; textBox4.Visible = false; textBox5.Visible = false; textBox7.Visible = false; textBox8.Visible = false; textBox9.Visible = false; comboBox1.Visible = false;dateTimePicker1.Visible = false;
                button17.Visible = true; button18.Visible = true; button19.Visible = true; button20.Visible = true; button21.Visible = true; button22.Visible = true; button23.Visible = true; button24.Visible = true; button25.Visible = true;

                while (MyReader2.Read())
                {
                }
                MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MySqlDataAdapter sda = new MySqlDataAdapter("select Name,Age,Weight,Gender,Blood,Last_donate,Phone,Address,Disease from donor where Email ='" + label101.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label14.Text = dt.Rows[0]["Name"].ToString();
                label15.Text = dt.Rows[0]["Age"].ToString();
                label16.Text = dt.Rows[0]["Weight"].ToString();
                label17.Text = dt.Rows[0]["Gender"].ToString();
                label18.Text = dt.Rows[0]["Blood"].ToString();
                label20.Text = dt.Rows[0]["Last_Donate"].ToString();
                label19.Text = dt.Rows[0]["Phone"].ToString();
                label21.Text = dt.Rows[0]["Address"].ToString();
                label22.Text = dt.Rows[0]["Disease"].ToString();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button33_Click(object sender, EventArgs e) // Change password
        {
            button33.Visible = false;
            label42.Visible = true;
            textBox10.Visible = true;

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e) // security setting btn
        {
            panelSSS.Visible = true;
            textBox2.Visible = false; textBox3.Visible = false; textBox4.Visible = false; textBox5.Visible = false; textBox7.Visible = false; textBox8.Visible = false; textBox9.Visible = false; comboBox1.Visible = false; dateTimePicker1.Visible = false;
            button17.Visible = true; button18.Visible = true; button19.Visible = true; button20.Visible = true; button21.Visible = true; button22.Visible = true; button23.Visible = true; button24.Visible = true; button25.Visible = true;


            MySqlDataAdapter sda = new MySqlDataAdapter("select Email,Password from Pass_user where Email ='" + label101.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label46.Text = dt.Rows[0]["Email"].ToString();
                textBox11.Text = dt.Rows[0]["Email"].ToString();
                textBox10.Text = dt.Rows[0]["Password"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button35_Click(object sender, EventArgs e) // edit email
        {
            button35.Visible = false;
            textBox11.Visible = true;
            textBox11.Text = label46.Text;
        }

        private void button36_Click(object sender, EventArgs e) // save security setting
        {
            try
            {
                string Query22 = "UPDATE pass_user SET Email='" + this.textBox11.Text + "',Password='" + this.textBox10.Text + "' where Email = '" + this.label46.Text + "'";
                MySqlCommand MyCommand2 = new MySqlCommand(Query22, MyConn);
                MySqlDataReader MyReader2;
                MyConn.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                //MessageBox.Show("Save Successfully!");

                textBox10.Visible = false; textBox11.Visible = false; label42.Visible = false; button35.Visible = true; button33.Visible = true;

                while (MyReader2.Read())
                {
                }
                MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MySqlDataAdapter sda = new MySqlDataAdapter("select Email,Password from Pass_user where Email ='" + textBox11.Text + "'", MyConn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                label46.Text = dt.Rows[0]["Email"].ToString();
                textBox10.Text = dt.Rows[0]["Password"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            panelProfile.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel133.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e) //log out
        {
            this.Hide();
            donorLogin dl = new donorLogin();
            dl.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            panelProfileSettings.Visible = false;
            Event.Instance.Visible = false;
        }

        private void button28_Click(object sender, EventArgs e) // blood request btn
        {
            Event.Instance.Visible = false;
            panelBloodRequest.Visible = true;

        }

        private void button27_Click(object sender, EventArgs e)
        {
            this.Hide();
            donorLogin dl = new donorLogin();
            dl.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string MyConnection2 = "server=localhost;user id=root;database=premonexbc";
                string Query = "insert into messagebox(Email,Date,Blood,Within,Message,Phone) values('" + this.textBox12.Text + "','" + this.dateTimePicker2.Text.ToString() + "','" + this.comboBox2.Text + "','" + this.dateTimePicker3.Text.ToString() + "','" + this.textBox6.Text + "','" + this.textBox1.Text + "');";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Successfully Send!");
                textBox1.Text = string.Empty; textBox12.Text = string.Empty; textBox6.Text = string.Empty; dateTimePicker2.Text = null; dateTimePicker3.Text = null;comboBox2.Text = null;

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

        private void button31_Click(object sender, EventArgs e) //upload
        {
            

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Image = Image.FromFile(opf.FileName);
            }

            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
                byte[] img = ms.ToArray();

                string Query = "update donor set Image = (@img) where Name = '" + label33.Text + "' "; // where email = '"+label101.Text+"'";

                //MySqlConnection MyConn = new MySqlConnection(con);

                MySqlCommand cmnd = new MySqlCommand(Query, MyConn);
                MySqlDataReader reader;
                MyConn.Open();
                cmnd.Parameters.Add("@img", MySqlDbType.Blob);
                cmnd.Parameters["@img"].Value = img;
                reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                }
                MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button13_Click(object sender, EventArgs e) //EVENT BTN
        {
            Event.Instance.Visible = true;
            panelProfile.Visible = true;
            panelProfileSettings.Visible = true;

            if (!panelProfile.Controls.Contains(Event.Instance))
            {
                panelProfile.Controls.Add(Event.Instance);
                Event.Instance.Dock = DockStyle.Fill;
                Event.Instance.BringToFront();

            }
            else
                Event.Instance.BringToFront();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            panelBloodRequest.Visible = true;
            Event.Instance.Visible = true;
            //panelProfile.Visible = true;
            //panelProfileSettings.Visible = true;

            if (!panelBloodRequest.Controls.Contains(Event.Instance))
            {
                panelBloodRequest.Controls.Add(Event.Instance);
                Event.Instance.Dock = DockStyle.Fill;
                Event.Instance.BringToFront();

            }
            else
                Event.Instance.BringToFront();
        }
    }
}
