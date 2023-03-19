using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Project_Main.Class;

namespace Sell_Clothes_Project
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();

            string tk = txtTaiKhoan.Text;
            string mk = txtMatKhau.Text;
            string login = "Select * From Account Where TaiKhoan = '" +tk+ "' And MatKhau = '" +mk+ "'";
            SqlDataAdapter da = new SqlDataAdapter(login, Functions.connection);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("Đăng nhập thàng công!", "Thông báo");
                this.Hide();
                FManageSellClothes f = new FManageSellClothes(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString());
                f.ShowDialog();
                this.Show();
                Functions.DisConnectionDatabase();
                da = null;
                dt.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!","Thông báo");
                txtTaiKhoan.Text = "";
                txtMatKhau.Text = "";
                txtTaiKhoan.Focus();
                Functions.DisConnectionDatabase();
                da = null;
                dt.Rows.Clear();
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Functions.DisConnectionDatabase();
            Application.Exit();
        }

        private void formLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát ra khỏi ứng dụng ?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        // Tạo phương thức đổi màu khi bật công tắc đổi màu
        private void switchChangeColor_CheckedChanged(object sender, EventArgs e)
        {
            if(switchChangeColor.Checked == true) 
            {
                this.BackColor = Color.FromArgb(255, 14, 17, 51);
                txtTaiKhoan.FillColor = Color.FromArgb(255, 14, 17, 51);
                txtMatKhau.FillColor = Color.FromArgb(255, 14, 17, 51);
                lbChangeColor.Text = "Đổi nền sáng";
            }
            else
            {
                this.BackColor = Color.White;
                txtTaiKhoan.FillColor = Color.White;
                txtMatKhau.FillColor = Color.White;
                lbChangeColor.Text = "Đổi nền tối";
            }
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            Functions.DisConnectionDatabase();
        }

        private void TopTab_Click(object sender, EventArgs e)
        {

        }
    }
}
