namespace VintageStore.Views;
using VintageStore.ViewModels;

public partial class ItemsPage : ContentPage
{
	public ItemsPage(ItemsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
    
}