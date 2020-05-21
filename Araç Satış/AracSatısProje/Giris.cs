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
    public partial class Giris : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public Giris()
        {
            InitializeComponent();
            baglanti = new SqlConnection(con.adres);
        }


        SqlDataReader data;


        public static string kullaniciAdi;
        public static string unvan;

        
        

        private void Giris_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ToolTip ac = new ToolTip();
            ac.SetToolTip(button2, "Şifre Göster");

            txtsifre.PasswordChar = '*';
            sayac = true;
        }
        public static string giris;
        public static string perid;

        SqlCommand komut;
        private void button1_Click(object sender, EventArgs e)
        {

          

            errorProvider1.Clear();

            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            komut = new SqlCommand("SELECT * FROM personelKayit WHERE kullaniciadi=@personel", baglanti);
            komut.Parameters.Add("@personel", SqlDbType.VarChar);

            komut.Parameters["@personel"].Value = txtkullaniciadi.Text;
            perid = txtkullaniciadi.Text;

            komut.Parameters["@personel"].Value = txtkullaniciadi.Text;
           
            perid = txtkullaniciadi.Text;

            data = komut.ExecuteReader();
            if (data.Read())
            {
                giris = data["Ad"].ToString();

               
                
                kullaniciAdi = txtkullaniciadi.Text;
                unvan = txtunvan.Text;
                
            

                baglanti.Close();
                baglanti.Open();
                komut = new SqlCommand("SELECT * FROM personelKayit WHERE kullaniciadi=@personel and sifre=@sifre", baglanti);
                komut.Parameters.Add("@personel", SqlDbType.VarChar);
                komut.Parameters["@personel"].Value = kullaniciAdi.ToString();
                komut.Parameters["@personel"].Value = txtkullaniciadi.Text;
                komut.Parameters.Add("@sifre", SqlDbType.NVarChar);
                komut.Parameters["@sifre"].Value = txtsifre.Text;
                perid = txtkullaniciadi.Text;
                data = komut.ExecuteReader();
                if (data.Read())
                {
                    Form b = new anaMenu();
                    b.Show();
                    this.Hide();
                    
                }
                else
                {
                    errorProvider1.SetError(txtsifre, "Sifre Hatalı");
                }
               
                
               
               
            }
            else
            {
                errorProvider1.SetError(txtkullaniciadi, "Kullanıcı adı hatalı");
            }

            



        }



        private void txtkullaniciadi_TextChanged(object sender, EventArgs e)
        {
            if (txtkullaniciadi.Text == "")
            {
                txtsifre.Clear();

                txtunvan.Clear();
            }
            try
            {
                baglanti.Close();
                baglanti.Open();
                SqlCommand k = new SqlCommand("select*from personelKayit where kullaniciadi=@ad", baglanti);
                k.Parameters.AddWithValue("@ad", txtkullaniciadi.Text);
                SqlDataReader oku = k.ExecuteReader();
                while (oku.Read())
                {

                    txtunvan.Text = oku["Gorev"].ToString();

                }
                oku.Close();
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public Boolean  sayac;
        private void button2_Click(object sender, EventArgs e)
        {

            if (sayac == true)
            {
                txtsifre.PasswordChar = '\0';
                sayac = false;
            }
            else
            {
                txtsifre.PasswordChar = '*';
                sayac = true;
            }

        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
