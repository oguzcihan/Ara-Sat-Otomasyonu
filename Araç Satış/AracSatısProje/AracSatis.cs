using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracSatısProje
{
    public partial class AracSatis : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public AracSatis()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();

        }

        private void AracSatis_Load(object sender, EventArgs e)
        {
            Giris g = new Giris();
            lblad.Text = Giris.kullaniciAdi;
            textBox4.MaxLength = 9;

            ToolTip acik = new ToolTip();
            acik.SetToolTip(button1, "Marka Seçiniz");
            acik.SetToolTip(button3, "Tc Ekle");


            acik.ToolTipTitle = "Bilgi";
            acik.ToolTipIcon = ToolTipIcon.Info;
            acik.IsBalloon = true;
            acik.SetToolTip(pictureBox3, "Müşteri Tc ve Marka adı yanında bulunan butonlardan ekleme yapınız.");

            listele();
            Random rnd = new Random();
            int sayi = rnd.Next(12345, 99999);
            label3.Text = sayi.ToString();
            comboBox5.SelectedIndex = 0;
            


            dataGridView1.Columns[0].HeaderText = "Satış Id";
            dataGridView1.Columns[1].HeaderText = "Araç Şase";
            dataGridView1.Columns[2].HeaderText = "Müşteri Tc";
            dataGridView1.Columns[3].HeaderText = "Model";
            dataGridView1.Columns[4].HeaderText = "Marka";
            dataGridView1.Columns[5].HeaderText = "Vites Türü";
            dataGridView1.Columns[6].HeaderText = "Yakıt Türü";
            dataGridView1.Columns[7].HeaderText = "Araç Yılı";
            dataGridView1.Columns[8].HeaderText = "Garanti Tarihi";
            dataGridView1.Columns[9].HeaderText = "Miktar";
            dataGridView1.Columns[10].HeaderText = "Birim Fiyat";
            dataGridView1.Columns[11].HeaderText = "Kullanıcı";
            dataGridView1.Columns[12].HeaderText = "Satış Tarih";

         
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select distinct TC from musteriKayit", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox5.Items.Add(oku["TC"].ToString());
            }
            oku.Close();
            baglanti.Close();

            comboBox5.AutoCompleteSource = AutoCompleteSource.ListItems;

        }
               

    private void timer1_Tick(object sender, EventArgs e)
        {
            label14.Text = DateTime.Now.ToString();
        }
        int a, b, c;
        public void insert()
        {
            try
            {

                if (textBox2.Text == "" && textBox4.Text == "" && textBox5.Text == "") { MessageBox.Show("Tüm alanlar dolu olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (comboBox5.SelectedIndex == 0) { MessageBox.Show("Geçerli bir müşteri Tc'si giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (txtyil.Text == "") { MessageBox.Show("Geçerli bir yıl seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                else
                {
                    errorProvider1.SetError(this.textBox4, "9 haneden fazla miktar girilemez.");
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand kmt = new SqlCommand("select * from aracKayit where AracSase='" + textBox2.Text + "'", baglanti);
                    SqlDataReader oku = kmt.ExecuteReader();
                    if (oku.Read())
                    {

                        if (textBox4.Text == "")
                        {
                            MessageBox.Show("Araç miktarı boş kalamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                        else
                        {
                            double sayi1, sayi2, x;
                            sayi1 = Convert.ToDouble(textBox5.Text);
                            sayi2 = Convert.ToDouble(textBox4.Text);
                            x = sayi1 * sayi2;
                            tfiyat.Text = x.ToString();

                            a = Convert.ToInt32(textBox4.Text);
                            b = Convert.ToInt32(oku["miktar"]);
                            oku.Close();
                            c = b - a;
                            dmiktar.Text = c.ToString();
                            if (c < 0)
                            {
                                dmiktar.Text = "";
                                MessageBox.Show("Stokta yeterli ürün yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {

                                DialogResult d;
                                d = MessageBox.Show("Araç satışı yapılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (d == DialogResult.Yes)
                                {
                                    guncel();

                                    SqlCommand komut = new SqlCommand("INSERT INTO aracSatis (satisId,aracSase,TC,miktar,aracModel,aracMarka,vitesTur,yakitTur,aracYili,satisTarihi,garanti,fiyat,kullanici) values (@id,@sase,@tc,@miktar,@model,@marka,@vites,@yakit,@yil,@satisTarih,@garanti,@fiyat,@kullanici)", baglanti);
                                    komut.Parameters.AddWithValue("@id", label3.Text);
                                    komut.Parameters.AddWithValue("@sase", textBox2.Text);
                                    komut.Parameters.AddWithValue("@tc", comboBox5.Text);
                                    komut.Parameters.AddWithValue("@miktar", textBox4.Text);
                                    komut.Parameters.AddWithValue("@model", txtmodel.Text);
                                    komut.Parameters.AddWithValue("@marka", txtmarka.Text);
                                    komut.Parameters.AddWithValue("@vites", txtvites.Text);
                                    komut.Parameters.AddWithValue("@yakit", txtyakit.Text);
                                    komut.Parameters.AddWithValue("@yil", int.Parse(txtyil.Text));
                                    komut.Parameters.AddWithValue("@satisTarih", DateTime.Parse(label14.Text));
                                    komut.Parameters.AddWithValue("@garanti", DateTime.Parse(dateTimePicker2.Text));
                                    komut.Parameters.AddWithValue("@fiyat", textBox5.Text);
                                    komut.Parameters.AddWithValue("@kullanici", lblad.Text);

                                    komut.ExecuteNonQuery();

                                    MessageBox.Show("Satış Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    komut.Dispose();
                                    baglanti.Close();
                                    listele();
                                    Random rnd = new Random();
                                    int sayi = rnd.Next(12345, 99999);
                                    label3.Text = sayi.ToString();
                                    clear();
                                }
                            }
                        }
                        baglanti.Close();
                    }

                }
            }catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        aracSec frm = new aracSec();
        private void button2_Click(object sender, EventArgs e)
        {
            if (frm == null)
            {
                frm = new aracSec();
            }
            frm.aracsecildi += frm_aracsecildi;
            frm.ShowDialog();

        }
        private void frm_aracsecildi()
        {
            textBox2.Text = frm.secilenarac;
            textBox5.Text = frm.secilarac;

        }
        markasec yeni = new markasec();
        private void button1_Click(object sender, EventArgs e)
        {
            if (yeni == null)
            {
                yeni = new markasec();
            }
            yeni.aracsecildi += yeni_markasecildi;
            yeni.ShowDialog();

        }
        private void yeni_markasecildi()
        {
            txtmarka.Text = yeni.secilenmarka;
            txtmodel.Text = yeni.secilenmodel;
            txtvites.Text = yeni.secilenvites;
            txtyakit.Text = yeni.secilenyakit;
            txtyil.Text = yeni.secilenyil;
        }
        public void guncel()
        {
            SqlCommand command = new SqlCommand("update aracKayit set miktar='" + c + "' where AracSase='" + textBox2.Text + "'", baglanti);
            command.ExecuteNonQuery();
            command.Dispose();

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            insert();

        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from aracSatis", baglanti);
            da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            komut.Dispose();
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ac = new musteriKayit(this.comboBox5);
            ac.ShowDialog();
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        public void clear()
        {
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox5.SelectedIndex = 0;
            txtmarka.Clear();
            txtmodel.Clear();
            txtvites.Clear();
            txtyakit.Clear();
            txtyil.Clear();
        }

    }
}
