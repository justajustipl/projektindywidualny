using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projekt_silka
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start start = new Start();
            start.Show();
            this.Close();
        }

        private void checkedListBoxDays_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string trener = comboTrainer.Text;
            string trening = comboTrainingType.Text;
            DateTime data = dateTimePicker1.Value;

            if (trener == "" || trening == "")
            {
                MessageBox.Show("Wybierz trenera i typ treningu!");
                return;
            }
            string wpis =
            trener + " | " +
            trening + " | " +
            data.ToShortDateString();

            listTrainings.Items.Add(wpis);

            MessageBox.Show(
                "Dodano dostępność:\n" +
                "Trener: " + trener +
                "\nTyp treningu: " + trening +
                "\nData: " + data.ToShortDateString()
            );
            
        }

        private void listTrainings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
