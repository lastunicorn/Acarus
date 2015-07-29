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

using System;
using System.Threading;
using System.Windows.Forms;
using ApplicationExit.WinForms.Main;

namespace ApplicationExit.WinForms
{
    internal class UserInterface : IUserInterface
    {
        private readonly SynchronizationContext synchronizationContext;

        public MainForm MainForm { get; set; }

        public UserInterface()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form form = new Form();
            form.Dispose();

            synchronizationContext = SynchronizationContext.Current;
        }

        public void Run()
        {
            Application.Run(MainForm);
        }

        public void Exit()
        {
            ExecuteOnMainThread(() => Application.Exit());
        }

        public void DisplayInfo(string text)
        {
            ExecuteOnMainThread(() => { MessageBox.Show(MainForm, text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); });
        }

        public bool? DisplayYesNoQuestion(string question, string title)
        {
            bool? result = null;

            ExecuteOnMainThread(() =>
            {
                DialogResult dialogResult = MessageBox.Show(MainForm, question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (dialogResult == DialogResult.Cancel)
                    result = null;

                result = dialogResult == DialogResult.Yes;
            });

            return result;
        }

        private void ExecuteOnMainThread(Action action)
        {
            synchronizationContext.Send(a => { action(); }, null);
        }
    }
}