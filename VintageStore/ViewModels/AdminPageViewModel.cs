using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using VintageStore.Models;
using VintageStore.Services;

namespace VintageStore.ViewModels
{
    public class AdminPageViewModel:ViewModel
    {

        private string _name;//שם 
        private Category _category;//קטגוריה
        private string _photo;//תמונה
        private int _price;//*מחיר*/

        public string ImageLocation { get => _photo; set {if( value != _photo ) { _photo = value; OnPropertyChange(); } } }

        private StoreService service;
        

        public ICommand AddJewleryCommand { get; protected set; }
        public ICommand UploadPhotoCommand {  get; protected set; }



        public string Name
        {
            get => _name;

            set { _name = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }

        }
        //public int Id
        //{
        //    get => _Id;

        //    set => _Id = value;
        //}

        public Category Category
        {
            get => _category;
            set { _category = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
        }
        public string Photo
        {
            get => _photo;
            set { _photo = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
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
            UploadPhotoCommand = new Command(async () => { await Shell.Current.DisplayAlert("g", "g", "ok"); });
        }

        private async Task Upload(FileResult file)
        {
            try
            {
                bool success = await service.UploadPhoto(file);
                if (success)
                {
                    var u = JsonSerializer.Deserialize<Jewelry>(await SecureStorage.Default.GetAsync("NewJewlery"));
                    ImageLocation = await service.GetImage() + $"{u.Id}.jpg";
                }
                else
                    Shell.Current.DisplayAlert("אין קשר לשרת", "לא הצלחתי להעלות את התמונה.נסה שוב", "אישור");
            }
            catch (Exception ex) { }
        }

        private bool EnableAddJewlery()
        {
            return !(IsNullOrEmpty(_name) ||  IsNullOrEmpty(_category) || IsNullOrEmpty(_photo) || IsNullOrEmpty(_price));
        }

        private bool IsNullOrEmpty(object o)
        {
            
            if (o == null)
            {
                return true;
            }
            return false;
        }
        private async void AddJewlery()
        {


            Jewelry j = new Jewelry() { Name = _name, Category = _category, Photo = _photo, Price = _price };

            //bool result = await service.AddJewleryAsync(j);

        }


    }
}

    
