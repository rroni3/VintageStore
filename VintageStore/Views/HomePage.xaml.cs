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
        

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm=BindingContext as HomePageViewModel;
        if (vm != null)
        {
            await vm.LoadJewels();
        }
    }

    //private List<Jewelry> GetClothes()
    //{
    //    return new List<Jewelry>
    //    {
    //        new Jewelry { OrderId = 1, Name = "yali top", Category = "shirt", Size = "s", Color = "baby blue", photo = "yali_top.svg" },
    //        new Jewelry { OrderId = 2, Name = "Ella hat", Category = "jewlery", Size = "unisex", Color = "gold", photo = "hand_jewelry.jpg" },
    //        new Jewelry { OrderId = 3, Name = "Noa pants", Category = "pants" ,Size = "s", Color = "green", photo = "neclace.jpg" }
    //    };


    //}

}