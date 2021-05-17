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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string kategori_id = "0";
        public static string detay_id = "0";
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fre2 = new Form2();
            fre2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fre3 = new Form3();
            fre3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 fre4 = new Form4();
            fre4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 fre5 = new Form5();
            fre5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
