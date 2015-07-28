using System;
using System.ComponentModel;

namespace ApplicationExit.Business
{
    /// <summary>
    /// Represents my application at the business level.
    /// </summary>
    class MyApplication
    {
        private readonly UserInterface userInterface;
        private readonly TheData theData;

        public event EventHandler<CancelEventArgs> Exiting;
        public event EventHandler BeforeExiting;
        public event EventHandler AfterExiting;

        public MyApplication(UserInterface userInterface, TheData theData)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (theData == null) throw new ArgumentNullException("theData");

            this.userInterface = userInterface;
            this.theData = theData;
        }

        public bool Exit()
        {
            OnBeforeExiting();

            CancelEventArgs args = new CancelEventArgs();
            OnExiting(args);

            if (args.Cancel)
                return false;

            bool allowToContinue = theData.AskAndSave();

            if (allowToContinue)
            {
                string text = theData.IsModified ? "The date is NOT saved." : "The data is saved.";
                userInterface.DisplayInfo(text);

                userInterface.Exit();
            }

            OnAfterExiting();

            return allowToContinue;
        }

        protected virtual void OnExiting(CancelEventArgs e)
        {
            EventHandler<CancelEventArgs> handler = Exiting;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnBeforeExiting()
        {
            EventHandler handler = BeforeExiting;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnAfterExiting()
        {
            EventHandler handler = AfterExiting;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
