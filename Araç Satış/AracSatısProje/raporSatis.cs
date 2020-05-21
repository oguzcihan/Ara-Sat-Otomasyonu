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
using CrystalDecisions.CrystalReports.Engine;

namespace AracSatısProje
{
    public partial class raporSatis : Form
    {
        SqlConnection baglanti = new SqlConnection();
        baglantiAdres con = new baglantiAdres();
        public raporSatis()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        ReportDocument rpt = new ReportDocument();
        DataTable dt = new DataTable();

        private void raporSatis_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        public void birimler()
        {
            if (comboBox1.Text == "Araç Şase")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracSatis where aracSase like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Kullanıcı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracSatis where kullanici like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }else if(comboBox1.Text=="Marka Adı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracSatis where aracMarka like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }else if (comboBox1.Text == "Model")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracSatis where aracModel like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }else if(comboBox1.Text=="Vites Tür")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracSatis where vitesTur like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }else if (comboBox1.Text == "Miktar")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracSatis where miktar like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand com = new SqlCommand("select*from aracSatis", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            dt.Clear();
            adapter.Fill(dt);
            rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\satisRapor.rpt");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            birimler();
        }
    }
}
