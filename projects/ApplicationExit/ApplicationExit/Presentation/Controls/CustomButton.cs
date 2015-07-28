using System;
using System.Windows.Forms;

namespace ApplicationExit.Presentation.Controls
{
    partial class CustomButton : Button
    {
        private ButtonViewModelBase viewModel;

        public ButtonViewModelBase ViewModel
        {
            get { return viewModel; }
            set
            {
                DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                    DataBindings.Add("Enabled", viewModel, "Enabled");
            }
        }

        public CustomButton()
        {
            InitializeComponent();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseEnter();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseLeave();

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.Clicked();

            base.OnClick(e);
        }
    }
}
