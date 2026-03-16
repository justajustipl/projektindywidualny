namespace Projekt_silka
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientForm okno = new ClientForm();
            okno.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            EmployeeForm okno = new EmployeeForm();
            okno.Show();
            this.Hide();
        }
    }
}