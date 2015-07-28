using ApplicationExit.Business;
using ApplicationExit.Presentation.UI;

namespace ApplicationExit
{
    internal class Bootstrapper
    {
        public void Initialize()
        {
            UserInterface userInterface = new UserInterface();
            

            MyApplication myApplication = new MyApplication(userInterface);
            TheData theData = new TheData(userInterface, myApplication);

            MainForm mainForm = new MainForm
            {
                ViewModel = new MainViewModel(myApplication, theData)
            };

            userInterface.MainForm = mainForm;
            userInterface.Run();
        }
    }
}