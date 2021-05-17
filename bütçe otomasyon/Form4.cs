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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        OleDbConnection baglantı= new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
        OleDbCommand komut;
        OleDbDataAdapter da;
        DataTable tablo = new DataTable();
        int id = 0;

        private void Form4_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Form1.kategori_id);
            
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


            OleDbDataAdapter daa = new OleDbDataAdapter("select* from detay", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            DataTable dtt = new DataTable();
            daa.Fill(dtt);

            comboBox2.DataSource = dtt;
            comboBox2.DisplayMember = "detay";
            comboBox2.ValueMember = "detayid";
            comboBox2.SelectedValue = Form1.detay_id;

            OleDbDataAdapter daaa = new OleDbDataAdapter("select* from kategori", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            DataTable dttt = new DataTable();
            daaa.Fill(dttt);

            comboBox4.DataSource = dttt;
            comboBox4.DisplayMember = "kategoriler";
            comboBox4.ValueMember = "kategoriid";
            comboBox4.SelectedValue = Form1.kategori_id;


            OleDbDataAdapter daaaa = new OleDbDataAdapter("select* from detay", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            DataTable dtttt = new DataTable();
            daaaa.Fill(dtttt);

            comboBox3.DataSource = dtttt;
            comboBox3.DisplayMember = "detay";
            comboBox3.ValueMember = "detayid";
            comboBox3.SelectedValue = Form1.detay_id;




        }




        private void listele()
        {
            tablo.Clear();
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            baglantı.Open();
            komut = new OleDbCommand("select*from kayit", baglantı);
            da = new OleDbDataAdapter(komut);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                listele();
            dataGridView1.Columns[0].Visible = true;

            //Kasa Girişi Toplama
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox1.Text = toplam.ToString();

            //Kasa Çıkışı Toplama
            int top = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                top += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            textBox5.Text = top.ToString();

            //Bakiye
            int bakiye = toplam - top;
            textBox6.Text = bakiye.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["tarih"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["detay"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["acıklama"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["giris"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["cıkıs"].Value.ToString();
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            komut = new OleDbCommand("INSERT INTO kayit(tarih,kategori,detay,acıklama,giris,cıkıs) values('" + dateTimePicker1.Value.ToString("dd/MM/yyyy").Replace('.', '/') + "','" + comboBox1.Text +  "','"+comboBox2.Text+"','" + textBox2.Text + "','" + textBox3.Text + "','"+ textBox4.Text + "')", baglantı);
            komut.ExecuteNonQuery();
            baglantı.Close();
            tablo.Clear();
            listele();
            comboBox1.Text = "";
            comboBox2.Text = "";

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }


            //Kasa Girişi Toplama
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox1.Text = toplam.ToString();

            //Kasa Çıkışı Toplama
            int top = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                top += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            textBox5.Text = top.ToString();

            //Bakiye
            int bakiye = toplam - top;
            textBox6.Text = bakiye.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM kayit where id=@id";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox1.Text = toplam.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // string sorgu = "UPDATE kayit set kategori=@kategori where id=@id";
            string sorgu = "UPDATE kayit set kategori='" + comboBox1.Text + "',detay='" + comboBox2.Text + "',tarih='"+dateTimePicker1.Value+"',acıklama='"+textBox2.Text+"',giris='"+textBox3.Text+"',cıkıs='"+textBox4.Text+"' where id=" + id + "";
            komut = new OleDbCommand(sorgu, baglantı);
          
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            listele();
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is TextBox)
                {
                    Controls[i].Text = "";
                }

            }



            //Kasa Girişi Toplama
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox1.Text = toplam.ToString();

            //Kasa Çıkışı Toplama
            int top = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                top += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            textBox5.Text = top.ToString();

            //Bakiye
            int bakiye = toplam - top;
            textBox6.Text = bakiye.ToString();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from kategori where detay=" + comboBox1.SelectedValue.ToString() + "", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //dataGridView1.Columns[0].Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            

        }

    
        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from detay where kategoriid=" + comboBox1.SelectedValue.ToString() + "", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "detay";
                comboBox2.ValueMember = "detayid";

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            baglantı.Open();
            OleDbDataAdapter adptr = new OleDbDataAdapter("SELECT * FROM kayit WHERE detay= '" + comboBox3.Text + "'", baglantı);
            adptr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglantı.Close();

            //Kasa Girişi Toplama
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            textBox1.Text = toplam.ToString();

            //Kasa Çıkışı Toplama
            int top = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                top += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            textBox5.Text = top.ToString();

            //Bakiye
            int bakiye = toplam - top;
            textBox6.Text = bakiye.ToString();
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue != null)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from detay where kategoriid=" + comboBox4.SelectedValue.ToString() + "", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "detay";
                comboBox3.ValueMember = "detayid";

            }
        }
    }
}
