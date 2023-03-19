using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;// Thư viện của ADO.NET
using System.Data.SqlClient; // Bổ sung thêm thư viện để sử dụng SQL Server
using System.Windows.Forms; // Sử dụng đối tượng MessageBox
using System.Drawing;


namespace Project_Main.Class
{
    public class Functions
    {
        // Chuỗi chứa địa chỉ kết nối tới cơ sở dữ liệu
        public static string stringConnection = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=SellClothes5;Integrated Security=True";

        // Đối tượng để thực hiện kết nối csdl
        public static SqlConnection connection = null;


        // Tạo phương thức Kết nối đến CSDL
        public static void ConnectionDataBase()
        {
            connection = new SqlConnection(stringConnection);
            connection.Open();
        }


        // Tạo phương thức ngắt kết nối CSDL
        public static void DisConnectionDatabase()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }


        // Tạo phương thức mang dữ liệu lên trên bảng DataGridView
        public static void BringDataIntoDGV(string query, DataTable dt, SqlDataAdapter adapter, DataGridView dataGridView)
        {
            adapter = new SqlDataAdapter(query, connection);

            dt.Rows.Clear();
            adapter.Fill(dt);
            dataGridView.DataSource = dt;
            adapter = null;
        }


        // Tạo phương thức mang dữ liệu lên trên bảng DataGridView phiên bản mới
        public static void BringDataIntoDGVNew(string query, DataGridView dataGridView)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();

            dt.Rows.Clear();
            adapter.Fill(dt);
            dataGridView.DataSource = dt;
            adapter = null;
        }


        // Tạo phương thức mang dữ liệu vào comboBox
        public static void BringDataIntoComboBox(string query, ComboBox comboBox, string Display, string Code)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(query, connection);

            dt.Rows.Clear();
            adapter.Fill(dt);
            comboBox.DataSource = dt;
            comboBox.DisplayMember = Display;
            comboBox.ValueMember = Code;
            comboBox.SelectedIndex = -1;
        }


        // Tạo phương thức lấy giá trị trong một trường
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }
    }
}
