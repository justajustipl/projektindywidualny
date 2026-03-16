using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projekt_silka
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start start = new Start();
            start.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboTraining_SelectedIndexChanged(object sender, EventArgs e)
        {
            listTrainers.Items.Clear();

            if (comboTraining.Text == "Indywidualny")
            {
                listTrainers.Items.Add("Trener Kowalski");
                listTrainers.Items.Add("Trener Nowak");
            }

            if (comboTraining.Text == "W parach")
            {
                listTrainers.Items.Add("Trener Wiśniewski");
            }

            if (comboTraining.Text == "Grupowy")
            {
                listTrainers.Items.Add("Trener Zieliński");
                listTrainers.Items.Add("Trener Kaczmarek");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string trening = comboTraining.Text;
            string trener = listTrainers.SelectedItem?.ToString();
            DateTime data = dateTimePicker1.Value;

            if (trening == "" || trener == null)
            {
                MessageBox.Show("Wybierz trening i trenera!");
                return;
            }

            MessageBox.Show(
                "Zapisano trening:\n" +
                "Typ: " + trening +
                "\nTrener: " + trener +
                "\nData: " + data.ToShortDateString()
            );
        }
    }
}
