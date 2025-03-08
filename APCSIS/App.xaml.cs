using APCSIS.Pages;

namespace APCSIS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NewPage1();
        }
    }
}
