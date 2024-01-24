using VintageStore.ViewModels;


namespace VintageStore.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		InitializeComponent();

		BindingContext=vm;
	}
}