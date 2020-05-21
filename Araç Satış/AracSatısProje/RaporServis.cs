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
    public partial class raporServis : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public raporServis()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        ReportDocument rpt = new ReportDocument();
        DataTable dt = new DataTable();
        private void raporServis_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        public void birimler()
        {
            if (comboBox1.Text == "Araç Şase")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from servisKayit where aracsase like '" +txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\servisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Parça Adı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from servisKayit where parcaadi like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\servisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Araç Marka")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from servisKayit where aracmarka like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\servisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Kasko")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from servisKayit where kasko like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\servisRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            birimler();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand com = new SqlCommand("select*from servisKayit", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            dt.Clear();
            adapter.Fill(dt);
            rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\servisRapor.rpt");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            baglanti.Close();
        }
    }
}
