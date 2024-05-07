using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageStore.Models;

namespace VintageStore.ViewModels
{
    public class AdminPageViewModel
    { }
}
private string _name;//שם 
private Category _category;//קטגוריה
private string _photo;//תמונה
private int _price;//*מחיר*/



private StoreService service;

public ICommand AddJewleryCommand { get; protected set; }




public string Name
{
    get => _Name;

    set { _Name = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }

}
//public int Id
//{
//    get => _Id;

//    set => _Id = value;
//}

public Category Category
{
    get => _Category;
    set { _Category = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
}
public string Photo
{
    get => _Photo;
    set { _Photo = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
}
public int Price
{
    get => _price;
    set { _price = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
}








public AdminPageViewModel(StoreService storeService)
{
    service = storeService;
    AddJewleryCommand = new Command(async =>
    {
        //try
        //{
        //    var response = await service.RegisterAsync(new User() { });
        //}


    }

    );

    AddJewleryCommand = new Command(AddJewlery, EnableAddJewlery);
}

private bool EnableAddJewlery()
{
    return !(string.IsNullOrEmpty(_name) || int.IsNullOrEmpty(_category) || string.IsNullOrEmpty(_photo) || string.IsNullOrEmpty(_price));
}

private async void AddJewlery()
{

    
    Jewelry j = new Jewelry() { Name = _name, Category = _category, Photo = _photo, Price = _price };

    bool result = await service.AddJewleryAsync(j);
   
}




    
