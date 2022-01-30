using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sqlReader
{
    public partial class tableDB : Form
    {
        public string nameDesktop = Form1.nameDesktopConnect;

        public tableDB()
        {
            InitializeComponent();
        }

        //refresh view
        private void button3_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=" + nameDesktop + ";Initial Catalog=Company;Integrated Security=True";
            try
            {
                string inputCombobox = comboBox1.SelectedItem.ToString();
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = @"SELECT * 
                                    FROM [SumberDayaAlam].[dbo]." + inputCombobox;

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dAdapter.Fill(ds);
                    dataGridView1.ReadOnly = true;
                    dataGridView1.DataSource = ds.Tables[0];
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=" + nameDesktop + ";Initial Catalog=Company;Integrated Security=True";
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = @"INSERT INTO [SumberDayaAlam].[dbo].SDA
                                    ([IdSDA]
                                    ,[Nama]
                                    ,[VolumeSDA])
                                    VALUES
                                    (@id, @namaSDA, @VolSDA)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar,50).Value = textBox1.Text;
                    cmd.Parameters.Add("@namaSDA", SqlDbType.VarChar,50).Value = textBox2.Text;
                    cmd.Parameters.Add("@VolSDA", SqlDbType.Int).Value = textBox3.Text;

                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (i != 0)
                    {
                        MessageBox.Show("Successful Insert.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                        MessageBox.Show("Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=" + nameDesktop + ";Initial Catalog=Company;Integrated Security=True";
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = @"DELETE FROM [SumberDayaAlam].[dbo].SDA WHERE idSDA = @id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = textBox4.Text;

                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (i != 0)
                    {
                        MessageBox.Show("Successful Delete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                        MessageBox.Show("Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string inputCombobox = comboBox2.SelectedItem.ToString();
            string connString = @"Data Source=" + nameDesktop + ";Initial Catalog=Company;Integrated Security=True";
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query =  "";
                    if (inputCombobox == "SDA hewan yang jumlahnya di bawah 500")
                    {
                        query = @"select nama as Nama,jenishewan as 'Jenis Hewan',volumesda as 'Volume SDA'
                                from [SumberDayaAlam].[dbo].sda
                                join [SumberDayaAlam].[dbo].hewan on [SumberDayaAlam].[dbo].sda.idsda = [SumberDayaAlam].[dbo].hewan.idsda
                                where volumesda < 500";
                    }
                    else if (inputCombobox == "SDA mineral dalam keadaan terancam")
                    {
                        query = @"select nama as Nama, jenismineral as 'Jenis Mineral', volumesda as 'Volume SDA'
                                from [SumberDayaAlam].[dbo].sda
                                join [SumberDayaAlam].[dbo].mineral on [SumberDayaAlam].[dbo].sda.idsda = [SumberDayaAlam].[dbo].mineral.idsda
                                where volumesda < 5400000
                                ";
                    } else if (inputCombobox == "Hasil dari pemanfaatan SDA migas")
                    {
                        query = @"select sda.nama as Nama,volumesda as 'Volume SDA',hp.nama as 'Nama Hasil Pemanfaatan'
                                from [SumberDayaAlam].[dbo].sda
                                join [SumberDayaAlam].[dbo].migas m on [SumberDayaAlam].[dbo].sda.idsda = m.idsda	
                                join [SumberDayaAlam].[dbo].dimanfaatkan d on [SumberDayaAlam].[dbo].sda.idsda = d.idsda
                                join [SumberDayaAlam].[dbo].hasil_pemanfaatan hp on d.idhasil = hp.idhasil
	                            ";
                    }
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dAdapter.Fill(ds);
                    dataGridView2.ReadOnly = true;
                    dataGridView2.DataSource = ds.Tables[0];
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
    }
}
