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

namespace Project_Main.form_thungan
{
    public partial class formthungan : Form
    {
        
        string TK = "", MK = "", TenND = "", GioiTinh = "", SDT = "", LoaiTk = "";
        DataTable dt = new DataTable();
        private void btnReset_Click(object sender, EventArgs e)
        {

            txtMaHoaDon.Text = "";
            cbbMaSP.Text = "";
            txtTenKhachHang.Text = "";
            
            cbbMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuongMua.Text = "";
            txtDonGia.Text = "";
            txtTongTien.Text = "";
            txtThanhTien.Text = "";
        }

        private void formthungan_Load(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            string queryComboBox = "Select tensp From Product";
            Functions.BringDataIntoComboBox(queryComboBox, txtTenSP, "tensp", "tensp");
            
            txtTaiKhoan.Text = TK;
            txtTenNV.Text = TenND;
            txtTaiKhoan.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            cbbMaSP.ReadOnly = true;
            txtTenSP.Text = "";
            txtDonGia.ReadOnly = true;
            txtDonGia.Text = "";
            txtTongTien.ReadOnly = true;
            txtThanhTien.ReadOnly = true;

            btnThemSanPham.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = false;
            txtThanhTien.Text = "0";



            string queryDGV = "Select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', SoLuong as N'Số lượng mua', DonGia as N'Đơn giá', TongTien as N'Tổng tiền' From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            Functions.BringDataIntoDGV(queryDGV, dt, adapter, dgvMuaSanPham);
        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            Functions.ConnectionDataBase();
            string str = "delete billdetail where mahd='" + txtMaHoaDon.Text + "'";
            SqlCommand command2 = new SqlCommand(str, Functions.connection);
            command2.ExecuteNonQuery();
            string o = "delete bill where mahd='" + txtMaHoaDon.Text + "'";
            SqlCommand command4 = new SqlCommand(o, Functions.connection);
            command4.ExecuteNonQuery();
            string str1 = "delete billdetail where mahd='" + txtMaHoaDon.Text + "'";
            SqlCommand command3 = new SqlCommand(str1, Functions.connection);
            command3.ExecuteNonQuery();
            txtMaHoaDon.Text = "";
            cbbMaSP.Text = "";
            txtTenKhachHang.Text = "";

            cbbMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuongMua.Text = "";
            txtDonGia.Text = "";
            txtTongTien.Text = "";
            txtThanhTien.Text = "";
            string queryDGV = "Select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', SoLuong as N'Số lượng mua', DonGia as N'Đơn giá', TongTien as N'Tổng tiền' From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            Functions.BringDataIntoDGV(queryDGV, dt, adapter, dgvMuaSanPham);


        }
        public static string v;

