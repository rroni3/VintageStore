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
        private Category _category;//קטגוריה
        private string _photo;//תמונה
        private int _price;//*מחיר*/
        public ObservableCollection<Category> categorieoptions =new ObservableCollection<Category>();
        
            

        public string ImageLocation { get => _photo; set {if( value != _photo ) { _photo = value; OnPropertyChange(); } } }

        
        private StoreService service;

        public bool IsAdmin()
        {
            if (service.GetCurrentUser().UserName == "Admin") { return true; }
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

            categorieoptions.Add(new Category() { Id = 1, Name = "Necklace" });
            categorieoptions.Add(new Category() { Id = 2, Name = "Braclet" });
            categorieoptions.Add(new Category() { Id = 3, Name = "Earrings" });
            categorieoptions.Add(new Category() { Id = 4, Name = "Rings" });

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
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage

                    //string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    //using Stream sourceStream = await photo.OpenReadAsync();
                    //using FileStream localFileStream = File.OpenWrite(localFilePath);

                    //await sourceStream.CopyToAsync(localFileStream);
                    string filename = await UploadProfileImage(photo);
                    //service.LoggedInUser.Image = filename;
                    

                    Image = $"{StoreService.ImageUrl}{service.LoggedInUser.Image}";
                }

            }
        }

        private async Task<string> UploadProfileImage(FileResult photo)
        {
            try
            {
                string filename = await service.UploadImage(photo);
                return filename;
            }
            catch (Exception ex) { }
            return null;
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


            Jewelry j = new Jewelry() { Name = _name, Category = _category, Price = _price };

            service.AddJewleryAsync(j);

        }


    }
}

    
