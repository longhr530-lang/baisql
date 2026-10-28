# baisql
## I. THÔNG TIN SINH VIÊN
* **Họ và tên:** Nguyễn Văn A
* **Mã số sinh viên:** 22110001
* **Lớp học phần:** Lập trình .NET - Mã Lớp: LTTB01

* **Link Repository:** https://github.com/nguyenvana/LTNET-
TEST02_22110001_NguyenVanA

---

## II. DANH SÁCH BÀI TẬP HOÀN THÀNH & TỰ ĐÁNH GIÁ

| Bài tập | Tiêu đề bài tập | Trạng thái | Mức độ hoàn thành (%) |
| :---: | :--- | :---: | :---: |

| **Bài 1** | Quản lý Dịch vụ Phòng Gym & Hội viên | Hoàn thành | 100% |
| **Bài 2** | Báo cáo & Lưu trữ Tài liệu Nhà sách | Hoàn thành | 100% |
| **Bài 3** | Hệ thống MDI Quản lý Bệnh viện & Async | Hoàn thành | 90% |

---

## III. MINH HỌA GIAO DIỆN & KỸ THUẬT NỔI BẬT

### 1. Bài 1: Quản lý Dịch vụ Phòng Gym & Hội viên
* **Kỹ thuật áp dụng:** Cấu hình DataGridView bằng code (`AutoGenerateColumns =
false`), Data Binding 2 chiều với `BindingSource`, Validation với `ErrorProvider`.
* **Hình ảnh minh họa Giao diện:**

![Giao diện chính Bài 1](docs/bai1_main.png)
*Mô tả: Giao diện chính hiển thị DataGridView và đồng bộ dữ liệu 2 chiều lên các
TextBox.*

![Bắt lỗi Validation Bài 1](docs/bai1_error.png)
*Mô tả: Kiểm tra hợp lệ dữ liệu nhập vào bằng ErrorProvider khi nhấn Thêm mới.*

---

### 2. Bài 2: Quản lý & Xuất Báo cáo Nhà sách
* **Kỹ thuật áp dụng:** `OpenFileDialog` (Tải ảnh bìa), `SaveFileDialog` /
`OpenFileDialog` (Đọc/Ghi file CSV), Cập nhật `StatusStrip` theo thời gian thực.
* **Hình ảnh minh họa Giao diện:**

![Giao diện Bài 2 và Load Ảnh](docs/bai2_main.png)
*Mô tả: Chọn ảnh bìa sách từ đĩa cứng hiển thị lên PictureBox và nạp dữ liệu từ file
CSV.*

---

### 3. Bài 3: Kiến trúc MDI & Xử lý Bất đồng bộ (Async/Await)
* **Kỹ thuật áp dụng:** MDI Container, Quản lý chống mở trùng Form con,
`async/await` chống treo UI, Safe Cross-Thread bằng `this.Invoke()`.
* **Hình ảnh minh họa Giao diện:**

![Giao diện Khung MDI](docs/bai3_mdi.png)
*Mô tả: Khung Form cha MDI điều khiển mở các Form con xếp tầng.*

![Minh họa Async không treo UI](docs/bai3_async.png)
*Mô tả: Tiến trình ngầm chạy tải dữ liệu nặng 5 giây, giao diện vẫn kéo rê và thao tác
bình thường.*

---
