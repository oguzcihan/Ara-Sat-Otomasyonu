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
    public partial class markasec : Form,IUaracSec
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public markasec()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        public event aracsecildiHandle aracsecildi;

        public void list()
        {

            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from markaStok", baglanti);
                da = new SqlDataAdapter(com);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
                com.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void markasec_Load(object sender, EventArgs e)
        {
            list();
            comboBox1.SelectedIndex = 3;
            dataGridView1.Columns[0].HeaderText = "Marka ID";
            dataGridView1.Columns[1].HeaderText = "Marka Adı";
            dataGridView1.Columns[2].HeaderText = "Model";
            dataGridView1.Columns[3].HeaderText = "Araç Yıl";
            dataGridView1.Columns[4].HeaderText = "Beygir Gücü";
            dataGridView1.Columns[5].HeaderText = "Motor Hacmi";
            dataGridView1.Columns[6].HeaderText = "Araç Tipi";
            dataGridView1.Columns[7].HeaderText = "Motor Tip";
            dataGridView1.Columns[8].HeaderText = "Vites Tür";
            dataGridView1.Columns[9].HeaderText = "Yakıt Tür";
        }
        public string secilenmarka { get; set; }
        public string secilenmodel { get; set; }
        public string secilenyil { get; set; }
        public string secilenvites { get; set; }
        public string secilenyakit { get; set; }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            secilenmarka = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            secilenmodel = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            secilenyil = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            secilenvites = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            secilenyakit = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            aracsecildi?.Invoke();
            this.Hide();
        }
        public void birimler()
        {
            if (comboBox1.Text == "Marka Adı")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from markaStok where aracmarka like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Model")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from markaStok where aracmodel like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Araç Tipi")
            {
                list();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from markaStok where aractipi like '" +
                txtara.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }           
            else
            {
                list();
            }
        }

        private void txtara_TextChanged(object sender, EventArgs e)
        {
            birimler();
        }
    }
}
