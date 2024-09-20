using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace client_with_ui
{
    public partial class Chat : Form
    {
        TcpClient client;
        NetworkStream stream;
        public Chat(NetworkStream stream, TcpClient client)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    listView1.Items.Add(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string messageString = richTextBox1.Text;
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
    }
}
