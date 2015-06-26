namespace DustInTheWind.OneWindowViewProperty
{
    class MainPresenter
    {
        // Is it better to receive the view on constructor or to have a View property that can be set later?

        public IMainView View { get; set; }

        public void ActionWasClicked()
        {
            View.DestinationText = View.SourceText;
        }
    }
}
