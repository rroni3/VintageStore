using VintageStore.ViewModels;

namespace VintageStore.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
        InitializeComponent();
        this.BindingContext = vm;


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm=this.BindingContext as ProfilePageViewModel;
        await vm.LoadOrders();  
    }
}