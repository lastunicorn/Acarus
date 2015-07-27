using System;
using System.Windows.Forms;

namespace ApplicationExit.Presentation
{
    partial class CustomButton : Button
    {
        private IButtonModel model;

        public IButtonModel Model
        {
            get { return model; }
            set
            {
                DataBindings.Clear();

                model = value;

                if (model != null)
                    DataBindings.Add("Enabled", model, "Enabled");
            }
        }

        public CustomButton()
        {
            InitializeComponent();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (Model != null)
                Model.MouseEnter();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Model != null)
                Model.MouseLeave();

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (Model != null)
                Model.Clicked();

            base.OnClick(e);
        }
    }
}
