using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

using VintageStore.Services;
using VintageStore.Models;
using System.Collections.ObjectModel;
using VintageStore.Views;

namespace VintageStore.ViewModels
{
    public class AdminPageViewModel:ViewModel
    {

        private string _name;//שם 
        private int _id;
        private string _category;//קטגוריה
        private string _photo;//תמונה
        private int _price;//*מחיר*/
        public ObservableCollection<string> categorieoptions { get; set; } = new ObservableCollection<string>();
        
            

        public string ImageLocation { get => _photo; set {if( value != _photo ) { _photo = value; OnPropertyChange(); } } }

        
        private StoreService service;

        public bool IsAdmin()
        {
            if (service.GetCurrentUser().UserName == "admin") { return true; }
            return false;
        }

        public ICommand AddJewleryCommand { get; protected set; }
        public ICommand UploadPhotoCommand {  get; protected set; }
        public ICommand AddPhotoCommand { get; protected set; }



        public string Name
        {
            get => _name;

            set { _name = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }

        }
        //public int OrderId
        //{
        //    get => _Id;

        //    set => _Id = value;
        //}

        public string Category
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

            categorieoptions.Add( "Necklace" );
            categorieoptions.Add("Braclet" );
            categorieoptions.Add( "Earrings" );
            categorieoptions.Add("Rings" );

            service = storeService;
            if(!IsAdmin()) 
            {
                 Shell.Current.DisplayAlert("Admin Page", "אין לך הרשאה לעמוד המנהל", "ok");


                Shell.Current.GoToAsync("///HomePage");
            }
            AddJewleryCommand = new Command(async =>
            {
                


            }

            );

            AddJewleryCommand = new Command(AddJewlery, EnableAddJewlery);
            UploadPhotoCommand = new Command(async () => { await Shell.Current.DisplayAlert("g", "g", "ok"); });
            AddPhotoCommand = new Command(async () => { UploadImage(); });
        }
        private async Task UploadImage()
        {
            FileResult photo=null;
            if (MediaPicker.Default.IsCaptureSupported)
            {
                 photo = await MediaPicker.Default.PickPhotoAsync();
            }

            if (photo != null)
             {

                    await UploadJewlImage(photo);



            }
             else
             {
                   await Shell.Current.DisplayAlert("image", " העלאת התמונה נכשלה", "ok");
            }


            
        }
       
        private async Task UploadJewlImage(FileResult photo)
        {
            try
            {
                bool filename = await service.UploadImage(photo,_id);
                if (filename == false)
                {
                    await Shell.Current.DisplayAlert("image", " העלאת התמונה נכשלה", "ok");
                }
                else 
                {
                    await Shell.Current.DisplayAlert("image", " העלאת התמונה בוצעה בהצלחה", "ok");
                }
            }
            catch (Exception ex) { }
            
        }
    

    private bool EnableAddJewlery()
        {
            return !(IsNullOrEmpty(_name) ||  IsNullOrEmpty(_category)  || IsNullOrEmpty(_price));
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


            Jewelry j = new Jewelry() { Name = _name, Category = new Category(_category ), Price = _price };

            _id=await service.AddJewleryAsync(j);
            if (_id == -1)
            {
               await Shell.Current.DisplayAlert("add jewlery", "הוספת התכשיט נכשלה", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("add jewlery", "הוספת התכשיט בוצעה בהצלחה, מוזמן להוסיף לתכשיט תמונה", "ok");

            }
            

        }


    }
}

    
