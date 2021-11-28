using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WS_DEFModel;

namespace Lab6_client
{
    public partial class Form1 : Form
    {
        private WS_DEFEntities client;
        public Form1()
        {
            client = new WS_DEFEntities(new Uri("https://localhost:44353/WcfDataService1.svc"));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.resultBox.Text = "";
            foreach (var student in client.Student.AsEnumerable())
            {
                this.resultBox.Text += string.Format("Id {0}: {1}\n", student.Id, student.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.resultBox.Text = "";
            foreach (var note in client.Note.AsEnumerable())
            {
                this.resultBox.Text += string.Format("Id {0}: Note '{1}' on exam '{2}' (Student ID: {3})\n", note.Id, note.Note1, note.Subj, note.StudentId);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.AddToStudent(new Student() { Name = textBox1.Text });
            client.SaveChanges();
            //client.Execute<Student>
            //    (new Uri($"https://localhost:44353/WcfDataService1.svc/InsertStudent?name=\'{textBox1.Text}\'"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client.Execute<Note>
                (new Uri($"https://localhost:44353/WcfDataService1.svc/InsertNote?" +
                $"subject=\'{textBox1.Text}\'&" +
                $"note1={textBox2.Text}&" +
                $"studentId={textBox3.Text}"
                ));
        }
    }
}
