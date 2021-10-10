using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form1 : Form
    {
        ASMXService client;
        //WCFService.WCFSiplexClient WCFclient;
        NetTcpBindingConsole.WCFSiplexClient WCFclient;
        Simplex proxyClient;

        public Form1()
        {
            this.client = new ASMXService();
            this.proxyClient = new Simplex();
            this.WCFclient = new NetTcpBindingConsole.WCFSiplexClient("BasicHttpBinding_IWCFSiplex1");
            InitializeComponent();
        }

        private void GetSum_Click_1(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.Text)
                {
                    case "ASMX ref":
                        result.Text = client.Add(int.Parse(x.Text), int.Parse(y.Text)).ToString();
                        break;
                    case "ASMX proxy":
                        result.Text = proxyClient.Add(int.Parse(x.Text), int.Parse(y.Text)).ToString();
                        break;
                    case "WCF":
                        result.Text = WCFclient.Add(int.Parse(x.Text), int.Parse(y.Text)).ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.Text)
                {
                    case "ASMX ref":
                        result.Text = client.Concat(x.Text, double.Parse(y.Text)).ToString();
                        break;
                    case "ASMX proxy":
                        result.Text = proxyClient.Concat(x.Text, double.Parse(y.Text)).ToString();
                        break;
                    case "WCF":
                        result.Text = WCFclient.Concat(x.Text, double.Parse(y.Text)).ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }
        }

        private void Cav_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.Text)
                {
                    case "ASMX ref":
                        var a1 = client.GetA();
                        a1.s = s1.Text;
                        a1.k = int.Parse(i1.Text);
                        a1.f = float.Parse(d1.Text);

                        var a2 = client.GetA();
                        a2.s = s2.Text;
                        a2.k = int.Parse(i2.Text);
                        a2.f = float.Parse(d2.Text);

                        var result = client.Sum(a1, a2);

                        result_1.Text = result.s;
                        result_2.Text = result.k.ToString();
                        result_3.Text = result.f.ToString();
                        break;
                    case "ASMX proxy":
                        var a1p = proxyClient.GetA();
                        a1p.s = s1.Text;
                        a1p.k = int.Parse(i1.Text);
                        a1p.f = float.Parse(d1.Text);

                        var a2p = proxyClient.GetA();
                        a2p.s = s2.Text;
                        a2p.k = int.Parse(i2.Text);
                        a2p.f = float.Parse(d2.Text);

                        var resultp = proxyClient.Sum(a1p, a2p);

                        result_1.Text = resultp.s;
                        result_2.Text = resultp.k.ToString();
                        result_3.Text = resultp.f.ToString();
                        break;
                    case "WCF":
                        var a1w = new Infrastructure.Model.Lab4.A();
                        a1w.s = s1.Text;
                        a1w.k = int.Parse(i1.Text);
                        a1w.f = float.Parse(d1.Text);

                        var a2w = new Infrastructure.Model.Lab4.A();
                        a2w.s = s2.Text;
                        a2w.k = int.Parse(i2.Text);
                        a2w.f = float.Parse(d2.Text);

                        var resultw = WCFclient.Sum(a1w, a2w);

                        result_1.Text = resultw.s;
                        result_2.Text = resultw.k.ToString();
                        result_3.Text = resultw.f.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                result_1.Text = ex.Message;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
