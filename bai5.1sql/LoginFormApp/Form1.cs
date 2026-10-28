using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LoginFormApp
{
    public partial class Form1 : Form
    {
        // Chuỗi kết nối trực tiếp tới SQL Server trên máy bạn
        private readonly string connStr = @"Server=(localdb)\MSSQLLocalDB;Database=ql_sinhvien;Integrated Security=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();

            // Thiết lập phím Enter/Esc và ẩn mật khẩu ban đầu
            this.AcceptButton = GetLoginButton();
            this.CancelButton = GetExitButton();

            TextBox txtPass = GetPasswordTextBox();
            if (txtPass != null)
            {
                txtPass.UseSystemPasswordChar = true;
            }

            // Gán sự kiện cho CheckBox ẩn/hiện mật khẩu nếu có
            CheckBox chkPass = GetCheckBox();
            if (chkPass != null)
            {
                chkPass.CheckedChanged += ChkShowPassword_CheckedChanged;
            }

            // Gán sự kiện Click cho nút Đăng nhập và Nút Thoát
            Button btnLog = GetLoginButton();
            if (btnLog != null)
            {
                btnLog.Click -= btnLogin_Click;
                btnLog.Click += btnLogin_Click;
            }

            Button btnExit = GetExitButton();
            if (btnExit != null)
            {
                btnExit.Click -= btnExit_Click;
                btnExit.Click += btnExit_Click;
            }
        }

        // 1. Hàm tự động lấy Control Tên đăng nhập (txtUsername / textBox1)
        private TextBox GetUsernameTextBox()
        {
            if (Controls.Find("txtUsername", true).Length > 0) return Controls.Find("txtUsername", true)[0] as TextBox;
            if (Controls.Find("textBox1", true).Length > 0) return Controls.Find("textBox1", true)[0] as TextBox;
            return null;
        }

        // 2. Hàm tự động lấy Control Mật khẩu (txtPassword / textBox2)
        private TextBox GetPasswordTextBox()
        {
            if (Controls.Find("txtPassword", true).Length > 0) return Controls.Find("txtPassword", true)[0] as TextBox;
            if (Controls.Find("textBox2", true).Length > 0) return Controls.Find("textBox2", true)[0] as TextBox;
            return null;
        }

        // 3. Hàm tự động lấy Control Nút Đăng nhập (btnLogin / button1)
        private Button GetLoginButton()
        {
            if (Controls.Find("btnLogin", true).Length > 0) return Controls.Find("btnLogin", true)[0] as Button;
            if (Controls.Find("button1", true).Length > 0) return Controls.Find("button1", true)[0] as Button;
            return null;
        }

        // 4. Hàm tự động lấy Control Nút Thoát (btnExit / button2 / button3)
        private Button GetExitButton()
        {
            if (Controls.Find("btnExit", true).Length > 0) return Controls.Find("btnExit", true)[0] as Button;
            if (Controls.Find("button3", true).Length > 0) return Controls.Find("button3", true)[0] as Button;
            if (Controls.Find("button2", true).Length > 0) return Controls.Find("button2", true)[0] as Button;
            return null;
        }

        // 5. Hàm tự động lấy Control CheckBox (chkShowPassword / checkBox1)
        private CheckBox GetCheckBox()
        {
            if (Controls.Find("chkShowPassword", true).Length > 0) return Controls.Find("chkShowPassword", true)[0] as CheckBox;
            if (Controls.Find("checkBox1", true).Length > 0) return Controls.Find("checkBox1", true)[0] as CheckBox;
            return null;
        }

        // Xử lý Sự kiện Ẩn / Hiện mật khẩu
        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            TextBox txtPass = GetPasswordTextBox();
            if (chk != null && txtPass != null)
            {
                txtPass.UseSystemPasswordChar = !chk.Checked;
            }
        }

        // Xử lý Sự kiện Nút Đăng nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            TextBox txtUser = GetUsernameTextBox();
            TextBox txtPass = GetPasswordTextBox();

            // 1. Kiểm tra dữ liệu đầu vào
            bool hasError = false;
            if (txtUser == null || string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hasError = true;
            }
            else if (txtPass == null || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hasError = true;
            }

            if (hasError) return;

            // 2. Hash mật khẩu nhập vào bằng SHA-256
            string passwordHash = HashSHA256(txtPass.Text.Trim());

            // 3. Xác thực dữ liệu với SQL Server
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM dbo.TaiKhoan WHERE Username = @Username AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUser.Text.Trim());
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (result > 0)
                        {
                            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Không thể kết nối SQL Server!\nChi tiết: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý Sự kiện Nút Thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Hàm Mã hóa mật khẩu SHA-256
        private string HashSHA256(string text)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}