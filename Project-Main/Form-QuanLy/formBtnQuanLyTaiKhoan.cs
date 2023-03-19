using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_Main.Class;

namespace Sell_Clothes_Project
{
    public partial class formBtnQuanLyTaiKhoan : Form
    {
        public formBtnQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        // Khai báo biến toàn cục
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        public static string GioiTinh = "", LoaiTK = "";

        private void formBtnQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            string queryAll = "Select * From Account";
            Functions.BringDataIntoDGV(queryAll, dt, adapter, dgvAccountInfo);
        }

        private void formBtnQuanLyTaiKhoan_Leave(object sender, EventArgs e)
        {
            Functions.connection.Close();
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTaiKhoan.Enabled = true;
            // Kiểm tra Giới tính
            if (radioNam.Checked == true)
                GioiTinh = "Nam";
            else
                GioiTinh = "Nữ";

            // Kiểm tra loại tài khoản
            if (radioNhanVien.Checked == true)
                LoaiTK = "Nhân viên";
            else
                LoaiTK = "Quản lý";

            // Thực hiện thao tác chèn dữ liệu vào csdl
            string Sql_Insert = "Insert Into Account Values (N'" + txtTaiKhoan.Text + "', N'" + txtMatKhau.Text + "', N'" + txtTenNguoiDung.Text + "', N'" + GioiTinh + "', '" + txtSoDT.Text + "', N'" + LoaiTK + "')";
            cmd = new SqlCommand(Sql_Insert, Functions.connection);
            cmd.ExecuteNonQuery();

            // Truy vấn và đưa dữ liệu lên bảng datagridview
            string query = "Select * From Account";
            Functions.BringDataIntoDGV(query, dt, adapter, dgvAccountInfo);
            cmd = null;
        }


        private void dgvAccountInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAccountInfo.Rows.Count == 0)
            {
                MessageBox.Show("Hiện không có dữ liệu trong bảng!", "Thông báo");
                return;
            }

            txtTaiKhoan.Enabled = false;
            txtMatKhau.Text = dgvAccountInfo.CurrentRow.Cells[0].Value.ToString();
            txtTaiKhoan.Text = dgvAccountInfo.CurrentRow.Cells[1].Value.ToString();
            txtTenNguoiDung.Text = dgvAccountInfo.CurrentRow.Cells[2].Value.ToString();

            // Kiểm tra radio về giới tính
            if (dgvAccountInfo.CurrentRow.Cells[3].Value.ToString() == "Nam")
                radioNam.Checked = true;
            else
                radioNu.Checked = true;

            txtSoDT.Text = dgvAccountInfo.CurrentRow.Cells[4].Value.ToString();

            // Kiểm tra radio về nhân viên
            if (dgvAccountInfo.CurrentRow.Cells[5].Value.ToString() == "Nhân viên")
                radioNhanVien.Checked = true;
            else
                radioQuanLy.Checked = true;

        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string DeleteRowDGV = "Delete From Account Where TaiKhoan = '" + txtTaiKhoan.Text + "'";
            cmd = new SqlCommand(DeleteRowDGV, Functions.connection);
            cmd.ExecuteNonQuery();

            string query = "Select * From Account";
            Functions.BringDataIntoDGV(query, dt, adapter, dgvAccountInfo);
            MessageBox.Show("Đã xóa một một tài khoản!", "Thông báo");
            cmd = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra không để dòng nào được bỏ trống
            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo");
                return;
            }
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo");
                return;
            }

            if (txtTenNguoiDung.Text == "")
            {
                MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo");
                return;
            }
            if (radioNam.Checked == false && radioNu.Checked == false)
            {
                MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo");
                return;
            }
            if (txtSoDT.Text == "")
            {
                MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo");
                return;
            }
            if (radioNhanVien.Checked == false && radioQuanLy.Checked == false)
            {
                MessageBox.Show("Vui lòng không bỏ trống thông tin!", "Thông báo");
                return;
            }

            // Gán lại giới tính
            if (radioNam.Checked == true)
                GioiTinh = "Nam";
            else
                GioiTinh = "Nữ";

            // Kiểm tra lại loại tài khoản
            if (radioNhanVien.Checked == true)
                LoaiTK = "Nhân viên";
            else
                LoaiTK = "Quản lý";

            txtTaiKhoan.Enabled = false;
            // Sửa và gán lại thông tin cho các hàng (Rows) đồng thời cập nhật lại trên Csdl
            string SqlUpdate = "Update Account Set MatKhau = N'" + txtMatKhau.Text + "', TenND = N'" + txtTenNguoiDung.Text + "', GioiTinh = N'" + GioiTinh + "', SDT = '" + txtSoDT.Text + "', LoaiTK = N'" + LoaiTK + "' Where TaiKhoan = '" + txtTaiKhoan.Text + "'";
            cmd = new SqlCommand(SqlUpdate, Functions.connection);
            cmd.ExecuteNonQuery();

            string query = "Select * From Account";
            Functions.BringDataIntoDGV(query, dt, adapter, dgvAccountInfo);
            cmd = null;
        }
    }
}
