using VintageStore.ViewModels;

namespace VintageStore.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}