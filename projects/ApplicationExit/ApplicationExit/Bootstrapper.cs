using ApplicationExit.Business;
using ApplicationExit.Presentation.UI;

namespace ApplicationExit
{
    internal class Bootstrapper
    {
        public void Initialize()
        {
            UserInterface userInterface = new UserInterface();
            TheData theData = new TheData(userInterface);

            MyApplication myApplication = new MyApplication(userInterface, theData);

            MainForm mainForm = new MainForm
            {
                ViewModel = new MainViewModel(myApplication, theData)
            };

            userInterface.MainForm = mainForm;
            userInterface.Run();
        }
    }
}