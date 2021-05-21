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
    public partial class clients : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.constr);
        SqlDataAdapter adapter;
        public clients()
        {
            InitializeComponent();
        }

        private void clients_Load(object sender, EventArgs e)
        {
            this.companyDataSet.EnforceConstraints = false;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDataSet.clients". При необходимости она может быть перемещена или удалена.
            this.clientsTableAdapter.Fill(this.companyDataSet.clients);
            
            

            //con.Open();
            //adapter = new SqlDataAdapter("select * from clients order by [Тэг] DESC", con);
            //SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //dataGridView1.ReadOnly = true;
            //dataGridView1.DataSource = dt;
            //adapter.Dispose();
            //con.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex].HeaderText=="Тэг" && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string valueTag = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                switch (valueTag)
                {
                    case "1":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SkyBlue;
                        break;
                    case "2":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(128, 255, 128);
                        break;
                    case "3":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                        break;
                    case "4":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addclient addclient = new addclient();
            addclient.ShowDialog();
            this.clientsTableAdapter.Fill(this.companyDataSet.clients);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clientsBindingSource.Filter = "Фамилия like '%" + textBox1.Text + "%'";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //con.Open();
            //adapter = new SqlDataAdapter("select * from clients where LOWER(Фамилия) like '%" + textBox1.Text.ToLower() + "'", con);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //dataGridView1.ReadOnly = true;
            //dataGridView1.DataSource = dt;
            //adapter.Dispose();
            //con.Close();
        }
    }
}
