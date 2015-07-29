// Acarus
// Copyright (C) 2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Drawing;
using System.Windows.Forms;
using ApplicationExit.WinForms.Presentation.Common;

namespace ApplicationExit.WinForms.Presentation.Main
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
                    binding1.Format += (s, e) => e.Value = GetBackColor((bool) e.Value);

                    Binding binding2 = labelTheData.CreateBinding(x => x.ForeColor, viewModel, x => x.IsChanged, true, DataSourceUpdateMode.Never);
                    binding2.Format += (s, e) => e.Value = GetForeColor((bool) e.Value);

                    Binding binding3 = labelTheData.CreateBinding(x => x.Text, viewModel, x => x.IsChanged, true, DataSourceUpdateMode.Never);
                    binding3.Format += (s, e) => e.Value = GetLabelText((bool) e.Value);
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

        private static string GetLabelText(bool value)
        {
            return value ? "Data is Changed" : "Data is Saved";
        }

        public TheDataView()
        {
            InitializeComponent();
        }
    }
}