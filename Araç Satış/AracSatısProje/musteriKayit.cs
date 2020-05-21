using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracSatısProje
{
    public partial class musteriKayit : Form
    {
        private SqlConnection baglanti;
        public ComboBox ComboBox { get; set; }

        public musteriKayit(ComboBox comboBox = null)
        {
            InitializeComponent();
            baglantiAdres con = new baglantiAdres();
            baglanti = new SqlConnection(con.adres);

            if (comboBox != null)
                ComboBox = comboBox;


        }

        private void musteriKayit_Load(object sender, EventArgs e)
        {
            kayitgetir();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dataGridView1.Columns[0].HeaderText = "TC";
            dataGridView1.Columns[1].HeaderText = "Müşteri Ad";
            dataGridView1.Columns[2].HeaderText = "Müşteri Soyad";
            dataGridView1.Columns[3].HeaderText = "Adres";
            dataGridView1.Columns[4].HeaderText = "Email";
            dataGridView1.Columns[5].HeaderText = "Telefon";
            dataGridView1.Columns[6].HeaderText = "Doğum Tarih";
          
        }


        private void insertValues(String tcNo, String musteriAd, String musteriSoyad, String Adres, String email, String telefon, DateTime dateTime) //inset komutu
        {
            try
            {
                baglanti.Open(); // Açıl susam açıl

                SqlCommand komut = new SqlCommand("INSERT INTO dbo.musteriKayit(TC,MusteriAd,MusteriSoyad,Adres,email,Telefon,DogumTarih) values(@TC,@MusteriAd,@MusteriSoyad,@Adres,@email,@Telefon,@DogumTarih)", baglanti);
                komut.Parameters.AddWithValue("@TC", tcNo);
                komut.Parameters.AddWithValue("@MusteriAd", musteriAd);
                komut.Parameters.AddWithValue("@MusteriSoyad", musteriSoyad);
                komut.Parameters.AddWithValue("@Adres", Adres);
                komut.Parameters.AddWithValue("@email", email);
                komut.Parameters.AddWithValue("@Telefon", telefon);
                komut.Parameters.AddWithValue("@DogumTarih", DateTime.Parse(dateTimePicker1.Text));
                try
                {
                    komut.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    MessageBox.Show("Bu TC var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                komut.Dispose();

                if (ComboBox != null)
                    ComboBox.Items.Add(textBox1.Text);

                baglanti.Close(); // Kapan susam kapan
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }


        private void updateValues(String tcNo, String musteriAd, String musteriSoyad, String Adres, String email, String telefon, DateTime dateTime) //update komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("UPDATE dbo.musteriKayit SET TC=@TC,MusteriAd=@MusteriAd,MusteriSoyad=@MusteriSoyad,Adres=@Adres,email=@email,Telefon=@Telefon,DogumTarih=@DogumTarih where TC=@TC", baglanti);
            komut.Parameters.AddWithValue("@TC", tcNo);
            komut.Parameters.AddWithValue("@MusteriAd", musteriAd);
            komut.Parameters.AddWithValue("@MusteriSoyad", musteriSoyad);
            komut.Parameters.AddWithValue("@Adres", Adres);
            komut.Parameters.AddWithValue("@email", email);
            komut.Parameters.AddWithValue("@Telefon", telefon);
            komut.Parameters.AddWithValue("@DogumTarih", DateTime.Parse(dateTimePicker1.Text));
            komut.ExecuteNonQuery();
            komut.Dispose();

            baglanti.Close(); // Kapan susam kapan
        }


        private void deleteValues(int tcNo) //delete komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("DELETE from dbo.musteriKayit where TC=@TC", baglanti);
            komut.Parameters.AddWithValue("@TC", tcNo);

            komut.ExecuteNonQuery();
            komut.Dispose();

            baglanti.Close(); // Kapan susam kapan
        }

        private void kayitgetir()  // dataGrid kayıt yerleştirme ilk baslangic
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *From dbo.musteriKayit", baglanti);
            DataSet ds = new DataSet();

            da.Fill(ds, "musteriKayit");
            dataGridView1.DataSource = ds.Tables["musteriKayit"];
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "") { MessageBox.Show("Boş bırakmayınız.","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else {
                if (this.denetleme())
                {
                    insertValues(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, DateTime.Parse(dateTimePicker1.Text));
                    MessageBox.Show("Kayıt Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    kayitgetir();
                } }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["TC"].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["MusteriAd"].Value);
            textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["MusteriSoyad"].Value);
            textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Adres"].Value);
            textBox5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["email"].Value);
            textBox6.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Telefon"].Value);
            dateTimePicker1.Text = (Convert.ToString(dataGridView1.CurrentRow.Cells["DogumTarih"].Value));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "") { MessageBox.Show("Boş bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                if (this.denetleme())
                {
                    updateValues(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, DateTime.Parse(dateTimePicker1.Text));
                    MessageBox.Show("İşlem Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    kayitgetir();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "") { MessageBox.Show("Boş bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                deleteValues(Convert.ToInt32(textBox1.Text));
                MessageBox.Show("İşlem Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                kayitgetir();
            }

            
        }
        private bool denetleme()
        {

                
            if (textBox1.Text.Length < 12 && textBox1.Text != null && textBox1.Text.Length > 10)
            {
                if (textBox2.Text.Length < 21 && textBox2.Text != null)
                {
                    if (textBox3.Text.Length < 21 && textBox3.Text != null)
                    {
                        if (textBox4.Text.Length < 51 && textBox4.Text != null)
                        {
                            if (textBox5.Text != null && textBox5.Text.Length < 51)
                            {
                                if (textBox6.Text != null && textBox6.Text.Length < 16)
                                {

                                    return true;
                                }
                                else
                                {

                                    MessageBox.Show("Telefon boş yada 15 karakterden fazla olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false;
                                }

                            }
                            else
                            {
                                MessageBox.Show("E-Mail boş yada 50 karakterden fazla olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;

                            }
                        }
                        else
                        {
                            MessageBox.Show("Adres boş yada 50 karakterden fazla olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Müşteri Soyadı boş yada 20 karakterden fazla olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Müşteri adı boş yada 20 karakterden fazla olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }

            else
            {
                MessageBox.Show("TC Boş veya 11 sayıdan yüksek olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            


        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
