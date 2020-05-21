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
    public partial class Raporyedekparca : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
        public Raporyedekparca()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("tyedekParca", baglanti);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\yedekparcaRapor.rpt");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            birimler();
        }
        ReportDocument rpt = new ReportDocument();
        DataTable dt = new DataTable();
        public void birimler()
        {
            if(comboBox1.Text=="Parça Adı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("pyedekParca", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@ad", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);
                
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\yedekparcaRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if(comboBox1.Text=="Parça Türü")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("pyedekParca", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@tur", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);
               
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\yedekparcaRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Model")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("pyedekParca", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@model", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);
              
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\yedekparcaRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Miktar")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("pyedekParca", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@miktar", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);
              
                rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\yedekparcaRapor.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
        }

        private void Raporyedekparca_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
