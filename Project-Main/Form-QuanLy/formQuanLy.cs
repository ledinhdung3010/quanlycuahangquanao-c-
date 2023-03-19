using Project_Main.Form_QuanLy;
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
    public partial class formQuanLy : Form
    {
        public formQuanLy()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnQuanLyTK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formBtnQuanLyTaiKhoan());
        }

        private void btnQuanLyDoanhThu_Click(object sender, EventArgs e)
        {
            Formquanlydoanhthu a=new Formquanlydoanhthu();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }
    }
}
