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

INSERT INTO LOP_HOC (gv_chu_nhiem_id, ten_lop, khoi, nien_khoa, si_so)
VALUES
(1, N'Lớp 10A1', N'10', N'2024-2025', 1),
(1, N'Lớp 11A1', N'11', N'2024-2025', 0);

INSERT INTO LOP_HOC_SINH (lop_id, hoc_sinh_id, trang_thai)
VALUES
(1, 1, 'dang_hoc');

INSERT INTO MON_HOC (ma_mon, ten_mon, so_tiet)
VALUES
('TOAN', N'Toán học', 90),
('VAN', N'Ngữ văn', 90),
('ANH', N'Tiếng Anh', 90);

INSERT INTO PHAN_CONG_GIANG_DAY (lop_id, giao_vien_id, mon_hoc_id, hoc_ky, nam_hoc)
VALUES
(1, 1, 1, 1, N'2024-2025');

INSERT INTO DIEM_DANH 
(
    lop_id,
    hoc_sinh_id,
    giao_vien_id,
    buoi_hoc,
    trang_thai,
    ghi_chu
)
VALUES
(
    1,
    1,
    1,
    N'sang',
    N'co_mat',
    N'Dữ liệu mẫu'
);

INSERT INTO DIEM
(
    hoc_sinh_id,
    lop_id,
    mon_hoc_id,
    giao_vien_id,
    loai_diem,
    gia_tri,
    hoc_ky,
    nam_hoc
)
VALUES
(
    1,
    1,
    1,
    1,
    N'mieng',
    8.5,
    1,
    N'2024-2025'
);