using VintageStore.ViewModels;

namespace VintageStore.Views;

public partial class MainPage : ContentPage
{
	
        public MainPage(MainPageViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
}
