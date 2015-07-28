using System.Drawing;
using System.Windows.Forms;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    partial class TheDataView : UserControl
    {
        private TheDataViewModel viewModel;

        public TheDataViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                labelTheData.DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                {
                    Binding binding1 = labelTheData.CreateBinding(x => x.BackColor, viewModel, x => x.IsChanged, true, DataSourceUpdateMode.Never);
                    binding1.Format += (s, e) => e.Value = GetBackColor((bool)e.Value);

                    Binding binding2 = labelTheData.CreateBinding(x => x.ForeColor, viewModel, x => x.IsChanged, true, DataSourceUpdateMode.Never);
                    binding2.Format += (s, e) => e.Value = GetForeColor((bool)e.Value);

                    Binding binding3 = labelTheData.CreateBinding(x => x.Text, viewModel, x => x.IsChanged, true, DataSourceUpdateMode.Never);
                    binding3.Format += (s, e) => e.Value = GetLabelText((bool)e.Value);
                }
            }
        }

        private static Color GetBackColor(bool value)
        {
            return value ? Color.IndianRed : Color.LawnGreen;
        }

        private static Color GetForeColor(bool value)
        {
            return value ? Color.Maroon : Color.Green;
        }

        private static string GetLabelText(bool b)
        {
            return b ? "Data is Changed" : "Data is Saved";
        }

        public TheDataView()
        {
            InitializeComponent();
        }
    }
}
