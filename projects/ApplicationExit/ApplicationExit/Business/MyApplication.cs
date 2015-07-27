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

        public MyApplication(UserInterface userInterface, TheData theData)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            UserInterface = userInterface;
            TheData = theData;
        }

        public bool Exit()
        {
            CancelEventArgs args = new CancelEventArgs();
            OnExiting(args);

            if (args.Cancel)
                return false;

            bool allowToContinue = TheData.AskAndSave();

            if (allowToContinue)
                UserInterface.Exit();

            return allowToContinue;
        }

        protected virtual void OnExiting(CancelEventArgs e)
        {
            EventHandler<CancelEventArgs> handler = Exiting;

            if (handler != null)
                handler(this, e);
        }
    }
}
