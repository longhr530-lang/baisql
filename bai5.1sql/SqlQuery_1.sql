-- 1. Tạo CSDL ql_sinhvien nếu chưa tồn tại
IF DB_ID(N'ql_sinhvien') IS NULL
BEGIN
    CREATE DATABASE ql_sinhvien;
END
GO

USE ql_sinhvien;
GO

-- 2. Tạo bảng TaiKhoan (Phục vụ chức năng Đăng nhập)
IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaiKhoan (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL
    );
END
GO

-- 3. Tạo bảng SinhVien
IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SinhVien (
        MaSV VARCHAR(20) PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        NgaySinh DATE,
        GioiTinh NVARCHAR(10),
        Lop NVARCHAR(50),
        DiemTB DECIMAL(4,2)
    );
END
GO

-- 4. Thêm dữ liệu mẫu vào bảng TaiKhoan (Mật khẩu mặc định: 123456)
IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE Username = 'longnguyen')
BEGIN
    INSERT INTO dbo.TaiKhoan (Username, PasswordHash) 
    VALUES ('longnguyen', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE Username = 'khoinguyen')
BEGIN
    INSERT INTO dbo.TaiKhoan (Username, PasswordHash) 
    VALUES ('khoinguyen', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92');
END
GO

-- 5. Thêm dữ liệu mẫu vào bảng SinhVien
IF NOT EXISTS (SELECT 1 FROM dbo.SinhVien WHERE MaSV = 'SV001')
    INSERT INTO dbo.SinhVien VALUES ('SV001', N'Nguyễn Văn An', '2003-05-15', N'Nam', N'CNTT K18', 8.50);

IF NOT EXISTS (SELECT 1 FROM dbo.SinhVien WHERE MaSV = 'SV002')
    INSERT INTO dbo.SinhVien VALUES ('SV002', N'Trần Thị Bình', '2003-08-20', N'Nữ', N'CNTT K18', 9.20);

IF NOT EXISTS (SELECT 1 FROM dbo.SinhVien WHERE MaSV = 'SV003')
    INSERT INTO dbo.SinhVien VALUES ('SV003', N'Lê Hoàng Cường', '2002-11-10', N'Nam', N'Kế Toán K18', 7.40);
GO

-- 6. Truy vấn kiểm tra dữ liệu sau khi khởi tạo
SELECT * FROM dbo.TaiKhoan;
SELECT * FROM dbo.SinhVien;