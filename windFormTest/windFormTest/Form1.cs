using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace windFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection con=new OleDbConnection("Provider=ORAOLEDB.ORACLE;Data Source=localhost;Persist Security Info=True;User ID=USER1;Password=password1;Unicode=True");
        private void button1_Click(object sender, EventArgs e)
        {
        con.Open();
            OleDbDataAdapter oda=new OleDbDataAdapter("select * from employees",con);
            DataTable dt=new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
    }
}
