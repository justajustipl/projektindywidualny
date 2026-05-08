namespace Projekt_silka
{
    partial class Start
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            button1 = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();

            // button1 — Panel użytkownika
            button1.Location = new Point(319, 185);
            button1.Name = "button1";
            button1.Size = new Size(200, 48);
            button1.TabIndex = 0;
            button1.Text = "Panel użytkownika";
            button1.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            button1.BackColor = System.Drawing.Color.FromArgb(80, 0, 120);
            button1.ForeColor = System.Drawing.Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Cursor = Cursors.Hand;
            button1.Click += button1_Click;

            // button2 — Panel pracownika
            button2.Location = new Point(319, 253);
            button2.Name = "button2";
            button2.Size = new Size(200, 48);
            button2.TabIndex = 1;
            button2.Text = "Panel pracownika";
            button2.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            button2.BackColor = System.Drawing.Color.FromArgb(120, 40, 180);
            button2.ForeColor = System.Drawing.Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Cursor = Cursors.Hand;
            button2.Click += button2_Click;

            // pictureBox1 — logo
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(40, 90);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(240, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;

            // label1 — Wybierz
            label1.AutoSize = true;
            label1.Location = new Point(355, 150);
            label1.Name = "label1";
            label1.Text = "Wybierz panel:";
            label1.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.FromArgb(80, 0, 120);
            label1.TabIndex = 3;
            label1.Click += label1_Click;

            // Start form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Start";
            Text = "Mobilna siłownia";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        private Label label1;
    }
}