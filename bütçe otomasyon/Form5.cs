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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
        OleDbCommand komut;
        OleDbDataAdapter da;
        DataTable tablo = new DataTable();


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
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            baglantı.Open();
            /* OleDbDataAdapter adptr = new OleDbDataAdapter("select*from kayit where tarih between @tr1 and @tr2",baglantı);

             adptr.SelectCommand.Parameters.AddWithValue("@tr1", dateTimePicker1.Value.ToString());
             adptr.SelectCommand.Parameters.AddWithValue("@tr2", dateTimePicker2.Value.ToString());
             adptr.Fill(tablo);*/

            OleDbDataAdapter adptr = new OleDbDataAdapter("SELECT * FROM kayit WHERE tarih between  #"+dateTimePicker1.Value.ToString("dd/MM/yyyy").Replace('.','/') + "# and #" + dateTimePicker2.Value.ToString("dd/MM/yyyy").Replace('.', '/') + "# and detay= '" + comboBox2.Text+ "'", baglantı);
            //adptr.SelectCommand.Parameters.AddWithValue("@tr1", dateTimePicker1.Value.ToString());
            //adptr.SelectCommand.Parameters.AddWithValue("@tr2", dateTimePicker2.Value.ToString());
            adptr.Fill(tablo);

            dataGridView1.DataSource = tablo;
            baglantı.Close();

            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Columns[0].Visible = true;

            OleDbDataAdapter da = new OleDbDataAdapter("select* from kategori", @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bütce.mdb");
            DataTable dt = new DataTable();
            da.Fill(dt);

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
    }
}
