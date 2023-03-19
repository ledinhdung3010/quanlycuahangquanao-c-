using Project_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_Main.Class;

namespace Sell_Clothes_Project
{
    public partial class formKho : Form
    {
        public formKho()
        {
            InitializeComponent();
        }

        string TenNV = "";

        public formKho(string tenNV)
        {
            InitializeComponent();
            this.TenNV = tenNV;
        }

        // Dùng là đối tượng chung cho các form con
        private Form currentFormChild;
        // Tạo phương thức hiển thị form con ở trong form cha
        private void OpenChildForm(Form ChildForm)
        {
            // Nếu currentFormChild đã khởi tạo thì đóng vào.
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            currentFormChild = ChildForm;

            ChildForm.TopLevel = false;
            ChildForm.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(ChildForm);
            panel_Body.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void btnInfoProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formBtnThongTinHangHoa());
        }

        private void btnInputOutputEdit_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formBtnNhapVaSuaHangHoa(TenNV));
        }

        private void btnThemLoaiSP_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formBtnThemLoaiSanPham());
        }

        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatHangHoa_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formBtnXuatHangHoa(TenNV));
        }
    }
}
