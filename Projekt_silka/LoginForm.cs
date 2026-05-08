using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt_silka
{
    public enum UserRole { Client, Employee }

    internal class LoginForm : Form
    {
        private readonly Database _db;
        private readonly UserRole _role;

        private Label lblTitle;
        private Label lblLogin;
        private Label lblPassword;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnBack;
        private Label lblError;

        public LoginForm(Database db, UserRole role)
        {
            _db = db;
            _role = role;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Mobilna Siłownia – Logowanie";
            this.Size = new Size(860, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            lblTitle = new Label
            {
                Text = _role == UserRole.Client ? "Panel użytkownika" : "Panel pracownika",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 0, 120),
                AutoSize = true,
                Location = new Point(320, 80)
            };

            lblLogin = new Label
            {
                Text = "Login:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(290, 160)
            };
            txtLogin = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(220, 30),
                Location = new Point(290, 185)
            };

            lblPassword = new Label
            {
                Text = "Hasło:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(290, 225)
            };
            txtPassword = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(220, 30),
                Location = new Point(290, 250),
                PasswordChar = '●'
            };

            lblError = new Label
            {
                Text = "",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(290, 290)
            };

            btnLogin = new Button
            {
                Text = "Zaloguj się",
                Font = new Font("Segoe UI", 10),
                Size = new Size(220, 38),
                Location = new Point(290, 315),
                BackColor = Color.FromArgb(80, 0, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

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

            this.AcceptButton = btnLogin;

            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblLogin, txtLogin,
                lblPassword, txtPassword,
                lblError, btnLogin, btnBack
            });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Wypełnij wszystkie pola.";
                return;
            }

            if (_role == UserRole.Client)
            {
                var result = _db.LoginClient(login, password);
                if (result == null)
                {
                    lblError.Text = "Nieprawidłowy login lub hasło.";
                    return;
                }
                this.Hide();
                var panel = new ClientForm(_db, result.Value.id, result.Value.name);
                panel.ShowDialog();
                this.Close();
            }
            else
            {
                var result = _db.LoginEmployee(login, password);
                if (result == null)
                {
                    lblError.Text = "Nieprawidłowy login lub hasło.";
                    return;
                }
                this.Hide();
                var panel = new EmployeeForm(_db, result.Value.id, result.Value.name);
                panel.ShowDialog();
                this.Close();
            }
        }
    }
}