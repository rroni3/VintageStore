using VintageStore.ViewModels;

namespace VintageStore.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
        InitializeComponent();
        this.BindingContext = vm;


    }
}