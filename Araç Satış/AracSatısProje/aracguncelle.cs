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
    public partial class aracguncelle : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public aracguncelle()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        private void aracguncelle_Load(object sender, EventArgs e)
        {
            Giris g = new Giris();
            lblad.Text = Giris.kullaniciAdi;
            list();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textBox2.MaxLength = 9;
            textBox3.MaxLength = 9;

            dataGridView1.Columns[0].HeaderText = "Araç Şase";
            dataGridView1.Columns[1].HeaderText = "Marka Adı";
            dataGridView1.Columns[2].HeaderText = "Fabrika Adı";
            dataGridView1.Columns[3].HeaderText = "Geliş Tarihi";
            dataGridView1.Columns[4].HeaderText = "Miktar";
            dataGridView1.Columns[5].HeaderText = "Fiyat";
            dataGridView1.Columns[6].HeaderText = "Kullanıcı";
            dataGridView1.Columns[7].HeaderText = "Tarih Saat";
            dataGridView1.Columns[8].HeaderText = "Araç Durum";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select aracmarka from markaStok", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["aracmarka"].ToString());
            }
            oku.Close();
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select fabrikadi from fabrikaKayit", baglanti);
            SqlDataReader oku1 = komut1.ExecuteReader();
            while (oku1.Read())
            {
                comboBox2.Items.Add(oku1["fabrikadi"].ToString());
            }
            oku1.Close();
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            update();
        }
        public void list()
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
        public void update()
        {
            if (textBox2.Text == "" || textBox3.Text == "") { MessageBox.Show("Geçerli bir miktar veya fiyat giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (comboBox1.SelectedIndex == 0) { MessageBox.Show("Geçerli bir marka seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (comboBox2.SelectedIndex == 0) { MessageBox.Show("Geçerli bir fabrika adı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {

                DialogResult d;
                d = MessageBox.Show(label9.Text + " Şase numaralı araç güncellesin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("UPDATE aracKayit set markaAdi=@marka,fabrikaAdi=@fabrika,gelisTarih=@gelisTarih,miktar=@miktar,fiyat=@fiyat,kullanici=@kullanici,tarihSaat=@tarihSaat where AracSase=@sase", baglanti);

                    komut.Parameters.AddWithValue("@marka", comboBox1.Text);
                    komut.Parameters.AddWithValue("@fabrika", comboBox2.Text);
                    komut.Parameters.AddWithValue("@gelisTarih", DateTime.Parse(dateTimePicker1.Text));
                    komut.Parameters.AddWithValue("@miktar", textBox2.Text);
                    komut.Parameters.AddWithValue("@fiyat", textBox3.Text);
                    komut.Parameters.AddWithValue("@kullanici", lblad.Text);
                    komut.Parameters.AddWithValue("@tarihSaat", DateTime.Parse(label8.Text));
                    komut.Parameters.AddWithValue("@sase", label9.Text);
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                    MessageBox.Show("Araç güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    list();
                    textBox2.Clear();
                    textBox3.Clear();

                }


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form ac = new MarkaStok(this.comboBox1);
            ac.ShowDialog();

            Form mrk = new markasec();
            mrk.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form fabrika = new fabrikaKayit(this.comboBox2);
            fabrika.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString();
        }
        public void delete(int kod)
        {
            DialogResult d;
            d = MessageBox.Show("Aracı silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from aracKayit where AracSase=@sase", baglanti);
                komut.Parameters.AddWithValue("sase", kod);
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                label9.Text = "";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
            {
                int kod = Convert.ToInt32(drow.Cells[0].Value);
                delete(kod);
            }
            list();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }
    }
}
