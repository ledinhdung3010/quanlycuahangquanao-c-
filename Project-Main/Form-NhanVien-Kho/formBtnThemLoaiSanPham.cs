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

namespace Project_Main
{
    public partial class formBtnThemLoaiSanPham : Form
    {
        public formBtnThemLoaiSanPham()
        {
            InitializeComponent();
        }


        // Tạo phương thức khi load form lên sẽ hiển thị thông tin của các danh mục lên các dgv
        private void formBtnThemLoaiSanPham_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();

            // Loại sản phẩm
            string query1 = "Select LoaiSP From ProductCategory Where LoaiSP != N'Rỗng'";
            SqlDataAdapter adapter1 = new SqlDataAdapter(query1, Functions.connection);
            DataTable dataTable1 = new DataTable();
            Functions.BringDataIntoDGV(query1, dataTable1, adapter1,dgvLoaiSP);


            // Nhãn hàng
            string query2 = "Select NhanHang From ProductCategory Where NhanHang != N'Rỗng'";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, Functions.connection);
            DataTable dataTable2 = new DataTable();
            Functions.BringDataIntoDGV(query2, dataTable2, adapter2, dgvNhanHang);


            // Xuất xứ
            string query3 = "Select XuatXu From ProductCategory Where XuatXu != N'Rỗng'";
            SqlDataAdapter adapter3 = new SqlDataAdapter(query2, Functions.connection);
            DataTable dataTable3 = new DataTable();
            Functions.BringDataIntoDGV(query3, dataTable3, adapter3, dgvXuatXu);

            Functions.DisConnectionDatabase();
        }


        // Tạo phương thức nhập nội dung vào txtLoaiSP, khi ấn vào nút btnThemLoaiSP thì sẽ thêm loại sản phẩm vào SQL và dgv
        private void btnThemLoaiSP_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            if (txtLoaiSP.Text != "")
            {
                string sqlInsert = "Insert Into ProductCategory (LoaiSP) Values (N'" + txtLoaiSP.Text + "')";
                SqlCommand cmd = new SqlCommand(sqlInsert, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select LoaiSP From ProductCategory Where LoaiSP != N'Rỗng'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Functions.BringDataIntoDGV(query, dt, adapter, dgvLoaiSP);
                txtLoaiSP.Text = "";
                Functions.DisConnectionDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập loại sản phẩm!","Thông báo");
                Functions.DisConnectionDatabase();
                return;
            }
        }


        // Tạo phương thức khi ấn vào một dòng trong dgv sẽ hiển thị nội dung của dòng đó lên txtLoaiSP
        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLoaiSP.Text = dgvLoaiSP.CurrentRow.Cells[0].Value.ToString();
        }


        // Tạo phương thức khi ấn vào nút xóa thì sẽ xóa dòng vừa chọn trong dgvLoaiSP
        private void btnXoaLoaiSP_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            if (txtLoaiSP.Text != "")
            {
                string sqlDelete = "Delete ProductCategory Where LoaiSP = N'" + txtLoaiSP.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlDelete, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select LoaiSP From ProductCategory Where LoaiSP != N'Rỗng'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Functions.BringDataIntoDGV(query, dt, adapter, dgvLoaiSP);
                txtLoaiSP.Text = "";
                MessageBox.Show("Xóa thành công!", "Thông báo");
                Functions.DisConnectionDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!", "Thông báo");
                Functions.DisConnectionDatabase();
                return;
            }
        }


        // Tạo phương thức nhập nội dung vào txtNhanHang, khi ấn vào nút btnThemNhanHang thì sẽ thêm Nhãn hàng vào SQL và dgv
        private void btnThemNhanHang_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            if (txtNhanHang.Text != "")
            {
                string sqlInsert = "Insert Into ProductCategory (NhanHang) Values (N'" +txtNhanHang.Text+ "')";
                SqlCommand cmd = new SqlCommand(sqlInsert, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select NhanHang From ProductCategory Where NhanHang != N'Rỗng'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Functions.BringDataIntoDGV(query, dt, adapter, dgvNhanHang);
                txtNhanHang.Text = "";
                Functions.DisConnectionDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập nhãn hàng!", "Thông báo");
                Functions.DisConnectionDatabase();
                return;
            }
        }


        // Tạo phương thức khi ấn vào một dòng trong dgv sẽ hiển thị nội dung của dòng đó lên txtNhanHang
        private void dgvNhanHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNhanHang.Text = dgvNhanHang.CurrentRow.Cells[0].Value.ToString();
        }


        // Tạo phương thức khi ấn vào nút xóa thì sẽ xóa dòng vừa chọn trong dgvNhanHang
        private void btnXoaNhanHang_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            if (txtNhanHang.Text != "")
            {
                string sqlDelete = "Delete ProductCategory Where NhanHang = N'" + txtNhanHang.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlDelete, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select NhanHang From ProductCategory Where NhanHang != N'Rỗng'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Functions.BringDataIntoDGV(query, dt, adapter, dgvNhanHang);
                txtNhanHang.Text = "";
                MessageBox.Show("Xóa thành công!", "Thông báo");
                Functions.DisConnectionDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!", "Thông báo");
                Functions.DisConnectionDatabase();
                return;
            }
        }


        // Tạo phương thức nhập nội dung vào txtNhanHang, khi ấn vào nút btnThemXuatXu thì sẽ thêm xuất xứ vào SQL và dgv
        private void btnThemXuatXu_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            if (txtXuatXu.Text != "")
            {
                string sqlInsert = "Insert Into ProductCategory (XuatXu) Values (N'" + txtXuatXu.Text + "')";
                SqlCommand cmd = new SqlCommand(sqlInsert, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select XuatXu From ProductCategory Where XuatXu != N'Rỗng'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Functions.BringDataIntoDGV(query, dt, adapter, dgvXuatXu);
                txtXuatXu.Text = "";
                Functions.DisConnectionDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập nơi xuất xứ!", "Thông báo");
                Functions.DisConnectionDatabase();
                return;
            }
        }


        // Tạo phương thức khi ấn vào một dòng trong dgv sẽ hiển thị nội dung của dòng đó lên txtXuatXu
        private void dgvXuatXu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtXuatXu.Text = dgvXuatXu.CurrentRow.Cells[0].Value.ToString();
        }


        // Tạo phương thức khi ấn vào nút xóa thì sẽ xóa dòng vừa chọn trong dgvXuatXu
        private void btnXoaXuatXu_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            if (txtXuatXu.Text != "")
            {
                string sqlDelete = "Delete ProductCategory Where XuatXu = N'" + txtXuatXu.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlDelete, Functions.connection);
                cmd.ExecuteNonQuery();

                string query = "Select XuatXu From ProductCategory Where XuatXu != N'Rỗng'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Functions.BringDataIntoDGV(query, dt, adapter, dgvXuatXu);
                txtXuatXu.Text = "";
                MessageBox.Show("Xóa thành công!", "Thông báo");
                Functions.DisConnectionDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!", "Thông báo");
                Functions.DisConnectionDatabase();
                return;
            }
        }
    }
}
