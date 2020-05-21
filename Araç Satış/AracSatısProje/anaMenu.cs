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
using System.IO;



namespace AracSatısProje
{
    public partial class anaMenu : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        SqlCommand komut;
        SqlDataReader data;

        public anaMenu()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form arac = new AracKayit();
            arac.ShowDialog();
        }

        public static void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form sat = new AracSatis();
            sat.ShowDialog();
        }

        private void araçGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form a = new aracguncelle();
            a.ShowDialog();
        }

        private void markaKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form b = new MarkaStok();
            b.ShowDialog();
        }

        private void araçServisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form servis = new servisKayit();
            servis.ShowDialog();
        }

        private void yedekParçaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form yedek = new YedekParca();
            yedek.ShowDialog();
        }

        private void kullanıcıDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anaMenu kapat = new anaMenu();
            kapat.Close();
            Giris frm1 = new Giris();
            frm1.Show();
            this.Hide();
        }

        private void anaMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //x tuşu kapatır.
                DialogResult g = MessageBox.Show("Uygulamayı kapatmak istiyor musunuz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (g == DialogResult.No)
                {
                    e.Cancel = true;
                    return;

                }
                Environment.Exit(0);
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form ser = new servisKayit();
            ser.ShowDialog();
        }

        private void yedekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Close();
                baglanti.Open();
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "(*.bak) | *.bak|(*.rar)|*.rar";
                save.FilterIndex = 0;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string sql = string.Format(@"BACKUP database AracSatıs to disk='{0}'", save.FileName);
                    SqlCommand cmd = new SqlCommand(sql, baglanti);
                    ;
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("Yedeklendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString()); }
        }


        private void hesapMakinesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");

        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti1;

            baglanti1 = new SqlConnection(@"Data Source = DESKTOP-4LNBJRV\game; Initial Catalog =master; Integrated Security = True");
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Backup File |*.bak";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string sql = "USE[MASTER];";
                    sql += "RESTORE DATABASE [AracSatıs] FROM  DISK ='" + open.FileName + "' WITH FILE = 1,  NOUNLOAD,  STATS = 5;";


                    SqlCommand command = new SqlCommand(sql, baglanti1);

                    baglanti1.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Database Recovered Successfully!");
                    baglanti1.Close();
                    baglanti1.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void müşteriKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form mus = new musteriKayit();
            mus.ShowDialog();
        }

        private void personelKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form per = new personelKayit();
            per.ShowDialog();
        }

        private void aracRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ac = new Raporarackayit();
            ac.ShowDialog();
        }

        private void kullanıcıismi()
        {
     
        }

        public static string yetki;
        private void anaMenu_Load(object sender, EventArgs e)
        {

            kullanıcıismi();

     
            Giris g = new Giris();
            lblad.Text = Giris.kullaniciAdi;
            lblunvan.Text = Giris.unvan;


            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            komut = new SqlCommand("SELECT * FROM personelKayit WHERE kullaniciadi=@personel", baglanti);
            komut.Parameters.Add("@personel", SqlDbType.VarChar);
            komut.Parameters["@personel"].Value = Giris.perid;
            data = komut.ExecuteReader();
            if (data.Read())
            {
                yetki = data["yetki"].ToString();

                if (yetki.Contains("1") == true)
                {
                    toolStripButton3.Visible = true;
                    araçSatışToolStripMenuItem.Visible = true;
                    if (yetki.Contains("2") == true)
                    {
                        toolStripButton1.Visible = true;
                        araçKayıtToolStripMenuItem.Visible = true;
                        if (yetki.Contains("3") == true)
                        {
                            araçGüncelleToolStripMenuItem.Visible = true;
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                        
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {

                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (yetki.Contains("3") == true)
                        {
                            araçGüncelleToolStripMenuItem.Visible = true;
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;
                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {

                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (yetki.Contains("2") == true)
                    {
                        toolStripButton1.Visible = true;
                        araçKayıtToolStripMenuItem.Visible = true;
                        if (yetki.Contains("3") == true)
                        {
                            araçGüncelleToolStripMenuItem.Visible = true;
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }

                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (yetki.Contains("3") == true)
                        {
                            araçGüncelleToolStripMenuItem.Visible = true;
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (yetki.Contains("4") == true)
                            {
                                müşteriKayıtToolStripMenuItem.Visible = true;
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                toolStripButton2.Visible = true;

                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (yetki.Contains("5") == true)
                                {
                                    personelKayıtToolStripMenuItem.Visible = true;
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {

                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                toolStripButton2.Visible = true;

                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (yetki.Contains("6") == true)
                                    {
                                        markaKayıtToolStripMenuItem.Visible = true;
                                        if (yetki.Contains("7") == true)
                                        {

                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (yetki.Contains("7") == true)
                                        {
                                            toolStripButton2.Visible = true;

                                            araçServisiToolStripMenuItem.Visible = true;
                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            if (yetki.Contains("8") == true)
                                            {
                                                yedekParçaToolStripMenuItem.Visible = true;
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (yetki.Contains("9") == true)
                                                {
                                                    ayarlarToolStripMenuItem.Visible = true;
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (yetki.Contains("0") == true)
                                                    {
                                                        satışSilToolStripMenuItem.Visible = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {

            }

        }


        private void satışSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form sil = new satisSil();
            sil.ShowDialog();
        }

        private void araçSatışToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form satis = new AracSatis();
            satis.ShowDialog();
        }

        private void araçKayıtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form arac = new AracKayit();
            arac.ShowDialog();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();


        }
        

        private void toolStripButton3_Click_2(object sender, EventArgs e)
        {
             Form arac = new AracSatis();
            arac.ShowDialog();
        }

        private void satışRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ac = new raporSatis();
            ac.ShowDialog();
        }

        private void servisRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ac = new raporServis();
            ac.ShowDialog();
        }

        private void yedekParçaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form ac = new Raporyedekparca();
            ac.ShowDialog();
        }

        private void fabrikaBİlgiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ac = new raporFabrika();
            ac.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (label4.Left > -340)
            {
                label4.Left -= 1;
            }
            else
            {
                label4.Left = 1220;
            }
        }

        private void silinenMüşteriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ac = new raporSilinen();
            ac.ShowDialog();
        }


        private void fabrikaKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ac = new fabrikaKayit();
            ac.ShowDialog();
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {


        }
    }
}
    


            
        
    

