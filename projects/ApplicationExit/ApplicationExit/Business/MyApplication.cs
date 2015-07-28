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

        public event EventHandler<CancelEventArgs> Exiting;
        public event EventHandler BeforeExiting;
        public event EventHandler AfterExiting;

        public MyApplication(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;
        }

        public bool Exit()
        {
            OnBeforeExiting();

            CancelEventArgs args = new CancelEventArgs();
            OnExiting(args);

            if (!args.Cancel)
                userInterface.Exit();

            OnAfterExiting();

            return !args.Cancel;
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
