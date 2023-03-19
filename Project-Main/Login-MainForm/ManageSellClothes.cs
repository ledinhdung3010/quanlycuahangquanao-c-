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
using Project_Main.Class;
using Project_Main.form_thungan;

namespace Sell_Clothes_Project
{
    public partial class FManageSellClothes : Form
    {
        string TK = "", MK = "", TenND = "", GioiTinh = "", SDT = "", LoaiTk = "";

        public FManageSellClothes(string TK, string MK, string TenND, string GioiTinh, string SDT, string LoaiTk)
        {
            InitializeComponent();
            this.TK = TK;
            this.MK = MK;
            this.TenND = TenND;
            this.GioiTinh = GioiTinh;
            this.SDT = SDT;
            this.LoaiTk = LoaiTk;
        }


        private void btnSlideOut_Click(object sender, EventArgs e)
        {
            LogoBrand.Visible = true;
            btnSlideOut.Visible = false;
            btnSlideIn.Visible = true;
            Line1.Visible = true;
            Line2.Visible = false;
            SlideBar.Visible = false;
            SlideBar.Width = 215;
            TransitionSlideBar.ShowSync(SlideBar);
        }

        private void btnSlideIn_Click(object sender, EventArgs e)
        {
            LogoBrand.Visible = true;
            SlideBar.Visible = false;
            btnSlideIn.Visible = false;
            btnSlideOut.Visible = true;
            Line1.Visible = false;
            Line2.Visible = true;
            SlideBar.Width = 99;
            TransitionSlideBar.ShowSync(SlideBar);
        }

        private void FManageSellClothes_Load(object sender, EventArgs e)
        {
            if(LoaiTk == "Nhân viên")
            {
                btnThanhToan.Location = btnKho.Location;
                btnKho.Location = btnQuanLy.Location;
                btnQuanLy.Visible = false;
            }
            else
            {
                btnQuanLy.Visible = true;
                btnQuanLy.Location = new Point(32, 161);
                btnKho.Location = new Point(32, 249);
                btnThanhToan.Location = new Point(32, 337);
            }
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

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formQuanLy());
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formthungan(TK, MK, TenND, GioiTinh, SDT, LoaiTk));
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formKho(TenND));
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
