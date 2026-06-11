CREATE DATABASE QuanLyTruongHocDB;
GO

USE QuanLyTruongHocDB;
GO

CREATE TABLE [USER] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    ho_ten NVARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    mat_khau VARCHAR(255) NOT NULL,
    vai_tro VARCHAR(20) NOT NULL,
    trang_thai BIT DEFAULT 1,
    ngay_tao DATETIME DEFAULT GETDATE(),
    cap_nhat DATETIME DEFAULT GETDATE()
);

CREATE TABLE GIAO_VIEN (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT UNIQUE NOT NULL,
    ma_gv VARCHAR(20) UNIQUE NOT NULL,
    chuyen_mon NVARCHAR(100),
    so_dien_thoai VARCHAR(20),
    ngay_tao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (user_id) REFERENCES [USER](id)
);

CREATE TABLE HOC_SINH (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT UNIQUE NOT NULL,
    ma_hs VARCHAR(20) UNIQUE NOT NULL,
    ngay_sinh DATE,
    gioi_tinh NVARCHAR(10),
    dia_chi NVARCHAR(MAX),
    hinh_anh VARCHAR(255),

    FOREIGN KEY (user_id) REFERENCES [USER](id)
);

CREATE TABLE LOP_HOC (
    id INT IDENTITY(1,1) PRIMARY KEY,
    gv_chu_nhiem_id INT NULL,
    ten_lop NVARCHAR(50) NOT NULL,
    khoi NVARCHAR(20),
    nien_khoa NVARCHAR(20),
    si_so INT DEFAULT 0,
    ngay_tao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (gv_chu_nhiem_id) REFERENCES GIAO_VIEN(id)
);

CREATE TABLE LOP_HOC_SINH (
    id INT IDENTITY(1,1) PRIMARY KEY,
    lop_id INT NOT NULL,
    hoc_sinh_id INT NOT NULL,
    trang_thai VARCHAR(20) DEFAULT 'dang_hoc',
    ngay_vao_lop DATETIME DEFAULT GETDATE(),
    ngay_roi_lop DATETIME NULL,

    FOREIGN KEY (lop_id) REFERENCES LOP_HOC(id),
    FOREIGN KEY (hoc_sinh_id) REFERENCES HOC_SINH(id)
);

CREATE TABLE MON_HOC (
    id INT IDENTITY(1,1) PRIMARY KEY,
    ma_mon VARCHAR(20) UNIQUE NOT NULL,
    ten_mon NVARCHAR(100) NOT NULL,
    so_tiet INT DEFAULT 0
);

CREATE TABLE PHAN_CONG_GIANG_DAY (
    id INT IDENTITY(1,1) PRIMARY KEY,
    lop_id INT NOT NULL,
    giao_vien_id INT NOT NULL,
    mon_hoc_id INT NOT NULL,
    hoc_ky INT NOT NULL,
    nam_hoc NVARCHAR(20) NOT NULL,

    FOREIGN KEY (lop_id) REFERENCES LOP_HOC(id),
    FOREIGN KEY (giao_vien_id) REFERENCES GIAO_VIEN(id),
    FOREIGN KEY (mon_hoc_id) REFERENCES MON_HOC(id)
);

CREATE TABLE DIEM_DANH (
    id INT IDENTITY(1,1) PRIMARY KEY,
    lop_id INT NOT NULL,
    hoc_sinh_id INT NOT NULL,
    giao_vien_id INT NULL,
    ngay_diem_danh DATE DEFAULT GETDATE(),
    buoi_hoc NVARCHAR(20),
    trang_thai NVARCHAR(50),
    ghi_chu NVARCHAR(255),
    ngay_tao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (lop_id) REFERENCES LOP_HOC(id),
    FOREIGN KEY (hoc_sinh_id) REFERENCES HOC_SINH(id),
    FOREIGN KEY (giao_vien_id) REFERENCES GIAO_VIEN(id)
);

CREATE TABLE DIEM (
    id INT IDENTITY(1,1) PRIMARY KEY,
    hoc_sinh_id INT NOT NULL,
    lop_id INT NOT NULL,
    mon_hoc_id INT NOT NULL,
    giao_vien_id INT NULL,
    loai_diem NVARCHAR(50),
    gia_tri DECIMAL(4,2),
    hoc_ky INT,
    nam_hoc NVARCHAR(20),
    ngay_nhap DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (hoc_sinh_id) REFERENCES HOC_SINH(id),
    FOREIGN KEY (lop_id) REFERENCES LOP_HOC(id),
    FOREIGN KEY (mon_hoc_id) REFERENCES MON_HOC(id),
    FOREIGN KEY (giao_vien_id) REFERENCES GIAO_VIEN(id)
);