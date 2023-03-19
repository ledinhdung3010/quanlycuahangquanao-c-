using Microsoft.Reporting.WinForms;
using Project_Main.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Main.form_thungan
{
    public partial class Formprint : Form
    {
        public Formprint()
        {
            InitializeComponent();
        }

        private void Formprint_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            DataTable dt= new DataTable();  
            string str = "select * from billdetail where mahd='" + formthungan.v + "'";
            SqlDataAdapter df= new SqlDataAdapter(str,Functions.connection);
            df.Fill(dt);
            reportViewer1.LocalReport.DataSources.Clear(); ;
            reportViewer1.LocalReport.ReportPath = @"C:\Users\admin\Documents\Zalo Received Files\Project-Main\Project-Main\Report1.rdlc";
            ReportDataSource dts= new ReportDataSource("Billdetail",dt);
            reportViewer1.LocalReport.DataSources.Add(dts);
            reportViewer1.RefreshReport();
        }
    }
}
