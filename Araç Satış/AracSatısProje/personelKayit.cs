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
    public partial class personelKayit : Form
    {
        public personelKayit()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        SqlCommand komut;
        SqlDataReader data;

        private void personelKayit_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            sayac = true;
            textBox7.PasswordChar = '*';
            comboBox1.SelectedIndex = 0;
            cmbgorev.SelectedIndex = 0;
            kayitGetir();
            dataGridView1.Columns[0].HeaderText = "Per İd";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "Cinsiyet";
            dataGridView1.Columns[4].HeaderText = "Görev";
            dataGridView1.Columns[5].HeaderText = "Adres";
            dataGridView1.Columns[6].HeaderText = "Tel";
            dataGridView1.Columns[7].HeaderText = "Doğum Tarihi";
            dataGridView1.Columns[8].HeaderText = "Yetki";
            dataGridView1.Columns[9].HeaderText = "Kullanıcı Adı";
            dataGridView1.Columns[10].Visible = false;
        
            textBox1.Text = Personel_Arama.secilenpersonel;
            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            try
            {
                komut = new SqlCommand("Select * from personelKayit where Perid=@Perid ", baglanti);
                komut.Parameters.Add("@Perid", SqlDbType.Int);
                komut.Parameters["@Perid"].Value = textBox1.Text;
                baglanti.Close();
                baglanti.Open();
                data = komut.ExecuteReader();
                if (data.Read())
                {
                    textBox2.Text = data[1].ToString();
                    textBox3.Text = data[2].ToString();
                    comboBox1.Text = data[3].ToString();
                    cmbgorev.Text = data[4].ToString();
                    textBox4.Text = data[5].ToString();
                    maskedTextBox1.Text = data[6].ToString();
                    dateTimePicker1.Text = data[7].ToString();

                }
            }
            catch
            {
            }

        }
        private void textbox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }
        private void textbox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }
        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



        public static string personelid;
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            try
            {

                personelid = textBox1.Text;
                SqlCommand komut = new SqlCommand("SELECT * From personelKayit WHERE Perid= @Perid", baglanti);
                komut.Parameters.Add("@Perid", SqlDbType.NChar);
                komut.Parameters["@Perid"].Value = personelid.ToString();


                SqlDataReader data = komut.ExecuteReader();
                if (data.Read())
                {


                    personelid = data["Perid"].ToString();
                    if (personelid != "")
                    {
                        güncelleekle();
                        baglanti.Close();

                    }

                }
                else
                {
                    DialogResult durum = MessageBox.Show("Veri seçilmedi yada bulunmadı yeni kayıt yapmak istermisiniz", "Kayıt Onayı", MessageBoxButtons.YesNo);
                    if (durum == DialogResult.Yes)
                    {
                        personelid = textBox1.Text;
                        güncelleekle();
                        baglanti.Close();



                    }
                }



            }

            catch
            {

            }
        }

        public string yetkiler;
        public Boolean kontrol;
        public string kullanici;
        private void kontroller()
        {
            baglanti.Close();
            baglanti.Open();
            komut = new SqlCommand("SELECT * FROM personelKayit WHERE Perid=@personel", baglanti);
            komut.Parameters.Add("@personel", SqlDbType.NChar);
            komut.Parameters["@personel"].Value = textBox1.Text;
            data = komut.ExecuteReader();
            if (data.Read())
            {
                kullanici = data["kullaniciadi"].ToString();
                if (data["kullaniciadi"].ToString() == textBox6.Text)
                {

             
                    DialogResult durum = MessageBox.Show("Yeni girilen kullanıcı adı kullanısıl mı", "Kayıt Onayı", MessageBoxButtons.YesNo);
                    if (durum == DialogResult.Yes)
                    {
                        
                        baglanti.Close();
                     
                        kontrol = false;
                       


                    }
                    else
                    {
                        baglanti.Close();
                        kontrol = true;
                    }
                    kontrol = true;


                }
                else
                {
                    kontrol = false;
                }
            }
        }
        private void güncelleekle()
        {
            try
            {
                baglanti = new SqlConnection(con.adres);
                baglanti.Open();
                komut = new SqlCommand("SELECT * FROM personelKayit WHERE Perid=@personel", baglanti);
                komut.Parameters.Add("@personel",SqlDbType.NChar);
                komut.Parameters["@personel"].Value = textBox1.Text;
                data = komut.ExecuteReader();
                if (data.Read())
                {
                    kontroller();
                   

                    if (checkBox1.Checked  || checkBox2.Checked  || checkBox3.Checked  || checkBox4.Checked  || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked  || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked && comboBox1.SelectedIndex==0 )
                    {
                        if (comboBox1.SelectedIndex!=0 && cmbgorev.SelectedIndex!=0 && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != "")
                        {
                            if (kontrol == true)
                            {
                                baglanti.Close();
                                baglanti.Open();
                                komut = new SqlCommand("Update personelKayit set Ad=@ad,Soyad=@soyad,Cinsiyet=@cinsiyet,Gorev=@gorev,Adres=@adres,Tel=@tel,DogumTarih=@dogum,yetki=@yetki,kullaniciadi=@kullaniciadi,sifre=@sifre where Perid=@personel", baglanti);


                                komut.Parameters.Add("@personel", SqlDbType.Int);
                                komut.Parameters["@personel"].Value = textBox1.Text;
                                komut.Parameters.Add("@ad", SqlDbType.NVarChar);
                                komut.Parameters["@ad"].Value = textBox2.Text;
                                komut.Parameters.Add("@soyad", SqlDbType.NVarChar);
                                komut.Parameters["@soyad"].Value = textBox3.Text;
                                komut.Parameters.Add("@cinsiyet", SqlDbType.NVarChar);
                                komut.Parameters["@cinsiyet"].Value = comboBox1.Text;
                                komut.Parameters.Add("@gorev", SqlDbType.NChar);
                                komut.Parameters["@gorev"].Value = cmbgorev.Text;
                                komut.Parameters.Add("@adres", SqlDbType.NVarChar);
                                komut.Parameters["@adres"].Value = textBox4.Text;
                                komut.Parameters.Add("@tel", SqlDbType.NChar);
                                komut.Parameters["@tel"].Value = maskedTextBox1.Text;
                                komut.Parameters.Add("@dogum", SqlDbType.Date);
                                komut.Parameters["@dogum"].Value = dateTimePicker1.Text;

                                komut.Parameters.Add("@kullaniciadi", SqlDbType.NVarChar);
                                komut.Parameters["@kullaniciadi"].Value = textBox6.Text;
                                komut.Parameters.Add("@sifre", SqlDbType.NVarChar);
                                komut.Parameters["@sifre"].Value = textBox7.Text;

                                if (checkBox1.Checked)
                                {
                                    yetkiler = yetkiler + "1";
                                }
                                if (checkBox2.Checked)
                                {
                                    yetkiler = yetkiler + "2";
                                }
                                if (checkBox3.Checked)
                                {
                                    yetkiler = yetkiler + "3";
                                }
                                if (checkBox4.Checked)
                                {
                                    yetkiler = yetkiler + "4";
                                }
                                if (checkBox5.Checked)
                                {
                                    yetkiler = yetkiler + "5";
                                }
                                if (checkBox6.Checked)
                                {
                                    yetkiler = yetkiler + "6";
                                }
                                if (checkBox7.Checked)
                                {
                                    yetkiler = yetkiler + "7";
                                }
                                if (checkBox8.Checked)
                                {
                                    yetkiler = yetkiler + "8";
                                }
                                if (checkBox9.Checked)
                                {
                                    yetkiler = yetkiler + "9";
                                }
                                if (checkBox10.Checked)
                                {
                                    yetkiler = yetkiler + "0";
                                }




                                komut.Parameters.Add("@yetki", SqlDbType.NChar);
                                komut.Parameters["@yetki"].Value = Convert.ToString(yetkiler);

                                komut.ExecuteNonQuery();
                                MessageBox.Show(" Kayıt Güncellendi");
                                temizle();
                                kayitGetir();

                                baglanti.Close();
                            }
                            else
                            {
                                baglanti.Close();
                                baglanti.Open();
                                komut = new SqlCommand("Update personelKayit set Ad=@ad,Soyad=@soyad,Cinsiyet=@cinsiyet,Gorev=@gorev,Adres=@adres,Tel=@tel,DogumTarih=@dogum,yetki=@yetki,kullaniciadi=@kullaniciadi,sifre=@sifre where Perid=@personel", baglanti);


                                komut.Parameters.Add("@personel", SqlDbType.Int);
                                komut.Parameters["@personel"].Value = textBox1.Text;
                                komut.Parameters.Add("@ad", SqlDbType.NVarChar);
                                komut.Parameters["@ad"].Value = textBox2.Text;
                                komut.Parameters.Add("@soyad", SqlDbType.NVarChar);
                                komut.Parameters["@soyad"].Value = textBox3.Text;
                                komut.Parameters.Add("@cinsiyet", SqlDbType.NVarChar);
                                komut.Parameters["@cinsiyet"].Value = comboBox1.Text;
                                komut.Parameters.Add("@gorev", SqlDbType.NChar);
                                komut.Parameters["@gorev"].Value = cmbgorev.Text;
                                komut.Parameters.Add("@adres", SqlDbType.NVarChar);
                                komut.Parameters["@adres"].Value = textBox4.Text;
                                komut.Parameters.Add("@tel", SqlDbType.NChar);
                                komut.Parameters["@tel"].Value = maskedTextBox1.Text;
                                komut.Parameters.Add("@dogum", SqlDbType.Date);
                                komut.Parameters["@dogum"].Value = dateTimePicker1.Text;

                                komut.Parameters.Add("@kullaniciadi", SqlDbType.NVarChar);
                                komut.Parameters["@kullaniciadi"].Value = kullanici.ToString();
                                komut.Parameters.Add("@sifre", SqlDbType.NVarChar);
                                komut.Parameters["@sifre"].Value = textBox7.Text;

                                if (checkBox1.Checked)
                                {
                                    yetkiler = yetkiler + "1";
                                }
                                if (checkBox2.Checked)
                                {
                                    yetkiler = yetkiler + "2";
                                }
                                if (checkBox3.Checked)
                                {
                                    yetkiler = yetkiler + "3";
                                }
                                if (checkBox4.Checked)
                                {
                                    yetkiler = yetkiler + "4";
                                }
                                if (checkBox5.Checked)
                                {
                                    yetkiler = yetkiler + "5";
                                }
                                if (checkBox6.Checked)
                                {
                                    yetkiler = yetkiler + "6";
                                }
                                if (checkBox7.Checked)
                                {
                                    yetkiler = yetkiler + "7";
                                }
                                if (checkBox8.Checked)
                                {
                                    yetkiler = yetkiler + "8";

                                }
                                if (checkBox9.Checked)
                                {
                                    yetkiler = yetkiler + "9";
                                }
                                if (checkBox10.Checked)
                                {
                                    yetkiler = yetkiler + "0";
                                }

                                komut.Parameters.Add("@yetki", SqlDbType.NChar);
                                komut.Parameters["@yetki"].Value = Convert.ToString(yetkiler);

                                komut.ExecuteNonQuery();
                                MessageBox.Show(" Kayıt Güncellendi");
                                temizle();
                                kayitGetir();

                                baglanti.Close();
                            }
                            baglanti.Close();
                        }
                        else
                        {
                            if (textBox2.Text == "")
                            {
                                errorProvider1.SetError(textBox2, "Boş olamaz");
                            }
                            if (textBox3.Text == "")
                            {
                                errorProvider1.SetError(textBox3, "Boş olamaz");
                            }
                            if (textBox4.Text == "")
                            {
                                errorProvider1.SetError(textBox4, "Boş olamaz");
                            }
                            if (maskedTextBox1.Text == "")
                            {
                                errorProvider1.SetError(maskedTextBox1, "Boş olamaz");
                            }
                            if (textBox6.Text == "")
                            {
                                errorProvider1.SetError(textBox6, "Boş olamaz");
                            }
                            if (textBox7.Text == "")
                            {
                                errorProvider1.SetError(textBox7, "Boş olamaz");
                            }
                            if (comboBox1.Text == "")
                            {
                                errorProvider1.SetError(textBox2, "Boş olamaz");
                            }
                            if (comboBox1.SelectedIndex==0)
                            {
                                errorProvider1.SetError(comboBox1, "Boş olamaz");
                            }
                            if (cmbgorev.SelectedIndex==0)
                            {
                                errorProvider1.SetError(cmbgorev, "Boş olamaz");
                            }
                            baglanti.Close();
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Yetki boş bıraklımaz");
                        baglanti.Close();
                    }
                   
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    komut = new SqlCommand("SELECT * FROM personelKayit WHERE kullaniciadi=@personel", baglanti);
                    komut.Parameters.Add("@personel", SqlDbType.NChar);
                    komut.Parameters["@personel"].Value = textBox6.Text;
                    data = komut.ExecuteReader();
                    if (data.Read())
                    {
                        if (data["kullaniciadi"].ToString() == textBox6.Text)
                        {
                            MessageBox.Show("Bu kullanıcı adı var");
                            kontrol = true;
                            baglanti.Close();

                        }
                        else
                        {
                            kontrol = false;
                        }
                    }
                    else
                    {
                        kontrol = false;
                    }


                    if (kontrol==false)
                    {
                        if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != ""&&comboBox1.SelectedIndex!=0&&cmbgorev.SelectedIndex!=0)
                        {
                            if ( checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked)
                            {

                                baglanti.Close();
                                baglanti.Open();
                                komut = new SqlCommand("INSERT INTO personelKayit (Ad,Soyad,Cinsiyet,Gorev,Adres,Tel,DogumTarih,yetki,kullaniciadi,sifre) VALUES(@ad,@soyad,@cinsiyet,@gorev,@adres,@tel,@dogum,@yetki,@kullaniciadi,@sifre)", baglanti);


                                komut.Parameters.Add("@ad", SqlDbType.NVarChar);
                                komut.Parameters["@ad"].Value = textBox2.Text;
                                komut.Parameters.Add("@soyad", SqlDbType.NVarChar);
                                komut.Parameters["@soyad"].Value = textBox3.Text;
                                komut.Parameters.Add("@cinsiyet", SqlDbType.NVarChar);
                                komut.Parameters["@cinsiyet"].Value = comboBox1.Text;
                                komut.Parameters.Add("@gorev", SqlDbType.NChar);
                                komut.Parameters["@gorev"].Value = cmbgorev.Text;
                                komut.Parameters.Add("@adres", SqlDbType.NVarChar);
                                komut.Parameters["@adres"].Value = textBox4.Text;
                                komut.Parameters.Add("@tel", SqlDbType.NChar);
                                komut.Parameters["@tel"].Value = maskedTextBox1.Text;
                                komut.Parameters.Add("@dogum", SqlDbType.Date);
                                komut.Parameters["@dogum"].Value = dateTimePicker1.Text;
                                komut.Parameters.Add("@kullaniciadi", SqlDbType.NVarChar);
                                komut.Parameters["@kullaniciadi"].Value = textBox6.Text;
                                komut.Parameters.Add("@sifre", SqlDbType.NVarChar);
                                komut.Parameters["@sifre"].Value = textBox7.Text;

                                if (checkBox1.Checked)
                                {
                                    yetkiler = yetkiler + "1";
                                }
                                if (checkBox2.Checked)
                                {
                                    yetkiler = yetkiler + "2";
                                }
                                if (checkBox3.Checked)
                                {
                                    yetkiler = yetkiler + "3";
                                }
                                if (checkBox4.Checked)
                                {
                                    yetkiler = yetkiler + "4";
                                }
                                if (checkBox5.Checked)
                                {
                                    yetkiler = yetkiler + "5";
                                }
                                if (checkBox6.Checked)
                                {
                                    yetkiler = yetkiler + "6";
                                }
                                if (checkBox7.Checked)
                                {
                                    yetkiler = yetkiler + "7";
                                }
                                if (checkBox8.Checked)
                                {
                                    yetkiler = yetkiler + "8";
                                }
                                if (checkBox9.Checked)
                                {
                                    yetkiler = yetkiler + "9";
                                }
                                if (checkBox10.Checked)
                                {
                                    yetkiler = yetkiler + "0";
                                }

                                komut.Parameters.Add("@yetki", SqlDbType.NChar);
                                komut.Parameters["@yetki"].Value = yetkiler;
                                komut.ExecuteNonQuery();
                                MessageBox.Show("Kayıt Başarılı");
                                temizle();
                                kayitGetir();
                                baglanti.Close();
                            }
                            else
                            {
                                MessageBox.Show("Yetki Seçmediniz");
                                baglanti.Close();
                            }
                        }
                        else
                        {
                            if (textBox2.Text == "")
                            {
                                errorProvider1.SetError(textBox2, "Boş olamaz");
                            }
                            if (textBox3.Text == "")
                            {
                                errorProvider1.SetError(textBox3, "Boş olamaz");
                            }
                            if (textBox4.Text == "")
                            {
                                errorProvider1.SetError(textBox4, "Boş olamaz");
                            }
                            if (maskedTextBox1.Text == "")
                            {
                                errorProvider1.SetError(maskedTextBox1, "Boş olamaz");
                            }
                            if (textBox6.Text == "")
                            {
                                errorProvider1.SetError(textBox6, "Boş olamaz");
                            }
                            if (textBox7.Text == "")
                            {
                                errorProvider1.SetError(textBox7, "Boş olamaz");
                            }
                            if (comboBox1.Text == "")
                            {
                                errorProvider1.SetError(textBox2, "Boş olamaz");
                            }
                            if (comboBox1.SelectedIndex == 0)
                            {
                                errorProvider1.SetError(comboBox1, "Boş olamaz");
                            }
                            if (cmbgorev.SelectedIndex == 0)
                            {
                                errorProvider1.SetError(cmbgorev, "Boş olamaz");
                            }
                            baglanti.Close();
                        }
                    }
                   
                    





                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
                throw;
            }
            
        }

        private void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            yetkiler = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            comboBox1.SelectedIndex = 0;
            cmbgorev.SelectedIndex = 0;
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Perid"].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Ad"].Value);
            textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Soyad"].Value);
            textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Adres"].Value);
            maskedTextBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Tel"].Value);
            textBox6.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["kullaniciadi"].Value);
            textBox7.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["sifre"].Value);
            comboBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Cinsiyet"].Value);
            cmbgorev.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Gorev"].Value);
            dateTimePicker1.Text= Convert.ToString(dataGridView1.CurrentRow.Cells["DogumTarih"].Value);
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Perİd boş olamaz");
                return;
            }

            DialogResult result = MessageBox.Show("Seçilen veriyi silmek istediğinize emin misiniz? İşlem geri alınamamaktadır!", "Sil", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {

                    baglanti.Open();


                    komut = new SqlCommand("Delete from personelKayit where Perid=@personel ", baglanti);

                    komut.Parameters.Add("@personel", SqlDbType.NChar);
                    komut.Parameters["@personel"].Value = textBox1.Text;

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Silme Başarılı");
                    temizle();
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                    dataGridView1.Refresh();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message.ToString());
                    return;
                }
                finally
                {
                    baglanti.Close();
                }


            }

            else
                MessageBox.Show("Lütfen Listeden Bir Veri Seçin");
        }
        private void kayitGetir()
        {
        
            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            string kayit = "SELECT * from personelKayit";

            SqlCommand komut = new SqlCommand(kayit, baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            

            baglanti.Close();
        }
      

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                       Form ara = new Personel_Arama();
           ara.Show();
            this.Close();
        }

        public Boolean sayac;
        private void button4_Click(object sender, EventArgs e)
        {
            if (sayac == true)
            {
                textBox7.PasswordChar = '\0';
                sayac = false;
            }
            else
            {
                textBox7.PasswordChar = '*';
                sayac = true;
            }
        }
    }

}


