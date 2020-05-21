using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracSatısProje
{
    public partial class servisKayit : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public servisKayit()
        {
            InitializeComponent();
            baglanti = new SqlConnection(con.adres);
        }


        DataTable dt = new DataTable();
        SqlDataAdapter da;

        public string aracsase;
        private void button4_Click(object sender, EventArgs e)
        {
            insert();
            
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            update();
        }

        private void servisKayit_Load(object sender, EventArgs e)
        {
            Giris g = new Giris();
            lblad.Text = Giris.kullaniciAdi;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cmbtc.SelectedIndex = 0;
            cmbparcad.SelectedIndex = 0;
            cmbkasko.SelectedIndex = 0;

            
            Random rnd = new Random();
            int sayi = rnd.Next(12345, 99999);
            textBox1.Text = Convert.ToString(sayi);
            kayıtgetır();
            oku();
            dataGridView1.Columns[0].HeaderText = "AraçŞase";
            dataGridView1.Columns[1].HeaderText = "TC";
            dataGridView1.Columns[2].HeaderText = "Parça Adı";
            dataGridView1.Columns[3].HeaderText = "Araç Marka";
            dataGridView1.Columns[4].HeaderText = "Araç Model";
            dataGridView1.Columns[5].HeaderText = "Araç Durum";
            dataGridView1.Columns[6].HeaderText = "Kasko";
            dataGridView1.Columns[7].HeaderText = "Araç Plaka";
            dataGridView1.Columns[8].HeaderText = "Giriş Tarih";
            dataGridView1.Columns[9].HeaderText = "Çıkış Tarih";
            dataGridView1.Columns[10].HeaderText = "Sigorta No";
            dataGridView1.Columns[11].HeaderText = "Fiyat";
            ToolTip acik = new ToolTip();
            acik.SetToolTip(button2, "TC Ekle");
            acik.SetToolTip(button3, "Yedek Parça Ekle");
            acik.SetToolTip(button1, "Marka Seç");

            cmbtc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbtc.MaxLength = 11;
            textBox7.MaxLength = 9;
            textBox6.MaxLength = 9;

        }

        public void kayıtgetır()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from servisKayit", baglanti);
            da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            komut.Dispose();
            baglanti.Close();
        }
        public void temizle()
        {
            Random rnd = new Random();
            int sayi = rnd.Next(12345, 99999);
            textBox1.Text = Convert.ToString(sayi);
            cmbtc.SelectedIndex = 0;
            cmbkasko.SelectedIndex = 0;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            cmbparcad.SelectedIndex = 0;
            txtmarka.Clear();
            cmbtc.SelectedIndex = 0;
            txtmodel.Clear();

        }


        public void update()
        {
            try
            {
                if (cmbkasko.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "") { MessageBox.Show("Geçerli bir araç seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (cmbtc.SelectedIndex == 0) { MessageBox.Show("Geçerli bir TC seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (cmbparcad.SelectedIndex == 0) { MessageBox.Show("Geçerli bir parça seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    DialogResult d;
                    d = MessageBox.Show(textBox1.Text + " Şase numaralı araç güncellesin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {

                        SqlCommand komut = new SqlCommand("UPDATE servisKayit set TC=@tc,parcaadi=@parca,aracmarka=@marka,aracmodel=@model,kasko=@kasko,aracplaka=@plaka,giristarih=@tarih,cikistarih=@ctarih,sigortano=@sigorta,fiyat=@fiyat,kullanici=@kullanici where aracsase=@sase", baglanti);

                        komut.Parameters.AddWithValue("@tc", cmbtc.Text);
                        komut.Parameters.AddWithValue("@parca", cmbparcad.Text);
                        komut.Parameters.AddWithValue("@marka", txtmarka.Text);
                        komut.Parameters.AddWithValue("@model", txtmodel.Text);
                        komut.Parameters.AddWithValue("@kasko", cmbkasko.Text);
                        komut.Parameters.AddWithValue("@plaka", textBox5.Text);
                        komut.Parameters.AddWithValue("@sigorta", textBox6.Text);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Parse(dateTimePicker1.Text));
                        komut.Parameters.AddWithValue("@fiyat", textBox7.Text);
                        komut.Parameters.AddWithValue("@kullanici", lblad.Text);
                        komut.Parameters.AddWithValue("@ctarih", DateTime.Parse(dateTimePicker2.Text));
                        komut.Parameters.AddWithValue("@sase", textBox1.Text);
                        komut.ExecuteNonQuery();
                        komut.Dispose();

                        MessageBox.Show("Araç güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        kayıtgetır();
                        temizle();

                    }

                    baglanti.Close();
                }
            }catch(Exception e) { MessageBox.Show(e.Message.ToString()); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cmbtc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbparcad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtmarka.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtmodel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbkasko.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form a = new musteriKayit(this.cmbtc);
            a.ShowDialog();
            kayıtgetır();
        }
        public void oku()
        {
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select distinct TC from musteriKayit", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbtc.Items.Add(oku["TC"].ToString());
            }
            oku.Close();
            baglanti.Close();

                      

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select distinct parcadi from yedekParca", baglanti);
            SqlDataReader oku3 = komut3.ExecuteReader();
            while (oku3.Read())
            {
                cmbparcad.Items.Add(oku3["parcadi"].ToString());
            }
            oku3.Close();
            baglanti.Close();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form yedek = new YedekParca(this.cmbparcad);
            yedek.ShowDialog();
        }

        markasec yeni = new markasec();
        private void button1_Click(object sender, EventArgs e)
        {
            if(yeni == null)
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
           
        }

        public void insert()
        {
            try
            {
                baglanti.Close();
                baglanti.Open();
                aracsase = textBox1.Text;
                SqlCommand komut = new SqlCommand("SELECT * From servisKayit WHERE aracsase = @aracsase", baglanti);
                komut.Parameters.Add("@aracsase", SqlDbType.NChar);
                komut.Parameters["@aracsase"].Value = aracsase.ToString();
                SqlDataReader data = komut.ExecuteReader();
                if (data.Read())
                {

                    MessageBox.Show("Bu Aracsaşe ile ilgili kayıt var");
                    temizle();

                }                
                else
                {
                    data.Close();
                    if (textBox1.Text != "" && cmbtc.SelectedIndex != 0 && cmbkasko.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && cmbparcad.SelectedIndex != 0 && txtmarka.Text != "" && txtmodel.Text != "")
                    {
                        try
                        {

                        }
                        catch (Exception)
                        {

                            throw;
                        }



                        komut = new SqlCommand("INSERT INTO servisKayit (aracsase,TC,parcaadi,aracmarka,aracmodel,kasko,aracplaka,giristarih,cikistarih,sigortano,fiyat,kullanici) VALUES(@aracsase,@TC,@parcaadi,@aracmarka,@aracmodel,@kasko,@aracplaka,@giristarih,@cikistarih,@sigortano,@fiyat,@kullanici)", baglanti);

                        Random rnd = new Random();
                        int sayi = rnd.Next(12345, 99999);
                        textBox1.Text = Convert.ToString(sayi);
                        komut.Parameters.Add("@aracsase", SqlDbType.Int);
                        komut.Parameters["@aracsase"].Value = Convert.ToInt32(textBox1.Text);
                        komut.Parameters.Add("@TC", SqlDbType.NChar);
                        komut.Parameters["@TC"].Value = cmbtc.Text;
                        komut.Parameters.Add("@parcaadi", SqlDbType.NVarChar);
                        komut.Parameters["@parcaadi"].Value = cmbparcad.Text;
                        komut.Parameters.Add("@aracmarka", SqlDbType.NChar);
                        komut.Parameters["@aracmarka"].Value = txtmarka.Text;
                        komut.Parameters.Add("@aracmodel", SqlDbType.NChar);
                        komut.Parameters["@aracmodel"].Value = txtmodel.Text;
                        komut.Parameters.Add("@kasko", SqlDbType.NChar);
                        komut.Parameters["@kasko"].Value = cmbkasko.Text;
                        komut.Parameters.Add("@aracplaka", SqlDbType.NChar);
                        komut.Parameters["@aracplaka"].Value = textBox5.Text;
                        komut.Parameters.Add("@giristarih", SqlDbType.Date);
                        komut.Parameters["@giristarih"].Value = dateTimePicker1.Text;
                        komut.Parameters.Add("@cikistarih", SqlDbType.Date);
                        komut.Parameters["@cikistarih"].Value = dateTimePicker2.Text;
                        komut.Parameters.Add("@sigortano", SqlDbType.Int);
                        komut.Parameters["@sigortano"].Value = Convert.ToInt32(textBox6.Text);
                        komut.Parameters.Add("@fiyat", SqlDbType.NVarChar);
                        komut.Parameters["@fiyat"].Value = textBox7.Text;
                        komut.Parameters.Add("@kullanici", SqlDbType.Char);
                        komut.Parameters["@kullanici"].Value = lblad.Text;

                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Başarılı");

                        temizle();
                        kayıtgetır();



                    }
                    else
                    {
                        MessageBox.Show("Tüm alanlar dolu olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    baglanti.Close();



                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void delete()
        {
            try
            {
                if (cmbkasko.SelectedIndex==0 && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "" && txtmarka.Text == "" && txtmodel.Text == "") { MessageBox.Show("Geçerli bir araç seçiniz"); }
                else
                {
                    DialogResult d;
                    d = MessageBox.Show("Servisten çıkarılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {
                        baglanti.Close();
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("delete from servisKayit where aracsase=@sase", baglanti);
                        komut.Parameters.AddWithValue("sase", textBox1.Text);
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        kayıtgetır();
                        temizle();

                    }
                }
            }catch(Exception e) { MessageBox.Show(e.Message.ToString()); }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
            {
                int kod = Convert.ToInt32(drow.Cells[0].Value);
                sil(kod);
            }
            kayıtgetır();

        }
        public void sil(int sase)
        {

            DialogResult d;
            d = MessageBox.Show("Tüm kayıtlar silinsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                baglanti.Close();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("truncate table servisKayit where aracsase=@sase", baglanti);
                komut.Parameters.AddWithValue("@sase", sase);
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                kayıtgetır();
                temizle();
                MessageBox.Show("Kayıtlar silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void read()
        {
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from yedekParca where parcadi=@ad", baglanti);
            komut.Parameters.AddWithValue("@ad",cmbparcad.Text);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textBox7.Text = oku["fiyat"].ToString();
               
            }
            oku.Close();
            baglanti.Close();

        }

        private void cmbparcad_SelectedIndexChanged(object sender, EventArgs e)
        {
            read();
        }

        private void cmbtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
