using VintageStore.ViewModels;
namespace VintageStore.Views {

    public partial class StorePage : ContentPage
    {
        public StorePage(StorePageViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var user = SecureStorage.GetAsync("LoggedUser");
            if (user == null)
            {
                await AppShell.Current.GoToAsync("MainPage");
            }
        }
    }
}