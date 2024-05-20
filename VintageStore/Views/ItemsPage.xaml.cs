namespace VintageStore.Views;
using VintageStore.ViewModels;

public partial class ItemsPage : ContentPage
{
	public ItemsPage(ItemsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = this.BindingContext as ItemsPageViewModel;
        await vm.LoadJewleries();
    }
}