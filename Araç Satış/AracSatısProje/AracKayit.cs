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
    public partial class AracKayit : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();

        public AracKayit()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        private void AracKayit_Load(object sender, EventArgs e)
        {
            Giris g = new Giris();
            lblad.Text = Giris.kullaniciAdi;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            listele();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            Random rnd = new Random();
            int sayi = rnd.Next(12345,99999);
            label9.Text = sayi.ToString();


            dataGridView1.Columns[0].HeaderText = "Araç Şase";
            dataGridView1.Columns[1].HeaderText = "Marka Adı";
            dataGridView1.Columns[2].HeaderText = "Fabrika Adı";
            dataGridView1.Columns[3].HeaderText = "Geliş Tarihi";
            dataGridView1.Columns[4].HeaderText = "Miktar";
            dataGridView1.Columns[5].HeaderText = "Fiyat";
            dataGridView1.Columns[6].HeaderText = "Kullanıcı";
            dataGridView1.Columns[7].HeaderText = "Tarih Saat";
            dataGridView1.Columns[8].HeaderText = "Araç Durum";

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select distinct fabrikadi from fabrikaKayit", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["fabrikadi"].ToString());
            }
            oku.Close();
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select distinct aracmarka from markaStok", baglanti);
            SqlDataReader oku1 = komut1.ExecuteReader();
            while (oku1.Read())
            {
                comboBox1.Items.Add(oku1["aracmarka"].ToString());
            }
            oku1.Close();
            baglanti.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString();
        }
        public void kayit()
        {
            if (textBox2.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("Tüm alanlar dolu olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(comboBox1.SelectedIndex==0)
            {
                MessageBox.Show("Lütfen geçerli bir marka seçiniz!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen geçerli bir fabrika seçiniz!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    DialogResult d;
                    d = MessageBox.Show(label9.Text + " Şase numaralı araç kaydedilsin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {
                        baglanti.Open();

                        SqlCommand komut = new SqlCommand("INSERT INTO aracKayit (AracSase,markaAdi,fabrikaAdi,gelisTarih,miktar,fiyat,kullanici,tarihSaat) values (@sase,@marka,@fabrika,@gelisTarih,@miktar,@fiyat,@kullanici,@tarihSaat)", baglanti);

                        komut.Parameters.AddWithValue("@sase", label9.Text);
                        komut.Parameters.AddWithValue("marka", comboBox1.Text);
                        komut.Parameters.AddWithValue("@fabrika", comboBox2.Text);
                        komut.Parameters.AddWithValue("@gelisTarih", DateTime.Parse(dateTimePicker1.Text));
                        komut.Parameters.AddWithValue("@miktar", textBox2.Text);
                        komut.Parameters.AddWithValue("@fiyat", textBox3.Text);
                        komut.Parameters.AddWithValue("@kullanici", lblad.Text);
                        komut.Parameters.AddWithValue("@tarihSaat", DateTime.Parse(label8.Text));
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Araç Kayıt Edildi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        komut.Dispose();
                        baglanti.Close();
                        comboBox1.SelectedIndex = 0;
                        comboBox2.SelectedIndex = 0;
                        textBox2.Clear();
                        textBox3.Clear();
                        Random rnd = new Random();
                        int sayi = rnd.Next(12345, 99999);
                        label9.Text = sayi.ToString();
                    }
                }
                catch (Exception hata) { MessageBox.Show(hata.Message.ToString()); }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayit();
        }
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from aracKayit", baglanti);
            da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            komut.Dispose();
            baglanti.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form fabrika = new fabrikaKayit(this.comboBox2);
            fabrika.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form ac = new MarkaStok(this.comboBox1);
            ac.ShowDialog();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
