using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        
        string serverIp = "localhost";
        int port = 8080;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient(serverIp, port);

            int byteCount = Encoding.ASCII.GetByteCount(textBox1.Text);
            
            byte[] sendBuffer = new byte[byteCount];
            byte[] recieveData = new byte[100];

            sendBuffer = Encoding.ASCII.GetBytes(textBox1.Text);
            NetworkStream stream = client.GetStream();
            stream.Write(sendBuffer,0,sendBuffer.Length);
            stream.Read(recieveData, 0, recieveData.Length);
            StringBuilder msg = new StringBuilder();

            foreach (byte b in recieveData)
            {
                if (b.Equals(00))
                {
                    break;
                }
                else
                    msg.Append(Convert.ToChar(b).ToString());
            }

            label1.Text = msg.ToString() + msg.Length;
            stream.Close();
        }
    }
}
