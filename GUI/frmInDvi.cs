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
using Microsoft.Reporting.WinForms;

namespace GUI
{
    public partial class frmInDvi : Form
    {
        SqlConnection conn = new SqlConnection(@"data source=DESKTOP-RH49N5V\VIET;initial catalog=HOTELS;integrated security=True");
        public frmInDvi()
        {
            InitializeComponent();
        }

        private void frmInDvi_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from tb_DonVi", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.ReportPath = @"C:\Users\GIGABYTE\source\repos\QUANLYTHUEPHONG\GUI\ReportDonVi.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
