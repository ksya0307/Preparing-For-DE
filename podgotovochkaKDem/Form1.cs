using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace podgotovochkaKDem
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.constr);
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") MessageBox.Show("error");
            else
            {
                con.Open();
                cmd = new SqlCommand("select login, pwd from users where login='" + textBox1.Text + "' and pwd='" + textBox2.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                string login = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        login = reader.GetString(0);
                        login = login.Trim(' ');
                    }
                    reader.Close();
                    cmd.Dispose();
                    clients clients = new clients();
                    clients.Show();
                }
                else MessageBox.Show("пароль чек");
                con.Close();
            }
        }
    }
}
