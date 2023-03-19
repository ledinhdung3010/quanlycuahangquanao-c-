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

namespace Project_Main
{
    public partial class formBtnXuatHangHoa : Form
    {
        string TenNV = "";
        int hieu;

        public formBtnXuatHangHoa(string TenNV)
        {
            InitializeComponent();
            this.TenNV = TenNV;
        }

        private void formBtnXuatHangHoa_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();

            // Load bảng thông tin hàng hóa
            string query = "Select MaSP, TenSP, SoLuong From Product";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Functions.connection);
            DataTable data = new DataTable();
            Functions.BringDataIntoDGV(query, data, adapter, dgvThongTinHangHoa);

            // Load bảng lịch sử xuất hàng
            string query2 = "Select * From DeliverHistory";
            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, Functions.connection);
            DataTable data2 = new DataTable();
            Functions.BringDataIntoDGV(query2, data2, adapter2, dgvLichSuXuatHang);

            txtMaSP.ReadOnly = true;
            txtSoLuong.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtTenNV.Text = TenNV;
            txtTenNV.ReadOnly = true;
         

            Functions.DisConnectionDatabase();
        }

        private void dgvThongTinHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = dgvThongTinHangHoa.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dgvThongTinHangHoa.CurrentRow.Cells[1].Value.ToString();
            txtSoLuong.Text = dgvThongTinHangHoa.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();

            int slk, slx;
            try
            {
                slk = int.Parse(txtSoLuong.Text);
                slx = int.Parse(txtXuatSL.Text);

                if (txtXuatSL.Text != "")
                {
                    if (slk >= slx)
                    {
                        hieu = slk - slx;

                        // Cập nhật lại thông tin cho bảng thông tin hàng hóa
                        string sqlUpdate1 = "Update Product Set SoLuong = '" + hieu.ToString() + "' Where MaSP = '" + txtMaSP.Text + "'";
                        SqlCommand cmd = new SqlCommand(sqlUpdate1, Functions.connection);
                        cmd.ExecuteNonQuery();

                        string query = "Select MaSP, TenSP, SoLuong From Product";
                        Functions.BringDataIntoDGVNew(query, dgvThongTinHangHoa);

                        // Cập nhật lại thông tin cho bảng lịch sử xuất hàng hóa
                        string sqlInsert = "Insert Into DeliverHistory Values('" + txtMaSP.Text + "', N'" + txtTenSP.Text + "', '" + txtXuatSL.Text + "', '" + dtpNgayXuat.Value + "', N'" + txtTenNV.Text + "')";
                        SqlCommand cmd2 = new SqlCommand(sqlInsert, Functions.connection);
                        cmd2.ExecuteNonQuery();

                        string query2 = "Select * From DeliverHistory Order by Day(NgayXuat) Asc, Month(NgayXuat) Asc, YEAR(NgayXuat) Asc";
                        Functions.BringDataIntoDGVNew(query2, dgvLichSuXuatHang);

                        if (hieu == 0)
                        {
                            // Xóa mặt hàng có số lượng bằng 0
                            string sqlDelete = "Delete Product Where SoLuong = 0 And MaSP = '" + txtMaSP.Text + "'";
                            SqlCommand cmdDelete = new SqlCommand(sqlDelete, Functions.connection);
                            cmdDelete.ExecuteNonQuery();
                            MessageBox.Show("Sản phẩm " + txtTenSP.Text + " đã hết hàng và đã xóa đi trong kho!", "Thông báo");

                            // Cập nhật lại bảng
                            string query3 = "Select MaSP, TenSP, SoLuong From Product";
                            Functions.BringDataIntoDGVNew(query3, dgvThongTinHangHoa);
                        }

                        txtMaSP.Text = "";
                        txtTenSP.Text = "";
                        txtSoLuong.Text = "";
                        txtXuatSL.Text = "";

                        Functions.DisConnectionDatabase();
                    }
                    else
                    {
                        MessageBox.Show("Số lượng muốn xuất vượt quá số lượng hàng trong kho!", "Thông báo");
                        Functions.DisConnectionDatabase();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhâp số lượng muốn xuất!", "Thông báo");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xuất kho!", "Thông báo");
            }
        }
    }


      
}

