using Forms.ASMXServiceLab4;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form1 : Form
    {
        ASMXServiceLab4.SimplexSoapClient client;

        public Form1()
        {
            this.client = new ASMXServiceLab4.SimplexSoapClient("SimplexSoap");
            InitializeComponent();
        }

        private void GetSum_Click_1(object sender, EventArgs e)
        {
            try
            {
                result.Text = client.Add(int.Parse(x.Text), int.Parse(y.Text)).ToString();
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
                result.Text = client.Concat(x.Text, double.Parse(y.Text)).ToString();
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

                var msu1 = new A();
                msu1.s = s1.Text;
                msu1.k = int.Parse(i1.Text);
                msu1.f = float.Parse(d1.Text);

                var msu2 = new A();
                msu2.s = s2.Text;
                msu2.k = int.Parse(i2.Text);
                msu2.f = float.Parse(d2.Text);

                var result = client.Sum(msu1, msu2);

                result_1.Text = result.s;
                result_2.Text = result.k.ToString();
                result_3.Text = result.f.ToString();
            }
            catch (Exception ex)
            {
                result_1.Text = ex.Message;
            }
        }
    }
}
