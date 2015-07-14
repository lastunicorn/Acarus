using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleBinding
{
    public partial class MyControl : UserControl
    {
        private string someText;

        // (2) create a event with the naming convention: <Propertyname>Changed.
        public event EventHandler SomeTextChanged;

        public string SomeText
        {
            get { return someText; }
            set
            {
                someText = value;

                // (2') raise the <Propertyname>Changed event when the value of the property changes.
                OnSomeTextChanged();

                Invalidate();
            }
        }

        public MyControl()
        {
            InitializeComponent();

            SomeText = "alez";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Font font = new Font("Arial", 20))
            {
                e.Graphics.DrawString(SomeText, font, Brushes.CornflowerBlue, ClientRectangle);
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected virtual void OnSomeTextChanged()
        {
            EventHandler handler = SomeTextChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}