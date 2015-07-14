using System;
using System.Windows.Forms;

namespace SimpleBinding
{
    public partial class Form1 : Form
    {
        private readonly SomeData someData;

        public Form1()
        {
            InitializeComponent();

            someData = new SomeData();

            // (3) create the binding between the control and the view model.
            myControl1.DataBindings.Add("SomeText", someData, "SomeText", false, DataSourceUpdateMode.OnPropertyChanged);

            textBox2.DataBindings.Add("Text", someData, "SomeText", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetMyControl();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SetMyControl();
                e.Handled = true;
            }
        }

        private void SetMyControl()
        {
            myControl1.SomeText = textBox1.Text;
            textBox1.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetSomeData();
        }

        private void SetSomeData()
        {
            someData.SomeText = textBox3.Text;
            textBox3.Text = string.Empty;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SetSomeData();
                e.Handled = true;
            }
        }
    }
}