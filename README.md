# 🏫 QuanLyTruongHoc – Phần mềm Quản lý Trường học

Ứng dụng desktop quản lý trường học được xây dựng bằng **WPF (.NET Framework 4.7.2)** kết hợp **SQL Server**, hỗ trợ quản lý học sinh, giáo viên, lớp học, môn học, phân công giảng dạy, điểm danh và điểm số theo từng vai trò người dùng (Admin / Giáo viên / Học sinh).

> Repository: [ducmanh14/QuanLyTruongHoc](https://github.com/ducmanh14/QuanLyTruongHoc)

---

## 📑 Mục lục

- [Giới thiệu](#-giới-thiệu)
- [Tính năng chính](#-tính-năng-chính)
- [Công nghệ sử dụng](#-công-nghệ-sử-dụng)
- [Cấu trúc thư mục](#-cấu-trúc-thư-mục)
- [Cơ sở dữ liệu](#-cơ-sở-dữ-liệu)
- [Hướng dẫn cài đặt](#-hướng-dẫn-cài-đặt)
- [Hướng dẫn sử dụng](#-hướng-dẫn-sử-dụng)
- [Phân quyền người dùng](#-phân-quyền-người-dùng)
- [Tài khoản mẫu](#-tài-khoản-mẫu)
- [Lộ trình phát triển](#-lộ-trình-phát-triển-roadmap)
- [Đóng góp](#-đóng-góp)
- [Thành viên thực hiện](#-thành-viên-thực-hiện)
---

## 📖 Giới thiệu

**QuanLyTruongHoc** là đồ án/dự án phần mềm quản lý trường học, mô phỏng các nghiệp vụ cơ bản của một trường phổ thông như: quản lý hồ sơ học sinh – giáo viên, quản lý lớp học, phân công giảng dạy, điểm danh hằng ngày và ghi nhận điểm số học sinh. Ứng dụng được xây dựng dưới dạng **desktop application (WPF)**, sử dụng **SQL Server / LocalDB** làm hệ quản trị cơ sở dữ liệu, phù hợp làm đồ án môn học hoặc nền tảng để phát triển thêm thành hệ thống quản lý trường học đầy đủ.

## ✨ Tính năng chính

Dựa trên cấu trúc mã nguồn hiện tại, ứng dụng bao gồm các module sau:

| Module | Mô tả |
|---|---|
| 🔐 **Đăng nhập / Xác thực** | Đăng nhập bằng email & mật khẩu, phân quyền theo vai trò (`admin`, `gv`, `hs`) |
| 🧑‍🎓 **Quản lý học sinh** | Thêm, sửa, xóa, tìm kiếm thông tin học sinh (mã HS, ngày sinh, giới tính, địa chỉ, hình ảnh) |
| 👨‍🏫 **Quản lý giáo viên** | Quản lý hồ sơ giáo viên (mã GV, chuyên môn, số điện thoại) |
| 🏫 **Quản lý lớp học** | Tạo, cập nhật lớp học, gán giáo viên chủ nhiệm, theo dõi sĩ số |
| 👥 **Quản lý học sinh theo lớp** | Xếp lớp cho học sinh, theo dõi trạng thái học (đang học/đã rời lớp) |
| 📚 **Quản lý môn học** | Thêm/sửa/xóa môn học, số tiết học |
| 📋 **Phân công giảng dạy** | Gán giáo viên phụ trách môn học cho từng lớp theo học kỳ/năm học |
| ✅ **Điểm danh** | Điểm danh học sinh theo buổi học, ghi chú tình trạng |
| 🕓 **Lịch sử điểm danh** | Tra cứu lại lịch sử điểm danh của học sinh/lớp |
| 📊 **Quản lý điểm** | Nhập điểm theo loại điểm (miệng, 15 phút, giữa kỳ, cuối kỳ...), theo học kỳ/năm học |
| 👤 **Hồ sơ cá nhân (Profile)** | Xem thông tin tài khoản cá nhân theo từng vai trò |
| 🔑 **Đổi mật khẩu** | Cho phép người dùng tự đổi mật khẩu đăng nhập |
| 🧭 **Giao diện điều hướng (Dashboard)** | Menu điều hướng động theo vai trò, ẩn/hiện chức năng phù hợp với quyền hạn |

## 🛠 Công nghệ sử dụng

- **Ngôn ngữ:** C#
- **Giao diện:** WPF (Windows Presentation Foundation) – XAML
- **Nền tảng:** .NET Framework 4.7.2
- **Cơ sở dữ liệu:** Microsoft SQL Server (LocalDB `(localdb)\MSSQLLocalDB`)
- **Kết nối dữ liệu:** ADO.NET (`System.Data.SqlClient` 4.9.1)
- **IDE đề xuất:** Visual Studio 2022 (hoặc mới hơn)
- **Kiến trúc mã nguồn:** Phân lớp theo mô hình gần giống 3-layer:
  - `Models/` – Các lớp đối tượng dữ liệu (Entity)
  - `Services/` – Xử lý nghiệp vụ & truy vấn dữ liệu
  - `Views/` – Giao diện người dùng (Pages/Windows)
  - `Data/` – Kết nối cơ sở dữ liệu & script SQL

## 📂 Cấu trúc thư mục

```
QuanLyTruongHoc/
├── Database/                      # Script SQL gốc (tạo DB, dữ liệu mẫu, truy vấn test)
│   ├── 01_Create_Tables.sql
│   ├── 02_Insert_Sample_Data.sql
│   └── 03_TestQuery.sql
├── Documents/                     # Tài liệu dự án (đặc tả, báo cáo...)
├── Images/                        # Hình ảnh sử dụng trong ứng dụng/tài liệu
└── SourceCode/
    └── QuanLyTruongHoc/            # Source code chính (WPF project)
        ├── App.xaml / App.xaml.cs
        ├── App.config              # Chuỗi kết nối SQL Server
        ├── MainWindow.xaml(.cs)    # Cửa sổ chính sau khi đăng nhập
        ├── QuanLyTruongHoc.csproj
        ├── packages.config
        ├── Data/
        │   ├── DatabaseHelper.cs   # Hỗ trợ kết nối & thực thi câu lệnh SQL
        │   └── *.sql
        ├── Models/                 # Diem, DiemDanh, GiaoVien, HocSinh, LopHoc,
        │                           # LopHocSinh, MonHoc, PhanCongGiangDay, Profile, User
        ├── Services/               # Logic nghiệp vụ tương ứng với từng Model
        └── Views/                  # Các trang giao diện (Login, Dashboard, các trang quản lý)
```

## 🗄 Cơ sở dữ liệu

Cơ sở dữ liệu tên **`QuanLyTruongHocDB`**, gồm các bảng chính:

| Bảng | Chức năng |
|---|---|
| `USER` | Tài khoản đăng nhập (họ tên, email, mật khẩu, vai trò, trạng thái) |
| `GIAO_VIEN` | Hồ sơ giáo viên, liên kết với `USER` |
| `HOC_SINH` | Hồ sơ học sinh, liên kết với `USER` |
| `LOP_HOC` | Thông tin lớp học, giáo viên chủ nhiệm |
| `LOP_HOC_SINH` | Quan hệ học sinh – lớp học (theo dõi trạng thái học) |
| `MON_HOC` | Danh mục môn học |
| `PHAN_CONG_GIANG_DAY` | Phân công giáo viên dạy môn học cho lớp theo học kỳ/năm học |
| `DIEM_DANH` | Ghi nhận điểm danh học sinh theo buổi học |
| `DIEM` | Ghi nhận điểm số học sinh theo loại điểm, học kỳ, năm học |

Script khởi tạo cơ sở dữ liệu nằm tại `Database/01_Create_Tables.sql` và `Database/02_Insert_Sample_Data.sql`.

## 🚀 Hướng dẫn cài đặt

### Yêu cầu hệ thống

- Windows 10/11
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (có cài đặt workload **.NET desktop development**)
- [SQL Server](https://www.microsoft.com/sql-server) hoặc **SQL Server Express LocalDB** (thường có sẵn khi cài Visual Studio)
- .NET Framework 4.7.2 (Developer Pack nếu chưa có)

### Các bước thực hiện

1. **Clone dự án**
   ```bash
   git clone https://github.com/ducmanh14/QuanLyTruongHoc.git
   cd QuanLyTruongHoc
   ```

2. **Khởi tạo cơ sở dữ liệu**
   - Mở **SQL Server Management Studio (SSMS)** hoặc dùng `sqlcmd`, kết nối tới `(localdb)\MSSQLLocalDB` (hoặc instance SQL Server của bạn).
   - Chạy lần lượt các script theo thứ tự:
     ```
     Database/01_Create_Tables.sql        -- Tạo database và các bảng
     Database/02_Insert_Sample_Data.sql   -- Chèn dữ liệu mẫu (tài khoản, lớp, môn học...)
     ```
   - (Tùy chọn) Chạy `Database/03_TestQuery.sql` để kiểm tra dữ liệu.

3. **Cấu hình chuỗi kết nối**
   - Mở file `SourceCode/QuanLyTruongHoc/App.config`.
   - Kiểm tra/chỉnh sửa chuỗi kết nối cho phù hợp với SQL Server của bạn:
     ```xml
     <connectionStrings>
       <add name="MyDbConnection"
            connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyTruongHocDB;Integrated Security=True;"
            providerName="System.Data.SqlClient" />
     </connectionStrings>
     ```
     > Nếu dùng SQL Server instance khác (ví dụ `.\SQLEXPRESS` hoặc server có user/pass), hãy cập nhật `Data Source` và thông tin đăng nhập tương ứng.

4. **Mở và build dự án**
   - Mở file `SourceCode/QuanLyTruongHoc.slnx` (hoặc solution tương ứng) bằng Visual Studio.
   - Chờ Visual Studio khôi phục các gói NuGet (`System.Data.SqlClient`).
   - Build dự án (`Ctrl + Shift + B`).

5. **Chạy ứng dụng**
   - Nhấn `F5` hoặc chọn **Start** để chạy chương trình.
   - Màn hình đăng nhập sẽ hiện ra đầu tiên — sử dụng tài khoản mẫu bên dưới để đăng nhập.

## 📘 Hướng dẫn sử dụng

1. Đăng nhập bằng email & mật khẩu được cấp (xem [Tài khoản mẫu](#-tài-khoản-mẫu)).
2. Sau khi đăng nhập, hệ thống sẽ hiển thị **Dashboard** với menu điều hướng bên trái, các mục hiển thị sẽ khác nhau tùy theo **vai trò** đăng nhập.
3. Chọn chức năng tương ứng ở menu để thao tác:
   - Quản lý học sinh / giáo viên / lớp học / môn học / phân công giảng dạy (dành cho quản trị viên/giáo viên).
   - Điểm danh & xem lịch sử điểm danh.
   - Nhập/xem điểm số.
   - Xem hồ sơ cá nhân, đổi mật khẩu.

## 🔐 Phân quyền người dùng

Hệ thống có 3 vai trò (`vai_tro`) tương ứng với 3 loại tài khoản:

| Vai trò | Mã | Quyền hạn (dự kiến) |
|---|---|---|
| Quản trị viên | `admin` | Toàn quyền: quản lý học sinh, giáo viên, lớp học, môn học, phân công giảng dạy |
| Giáo viên | `gv` | Điểm danh, nhập điểm cho lớp/môn được phân công, xem thông tin lớp chủ nhiệm |
| Học sinh | `hs` | Xem thông tin cá nhân, lớp học, điểm số, lịch sử điểm danh của bản thân |

## 🔑 Tài khoản mẫu

Sau khi chạy script `02_Insert_Sample_Data.sql`, các tài khoản sau sẽ có sẵn để đăng nhập thử nghiệm:

| Vai trò | Email | Mật khẩu |
|---|---|---|
| Quản trị viên | `admin@gmail.com` | `123456` |
| Giáo viên | `giaovien@gmail.com` | `123456` |
| Học sinh | `hocsinh@gmail.com` | `123456` |

> ⚠️ **Lưu ý bảo mật:** Đây là dữ liệu mẫu dùng cho mục đích học tập/demo. Mật khẩu hiện đang được lưu ở dạng plain-text trong cơ sở dữ liệu — **không sử dụng cấu hình này trong môi trường thực tế**. Trước khi triển khai thật, cần bổ sung mã hóa/băm mật khẩu (hashing, ví dụ BCrypt) và các biện pháp bảo mật khác.

## 🗺 Lộ trình phát triển (Roadmap)

Một số định hướng có thể phát triển thêm trong tương lai:

- [ ] Mã hóa mật khẩu (hashing) thay vì lưu plain-text
- [ ] Thống kê, báo cáo (dashboard biểu đồ điểm, tỉ lệ chuyên cần...)
- [ ] Xuất báo cáo điểm/điểm danh ra Excel, PDF
- [ ] Thông báo/nhắc nhở (email, popup)
- [ ] Quản lý học phí
- [ ] Chuyển đổi sang kiến trúc client-server / web (ASP.NET Core, API)

## 🤝 Đóng góp

Mọi đóng góp đều được hoan nghênh! Để đóng góp cho dự án:

1. Fork dự án
2. Tạo nhánh tính năng mới (`git checkout -b feature/ten-tinh-nang`)
3. Commit thay đổi (`git commit -m "Thêm tính năng ..."`)
4. Push lên nhánh (`git push origin feature/ten-tinh-nang`)
5. Tạo Pull Request

## 👨‍👩‍👧‍👦 Thành viên thực hiện


| STT | Họ và tên | MSSV/Mã số | Vai trò trong dự án | GitHub | Email |
|---|---|---|---|---|---|
| 1 | Mai Đức Mạnh | 23010814 | Leader | ducmanh14 |  |
| 2 | Nguyễn Hoàng Long | 21010640 | Thành Viên | nhlong306 |  |
| 3 | Nguyễn Thành Nam | 	22010255 | Thành Viên | Namcoder04 |  |
| 4 | Bùi Minh Quân | 	23010725 | Thành Viên | BuiQuan1702 |  |

**Giảng viên hướng dẫn:** _Phạm Văn Hà_

**Lớp / Môn học:** _Công Nghệ.Net-1-3-25(N01)_

**Trường / Khoa:** _Đại Học Phenikaa/CNTT_

---

<p align="center">Made with ❤️ for học tập và nghiên cứu quản lý trường học.</p>
