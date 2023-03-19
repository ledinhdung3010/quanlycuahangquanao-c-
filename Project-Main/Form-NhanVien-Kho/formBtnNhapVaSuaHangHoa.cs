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
using System.IO;

namespace Project_Main
{
    public partial class formBtnNhapVaSuaHangHoa : Form
    {
        public formBtnNhapVaSuaHangHoa()
        {
            InitializeComponent();
        }

        string TenNV = "";

        public formBtnNhapVaSuaHangHoa(string TenNV)
        {
            InitializeComponent();
            this.TenNV = TenNV;
        }

        // Các đối tượng toàn cục
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();


        private void formBtnNhapVaSuaHangHoa_Load_1(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();

            // Load comboBox
            string query1 = "Select LoaiSP From ProductCategory Where LoaiSP != N'Rỗng'";
            Functions.BringDataIntoComboBox(query1, cbbLoaiSP, "LoaiSP", "LoaiSP");

            string query2 = "Select NhanHang From ProductCategory Where NhanHang != N'Rỗng'";
            Functions.BringDataIntoComboBox(query2, cbbNhanHang, "NhanHang", "NhanHang");

            string query3 = "Select XuatXu From ProductCategory Where XuatXu != N'Rỗng'";
            Functions.BringDataIntoComboBox(query3, cbbXuatXu, "XuatXu", "XuatXu");

            txtTenNV.Text = TenNV;
            txtTenNV.ReadOnly = true;
            btnSuaHang.Enabled = false;

            // Load DataGridView
            string query4 = "Select * From Product";
            Functions.BringDataIntoDGV(query4, dt, adapter, dgvThongTinSanPham);

            Functions.DisConnectionDatabase();
        }


        // Tạo phương thức thêm hàng hóa
        private void btnThemHang_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            try
            {
                string sqlInsert = "Insert Into Product Values ('" + txtMaSP.Text + "', N'" + txtTenSP.Text + "', N'" + cbbLoaiSP.Text + "', N'" + cbbNhanHang.Text + "', N'" + cbbXuatXu.Text + "', '" + txtSoLuong.Text + "', '" + txtDonGia.Text + "', '" + dtpNgayNhap.Value + "', N'" + txtTenNV.Text + "')";
                cmd = new SqlCommand(sqlInsert, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select * From Product";
                Functions.BringDataIntoDGV(query, dt, adapter, dgvThongTinSanPham);
                Functions.DisConnectionDatabase();

                txtMaSP.Text = "";
                txtTenSP.Text = "";
                cbbLoaiSP.Text = "";
                cbbNhanHang.Text = "";
                cbbXuatXu.Text = "";
                txtSoLuong.Text = "";
                txtDonGia.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng nhập hết tất cả các thông tin trước khi thêm sản phẩm", "Thông báo");
                Functions.DisConnectionDatabase();
            }
        }

        private void dgvThongTinSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSuaHang.Enabled = true;
            txtMaSP.Text = dgvThongTinSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dgvThongTinSanPham.CurrentRow.Cells[1].Value.ToString();
            cbbLoaiSP.Text = dgvThongTinSanPham.CurrentRow.Cells[2].Value.ToString();
            cbbNhanHang.Text = dgvThongTinSanPham.CurrentRow.Cells[3].Value.ToString();
            cbbXuatXu.Text = dgvThongTinSanPham.CurrentRow.Cells[4].Value.ToString();
            txtSoLuong.Text = dgvThongTinSanPham.CurrentRow.Cells[5].Value.ToString();
            txtDonGia.Text = dgvThongTinSanPham.CurrentRow.Cells[6].Value.ToString();
            dtpNgayNhap.Value = Convert.ToDateTime(dgvThongTinSanPham.CurrentRow.Cells[7].Value.ToString());
            txtTenNV.Text = dgvThongTinSanPham.CurrentRow.Cells[8].Value.ToString();
        }

        private void btnSuaHang_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            string sqlUpdate = "Update Product Set TenSP = N'" +txtTenSP.Text+ "', LoaiSP = N'" +cbbLoaiSP.Text+ "', NhanHang = N'" +cbbNhanHang.Text+ "', XuatXu = N'" +cbbXuatXu.Text+ "', SoLuong = '" +txtSoLuong.Text+ "', DonGia = '" +txtDonGia.Text+ "', NgayNhap = '" +dtpNgayNhap.Value+ "', TenND = '" +txtTenNV.Text+ "' Where MaSP = '" +txtMaSP.Text+ "'";
            cmd = new SqlCommand(sqlUpdate, Functions.connection);
            cmd.ExecuteNonQuery();

            string query = "Select * From Product";
            Functions.BringDataIntoDGV(query, dt, adapter, dgvThongTinSanPham);
            Functions.DisConnectionDatabase();
        }
    }
}
