using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AracSatısProje
{
    public partial class YedekParca : Form
    {
        private SqlConnection baglanti;

        public ComboBox ComboBox { get; set; }

        public YedekParca(ComboBox comboBox = null)
        {
            InitializeComponent();

            baglantiAdres con = new baglantiAdres();
            baglanti = new SqlConnection(con.adres);
            kayitgetir();
            checkBoxBaslangic();

            if (comboBox != null)
                ComboBox = comboBox;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Random rnd = new Random();
            int sayi = rnd.Next(12345, 99999);
            textBox1.Text = sayi.ToString();

            cmbstok.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

        }


        private void insertValues(String parcaid, String parcadi, String parcaTuru, String parcaModel, String fabrikaBilgi, String stokDurum, int miktar, string fiyat) //inset komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("INSERT INTO dbo.yedekParca(parcaid,parcadi,parcaTuru,parcaModel,fabrikaBilgi,stokDurum,miktar,fiyat) values(@parcaid,@parcadi, @parcaturu, @parcamodel, @fabrikabilgi, @stokdurum, @miktar, @fiyat)", baglanti);
            komut.Parameters.AddWithValue("@parcaid", parcaid);
            komut.Parameters.AddWithValue("@parcadi", parcadi);
            komut.Parameters.AddWithValue("@parcaTuru", parcaTuru);
            komut.Parameters.AddWithValue("@parcaModel", parcaModel);
            komut.Parameters.AddWithValue("@fabrikaBilgi", fabrikaBilgi);
            komut.Parameters.AddWithValue("@stokDurum", stokDurum);
            komut.Parameters.AddWithValue("@miktar", miktar);
            komut.Parameters.AddWithValue("@fiyat", fiyat);
            try
            {
                komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Bu parça id var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                komut.Dispose();

                baglanti.Close(); // Kapan susam kapan
            }
            komut.Dispose();
            if (ComboBox != null)
                ComboBox.Items.Add(textBox2.Text);

            baglanti.Close(); // Kapan susam kapan
        }


        private void updateValues(String parcaid, String parcadi, String parcaTuru, String parcaModel, String fabrikaBilgi, String stokDurum, int miktar, string fiyat) //update komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("UPDATE dbo.yedekParca SET parcaid=@parcaid,parcadi=@parcadi,parcaTuru=@parcaTuru,parcaModel=@parcaModel,fabrikaBilgi=@fabrikaBilgi,stokDurum=@stokDurum,miktar=@miktar,fiyat=@fiyat where parcaid=@parcaid", baglanti);
            komut.Parameters.AddWithValue("@parcaid", parcaid);
            komut.Parameters.AddWithValue("@parcadi", parcadi);
            komut.Parameters.AddWithValue("@parcaTuru", parcaTuru);
            komut.Parameters.AddWithValue("@parcaModel", parcaModel);
            komut.Parameters.AddWithValue("@fabrikaBilgi", fabrikaBilgi);
            komut.Parameters.AddWithValue("@stokDurum", stokDurum);
            komut.Parameters.AddWithValue("@miktar", miktar);
            komut.Parameters.AddWithValue("@fiyat", fiyat);
            komut.ExecuteNonQuery();
            komut.Dispose();

            baglanti.Close(); // Kapan susam kapan
        }


        private void deleteValues(String parcaid) //delete komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("DELETE from dbo.yedekParca where parcaid=@parcaid", baglanti);
            komut.Parameters.AddWithValue("@parcaid", parcaid);

            komut.ExecuteNonQuery();
            komut.Dispose();

            baglanti.Close(); // Kapan susam kapan
        }

        private void kayitgetir()  // dataGrid kayıt yerleştirme ilk baslangic
        {
            SqlDataAdapter da = new SqlDataAdapter("Select *From dbo.yedekParca", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "yedekParca");
            dataGridView1.DataSource = ds.Tables["yedekParca"];
            baglanti.Close();
        }

        private void checkBoxBaslangic()
        {
            baglanti.Open(); // Açıl susam açıl
            SqlCommand komut = new SqlCommand("Select fabrikadi From dbo.fabrikaKayit", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["fabrikadi"].ToString());
            }

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && cmbstok.SelectedIndex != 0 && textBox7.Text != "" && textBox8.Text != "" && comboBox1.SelectedIndex != 0)
            {
                insertValues(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text, cmbstok.Text, Convert.ToInt32(textBox7.Text), textBox8.Text);
                MessageBox.Show("Kayıt Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Random rnd = new Random();
                int sayi = rnd.Next(12345, 99999);
                textBox1.Text = sayi.ToString();
                kayitgetir();
                clear();
            }
            else
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

<<<<<<< HEAD
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && comboBox1.SelectedIndex != 0)
            { 
                 updateValues(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text, textBox6.Text, Convert.ToInt32(textBox7.Text), Convert.ToInt32(textBox8.Text));
                kayitgetir();
            }
            else
            {
               MessageBox.Show("Boş Bırakmayınız");
=======
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && cmbstok.SelectedIndex == 0 && textBox7.Text == "" && textBox8.Text == "" && comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Boş Bırakmayınız","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                updateValues(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text, cmbstok.Text, Convert.ToInt32(textBox7.Text),textBox8.Text);
                MessageBox.Show("Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Random rnd = new Random();
                int sayi = rnd.Next(12345, 99999);
                textBox1.Text = sayi.ToString();
                kayitgetir();
                clear();
>>>>>>> 26d4034caafa4f0bfa4e321726ed4332eeb8296f
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && comboBox1.SelectedIndex != 0)
            {
                deleteValues(textBox1.Text);
                kayitgetir();
              
            }
            else
            {
                          MessageBox.Show("Boş Bırakmayınız");
=======
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && cmbstok.SelectedIndex==0 && textBox7.Text == "" && textBox8.Text == "" && comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Boş Bırakmayınız","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                deleteValues(textBox1.Text);
                Random rnd = new Random();
                int sayi = rnd.Next(12345, 99999);
                textBox1.Text = sayi.ToString();
                MessageBox.Show("Kayıt silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kayitgetir();
                clear();
>>>>>>> 26d4034caafa4f0bfa4e321726ed4332eeb8296f
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["parcaid"].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["parcadi"].Value);
            textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["parcaturu"].Value);
            textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["parcamodel"].Value);
            comboBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["fabrikabilgi"].Value);
            cmbstok.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["stokdurum"].Value);
            textBox7.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["miktar"].Value);
            textBox8.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["fiyat"].Value);
        }



        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

<<<<<<< HEAD
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

=======
        private void button4_Click(object sender, EventArgs e)
        {
            Form ac = new fabrikaKayit(this.comboBox1);
            ac.ShowDialog();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public void clear()
        {
        
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
            textBox8.Clear();
            comboBox1.SelectedIndex = 0;
            cmbstok.SelectedIndex = 0;
>>>>>>> 26d4034caafa4f0bfa4e321726ed4332eeb8296f
        }
    }
}
