using System.Windows.Forms;
using ApplicationExit.Presentation;

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

        public bool? DisplayYesNoQuestion(string question, string title)
        {
            DialogResult dialogResult = MessageBox.Show(MainForm, question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return null;

            return dialogResult == DialogResult.Yes;
        }

        public void Run()
        {
            Application.Run(MainForm);
        }

        public void Exit()
        {
            Application.Exit();
        }
    }
}
