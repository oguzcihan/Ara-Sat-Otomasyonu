using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

namespace AracSatısProje
{
    public partial class Raporarackayit : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public Raporarackayit()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        ReportDocument rpt = new ReportDocument();
        DataTable dt = new DataTable();
        
        private void Raporarackayit_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
        }
        public void birimler()
        {
            if (comboBox1.Text == "Araç Şase")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracKayit where AracSase like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\aracRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }else if (comboBox1.Text == "Kullanıcı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracKayit where kullanici like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\aracRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if(comboBox1.Text=="Araç Durum")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracKayit where durum like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\aracRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if(comboBox1.Text=="Marka Adı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select*from aracKayit where markaAdi like '" +
               txtara.Text + "%'", baglanti);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\aracRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand com = new SqlCommand("select*from aracKayit", baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            dt.Clear();
            adapter.Fill(dt);
            rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\aracRapor.rpt");
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
