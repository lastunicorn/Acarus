using System;
using System.ComponentModel;

namespace ApplicationExit.Business
{
    /// <summary>
    /// Represents my application at the business level.
    /// </summary>
    class MyApplication
    {
        public TheData TheData { get; private set; }
        public UserInterface UserInterface { get; private set; }

        public event EventHandler<CancelEventArgs> Exiting;
        public event EventHandler BeforeExiting;
        public event EventHandler Exited;

        public MyApplication(UserInterface userInterface, TheData theData)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            UserInterface = userInterface;
            TheData = theData;
        }

        public bool Exit()
        {
            OnBeforeExiting();

            CancelEventArgs args = new CancelEventArgs();
            OnExiting(args);

            if (args.Cancel)
                return false;

            bool allowToContinue = TheData.AskAndSave();

            if (allowToContinue)
            {
                string text = TheData.IsModified ? "The date is NOT saved." : "The data is saved.";
                UserInterface.DisplayInfo(text);

                UserInterface.Exit();
            }

            OnExited();

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

        protected virtual void OnExited()
        {
            EventHandler handler = Exited;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
