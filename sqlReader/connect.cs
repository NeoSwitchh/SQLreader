using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqlReader
{
    public partial class Form1 : Form
    {
        public static string nameDesktopConnect="";
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            nameDesktopConnect = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString;
                SqlConnection cnn;
                connectionString = @"Data Source=" + nameDesktopConnect + ";Initial Catalog=Company;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
                MessageBox.Show("Connection Open  !");
                cnn.Close();
                tableDB tableform = new tableDB();
                tableform.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
    }
}
