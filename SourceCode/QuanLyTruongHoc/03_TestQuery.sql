USE QuanLyTruongHocDB;
GO

-- Kiểm tra USER
SELECT * FROM [USER];

-- Kiểm tra giáo viên
SELECT * FROM GIAO_VIEN;

-- Kiểm tra học sinh
SELECT * FROM HOC_SINH;

-- Kiểm tra lớp học
SELECT * FROM LOP_HOC;

-- Kiểm tra môn học
SELECT * FROM MON_HOC;

-- Danh sách học sinh theo lớp
SELECT
    hs.ma_hs,
    u.ho_ten,
    lh.ten_lop
FROM HOC_SINH hs
INNER JOIN [USER] u
    ON hs.user_id = u.id
INNER JOIN LOP_HOC_SINH lhs
    ON hs.id = lhs.hoc_sinh_id
INNER JOIN LOP_HOC lh
    ON lhs.lop_id = lh.id;

-- Điểm danh
SELECT * FROM DIEM_DANH;

-- Điểm học tập
SELECT * FROM DIEM;