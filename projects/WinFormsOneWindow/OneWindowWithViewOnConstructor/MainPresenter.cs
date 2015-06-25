using System;

namespace DustInTheWind.OneWindowViewOnConstructor
{
    class MainPresenter
    {
        private readonly IMainView view;

        // Is it better to receive the view on constructor or to have a View property that can be set later?

        public MainPresenter(IMainView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            this.view = view;
        }

        public void ActionWasClicked()
        {
            view.DestinationText = view.SourceText;
        }
    }
}
