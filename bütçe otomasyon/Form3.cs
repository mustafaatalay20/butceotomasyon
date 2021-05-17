using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace bütçe_otomasyon
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        OleDbConnection baglantı;
        OleDbCommand komut;
        OleDbDataAdapter da;
        DataTable tablo = new DataTable();

        private void Form3_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Form1.kategori_id);
            textBox1.Visible = false;
            //OleDbConnection baglantı2 = new OleDbConnection();
            //baglantı2.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb";
            //OleDbCommand komut = new OleDbCommand();
            //komut.CommandText = "select* from kategori";
            //komut.Connection = baglantı2;
            //komut.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter("select* from kategori", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            DataTable dt = new DataTable();
            da.Fill(dt);
            //baglantı2.Open();
            //dr = komut.ExecuteReader();
            //while (dr.Read())
            //{
            //    comboBox1.Items.Add(dr["kategoriler"]);

            //}
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "kategoriler";
            comboBox1.ValueMember = "kategoriid";
            comboBox1.SelectedValue = Form1.kategori_id;




        }



        private void listele()
        {
            tablo.Clear();
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            baglantı.Open();
            komut = new OleDbCommand("select*from detay", baglantı);
            da = new OleDbDataAdapter(komut);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listele();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from detay  where kategoriid=" + comboBox1.SelectedValue.ToString() + "", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //OleDbConnection baglantı = new OleDbConnection();
            //baglantı.Open();
            //string sorgu = "INSERT INTO detay(detay,kategoriid) values(@detay,@kategoriid)";
            //komut = new OleDbCommand(sorgu, baglantı);
            //komut.Parameters.AddWithValue("@detay", textBox2.Text);
            //komut.Parameters.AddWithValue("@kategoriid", Form1.kategori_id);
            //komut.ExecuteNonQuery();
            //baglantı.Close();

            using (OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb"))
            {
                OleDbCommand da = new OleDbCommand("INSERT INTO detay(detay,kategoriid) values('" + textBox2.Text + "','" + comboBox1.SelectedValue.ToString() + "')", baglan);
                baglan.Open();
                da.ExecuteNonQuery();
                baglan.Close();
                baglan.Dispose();

                listele();

                comboBox1.Text = "";
                textBox2.Text = "";

            }
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }
        }


        

        private void button3_Click(object sender, EventArgs e)
        {
         
            string sorgu = "DELETE FROM detay where detayid=@detayid";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@detayid", dataGridView1.CurrentRow.Cells[0].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();

            comboBox1.Text = "";
            textBox2.Text = "";

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE detay set detay='" + textBox2.Text + "', where detayid=" + textBox1.Text + ""; ;

           // string sorgu = "UPDATE detay set detay=@detay where detayid=@detayid";
            komut = new OleDbCommand(sorgu, baglantı);
            //komut.Parameters.AddWithValue("@detay", textBox2.Text);
            //komut.Parameters.AddWithValue("@detayid", Convert.ToInt32(textBox1.Text));
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();

            comboBox1.Text = "";
            textBox2.Text = "";

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells["detay"].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells["detayid"].Value.ToString();

        }
    }
}
