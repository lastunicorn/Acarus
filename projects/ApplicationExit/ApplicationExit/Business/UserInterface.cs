using System.Windows.Forms;
using ApplicationExit.Presentation.UI;

namespace ApplicationExit.Business
{
    class UserInterface
    {
        public MainForm MainForm { get; set; }

        public UserInterface()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public void Run()
        {
            Application.Run(MainForm);
        }

        public void Exit()
        {
            Application.Exit();
        }

        public void DisplayInfo(string text)
        {
            MessageBox.Show(MainForm, text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool? DisplayYesNoQuestion(string question, string title)
        {
            DialogResult dialogResult = MessageBox.Show(MainForm, question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return null;

            return dialogResult == DialogResult.Yes;
        }
    }
}
