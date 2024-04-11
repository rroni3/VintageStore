using System.Windows.Input;
using VintageStore.Views;

using VintageStore.Services;
using VintageStore.Models;

namespace VintageStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Register", typeof(RegisterPage));
            Routing.RegisterRoute("Login", typeof(MainPage));
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("ProfilePage", typeof (ProfilePage));
            Routing.RegisterRoute("AboutPage", typeof(AboutPage));
            Routing.RegisterRoute("EnvironmentalPage", typeof(EnvironmentalPage));
            Routing.RegisterRoute("LogOutPage", typeof(LogOutPage));
            
            LogOutCommand = new Command<StoreService>(LogOut);
        }

        StoreService service;
        public ICommand LogOutCommand { get; private set; }
        private async void LogOut (StoreService _service)
        {
            service=_service;
           _service.SetCurrentUser(null);
            await Shell.Current.GoToAsync("///MainPage");


        }
    }
}