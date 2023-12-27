using DevExpress.XtraEditors;
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
    public partial class frmIn : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection conn = new SqlConnection(@"data source=DESKTOP-RH49N5V\VIET;initial catalog=HOTELS;integrated security=True");
       
        public frmIn()
        {
            InitializeComponent();
        }

        private void frmIn_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from tb_CongTy where MACTY='CNMB'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet2", dt);
            reportViewer1.LocalReport.ReportPath = @"C:\Users\GIGABYTE\source\repos\QUANLYTHUEPHONG\GUI\ReportCty.rdlc";
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}