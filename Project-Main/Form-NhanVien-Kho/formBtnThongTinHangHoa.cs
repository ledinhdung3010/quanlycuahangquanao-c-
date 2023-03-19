using Project_Main.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sell_Clothes_Project
{
    public partial class formBtnThongTinHangHoa : Form
    {
        public formBtnThongTinHangHoa()
        {
            InitializeComponent();
        }


        // Khởi tạo phương thức khi mới mở form lên thì đã sẵn sàng cập nhật dữ liệu lên DataGridView
        private void formBtnThongTinHangHoa_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();

            // Load dữ liệu lên 3 comboBox
            string query1 = "Select LoaiSP From ProductCategory Where LoaiSP != N'Rỗng'";
            Functions.BringDataIntoComboBox(query1, cbbLoaiSP, "LoaiSP", "LoaiSP");
            
            string query2 = "Select NhanHang From ProductCategory Where NhanHang != N'Rỗng'";
            Functions.BringDataIntoComboBox(query2, cbbNhanHang, "NhanHang", "NhanHang");

            string query3 = "Select XuatXu From ProductCategory Where XuatXu != N'Rỗng'";
            Functions.BringDataIntoComboBox(query3, cbbXuatXu, "XuatXu", "XuatXu");

            // Load dữ liệu lên dgv
            string query4 = "Select MaSP, TenSP, LoaiSP, NhanHang, XuatXu, DonGia From Product";
            Functions.BringDataIntoDGVNew(query4, dgvThongTinSanPham);
        }


        // Tạo phương thức khi chữ thay đổi thì hệ thống sẽ kiểm tra tìm kiếm gần đúng trong csdl và trả lên bảng dgv
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "Select MaSP, TenSP, LoaiSP, NhanHang, XuatXu, DonGia From Product Where TenSP Like N'%" + txtSearch.Text.Trim()+ "%'";
            Functions.BringDataIntoDGVNew(query, dgvThongTinSanPham);
            cbbLoaiSP.StartIndex = -1;
            cbbNhanHang.StartIndex = -1;
            cbbXuatXu.StartIndex = -1;
        }


        private void cbbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "Select MaSP, TenSP, LoaiSP, NhanHang, XuatXu, DonGia From Product Where LoaiSP Like N'%" + cbbLoaiSP.Text.Trim() + "%' And NhanHang Like N'%" +cbbNhanHang.Text+ "%' And XuatXu Like N'%" +cbbXuatXu.Text+ "%'";
            Functions.BringDataIntoDGVNew(query, dgvThongTinSanPham);
        }


        private void cbbNhanHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "Select MaSP, TenSP, LoaiSP, NhanHang, XuatXu, DonGia From Product Where LoaiSP Like N'%" + cbbLoaiSP.Text.Trim() + "%' And NhanHang Like N'%" + cbbNhanHang.Text + "%' And XuatXu Like N'%" + cbbXuatXu.Text + "%'";
            Functions.BringDataIntoDGVNew(query, dgvThongTinSanPham);
        }


        private void cbbXuatXu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "Select MaSP, TenSP, LoaiSP, NhanHang, XuatXu, DonGia From Product Where LoaiSP Like N'%" + cbbLoaiSP.Text.Trim() + "%' And NhanHang Like N'%" + cbbNhanHang.Text + "%' And XuatXu Like N'%" + cbbXuatXu.Text + "%'";
            Functions.BringDataIntoDGVNew(query, dgvThongTinSanPham);
        }


        // Tạo phương thức khi ấn làm mới thì sẽ về lại trạng thái như lúc bật form
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cbbLoaiSP.StartIndex = -1;
            cbbNhanHang.StartIndex = -1;
            cbbXuatXu.StartIndex = -1;
            txtSearch.Text = "";
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
