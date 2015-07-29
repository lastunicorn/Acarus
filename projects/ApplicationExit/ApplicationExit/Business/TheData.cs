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
using System.ComponentModel;
using ApplicationExit.Presentation;

namespace ApplicationExit.Business
{
    /// <summary>
    /// purpose:
    ///     - to store the data (ex: a list of items loaded from file) and its meta-data (ex: the file from where it was loaded the list);
    ///     - to perform different actions on the data (ex: load, save, etc...).
    /// 
    /// note: the actions may be delegated to some tools. because the logic is complex enough, it can be extracted in separate classes.
    /// </summary>
    internal class TheData
    {
        private readonly IUserInterface userInterface;
        private readonly MyApplication myApplication;

        private bool isModified;

        public bool IsModified
        {
            get { return isModified; }
            private set
            {
                isModified = value;
                OnChanged();
            }
        }

        public TheData(IUserInterface userInterface, MyApplication myApplication)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (myApplication == null) throw new ArgumentNullException("myApplication");

            this.userInterface = userInterface;
            this.myApplication = myApplication;

            IsModified = true;

            myApplication.Exiting += HandleMyApplicationExiting;
        }

        private void HandleMyApplicationExiting(object sender, CancelEventArgs e)
        {
            bool allowToContinue = AskAndSave();

            if (allowToContinue)
            {
                string text = IsModified ? "The date is NOT saved." : "The data is saved.";
                userInterface.DisplayInfo(text);
            }

            e.Cancel = e.Cancel || !allowToContinue;
        }

        public event EventHandler Changed;

        public bool AskAndSave()
        {
            if (!IsModified)
                return true;

            string text = LocalizedResources.EnsureAddressBookIsSaved_Question;
            string title = LocalizedResources.EnsureAddressBookIsSaved_Title;

            bool? response = userInterface.DisplayYesNoQuestion(text, title);

            if (response == null)
                return false;

            if (response.Value)
                Save();

            return true;
        }

        public void Save()
        {
            // to do: instantiate some helper and use it to save the data.

            IsModified = false;
        }

        public void ChangeData()
        {
            IsModified = true;
        }

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}