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
    public partial class satisSil : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public satisSil()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        public void delete(int id)
        {
            try
            {
                baglanti.Open();
                SqlCommand ole = new SqlCommand("delete from aracSatis where satisId=@id", baglanti);
                ole.Parameters.AddWithValue("@id", id);
                ole.ExecuteNonQuery();
                ole.Dispose();
                baglanti.Close();
                MessageBox.Show("Satış Silindi", "Sİstem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
            {
                int kod = Convert.ToInt32(drow.Cells[0].Value);
                delete(kod);
            }
            listele();
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

        private void satisSil_Load(object sender, EventArgs e)
        {
            listele();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }
    }
}
