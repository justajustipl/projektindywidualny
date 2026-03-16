namespace Projekt_silka
{
    partial class EmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            comboTrainer = new ComboBox();
            label2 = new Label();
            comboTrainingType = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            buttonAdd = new Button();
            listTrainings = new ListBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(713, 415);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Powrót";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(248, 40);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 1;
            label1.Text = "Wybierz trenera:";
            label1.Click += label1_Click;
            // 
            // comboTrainer
            // 
            comboTrainer.FormattingEnabled = true;
            comboTrainer.Items.AddRange(new object[] { "Kowalski", "Nowak", "Wiśniewski" });
            comboTrainer.Location = new Point(236, 84);
            comboTrainer.Name = "comboTrainer";
            comboTrainer.Size = new Size(116, 23);
            comboTrainer.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(234, 136);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 3;
            label2.Text = "Wybierz typ treningu:";
            // 
            // comboTrainingType
            // 
            comboTrainingType.FormattingEnabled = true;
            comboTrainingType.Items.AddRange(new object[] { "Indywidualny", "W parze", "Grupowy" });
            comboTrainingType.Location = new Point(234, 180);
            comboTrainingType.Name = "comboTrainingType";
            comboTrainingType.Size = new Size(121, 23);
            comboTrainingType.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(181, 276);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(226, 23);
            dateTimePicker1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(255, 232);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 6;
            label3.Text = "Wybierz datę:";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(237, 328);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(114, 23);
            buttonAdd.TabIndex = 7;
            buttonAdd.Text = "Zapisz dostepność";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // listTrainings
            // 
            listTrainings.FormattingEnabled = true;
            listTrainings.Location = new Point(525, 123);
            listTrainings.Name = "listTrainings";
            listTrainings.Size = new Size(227, 214);
            listTrainings.TabIndex = 8;
            listTrainings.SelectedIndexChanged += listTrainings_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(591, 84);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 9;
            label4.Text = "Dodane treningi:";
            // 
            // EmployeeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(listTrainings);
            Controls.Add(buttonAdd);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(comboTrainingType);
            Controls.Add(label2);
            Controls.Add(comboTrainer);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "EmployeeForm";
            Text = "Mobilna siłownia - Panel pracownika";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private ComboBox comboTrainer;
        private Label label2;
        private ComboBox comboTrainingType;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private Button buttonAdd;
        private ListBox listTrainings;
        private Label label4;
    }
}