using CrystalDecisions.CrystalReports.Engine;
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
    public partial class raporFabrika : Form
    {
        SqlConnection baglanti;
        baglantiAdres con = new baglantiAdres();
       
        public raporFabrika()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        private void fabrikabilgi_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("alfabrikaKayit", baglanti);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\fabrikaRapor.rpt");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            baglanti.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("secilenFabrika", baglanti);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ad", textBox1.Text);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"D:\vtys-proje\AracSatısProje\Report\fabrikaRapor.rpt");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            baglanti.Close();
        }
       
    }
}
