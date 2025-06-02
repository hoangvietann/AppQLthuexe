-- BẢNG KHÁCH HÀNG
CREATE TABLE KhachHang (
    MaKH SERIAL PRIMARY KEY,
    Hoten VARCHAR(100) NOT NULL,
    CCCD VARCHAR(20) UNIQUE NOT NULL,
    Diachi TEXT,
    SDT VARCHAR(15)
);
SELECT Hoten, CCCD 
FROM KhachHang 
WHERE to_tsvector('simple', Hoten) @@ plainto_tsquery('simple', 'Hùng');

INSERT INTO Khachhang (Hoten, CCCD, Diachi, SDT) 
VALUES 
    ('Nguyễn Văn A', '123456789012', 'Hà Nội', '0987654321'),
    ('Trần Thị B', '987654321098', 'Bắc Giang', '0912345678'),
    ('Lê Văn C', '567890123456', 'TP.HCM', '0938765432');
INSERT INTO KhachHang (Hoten, CCCD, Diachi, SDT) VALUES
('Nguyễn Văn Hùng', '123456789001', 'Thị trấn Đồi Ngô, huyện Lục Nam, Bắc Giang', '0987654321'),
('Trần Thị Mai', '123456789002', 'Xã Tiên Hưng, huyện Lục Nam, Bắc Giang', '0978123456'),
('Lê Văn Khánh', '123456789003', 'Xã Bảo Sơn, huyện Lục Nam, Bắc Giang', '0367891234'),
('Phạm Thị Hòa', '123456789004', 'Xã Cương Sơn, huyện Lục Nam, Bắc Giang', '0912345678'),
('Hoàng Văn Quý', '123456789005', 'Xã Trường Sơn, huyện Lục Nam, Bắc Giang', '0905123456'),
('Vũ Thị Ngọc', '123456789006', 'Xã Nghĩa Phương, huyện Lục Nam, Bắc Giang', '0981122334'),
('Đặng Văn Toàn', '123456789007', 'Xã Tam Dị, huyện Lục Nam, Bắc Giang', '0937894561'),
('Ngô Thị Lan', '123456789008', 'Xã Phương Sơn, huyện Lục Nam, Bắc Giang', '0941122333'),
('Bùi Văn Sơn', '123456789009', 'Xã Đông Hưng, huyện Lục Nam, Bắc Giang', '0922233445'),
('Đinh Thị Hương', '123456789010', 'Xã Bắc Lũng, huyện Lục Nam, Bắc Giang', '0966677889');
	
-- BẢNG TÀI KHOẢN
CREATE TABLE TaiKhoan (
    TenTK VARCHAR(50) PRIMARY KEY,
    Matkhau TEXT NOT NULL,
    LoaiTK VARCHAR(20) DEFAULT 'nhanvien' -- admin / nhanvien
);
INSERT INTO TaiKhoan (TenTK, Matkhau, LoaiTK) 
VALUES 
    ('admin', '123456', 'admin'),
    ('nhanvien01', 'abcdef', 'nhanvien'),
    ('nhanvien02', 'xyz123', 'nhanvien');

-- BẢNG XE Ô TÔ
CREATE TABLE XeOto (
    MaX SERIAL PRIMARY KEY,
    TenX VARCHAR(100) NOT NULL,
	Theloai VARCHAR(100) NOT NULL,
    Bienso VARCHAR(20) UNIQUE NOT NULL,
    DongiaX INTEGER CHECK (DongiaX >= 0),
    Trangthai VARCHAR(20) DEFAULT 'Trống' -- Trống / Đang thuê / Bảo trì
);
DROP TABLE XeOto;

INSERT INTO XeOto (TenX, Theloai, Bienso, NamSX, DongiaX, Trangthai) VALUES
-- Xe 5 chỗ Hyundai Accent
('Hyundai Accent', '5 chỗ', '30A-10001', 100000, 'Trống'),
('Hyundai Accent', '5 chỗ', '30A-10002', 100000, 'Trống'),

-- Xe 5 chỗ Vios
('Vios', '5 chỗ', '30A-20001', 150000, 'Trống'),
('Vios', '5 chỗ', '30A-20002', 150000, 'Trống'),

-- Xe 7 chỗ Expander
('Expander', '7 chỗ', '30A-30001', 200000, 'Trống'),
('Expander', '7 chỗ', '30A-30002', 200000, 'Trống'),

