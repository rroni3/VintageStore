using VintageStore.ViewModels;

namespace VintageStore.Views;

public partial class HomePage : ContentPage
{
	
    public HomePage(HomePageViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
        HomePage = new CollectionViewDemo();
        
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}