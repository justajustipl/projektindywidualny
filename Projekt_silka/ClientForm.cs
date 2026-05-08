using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt_silka
{
    public partial class ClientForm : Form
    {
        private readonly Database _db;
        private readonly int _clientId;
        private readonly string _clientName;

        private Label lblAvailable;
        private ComboBox comboTraining;
        private Label lblDetails;
        private ListBox listTrainers;
        private Button buttonSave;

        private Label lblMyReg;
        private ListBox listMyRegistrations;
        private Button buttonCancel;

        private Label lblWelcome;
        private Button btnBack;

        private static readonly Color Purple = Color.FromArgb(80, 0, 120);
        private static readonly Color LightPurple = Color.FromArgb(120, 40, 180);
        private static readonly Color BgColor = Color.FromArgb(245, 245, 250);
        private static readonly Color PanelColor = Color.White;

        public ClientForm(Database db, int clientId, string clientName)
        {
            InitializeComponent();
            _db = db;
            _clientId = clientId;
            _clientName = clientName;
            BuildUI();
            LoadAvailableTrainings();
            LoadMyRegistrations();
        }

        private void BuildUI()
        {
            this.Size = new Size(900, 570);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = BgColor;
            this.Text = $"Mobilna siłownia - Panel użytkownika ({_clientName})";

            var topBar = new Panel
            {
                BackColor = Purple,
                Dock = DockStyle.Top,
                Height = 60
            };

            lblWelcome = new Label
            {
                Text = $"Witaj, {_clientName}!",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            btnBack = new Button
            {
                Text = "← Wyloguj",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                BackColor = LightPurple,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 34),
                Location = new Point(760, 13),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => this.Close();

            topBar.Controls.Add(lblWelcome);
            topBar.Controls.Add(btnBack);

            // ── LEFT PANEL ───────────────────────────────────────
            var leftPanel = new Panel
            {
                BackColor = PanelColor,
                Size = new Size(400, 450),
                Location = new Point(20, 80),
            };
            leftPanel.Paint += PanelBorder;

            var lblLeft = new Label
            {
                Text = "DOSTĘPNE TRENINGI",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Purple,
                AutoSize = true,
                Location = new Point(15, 15)
            };

            lblAvailable = new Label
            {
                Text = "Wybierz trening:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 45)
            };

            comboTraining = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(365, 30),
                Location = new Point(15, 68),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            comboTraining.SelectedIndexChanged += comboTraining_SelectedIndexChanged;

            lblDetails = new Label
            {
                Text = "Szczegóły:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 115)
            };

            listTrainers = new ListBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(365, 130),
                Location = new Point(15, 138),
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = SelectionMode.None
            };

            buttonSave = new Button
            {
                Text = "✓  Zapisz się na trening",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(365, 40),
                Location = new Point(15, 390),
                BackColor = Purple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.Click += buttonSave_Click;

            leftPanel.Controls.AddRange(new Control[]
            {
                lblLeft, lblAvailable, comboTraining,
                lblDetails, listTrainers, buttonSave
            });

            // ── RIGHT PANEL ──────────────────────────────────────
            var rightPanel = new Panel
            {
                BackColor = PanelColor,
                Size = new Size(420, 450),
                Location = new Point(440, 80)
            };
            rightPanel.Paint += PanelBorder;

            var lblRight = new Label
            {
                Text = "MOJE ZAPISY",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Purple,
                AutoSize = true,
                Location = new Point(15, 15)
            };

            lblMyReg = new Label
            {
                Text = "Wybierz trening aby go zmienić lub wypisać:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 45)
            };

            listMyRegistrations = new ListBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(385, 270),
                Location = new Point(15, 68),
                BorderStyle = BorderStyle.FixedSingle
            };

            var buttonReschedule = new Button
            {
                Text = "↔  Zmień termin",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(385, 40),
                Location = new Point(15, 350),
                BackColor = Color.FromArgb(20, 100, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            buttonReschedule.FlatAppearance.BorderSize = 0;
            buttonReschedule.Click += buttonReschedule_Click;

            buttonCancel = new Button
            {
                Text = "✕  Wypisz się z treningu",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(385, 40),
                Location = new Point(15, 400),
                BackColor = Color.FromArgb(180, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.Click += buttonCancel_Click;

            rightPanel.Controls.AddRange(new Control[]
            {
                lblRight, lblMyReg, listMyRegistrations,
                buttonReschedule, buttonCancel
            });

            this.Controls.AddRange(new Control[]
            {
                topBar, leftPanel, rightPanel
            });
        }

        private void PanelBorder(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var panel = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                Color.FromArgb(220, 220, 230), ButtonBorderStyle.Solid);
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadAvailableTrainings()
        {
            comboTraining.Items.Clear();
            var trainings = _db.GetAllTrainings();
            foreach (var t in trainings)
                comboTraining.Items.Add(t);
        }

        private void LoadMyRegistrations()
        {
            listMyRegistrations.Items.Clear();
            var registrations = _db.GetClientRegistrations(_clientId);
            if (registrations.Count == 0)
                listMyRegistrations.Items.Add("Brak zapisów.");
            else
                foreach (var t in registrations)
                    listMyRegistrations.Items.Add(t);
        }

        private void comboTraining_SelectedIndexChanged(object sender, EventArgs e)
        {
            listTrainers.Items.Clear();
            if (comboTraining.SelectedItem is Training t)
            {
                listTrainers.Items.Add($"  Trening:   {t.Title}");
                listTrainers.Items.Add($"  Trener:    {t.EmployeeName}");
                listTrainers.Items.Add($"  Data:      {t.Date}");
                listTrainers.Items.Add($"  Godzina:   {t.Time}");
                listTrainers.Items.Add($"  Miejsca:   {t.RegisteredCount}/{t.MaxSlots}");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboTraining.SelectedItem is not Training selected)
            {
                MessageBox.Show("Wybierz trening z listy!", "Uwaga",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = _db.RegisterClient(_clientId, selected.Id);

            if (!success)
            {
                var regs = _db.GetClientRegistrations(_clientId);
                bool already = regs.Exists(r => r.Id == selected.Id);
                MessageBox.Show(
                    already ? "Jesteś już zapisany na ten trening."
                            : "Trening jest pełny! Brak wolnych miejsc.",
                    "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(
                $"Zapisano!\n\n{selected.Title}\n{selected.Date} o {selected.Time}",
                "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadAvailableTrainings();
            LoadMyRegistrations();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (listMyRegistrations.SelectedItem is not Training selected)
            {
                MessageBox.Show("Wybierz trening z listy 'Moje zapisy'.", "Uwaga",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Wypisać się z:\n{selected.Title} ({selected.Date} {selected.Time})?",
                "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _db.CancelRegistration(_clientId, selected.Id);
                MessageBox.Show("Wypisano z treningu.", "Gotowe",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAvailableTrainings();
                LoadMyRegistrations();
            }
        }

        private void buttonReschedule_Click(object sender, EventArgs e)
        {
            if (listMyRegistrations.SelectedItem is not Training oldTraining)
            {
                MessageBox.Show(
                    "Najpierw wybierz trening z 'Moje zapisy' który chcesz zmienić.",
                    "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboTraining.SelectedItem is not Training newTraining)
            {
                MessageBox.Show(
                    "Następnie wybierz nowy termin z listy 'Dostępne treningi'.",
                    "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (oldTraining.Id == newTraining.Id)
            {
                MessageBox.Show(
                    "Wybrałeś ten sam trening. Wybierz inny termin.",
                    "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Zmienić termin?\n\n" +
                $"Z:  {oldTraining.Title} ({oldTraining.Date} {oldTraining.Time})\n" +
                $"Na: {newTraining.Title} ({newTraining.Date} {newTraining.Time})",
                "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool success = _db.RegisterClient(_clientId, newTraining.Id);
                if (!success)
                {
                    MessageBox.Show(
                        "Nowy termin jest pełny lub jesteś już na niego zapisany.",
                        "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _db.CancelRegistration(_clientId, oldTraining.Id);

                MessageBox.Show(
                    $"Termin zmieniony!\n\n{newTraining.Title}\n{newTraining.Date} o {newTraining.Time}",
                    "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAvailableTrainings();
                LoadMyRegistrations();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}