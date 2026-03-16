namespace Projekt_silka
{
    partial class ClientForm
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
            Back_Client = new Button();
            comboTraining = new ComboBox();
            label1 = new Label();
            listTrainers = new ListBox();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            buttonSave = new Button();
            SuspendLayout();
            // 
            // Back_Client
            // 
            Back_Client.Location = new Point(713, 415);
            Back_Client.Name = "Back_Client";
            Back_Client.Size = new Size(75, 23);
            Back_Client.TabIndex = 0;
            Back_Client.Text = "Powrót";
            Back_Client.UseVisualStyleBackColor = true;
            Back_Client.Click += button1_Click;
            // 
            // comboTraining
            // 
            comboTraining.FormattingEnabled = true;
            comboTraining.Items.AddRange(new object[] { "Indywidualny", "W parach", "Grupowy" });
            comboTraining.Location = new Point(293, 62);
            comboTraining.Name = "comboTraining";
            comboTraining.Size = new Size(192, 23);
            comboTraining.TabIndex = 1;
            comboTraining.SelectedIndexChanged += comboTraining_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(321, 44);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 2;
            label1.Text = "Wybierz rodzaj treningu:";
            label1.Click += label1_Click;
            // 
            // listTrainers
            // 
            listTrainers.FormattingEnabled = true;
            listTrainers.Location = new Point(339, 137);
            listTrainers.Name = "listTrainers";
            listTrainers.Size = new Size(106, 124);
            listTrainers.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(339, 119);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 4;
            label2.Text = "Dostępni trenerzy:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(281, 327);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(229, 23);
            dateTimePicker1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(352, 309);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 6;
            label3.Text = "Wybierz datę:";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(355, 392);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 7;
            buttonSave.Text = "Zapisz";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonSave);
            Controls.Add(label3);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(listTrainers);
            Controls.Add(label1);
            Controls.Add(comboTraining);
            Controls.Add(Back_Client);
            Name = "ClientForm";
            Text = "Mobilna siłownia - Panel użytkownika ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Back_Client;
        private ComboBox comboTraining;
        private Label label1;
        private ListBox listTrainers;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private Button buttonSave;
    }
}