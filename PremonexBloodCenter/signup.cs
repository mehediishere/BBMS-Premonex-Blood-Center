using MySql.Data.MySqlClient;
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

namespace PremonexBloodCenter
{
    public partial class signup : Form
    {
        public MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=premonexbc");
        public MySqlCommand cmd;
        public DataSet ds = new DataSet();
        public signup()
        {
            InitializeComponent(); 
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch) && !char.IsWhiteSpace(ch))
            {
                e.Handled = true;
            }
        }

        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) //Home button
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button5_Click(object sender, EventArgs e) //upload btn
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)  //submit btn
        {
            int i = 1;
            do
            {
                if (txt1.Text == string.Empty)
                {
                    MessageBox.Show("Your Fullname required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt2.Text == string.Empty)
                {
                    MessageBox.Show("Your Email required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                if (txt3.Text == string.Empty)
                {
                    MessageBox.Show("A password required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt4.Text == string.Empty)
                {
                    MessageBox.Show("Re enter your password", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt3.Text != txt4.Text)
                {
                    MessageBox.Show("Password does not matched", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt5.Text == string.Empty)
                {
                    MessageBox.Show("Your current Age required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                if (txt6.Text == string.Empty)
                {
                    MessageBox.Show("Your Weight required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt7.Text == string.Empty)
                {
                    MessageBox.Show("Select gender", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt8.Text == string.Empty)
                {
                    MessageBox.Show("Your mobile number required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt12.Text == string.Empty)
                {
                    MessageBox.Show("Your city required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt9.Text == string.Empty)
                {
                    MessageBox.Show("Your address required", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                if (txt10.Text == string.Empty)
                {
                    MessageBox.Show("Select your Blood type", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                
                if (pictureBox2.Image==null)
                {
                    MessageBox.Show("Profile picture required with 130 x 140 size!", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Email already used!");
                        ds.Clear();
                        break;
                    }
                    cmd = new MySqlCommand("select * from donor where Phone ='" + txt8.Text + "'", con);
                    MySqlDataAdapter daa = new MySqlDataAdapter(cmd);
                    daa.Fill(ds);
                    int k = ds.Tables[0].Rows.Count;
                    if (k > 0)
                    {
                        MessageBox.Show("This Mobile number already used!");
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

                            string Query = "insert into donor(Name,Email,Age,Weight,Gender,Phone,City,Address,Blood,Last_donate,Disease,Image) values('" + this.txt1.Text + "','" + this.txt2.Text + "','" + this.txt5.Text + "','" + this.txt6.Text + "','" + this.txt7.Text + "','" + this.txt8.Text + "','" + this.txt12.Text + "','" + this.txt9.Text + "','" + this.txt10.Text + "','" + this.dateTimePicker1.Value.ToString("yyyy-M-d") + "','" + this.txt11.Text + "',@img);";

                            //MySqlConnection MyConn = new MySqlConnection(con);

                            MySqlCommand cmnd = new MySqlCommand(Query, con);
                            MySqlDataReader reader;
                            con.Open();
                            cmnd.Parameters.Add("@img", MySqlDbType.Blob);
                            cmnd.Parameters["@img"].Value = img;
                            reader = cmnd.ExecuteReader();
                           // MessageBox.Show("WELCOME!! Now You Can Login");
                           // txt1.Text = string.Empty; txt2.Text = string.Empty; txt5.Text = string.Empty; txt6.Text = string.Empty; txt7.Text = null; txt8.Text = string.Empty; txt9.Text = string.Empty; txt10.Text = null; txt11.Text = string.Empty; dateTimePicker1.Text = null; pictureBox2.Image = null;
                            while (reader.Read())
                            {
                            }
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        try
                        {

                            string Query = "insert into pass_user(Email,Password) values('" + this.txt2.Text + "','" + this.txt3.Text + "');";

                            //MySqlConnection MyConn = new MySqlConnection(con);

                            MySqlCommand cmnd = new MySqlCommand(Query, con);
                            MySqlDataReader reader;
                            con.Open();
                            reader = cmnd.ExecuteReader();
                            MessageBox.Show("WELCOME!! Now You Can Login","info");
                            txt1.Text = string.Empty; txt2.Text = string.Empty; txt12.Text = string.Empty; txt3.Text = string.Empty; txt4.Text = string.Empty; txt5.Text = string.Empty; txt6.Text = string.Empty; txt7.Text = null; txt8.Text = string.Empty; txt9.Text = string.Empty; txt10.Text = null; txt11.Text = string.Empty; dateTimePicker1.Text = null; pictureBox2.Image = null;
                            while (reader.Read())
                            {
                            }
                            con.Close();
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

        private void bunifuMetroTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && !char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt1.Text = string.Empty; txt2.Text = string.Empty; txt12.Text = string.Empty; txt3.Text = string.Empty; txt4.Text = string.Empty; txt5.Text = string.Empty; txt6.Text = string.Empty; txt7.Text = null; txt8.Text = string.Empty; txt9.Text = string.Empty; txt10.Text = null; txt11.Text = string.Empty; dateTimePicker1.Text = null; pictureBox2.Image = null;

            this.Hide();
            donorLogin df = new donorLogin();
            df.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if(txt3.isPassword == false){

                txt3.isPassword = true;
            }
            else
                txt3.isPassword = false;
        }
    }
}