-- 16 chỗ Ford Transit
('Ford Transit', '16 chỗ', '30B-40001', 300000, 'Trống'),
('Ford Transit', '16 chỗ', '30B-40002', 300000, 'Trống'),

-- Xe 16 chỗ Hyundai Solati
('Hyundai Solati', '16 chỗ', '30B-50001', 320000, 'Trống'),
('Hyundai Solati', '16 chỗ', '30B-50002', 320000, 'Trống'),

-- Xe 29 chỗ Hyundai County
('Hyundai County', '29 chỗ', '30F-60001', 400000, 'Trống'),
('Hyundai County', '29 chỗ', '30F-60002', 400000, 'Trống'),

-- Xe 29 chỗ Samco
('Samco', '29 chỗ', '30F-70001', 450000, 'Trống'),
('Samco', '29 chỗ', '30F-70002', 450000, 'Trống'),

-- Xe 29 chỗ Thaco
('Thaco', '29 chỗ', '30F-80001', 400000, 'Trống'),
('Thaco', '29 chỗ', '30F-80002', 400000, 'Trống'),

-- Xe 35 chỗ Samco
('Samco', '35 chỗ', '30G-90001', 420000, 'Trống'),
('Samco', '35 chỗ', '30G-90002', 420000, 'Trống'),

-- Xe 45 chỗ Thaco
('Thaco', '45 chỗ', '30H-00001', 500000, 'Trống'),
('Thaco', '45 chỗ', '30H-00002', 500000, 'Trống');

