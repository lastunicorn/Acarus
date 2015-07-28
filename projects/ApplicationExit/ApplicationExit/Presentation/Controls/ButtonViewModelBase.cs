namespace ApplicationExit.Presentation.Controls
{
    abstract class ButtonViewModelBase : ViewModelBase
    {
        private bool enabled;

        public abstract string Description { get; }

        public bool Enabled
        {
            get { return enabled; }
            protected set
            {
                enabled = value;
                OnPropertyChanged();
            }
        }

        protected ButtonViewModelBase()
        {
            enabled = true;
        }

        public void MouseEnter()
        {
            // to do: display a description into the status bar.
        }

        public void MouseLeave()
        {
            // to do: remove the description from the status bar.
        }

        public void Clicked()
        {
            if(!enabled)
                return;

            Execute();
        }

        protected abstract void Execute();
    }
}