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

    public partial class MarkaStok : Form
    {
        private SqlConnection baglanti;

        public ComboBox ComboBox { get; set; }
        public MarkaStok(ComboBox comboBox = null)

        {
            InitializeComponent();
            if (comboBox != null)
                ComboBox = comboBox;

            baglantiAdres con = new baglantiAdres();
            baglanti = new SqlConnection(con.adres);
            kayitgetir();

        }

        private void MarkaStok_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Random rnd = new Random();
            int sayi = rnd.Next(12345, 99999);
            textBox1.Text = sayi.ToString();

            DateTime mydate = System.DateTime.Now;
            string year = mydate.Year.ToString();
            for (int i = 1990; i < mydate.Year; i++)
            {
                comboBox6.Items.Add(i);
            }

            comboBox6.SelectedIndex = 0;
            vitestur.SelectedIndex = 0;
            yakittur.SelectedIndex = 0;

            dataGridView1.Columns[0].HeaderText = "Marka İd";
            dataGridView1.Columns[1].HeaderText = "Araç Marka";
            dataGridView1.Columns[2].HeaderText = "Araç Model";
            dataGridView1.Columns[3].HeaderText = "Araç Yılı";
            dataGridView1.Columns[4].HeaderText = "Beygir Gücü";
            dataGridView1.Columns[5].HeaderText = "Motor Hacmi";
            dataGridView1.Columns[6].HeaderText = "Araç Tipi";
            dataGridView1.Columns[7].HeaderText = "Motor Tipi";
            dataGridView1.Columns[8].HeaderText = "Vites";
            dataGridView1.Columns[9].HeaderText = "Yakıt";



        }


        private void insertValues(int markaid, String aracmarka, String aracmodel, int aracyili, String beygirgucu, String motorhacmi, String aractipi, String motortipi, String vites, String yakit) //inset komutu
        {
            DialogResult r;
            r = MessageBox.Show("Kayıt etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {

                try
                {
                    baglanti.Open(); // Açıl susam açıl


                    SqlCommand komut1 = new SqlCommand("SELECT * FROM markaStok WHERE markaid=@markaid", baglanti);
                    komut1.Parameters.Add("@markaid", SqlDbType.Int);
                    komut1.Parameters["@markaid"].Value = textBox1.Text;
                    SqlDataReader data = komut1.ExecuteReader();
                    if (data.Read())
                    {
                        if (data["markaid"].ToString() == textBox1.Text)
                        {
                            MessageBox.Show("Bu fabrika var");

                            baglanti.Close();

                        }
<<<<<<< HEAD
                       
=======
>>>>>>> 26d4034caafa4f0bfa4e321726ed4332eeb8296f

                    }
                    else
                    {
                        baglanti.Close();
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("INSERT INTO dbo.markaStok(markaid,aracmarka,aracmodel,aracyili,beygirgucu,motorhacmi,aractipi,motortipi,vites,yakit) values(@markaid,@aracmarka,@aracmodel,@aracyili,@beygirgucu,@motorhacmi,@aractipi,@motortipi,@vites,@yakit)", baglanti);
                        komut.Parameters.AddWithValue("@markaid", markaid);
                        komut.Parameters.AddWithValue("@aracmarka", aracmarka);
                        komut.Parameters.AddWithValue("@aracmodel", aracmodel);
                        komut.Parameters.AddWithValue("@aracyili", aracyili);
                        komut.Parameters.AddWithValue("@beygirgucu", beygirgucu);
                        komut.Parameters.AddWithValue("@motorhacmi", motorhacmi);
                        komut.Parameters.AddWithValue("@aractipi", aractipi);
                        komut.Parameters.AddWithValue("@motortipi", motortipi);
                        komut.Parameters.AddWithValue("@vites", vites);
                        komut.Parameters.AddWithValue("@yakit", yakit);
                        komut.ExecuteNonQuery();
                        komut.Dispose();

                        if (ComboBox != null)
                            ComboBox.Items.Add(textBox2.Text);

                        MessageBox.Show("Kaydedildi");
                        baglanti.Close(); // Kapan susam kapan
                    }

                }
                catch (Exception hata) { MessageBox.Show(hata.Message.ToString()); }
            }

        }


        private void updateValues(int markaid, String aracmarka, String aracmodel, int aracyili, String beygirgucu, String motorhacmi, String aractipi, String motortipi, String vites, String yakit) //update komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("UPDATE dbo.markaStok SET markaid=@markaid,aracmarka=@aracmarka,aracmodel=@aracmodel,aracyili=@aracyili,beygirgucu=@beygirgucu,motorhacmi=@motorhacmi,aractipi=@aractipi,motortipi=@motortipi,vites=@vites,yakit=@yakit where markaid=@markaid", baglanti);

            komut.Parameters.AddWithValue("@markaid", markaid);
            komut.Parameters.AddWithValue("@aracmarka", aracmarka);
            komut.Parameters.AddWithValue("@aracmodel", aracmodel);
            komut.Parameters.AddWithValue("@aracyili", aracyili);
            komut.Parameters.AddWithValue("@beygirgucu", beygirgucu);
            komut.Parameters.AddWithValue("@motorhacmi", motorhacmi);
            komut.Parameters.AddWithValue("@aractipi", aractipi);
            komut.Parameters.AddWithValue("@motortipi", motortipi);
            komut.Parameters.AddWithValue("@vites", vites);
            komut.Parameters.AddWithValue("@yakit", yakit);

            komut.ExecuteNonQuery();
            komut.Dispose();

            baglanti.Close(); // Kapan susam kapan
        }


        private void deleteValues(int markaid) //delete komutu
        {
            baglanti.Open(); // Açıl susam açıl

            SqlCommand komut = new SqlCommand("DELETE from dbo.markaStok where markaid=@markaid", baglanti);
            komut.Parameters.AddWithValue("@markaid", markaid);

            komut.ExecuteNonQuery();
            komut.Dispose();
            clear();
            baglanti.Close(); // Kapan susam kapan
        }


        private void kayitgetir()  // dataGrid kayıt yerleştirme ilk baslangic
        {
            baglanti.Close();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *From dbo.markastok", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "markastok");
            dataGridView1.DataSource = ds.Tables["markastok"];
            baglanti.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)

        {
            deleteValues(Convert.ToInt32(textBox1.Text));
            kayitgetir();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            updateValues(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, Convert.ToInt32(comboBox6.Text), textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, vitestur.Text, yakittur.Text);
            kayitgetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "" && textBox8.Text == "") { MessageBox.Show("Tüm alanlar dolu olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (comboBox6.SelectedIndex == 0) { MessageBox.Show("Geçerli araç yılı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (yakittur.SelectedIndex == 0) { MessageBox.Show("Geçerli yakıt türü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (vitestur.SelectedIndex == 0) { MessageBox.Show("Geçerli vites türü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                insertValues(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, Convert.ToInt32(comboBox6.Text), textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, vitestur.Text, yakittur.Text);

                clear();
            }
            kayitgetir();

            Random rnd = new Random();
            int sayi = rnd.Next(12345, 99999);
            textBox1.Text = sayi.ToString();

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["markaid"].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["aracmarka"].Value);
            textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["aracmodel"].Value);
            comboBox6.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["aracyili"].Value);
            textBox5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["beygirgucu"].Value);
            textBox6.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["motorhacmi"].Value);
            textBox7.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["aractipi"].Value);
            textBox8.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["motortipi"].Value);
            vitestur.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["vites"].Value);
            yakittur.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["yakit"].Value);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteValues(Convert.ToInt32(textBox1.Text));
            kayitgetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && comboBox6.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "" && textBox8.Text == "" && vitestur.Text == "" && yakittur.Text == "") { MessageBox.Show("Tüm alanlar dolu olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (comboBox6.SelectedIndex == 0) { MessageBox.Show("Geçerli araç yılı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (yakittur.SelectedIndex == 0) { MessageBox.Show("Geçerli yakıt türü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (vitestur.SelectedIndex == 0) { MessageBox.Show("Geçerli vites türü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                updateValues(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, Convert.ToInt32(comboBox6.Text), textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, vitestur.Text, yakittur.Text);

                clear();
            }
            kayitgetir();
        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            comboBox6.SelectedIndex = 0;
            yakittur.SelectedIndex = 0;
            vitestur.SelectedIndex = 0;
        }


    }
}
