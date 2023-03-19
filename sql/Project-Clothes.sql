Create Database SellClothes5

Use SellClothes5

-- --------------------------------------------------------------------------
-- --------------------------------------------------------------------------
-- --------------------------------------------------------------------------
-- Create Table

-- Account
-- Product
-- Bill
-- BillDetail


Create Table Account
(
	TaiKhoan nvarchar(100) Not null,
	MatKhau nvarchar(1000) Not null,
	TenND nvarchar(100) Not null,
	GioiTinh nvarchar(100),
	SDT char(50) Not null,
	LoaiTK nvarchar(100) Not null,
	Primary key (TaiKhoan)
)
/*
	TenHT: Tên hiển thị
	LoaiTk: Loại tài khoản (Liên quan tới phân quyền tài khoản)
	0 là quản lý
	1 là nhân viên
*/


Create Table Product
(
	MaSP varchar(100) Not null,
	TenSP nvarchar(300) Not null,
	LoaiSP nvarchar(200) Not null,
	NhanHang nvarchar(100) Not null,
	XuatXu nvarchar(100) Not null,
	SoLuong varchar(2000) Not null default 0,
	DonGia Float Not null,
	NgayNhap Date Not null,
	TenND nvarchar(100) Not null,
	Primary key (MaSP) 
)
Go
select * from Product
drop table Product

Create Table ProductCategory
(
	LoaiSP nvarchar(200) Default N'Rỗng',
	NhanHang nvarchar(100) Default N'Rỗng',
	XuatXu nvarchar(100) Default N'Rỗng'
)
Go


Create Table DeliverHistory
(
	MaSP varchar(100) Not null,
	TenSP nvarchar(300) Not null,
	SLXuat int Not null,
	NgayXuat Date Not null,
	TenND nvarchar(100) Not null,
)


Create Table Bill
(
	MaHD varchar(10) Not null,
	thanhtien float default 0,
	
	Primary key (MaHD)
	
)

select * from Bill
drop table bill
insert into bill (mahd) values('a')
delete from bill
Create Table BillDetail
(
	MaHD varchar(10)Not null,
	MaSP char(10) Not null,
	TenSP nvarchar(300)not null,
	SoLuong char(10) Not null,
	DonGia char(10) Not null,
	TaiKhoan nvarchar(100) Not null,
	TenND nvarchar(100) Not null,
	NgayBan Date Not Null,
	TenKhach nvarchar(300) Not null,
	ThanhTien Float Default 0,
	TongTien Float Not null,
	Primary key (MaHD,TenSP),
	Foreign key (MaHD) References Bill(MaHD)
	
)
select * from BillDetail
select masp, tensp, soluong,DonGia,tongtien from billdetail
delete from BillDetail 
Drop Table Account
Drop Table Product
Drop Table Bill
Drop Table BillDetail



-- --------------------------------------------------------------------------
-- --------------------------------------------------------------------------
-- --------------------------------------------------------------------------
-- Insert

Insert Into Account
Values (N'Admin', N'admin', N'Chủ cửa hàng', N'Nam', '0328895451', N'Quản lý'),
	   (N'Phivanduc', N'phivanduc', N'Phí Văn Đức', N'Nam', '6413782462', N'Nhân viên')


Update Bill 
Set ThanhTien = (
	Select Sum(TongTien)
	From BillDetail
)



-- --------------------------------------------------------------------------
-- --------------------------------------------------------------------------
-- --------------------------------------------------------------------------
-- Truy vấn


Select * From Bill

Select * From BillDetail