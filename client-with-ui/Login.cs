using System.Net.Sockets;
using System.Text;

namespace client_with_ui
{
    public partial class Login : Form
    {
        TcpClient client;
        NetworkStream stream;

        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            client = new TcpClient("34.116.253.154", 25565);
            stream = client.GetStream();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Register();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text;
            string password = textBox2.Text;

            string messageString = name + " " + password;
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);           
            stream.Write(messageBytes, 0, messageBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (response == "Login successful")
            {
                MessageBox.Show("Login successful");
                Form form = new Chat(stream, client);
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }        

    }
}