        private void txtTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string str = "select MaSP from product where tensp='" + txtTenSP.Text + "'";
            cbbMaSP.Text = Functions.GetFieldValues(str);
            string s = "select (DonGia+DonGia*0.5) from product where tensp='" + txtTenSP.Text + "' and masp='" + cbbMaSP.Text + "'";
            txtDonGia.Text = Functions.GetFieldValues(s);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string str = "update  billdetail set soluong='" + txtSoLuongMua.Text + "',tongtien='" + txtTongTien.Text + "' where mahd='" + txtMaHoaDon.Text + "'and masp='" + cbbMaSP.Text + "'";
            SqlCommand cmd = new SqlCommand(str, Functions.connection);
            cmd.ExecuteNonQuery();
            string query = "Select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', SoLuong as N'Số lượng mua', DonGia as N'Đơn giá', TongTien as N'Tổng tiền' From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            SqlDataAdapter adapter = new SqlDataAdapter();
            Functions.BringDataIntoDGV(query, dt, adapter, dgvMuaSanPham);
            string l = "update billdetail set thanhtien=(select sum(tongtien) from billdetail where mahd='" + txtMaHoaDon.Text + "') where mahd='" + txtMaHoaDon.Text + "'";
            SqlCommand cmd1 = new SqlCommand(l, Functions.connection);
            cmd1.ExecuteNonQuery();
            string p = "select thanhtien from billdetail where mahd='" + txtMaHoaDon.Text + "'";
            txtThanhTien.Text = Functions.GetFieldValues(p);
        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text != "")
            {
                btnThemSanPham.Enabled = true;
                btnInHoaDon.Enabled = true;
                btnHuyHoaDon.Enabled = true;
              

                // Làm mới giá trị thành tiền lại về 0

            }
            
        }

        private void cbbMaSP_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSoLuongMua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string number, unit_price;
                double multi;

                number = txtSoLuongMua.Text;
                unit_price = txtDonGia.Text;
                if (number != "")
                {
                    multi = double.Parse(number) * double.Parse(unit_price);
                    txtTongTien.Text = multi.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn cần nhập sô");
            }

        }
        string o;
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            txtSoLuongMua.Text = "";
            txtDonGia.Text = "";
            txtTenSP.Text = "";
            txtTongTien.Text = "";
            txtTenSP.Text = "";
            cbbMaSP.Text = "";
            txtThanhTien.Text = "0";
            txtTenKhachHang.Text = "";

            Functions.ConnectionDataBase();
            string l = "select count(mahd) from bill";
            SqlCommand cmd= new SqlCommand(l,Functions.connection);
            SqlDataReader rdr = cmd.ExecuteReader();
           
                while (rdr.Read())
                {
                    o = rdr[0].ToString();
                }
            if (o == "0")
            {
                o = "1";
            }
            else
            {
                o=(int.Parse(o)+1).ToString();
            }
            
            txtMaHoaDon.Text = "HD" + o;
            rdr.Close();
            string str = "insert into bill(mahd) values('" + txtMaHoaDon.Text + "')";
            SqlCommand cmd1=new SqlCommand(str,Functions.connection);
            cmd1.ExecuteNonQuery();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if(txtTenKhachHang.Text=="" || txtTenSP .Text== "" || txtSoLuongMua.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin","Thông Báo" );
            }
            else
            {
                try
                {
                    string sqlInsert = "Insert Into BillDetail Values('" + txtMaHoaDon.Text + "', '" + cbbMaSP.Text + "', N'" + txtTenSP.Text + "', '" + txtSoLuongMua.Text + "', '" + txtDonGia.Text + "','" + txtTaiKhoan.Text + "','" + txtTenNV.Text + "','" + dtpNgayBan.Value.ToString() + "','" + txtTenKhachHang.Text + "','" + txtThanhTien.Text + "', '" + double.Parse(txtTongTien.Text) + "')";
                    SqlCommand cmd = new SqlCommand(sqlInsert, Functions.connection);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Sản Phẩm Đã Có Trong Hóa Đơn", "Thông Báo");
                }
            }
            

            string query = "Select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', SoLuong as N'Số lượng mua', DonGia as N'Đơn giá', TongTien as N'Tổng tiền' From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            SqlDataAdapter adapter = new SqlDataAdapter();

            Functions.BringDataIntoDGV(query, dt, adapter, dgvMuaSanPham);



            string sqlUpdate = "Update Billdetail Set ThanhTien = (Select Sum(TongTien) From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "') where mahd='" + txtMaHoaDon.Text + "'";
            SqlCommand command = new SqlCommand(sqlUpdate, Functions.connection);
            command.ExecuteNonQuery();

            string queryMoney = "Select ThanhTien From Billdetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            txtThanhTien.Text = Functions.GetFieldValues(queryMoney);
            txtTongTien.Text = "";
            txtSoLuongMua.Text = "";
            string o = "update bill set thanhtien='" + txtThanhTien.Text + "' where mahd='"+txtMaHoaDon.Text+"'";
            SqlCommand cmd1=new SqlCommand(o,Functions.connection);
            cmd1.ExecuteNonQuery();
        }

        private void dgvMuaSanPham_Click(object sender, EventArgs e)
        {
            cbbMaSP.Text = dgvMuaSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dgvMuaSanPham.CurrentRow.Cells[1].Value.ToString();
            txtSoLuongMua.Text = dgvMuaSanPham.CurrentRow.Cells[2].Value.ToString();
            txtDonGia.Text = dgvMuaSanPham.CurrentRow.Cells[3].Value.ToString();
            txtTongTien.Text = dgvMuaSanPham.CurrentRow.Cells[4].Value.ToString();
            string str = "select thanhtien from billdetail where mahd='" + txtMaHoaDon.Text + "'";
            txtThanhTien.Text = Functions.GetFieldValues(str);
        }

        private void dgvMuaSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cbbMaSP.Text = dgvMuaSanPham.CurrentRow.Cells[0].Value.ToString();
            Functions.ConnectionDataBase();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string str = "delete from billdetail where mahd='" + txtMaHoaDon.Text + "'and masp='" + cbbMaSP.Text + "'";
            SqlCommand cmd = new SqlCommand(str, Functions.connection);
            cmd.ExecuteNonQuery();
            string l = "Select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', SoLuong as N'Số lượng mua', DonGia as N'Đơn giá', TongTien as N'Tổng tiền' From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            Functions.BringDataIntoDGV(l, dt, adapter, dgvMuaSanPham);
            string sqlUpdate = "Update Billdetail Set ThanhTien = (Select Sum(TongTien) From BillDetail Where MaHD = '" + txtMaHoaDon.Text + "') where mahd='" + txtMaHoaDon.Text + "'";
            SqlCommand command = new SqlCommand(sqlUpdate, Functions.connection);
            command.ExecuteNonQuery();
            string queryMoney = "Select ThanhTien From Billdetail Where MaHD = '" + txtMaHoaDon.Text + "'";
            txtThanhTien.Text = Functions.GetFieldValues(queryMoney);
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            v = txtMaHoaDon.Text;
            Formprint a=new Formprint();
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        float ThanhTien = 0;

        public formthungan(string TK, string MK, string TenND, string GioiTinh, string SDT, string LoaiTk)
        {
            InitializeComponent();
            this.TK = TK;
            this.MK = MK;
            this.TenND = TenND;
            this.GioiTinh = GioiTinh;
            this.SDT = SDT;
            this.LoaiTk = LoaiTk;

        }
        
        private void dgvMuaSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbbMaSP.Text = dgvMuaSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dgvMuaSanPham.CurrentRow.Cells[1].Value.ToString();
            txtSoLuongMua.Text = dgvMuaSanPham.CurrentRow.Cells[2].Value.ToString();
            txtDonGia.Text = dgvMuaSanPham.CurrentRow.Cells[3].Value.ToString();
            txtTongTien.Text = dgvMuaSanPham.CurrentRow.Cells[4].Value.ToString();
            string str = "select thanhtien from billdetail where mahd='" + txtMaHoaDon.Text + "'";
            txtThanhTien.Text = Functions.GetFieldValues(str);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
