using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.Sql;
using System.Data.SqlClient;

namespace podgotovochkaKDem
{
    public partial class addclient : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.constr);
        SqlCommand cmd;
        public addclient()
        {
            InitializeComponent();
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            //string pattern = "^(" +
            //    "[0-9a-zA-Z]" +
            //    "(" +
            //    "[-\\.\\w]" +
            //    "*[0-9a-zA-Z]" +
            //    ")" +
            //    "*@" +
            //    "(" +
            //    "[0-9a-zA-Z]" +
            //    "[-\\w]" +
            //    "*[0-9a-zA-Z]\\" +
            //    ".)" +
            //    "+" +
            //    "[a-zA-Z]" +
            //    "{2,9}" +
            //    ")$";
            //if (Regex.IsMatch(textBox4.Text, pattern))
            //{
            //    errorProvider1.Clear();
            //}
            //else
            //{
            //    errorProvider1.SetError(this.textBox4,"Введите коректный емаил");
            //    return;
            //}
            bool ans = isValid(textBox4.Text);
            if (ans) errorProvider1.Clear();
            else
            {
                errorProvider1.SetError(this.textBox4, "Введите коректный емаил");
                return;
            }
        }

        public bool isValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            openFileDialog1.Filter = "Image FIles(*.jpg; *.BMP; *.png; *.jpeg)|*.jpg; *.BMP; *.png; *.jpeg|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("невозможно открыть выбранный файл", "ОШибка");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = openFileDialog1.FileName.ToString();
            Console.WriteLine(path);

            string tag = "";
            if (radioButton3.Checked)
            {
                tag = radioButton3.Text;
            }
            if (radioButton4.Checked)
            {
                tag = radioButton4.Text;
            }
            if (radioButton5.Checked)
            {
                tag = radioButton5.Text;
            }
            if (radioButton6.Checked)
            {
                tag = radioButton6.Text;
            }

            con.Open();
            cmd = new SqlCommand("select id from tags where tag='"+tag+"'",con);
            int idtag = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            con.Open();
            cmd.Dispose();
            cmd = new SqlCommand("users_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@last_name", SqlDbType.NChar));
            cmd.Parameters["@last_name"].Value = textBox1.Text;

            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar));
            cmd.Parameters["@name"].Value = textBox2.Text;

            cmd.Parameters.Add(new SqlParameter("@dad_name", SqlDbType.NChar));
            cmd.Parameters["@dad_name"].Value = textBox3.Text;

            string sex="";
            if (radioButton1.Checked)
            {
                sex = radioButton1.Text;
            }
            else if (radioButton2.Checked) sex = radioButton2.Text;

            cmd.Parameters.Add(new SqlParameter("@sex", SqlDbType.NChar));
            cmd.Parameters["@sex"].Value = sex;


            cmd.Parameters.Add(new SqlParameter("@phone", SqlDbType.NChar));
            cmd.Parameters["@phone"].Value = maskedTextBox1.Text;

            cmd.Parameters.Add(new SqlParameter("@photo_path", SqlDbType.NChar));
            cmd.Parameters["@photo_path"].Value = path;

            cmd.Parameters.Add(new SqlParameter("@birth", SqlDbType.Date));
            cmd.Parameters["@birth"].Value =dateTimePicker1.Text;

            cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NChar));
            cmd.Parameters["@email"].Value = textBox4.Text;

            cmd.Parameters.Add(new SqlParameter("@dateregistr", SqlDbType.Date));
            cmd.Parameters["@dateregistr"].Value = dateTimePicker2.Text;

            cmd.Parameters.Add(new SqlParameter("@tag", SqlDbType.Int));
            cmd.Parameters["@tag"].Value = idtag;

            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Пользователь успешно добавлен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибочка!");
            }
            con.Close();
        }
    }
}
