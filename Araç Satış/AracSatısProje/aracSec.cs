using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AracSatısProje
{
    public partial class aracSec : Form,IUaracSec
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        SqlDataAdapter da;
        DataTable dt=new DataTable();
        public aracSec()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        public event aracsecildiHandle aracsecildi;

        private void aracSec_Load(object sender, EventArgs e)
        {
            list();
            comboBox1.SelectedIndex = 4;            
            dataGridView1.Columns[0].HeaderText = "Araç Şase";
            dataGridView1.Columns[1].HeaderText = "Marka Adı";
            dataGridView1.Columns[2].HeaderText = "Fabrika Adı";
            dataGridView1.Columns[3].HeaderText = "Geliş Tarihi";
            dataGridView1.Columns[4].HeaderText = "Miktar";
            dataGridView1.Columns[5].HeaderText = "Fiyat";
            dataGridView1.Columns[6].HeaderText = "Kullanıcı";
            dataGridView1.Columns[7].HeaderText = "Tarih Saat";
            dataGridView1.Columns[8].HeaderText = "Araç Durum";
        }
        public void list()
        {

            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from aracKayit", baglanti);
                da = new SqlDataAdapter(com);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
                com.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public string secilenarac { get; set; }
        public string secilarac { get; set; }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {           
            secilenarac = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            secilarac = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            aracsecildi?.Invoke();
            this.Hide();
        }
        public void birimler()
        {
            if(comboBox1.Text=="Araç Şase")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from aracKayit where AracSase like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else if(comboBox1.Text=="Marka Adı")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from aracKayit where markaAdi like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Kullanıcı")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from aracKayit where kullanici like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else if(comboBox1.Text=="Araç Durum")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from aracKayit where durum like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else
            {
                list();
            }
        }

        private void txtara_TextChanged(object sender, EventArgs e)
        {
            birimler();
        }
    }
}
