using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab7_WinForm_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //rss
        {
            proccessRequest("rss");
        }

        private void button2_Click(object sender, EventArgs e) //atom
        {
            proccessRequest("atom");
        }

        private void proccessRequest(string format)
        {
            var req = (HttpWebRequest)WebRequest.Create(@"http://localhost:8008/FeedService/students/" + textBox1.Text + "/notes?format=" + format);
            var res = (HttpWebResponse)req.GetResponse();
            string content = new StreamReader(res.GetResponseStream()).ReadToEnd();
            richTextBox1.Text = FormatXml(content);
        }

        private string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }
    }
}
