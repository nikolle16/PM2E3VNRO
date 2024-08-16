using Plugin.Maui.Audio;
using PM2E3VNRO.Views;
using PM2E3VNRO.Controllers;
using PM2E3VNRO.Models;

namespace PM2E3VNRO
{
    public partial class App : Application
    {
        public static FirebaseController db = new FirebaseController();
        public static Notas nota;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Principal(AudioManager.Current));
        }
    }
}
