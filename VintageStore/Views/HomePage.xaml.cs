using System.ComponentModel;
using VintageStore.Models;
using VintageStore.ViewModels;

namespace VintageStore.Views;

public partial class HomePage : ContentPage
{

    public HomePage(HomePageViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
        CollectionView.ItemsSource=GetClothes();

    }

    private List<Clothes> GetClothes()
    {
        return new List<Clothes>
        {
            new Clothes { Id = 1, Name = "yali top", Category = "shirt", Size = "s", Color = "baby blue", photo = "dotnet_bot.svg" },
            new Clothes { Id = 2, Name = "Ella hat", Category = "hats", Size = "s", Color = "purple", photo = "" },
            new Clothes { Id = 3, Name = "Noa pants", Category = "pants" ,Size = "s", Color = "green", photo = "" }
        };


    }

}