-- BẢNG HỢP ĐỒNG
CREATE TABLE HopDong (
    MaHD SERIAL PRIMARY KEY,
    MaKH INT NOT NULL,
    NgayLap TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	Tongtien INTEGER CHECK (Tongtien >= 0),
    CONSTRAINT fk_khachhang_hd FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

DROP TABLE HopDong;

-- BẢNG CHI TIẾT HỢP ĐỒNG
CREATE TABLE ChiTietHD (
    MaCTHD SERIAL PRIMARY KEY,
    MaHD INT NOT NULL,
    MaX INT NOT NULL,
	Thuetaixe VARCHAR(20) NOT NULL,
    NgayThue TIMESTAMP NOT NULL,
    NgayTra TIMESTAMP NOT NULL,
    Dongia INTEGER CHECK (Dongia >= 0),
    Sogiothue NUMERIC(10,2),
    Thanhtien INTEGER CHECK (Thanhtien >= 0),
    CONSTRAINT fk_hd_ct FOREIGN KEY (MaHD) REFERENCES HopDong(MaHD),
    CONSTRAINT fk_xe_ct FOREIGN KEY (MaX) REFERENCES XeOto(MaX)
);
DROP TABLE ChiTietHD;

INSERT INTO HopDong (MaKH, NgayLap) VALUES
(13, '2024-04-18 19:14:00'),
(14, '2025-02-02 19:29:00'),
(10, '2025-05-24 06:01:00'),
(11, '2024-06-23 10:52:00'),
(20, '2025-06-14 14:45:00'),
(16, '2024-10-28 05:34:00'),
(15, '2024-07-10 08:45:00'),
(11, '2024-10-03 14:20:00'),
(18, '2024-12-21 10:00:00'),
(12, '2025-01-11 09:30:00'),
(17, '2025-03-02 16:50:00');

INSERT INTO ChiTietHD (MaHD, MaX, Thuetaixe, NgayThue, NgayTra, Dongia, Sogiothue, Thanhtien) VALUES
-- Hợp đồng 19 (MaHD = 19)
(19, 2, 'Có', '2024-04-18 20:00:00', '2024-04-19 08:00:00', 100000, 12, ROUND(100000 * 12 * 1.05)),
(19, 4, 'Không', '2024-04-19 09:00:00', '2024-04-19 18:00:00', 150000, 9, ROUND(150000 * 9)), 

-- Hợp đồng 20 (MaHD = 20)
(20, 6, 'Không', '2025-02-02 20:00:00', '2025-02-03 08:00:00', 200000, 12, ROUND(200000 * 12)),
(20, 7, 'Có', '2025-02-03 09:00:00', '2025-02-03 18:00:00', 300000, 9, ROUND(300000 * 9 * 1.05)),

-- Hợp đồng 21 (MaHD = 21)
(21, 9, 'Có', '2025-05-24 06:30:00', '2025-05-24 20:30:00', 320000, 14, ROUND(320000 * 14 * 1.05)),
(21, 11, 'Không', '2025-05-25 08:00:00', '2025-05-25 18:00:00', 400000, 10, ROUND(400000 * 10)), 

-- Hợp đồng 22 (MaHD = 22)
(22, 10, 'Không', '2024-06-23 11:00:00', '2024-06-23 20:00:00', 320000, 9, ROUND(320000 * 9)),
(22, 15, 'Có', '2024-06-24 08:00:00', '2024-06-24 17:00:00', 400000, 9, ROUND(400000 * 9 * 1.05)),

-- Hợp đồng 23 (MaHD = 23)
(23, 16, 'Không', '2025-06-14 14:30:00', '2025-06-15 02:30:00', 400000, 12, ROUND(400000 * 12)),
(23, 17, 'Có', '2025-06-15 08:00:00', '2025-06-15 20:00:00', 450000, 12, ROUND(450000 * 12 * 1.05)),
(23, 18, 'Không', '2025-06-16 08:00:00', '2025-06-16 18:00:00', 420000, 10, ROUND(420000 * 10)),

-- Hợp đồng 24 (MaHD = 24)
(24, 19, 'Có', '2024-10-28 05:30:00', '2024-10-28 17:30:00', 500000, 12, ROUND(500000 * 12 * 1.05)),
(24, 20, 'Không', '2024-10-29 08:00:00', '2024-10-29 18:00:00', 500000, 10, ROUND(500000 * 10)),

-- Hợp đồng 25 (MaHD = 25)
(25, 3, 'Có', '2024-07-10 09:00:00', '2024-07-10 21:00:00', 150000, 12, ROUND(150000 * 12 * 1.05)),
(25, 5, 'Không', '2024-07-11 08:00:00', '2024-07-11 18:00:00', 200000, 10, ROUND(200000 * 10)),

-- Hợp đồng 26 (MaHD = 26)
(26, 11, 'Không', '2024-10-03 14:30:00', '2024-10-03 22:30:00', 400000, 8, ROUND(400000 * 8)),
(26, 4, 'Có', '2024-10-04 07:00:00', '2024-10-04 19:00:00', 150000, 12, ROUND(150000 * 12 * 1.05)),

-- Hợp đồng 27 (MaHD = 27)
(27, 18, 'Có', '2024-12-21 10:00:00', '2024-12-21 22:00:00', 420000, 12, ROUND(420000 * 12 * 1.05)),

-- Hợp đồng 28 (MaHD = 28)
(28, 12, 'Không', '2025-01-11 09:30:00', '2025-01-11 19:30:00', 400000, 10, ROUND(400000 * 10)),

-- Hợp đồng 29 (MaHD = 29)
(29, 17, 'Có', '2025-03-02 16:50:00', '2025-03-03 04:50:00', 450000, 12, ROUND(450000 * 12 * 1.05));
CREATE OR REPLACE FUNCTION tinh_tong_tien()
RETURNS TRIGGER AS $$
DECLARE
    ma_hd INT;
BEGIN
    -- Lấy mã hợp đồng cần cập nhật
    IF (TG_OP = 'DELETE') THEN
        ma_hd := OLD.MaHD;
    ELSE
        ma_hd := NEW.MaHD;
    END IF;

    -- Cập nhật tổng tiền trong bảng HopDong
    UPDATE HopDong
    SET Tongtien = (
        SELECT COALESCE(SUM(Thanhtien), 0)
        FROM ChiTietHD
        WHERE MaHD = ma_hd
    )
    WHERE MaHD = ma_hd;

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_capnhat_tongtien
AFTER INSERT OR UPDATE OR DELETE ON ChiTietHD
FOR EACH ROW
EXECUTE FUNCTION tinh_tong_tien();

SELECT x.theloai, SUM(ct.thanhtien) AS doanhthu
FROM chitiethd ct
JOIN xeoto x ON ct.max = x.max
GROUP BY x.theloai
ORDER BY doanhthu DESC;

SELECT datname FROM pg_database;
CREATE DATABASE QLthuexe;

