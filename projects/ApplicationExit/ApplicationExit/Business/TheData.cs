using System;
using ApplicationExit.Presentation;

namespace ApplicationExit.Business
{
    /// <summary>
    /// purpose:
    ///     - to store the data (ex: an address book) and its meta-data (ex: location from where it was loaded);
    ///     - to perform different actions on the data (ex: load, save, etc...).
    /// 
    /// note: the actions may be delegated to some tools, because the logic is complex enough to be extracted in separate classes.
    /// </summary>
    class TheData
    {
        private readonly UserInterface userInterface;
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

        public TheData(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;
            IsModified = true;
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
            // to do: instantiate some tool and use it to save the data.

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
