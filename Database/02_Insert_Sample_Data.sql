USE QuanLyTruongHocDB;
GO

INSERT INTO [USER] (ho_ten, email, mat_khau, vai_tro)
VALUES 
(N'Quản trị viên', 'admin@gmail.com', '123456', 'admin'),
(N'Nguyễn Văn Giáo Viên', 'giaovien@gmail.com', '123456', 'gv'),
(N'Trần Văn Học Sinh', 'hocsinh@gmail.com', '123456', 'hs');

INSERT INTO GIAO_VIEN (user_id, ma_gv, chuyen_mon, so_dien_thoai)
VALUES 
(2, 'GV001', N'Toán học', '0987654321');

INSERT INTO HOC_SINH (user_id, ma_hs, ngay_sinh, gioi_tinh, dia_chi)
VALUES 
(3, 'HS001', '2010-05-20', N'Nam', N'Hà Nội');