﻿using System;
using System.Data;
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

        OleDbConnection con=new OleDbConnection("Provider=ORAOLEDB.ORACLE;DATA SOURCE=localhost:1521/orcl3;USER ID=USER1;Password=password1");

        private void button1_Click(object sender, EventArgs e)
        {
        con.Open();
            OleDbDataAdapter oda=new OleDbDataAdapter("select * from USER1.emp_temp",con);
            DataTable dt=new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
