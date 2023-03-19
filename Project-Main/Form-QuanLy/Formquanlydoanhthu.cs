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
using TheArtOfDevHtmlRenderer.Adapters;

namespace Project_Main.Form_QuanLy
{
    public partial class Formquanlydoanhthu : Form
    {
        public Formquanlydoanhthu()
        {
            InitializeComponent();
        }
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(Functions.stringConnection);

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txTKDT_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btTinh_Click(object sender, EventArgs e)
        {
          
        }

        private void Formquanlydoanhthu_Load(object sender, EventArgs e)
        {

           
        }
    }
}
