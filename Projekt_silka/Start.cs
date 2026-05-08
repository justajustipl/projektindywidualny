namespace Projekt_silka
{
    public partial class Start : Form
    {
        private Database _db = new Database();

        public Start()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = new LoginForm(_db, UserRole.Client);
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var login = new LoginForm(_db, UserRole.Employee);
            login.ShowDialog();
        }
    }
}