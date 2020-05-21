using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace AracSatısProje
{
    public partial class fabrikaKayit : Form
    {
        public ComboBox ComboBox { get; set; }
        public fabrikaKayit(ComboBox comboBox = null)
        {
            InitializeComponent();
            if (comboBox != null)
                ComboBox = comboBox;
        }

        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        SqlCommand komut;
        SqlDataReader data;

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void textbox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void FabrikBilgi_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dataGridView2.Visible = false;

            kayitGetir();
        }


        private void kayitGetir()
        {
            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            string kayit = "SELECT * from fabrikaKayit";

            SqlCommand komut = new SqlCommand(kayit, baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            baglanti.Close();
        }
        public static string fabrikaid;
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(con.adres);
            baglanti.Open();
            try
            {

                fabrikaid = textBox1.Text;
                SqlCommand komut = new SqlCommand("SELECT * From fabrikaKayit WHERE fabrikaid = @fabrikaid", baglanti);
                komut.Parameters.Add("@fabrikaid", SqlDbType.NChar);
                komut.Parameters["@fabrikaid"].Value = fabrikaid.ToString();


                SqlDataReader data = komut.ExecuteReader();
                if (data.Read())
                {


                    fabrikaid = data["fabrikaid"].ToString();
                    if (fabrikaid != "")
                    {
                        fabrikaid = textBox1.Text;
                        güncelleekle();
                        baglanti.Close();

                    }

                }
                else
                {
                    DialogResult durum = MessageBox.Show("Veri seçilmedi yada bulunmadı yeni kayıt yapmak istermisiniz", "Kayıt Onayı", MessageBoxButtons.YesNo);
                    if (durum == DialogResult.Yes)
                    {
                        fabrikaid = textBox1.Text;
                        güncelleekle();
                        baglanti.Close();



                    }
                }
            }

            catch
            {

            }
        }

        public static Boolean kontrol;
        private void güncelleekle()
        {
            try
            {
                baglanti = new SqlConnection(con.adres);
                baglanti.Open();
                komut = new SqlCommand("SELECT * FROM fabrikaKayit WHERE fabrikaid=@fabrika and fabrikadi=@fabrikadi", baglanti);
                komut.Parameters.Add("@fabrika", SqlDbType.VarChar);
                komut.Parameters["@fabrika"].Value = textBox1.Text;
                komut.Parameters.Add("@fabrikadi", SqlDbType.VarChar);
                komut.Parameters["@fabrikadi"].Value = textBox2.Text;
                data = komut.ExecuteReader();
                if (data.Read())
                {
                    baglanti.Close();
                    baglanti.Open();
                    komut = new SqlCommand("Update fabrikaKayit set fabrikadi=@fabrikadi,adres=@adres,email=@email,tel=@tel where fabrikaid=@fabrika", baglanti);
                    komut.Parameters.Add("@fabrika", SqlDbType.VarChar);
                    komut.Parameters["@fabrika"].Value = textBox1.Text;
                    komut.Parameters.Add("@fabrikadi", SqlDbType.NChar);
                    komut.Parameters["@fabrikadi"].Value = textBox2.Text;
                    komut.Parameters.Add("@adres", SqlDbType.NVarChar);
                    komut.Parameters["@adres"].Value = textBox3.Text;
                    komut.Parameters.Add("@email", SqlDbType.NVarChar);
                    komut.Parameters["@email"].Value = textBox4.Text;
                    komut.Parameters.Add("@tel", SqlDbType.NVarChar);
                    komut.Parameters["@tel"].Value = maskedTextBox1.Text;



                    komut.ExecuteNonQuery();
                    MessageBox.Show(" Kayıt Güncellendi");
                   
                    textBox1.Enabled = false;
                    temizle();
                    kayitGetir();
                    baglanti.Close();
                }
                else
                {
                    if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != "")
                    {
                        baglanti.Close();
                        baglanti.Open();
                        komut = new SqlCommand("SELECT * FROM fabrikaKayit WHERE fabrikadi=@fabrikadi", baglanti);

                        komut.Parameters.Add("@fabrikadi", SqlDbType.VarChar);
                        komut.Parameters["@fabrikadi"].Value = textBox2.Text;
                        data = komut.ExecuteReader();
                        if (data.Read())
                        {
                            if (textBox2.Text == data["fabrikadi"].ToString())
                            {
                                MessageBox.Show("Bu adla bir fabrika var lütfen kontrol ediniz");
                                kontrol = false;

                            }


                        }
                        else
                        {
                            kontrol = true;
                        }
                        if (kontrol == true)
                        {
                            baglanti.Close();
                            baglanti.Open();


                            komut = new SqlCommand("INSERT INTO fabrikaKayit (fabrikadi,adres,email,tel) VALUES(@fabrikadi,@adres,@email,@tel)", baglanti);

                           
                            komut.Parameters.Add("@fabrikadi", SqlDbType.NChar);
                            komut.Parameters["@fabrikadi"].Value = textBox2.Text;
                            komut.Parameters.Add("@adres", SqlDbType.NVarChar);
                            komut.Parameters["@adres"].Value = textBox3.Text;
                            komut.Parameters.Add("@email", SqlDbType.NVarChar);
                            komut.Parameters["@email"].Value = textBox4.Text;
                            komut.Parameters.Add("@tel", SqlDbType.NVarChar);
                            komut.Parameters["@tel"].Value = maskedTextBox1.Text;

                            komut.ExecuteNonQuery();
                            MessageBox.Show("Kayıt Başarılı");

                            if (ComboBox != null)
                                ComboBox.Items.Add(textBox2.Text);

                            temizle();
                            kayitGetir();

                            baglanti.Close();
                        }



                    }
                    else
                    {
                        MessageBox.Show("Lütfen boş bırakmayınız");

                    }

                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
                baglanti.Close();
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["fabrikaid"].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["fabrikadi"].Value);
            textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["adres"].Value);
            textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["email"].Value);
            maskedTextBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["tel"].Value);
            textBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Fabrika İd boş olamaz");
                return;
            }

            DialogResult result = MessageBox.Show("Seçilen veriyi silmek istediğinize emin misiniz? İşlem geri alınamamaktadır!", "Sil", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {

                    baglanti.Open();


                    komut = new SqlCommand("Delete from fabrikaKayit where fabrikaid=@fabrikaid ", baglanti);

                    komut.Parameters.Add("@fabrikaid", SqlDbType.NChar);
                    komut.Parameters["@fabrikaid"].Value = textBox1.Text;

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Silme Başarılı");
                   
                    temizle();
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                    dataGridView1.Refresh();

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
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
        private void temizle()
        {

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "(*.csv)|*.csv|(*.txt)|*.txt";

            if (file.ShowDialog() == DialogResult.OK)
            {
                VeriCek(file.FileName);
            }


        }


        private void VeriCek(string filePath)
        {
            DataTable dt = new DataTable();
            string[] satirlar = System.IO.File.ReadAllLines(filePath);
            if (satirlar.Length > 0)
            {

                //ilk satır başlık satırımız
                string ilkSatir = satirlar[0];
                string[] basliklar = ilkSatir.Split(',');
                foreach (string baslik in basliklar)
                {
                    dt.Columns.Add(new DataColumn(baslik));
                }
                //Veriler için kodlarımız
                for (int i = 1; i < satirlar.Length; i++)
                {


                    string[] veriler = satirlar[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;

                    foreach (string veri in basliklar)
                    {

                        dr[veri] = veriler[columnIndex++];

                    }
                    dt.Rows.Add(dr);

                }
            }
            if (dt.Rows.Count > 0)
            {

                dataGridView2.DataSource = dt;

            }
            string kontrol = dataGridView2.Columns[0].HeaderText;
            string kontrol2 = dataGridView2.Columns[1].HeaderText;
            string kontrol3 = dataGridView2.Columns[2].HeaderText;
            string kontrol4 = dataGridView2.Columns[3].HeaderText;
            if (kontrol == "fabrikadi" && kontrol2 == "adres" && kontrol3 == "email" && kontrol4 == "tel")
            {
                SqlCommand cmd = new SqlCommand("SELECT fabrikadi,adres,email,tel FROM fabrikaKayit", baglanti);
                baglanti.Open();
                bool tabloVar = cmd.ExecuteScalar() != null;

                // Tablo yoksa oluştur.

                using (var bulkCopy = new SqlBulkCopy(baglanti.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "fabrikaKayit";
                    bulkCopy.WriteToServer(dt);
                    baglanti.Close();
                }
                baglanti.Close();
            }
            else if (kontrol == "fabrikaid")
            {
                MessageBox.Show("Fabrika İd'sini tablonuzdan siliniz");

            }
            else
            {
                MessageBox.Show("Hatalı veri kontrol ediniz başlık su şekilde olmalı fabrikadi,adres,email,tel");
            }






        }

        public int id;
        
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            baglanti.Open();

            SqlCommand sqlCmd = new SqlCommand(
                "Select fabrikadi,adres,email,tel from fabrikaKayit", baglanti);
            SqlDataReader reader = sqlCmd.ExecuteReader();
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "(*.csv)|*.csv |(*.txt)|*.txt |(*.xls)|*.xls ";
            save.FilterIndex = 0;
            if (save.ShowDialog() == DialogResult.OK)
            {
                string fileName = save.FileName;

                StreamWriter sw = new StreamWriter(fileName);
                object[] output = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                    output[i] = reader.GetName(i);

                sw.WriteLine(string.Join(",", output));

                while (reader.Read())
                {
                    reader.GetValues(output);
                    sw.WriteLine(string.Join(",", output));
                }

                sw.Close();
                reader.Close();
                baglanti.Close();
            }
            baglanti.Close();

        }
    }
}
