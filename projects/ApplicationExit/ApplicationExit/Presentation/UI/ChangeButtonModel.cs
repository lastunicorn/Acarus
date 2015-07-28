using System;
using ApplicationExit.Business;
using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    internal class ChangeButtonModel : ButtonViewModelBase
    {
        private readonly TheData theData;

        public ChangeButtonModel(TheData theData)
        {
            if (theData == null) throw new ArgumentNullException("theData");
            this.theData = theData;
        }

        public override string Description
        {
            get { return "Modifies the data."; }
        }

        protected override void Execute()
        {
            theData.ChangeData();
        }
    }
}