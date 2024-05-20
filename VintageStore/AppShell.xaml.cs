using System.Windows.Input;
using VintageStore.Views;

using VintageStore.Services;
using VintageStore.Models;
//using Java.Lang;

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
            Routing.RegisterRoute("AdminPage", typeof(AdminPage));
            Routing.RegisterRoute("LogOutPage", typeof(LogOutPage));
            Routing.RegisterRoute("ItemsPage", typeof(ItemsPage));
            

        }

        StoreService service;

        //private async bool IsAdminCommand()
        //{
        //    return User.IsAdmin();
        //}
        private async void LogOutOnClick(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("///MainPage");
        }

        
    }
}