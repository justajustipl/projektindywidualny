using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt_silka
{
    internal class RegisterForm : Form
    {
        private readonly Database _db;
        public string? RegisteredLogin { get; private set; }

        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private Label lblError;
        private Button btnRegister;
        private Button btnBack;

        private static readonly Color Purple = Color.FromArgb(80, 0, 120);

        public RegisterForm(Database db)
        {
            _db = db;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Mobilna Siłownia – Rejestracja";
            this.Size = new Size(860, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            var lblTitle = new Label
            {
                Text = "Rejestracja nowego klienta",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Purple,
                AutoSize = true,
                Location = new Point(270, 50)
            };

            var lblName = new Label { Text = "Imię i nazwisko:", Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(290, 110) };
            txtName = new TextBox { Font = new Font("Segoe UI", 10), Size = new Size(220, 30), Location = new Point(290, 133) };

            var lblEmail = new Label { Text = "Adres e-mail:", Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(290, 173) };
            txtEmail = new TextBox { Font = new Font("Segoe UI", 10), Size = new Size(220, 30), Location = new Point(290, 196) };

            var lblLogin = new Label { Text = "Login:", Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(290, 236) };
            txtLogin = new TextBox { Font = new Font("Segoe UI", 10), Size = new Size(220, 30), Location = new Point(290, 259) };

            var lblPassword = new Label { Text = "Hasło:", Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(290, 299) };
            txtPassword = new TextBox { Font = new Font("Segoe UI", 10), Size = new Size(220, 30), Location = new Point(290, 322), PasswordChar = '●' };

            var lblConfirm = new Label { Text = "Potwierdź hasło:", Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(290, 362) };
            txtConfirm = new TextBox { Font = new Font("Segoe UI", 10), Size = new Size(220, 30), Location = new Point(290, 385), PasswordChar = '●' };

            lblError = new Label { Text = "", ForeColor = Color.Red, Font = new Font("Segoe UI", 9), AutoSize = true, Location = new Point(290, 425) };

            btnRegister = new Button
            {
                Text = "Zarejestruj się",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(220, 38),
                Location = new Point(290, 450),
                BackColor = Purple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnBack = new Button
            {
                Text = "← Wróć",
                Font = new Font("Segoe UI", 9),
                Size = new Size(100, 30),
                Location = new Point(20, 20),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.Click += (s, e) => this.Close();

            this.AcceptButton = btnRegister;

            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblName, txtName,
                lblEmail, txtEmail,
                lblLogin, txtLogin,
                lblPassword, txtPassword,
                lblConfirm, txtConfirm,
                lblError, btnRegister, btnBack
            });
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirm))
            {
                lblError.Text = "Wypełnij wszystkie pola.";
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                lblError.Text = "Podaj prawidłowy adres e-mail.";
                return;
            }

            if (password != confirm)
            {
                lblError.Text = "Hasła nie są identyczne.";
                return;
            }

            if (password.Length < 4)
            {
                lblError.Text = "Hasło musi mieć co najmniej 4 znaki.";
                return;
            }

            bool success = _db.RegisterNewClient(name, login, password, email);
            if (!success)
            {
                lblError.Text = "Ten login jest już zajęty.";
                return;
            }

            MessageBox.Show(
                $"Konto zostało utworzone!\nMożesz się teraz zalogować.",
                "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RegisteredLogin = login;
            this.Close();
        }
    }
}