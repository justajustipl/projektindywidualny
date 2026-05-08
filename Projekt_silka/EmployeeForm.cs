using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt_silka
{
    public partial class EmployeeForm : Form
    {
        private readonly Database _db;
        private readonly int _employeeId;
        private readonly string _employeeName;

        private Label lblWelcome;
        private Button btnBack;

        private ComboBox comboTrainingType;
        private ComboBox comboRoom;
        private DateTimePicker datePickerDate;
        private DateTimePicker datePickerTime;
        private Button buttonAdd;

        private ListBox listTrainings;
        private Button buttonDelete;

        private static readonly Color Purple = Color.FromArgb(80, 0, 120);
        private static readonly Color LightPurple = Color.FromArgb(120, 40, 180);
        private static readonly Color BgColor = Color.FromArgb(245, 245, 250);
        private static readonly Color PanelColor = Color.White;

        public EmployeeForm(Database db, int employeeId, string employeeName)
        {
            InitializeComponent();
            _db = db;
            _employeeId = employeeId;
            _employeeName = employeeName;
            BuildUI();
            LoadMyTrainings();
        }

        private void BuildUI()
        {
            this.Size = new Size(900, 570);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = BgColor;
            this.Text = $"Mobilna siłownia - Panel pracownika ({_employeeName})";

            // ── TOP BAR ──────────────────────────────────────────
            var topBar = new Panel
            {
                BackColor = Purple,
                Dock = DockStyle.Top,
                Height = 60
            };

            lblWelcome = new Label
            {
                Text = $"Panel pracownika — {_employeeName}",
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
                Size = new Size(380, 450),
                Location = new Point(20, 80)
            };
            leftPanel.Paint += PanelBorder;

            var lblLeft = new Label
            {
                Text = "DODAJ TRENING",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Purple,
                AutoSize = true,
                Location = new Point(15, 15)
            };

            var lblType = new Label
            {
                Text = "Typ treningu:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 50)
            };

            comboTrainingType = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(345, 30),
                Location = new Point(15, 73),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            comboTrainingType.Items.AddRange(new object[] { "Indywidualny", "W parze", "Grupowy" });
            comboTrainingType.SelectedIndexChanged += (s, e) =>
            {
                comboRoom.Enabled = comboTrainingType.Text == "Grupowy";
                if (!comboRoom.Enabled) comboRoom.SelectedIndex = -1;
            };

            var lblRoom = new Label
            {
                Text = "Sala (tylko dla Grupowy):",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 110)
            };

            comboRoom = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(345, 30),
                Location = new Point(15, 133),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            comboRoom.Items.AddRange(new object[] { "Sala 1", "Sala 2", "Sala 3" });

            var lblDate = new Label
            {
                Text = "Data treningu:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 205)
            };

            datePickerDate = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(345, 30),
                Location = new Point(15, 228),
                Format = DateTimePickerFormat.Short
            };

            var lblTime = new Label
            {
                Text = "Godzina treningu:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 280)
            };

            datePickerTime = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(345, 30),
                Location = new Point(15, 303),
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true
            };

            var lblSlots = new Label
            {
                Text = "Maks. uczestników: Ind.=1 / Para=2 / Grup.=15",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 355)
            };

            buttonAdd = new Button
            {
                Text = "✓  Dodaj trening",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(345, 40),
                Location = new Point(15, 390),
                BackColor = Purple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            buttonAdd.FlatAppearance.BorderSize = 0;
            buttonAdd.Click += buttonAdd_Click;

            leftPanel.Controls.AddRange(new Control[]
            {
                lblLeft, lblType, comboTrainingType,
                lblRoom, comboRoom,
                lblDate, datePickerDate,
                lblTime, datePickerTime,
                lblSlots, buttonAdd
            });

            // ── RIGHT PANEL ──────────────────────────────────────
            var rightPanel = new Panel
            {
                BackColor = PanelColor,
                Size = new Size(460, 450),
                Location = new Point(420, 80)
            };
            rightPanel.Paint += PanelBorder;

            var lblRight = new Label
            {
                Text = "MÓJ HARMONOGRAM",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Purple,
                AutoSize = true,
                Location = new Point(15, 15)
            };

            var lblSub = new Label
            {
                Text = "Zaplanowane treningi:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 45)
            };

            listTrainings = new ListBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(425, 320),
                Location = new Point(15, 68),
                BorderStyle = BorderStyle.FixedSingle
            };

            buttonDelete = new Button
            {
                Text = "✕  Usuń trening",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(425, 40),
                Location = new Point(15, 390),
                BackColor = Color.FromArgb(180, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.Click += buttonDelete_Click;

            rightPanel.Controls.AddRange(new Control[]
            {
                lblRight, lblSub, listTrainings, buttonDelete
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

        private void EmployeeForm_Load(object sender, EventArgs e) { }

        private void LoadMyTrainings()
        {
            listTrainings.Items.Clear();
            var trainings = _db.GetEmployeeTrainings(_employeeId);
            if (trainings.Count == 0)
                listTrainings.Items.Add("Brak zaplanowanych treningów.");
            else
                foreach (var t in trainings)
                    listTrainings.Items.Add(t);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboTrainingType.SelectedItem == null)
            {
                MessageBox.Show("Wybierz typ treningu!", "Uwaga",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trening = comboTrainingType.Text;
            string data = datePickerDate.Value.ToString("yyyy-MM-dd");
            string godzina = datePickerTime.Value.ToString("HH:mm");
            string room = "";

            if (trening == "Grupowy")
            {
                if (comboRoom.SelectedItem == null)
                {
                    MessageBox.Show("Wybierz salę dla treningu grupowego!", "Uwaga",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                room = comboRoom.Text;

                if (_db.IsRoomTaken(room, data, godzina))
                {
                    MessageBox.Show(
                        $"Sala '{room}' jest już zajęta o {godzina} w dniu {data}!\nWybierz inną salę lub godzinę.",
                        "Konflikt sali", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int maxSlots = trening switch
            {
                "Indywidualny" => 1,
                "W parze" => 2,
                "Grupowy" => 15,
                _ => 10
            };

            _db.AddTraining(trening, _employeeId, data, godzina, maxSlots, room);
            _db.AddAvailability(_employeeId, data, "08:00", "20:00");

            string roomInfo = room != "" ? $"\nSala: {room}" : "";
            MessageBox.Show(
                $"Dodano trening:\n\n{trening}\n{data} o {godzina}{roomInfo}",
                "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadMyTrainings();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listTrainings.SelectedItem is not Training selected)
            {
                MessageBox.Show("Wybierz trening z listy aby go usunąć.", "Uwaga",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Usunąć trening:\n{selected.Title} ({selected.Date} {selected.Time})?",
                "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _db.DeleteTraining(selected.Id);
                MessageBox.Show("Trening usunięty.", "Gotowe",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMyTrainings();
            }
        }

        private void button1_Click(object sender, EventArgs e) { }
        private void listTrainings_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void checkedListBoxDays_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}