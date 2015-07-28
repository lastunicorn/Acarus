using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    internal class SaveButtonModel : ButtonViewModelBase
    {
        private readonly TheData theData;

        public override string Description
        {
            get { return "Saves the data."; }
        }

        public SaveButtonModel(TheData theData)
        {
            if (theData == null) throw new ArgumentNullException("theData");
            this.theData = theData;

            theData.Changed += HandleTheDataChanged;

            Enabled = theData.IsModified;
        }

        protected override void Execute()
        {
            theData.Save();
        }

        private void HandleTheDataChanged(object sender, EventArgs eventArgs)
        {
            Enabled = theData.IsModified;
        }
    }
}