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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection baglantı;
        OleDbCommand komut;
        OleDbDataAdapter da;
        DataTable tablo = new DataTable();

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Columns[0].Visible = true;
        }

        private void listele()
        {
            tablo.Clear();
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            baglantı.Open();
            komut = new OleDbCommand("select*from kategori", baglantı);
            da = new OleDbDataAdapter(komut);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            string sorgu = "INSERT INTO kategori(kategoriler) values(@kategoriler)";
            komut = new OleDbCommand(sorgu, baglantı);
            
            komut.Parameters.AddWithValue("@kategoriler", textBox1.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();

            textBox1.Text = "";
            textBox2.Text = "";

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
            string sorgu = "DELETE FROM kategori where kategoriid=@kategoriid";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@kategoriid", dataGridView1.CurrentRow.Cells[0].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();

            textBox1.Text = "";
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
            string sorgu = "UPDATE kategori set kategoriler='" + textBox1.Text + "', where kategoriid=" + Convert.ToInt32( textBox2.Text )+'"'; 

            //string sorgu = "UPDATE kategori set kategoriler=@kategoriler where kategoriid=@kategoriid";
            komut = new OleDbCommand(sorgu, baglantı);
            //komut.Parameters.AddWithValue("@kategoriler", textBox1.Text);
            //komut.Parameters.AddWithValue("@kategoriid", Convert.ToInt32(textBox2.Text));
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();

            textBox1.Text = "";
            textBox2.Text = "";

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }





        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kategoriler"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["kategoriid"].Value.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kategoriler"].Value.ToString();
            //  kategori_id = dataGridView1.CurrentRow.Cells["kategoriid"].Value.ToString();
            Form1.kategori_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            Form3 fre3 = new Form3();
            fre3.Show();



        }
    }
}
