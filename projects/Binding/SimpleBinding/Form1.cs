using System;
using System.Windows.Forms;

namespace SimpleBinding
{
    public partial class Form1 : Form
    {
        private readonly SomeData viewModel;

        public Form1()
        {
            InitializeComponent();

            viewModel = new SomeData();

            // (3) create the binding between the control and the view model.
            myControl1.DataBindings.Add("SomeText", viewModel, "SomeText", false, DataSourceUpdateMode.OnPropertyChanged);

            textBox2.DataBindings.Add("Text", viewModel, "SomeText", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetMyControl();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
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
    }
}