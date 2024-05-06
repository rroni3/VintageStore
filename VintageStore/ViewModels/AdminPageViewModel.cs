using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageStore.ViewModels
{
    public class AdminPageViewModel
    { }
}
        //private string _name;//שם 
        //private int _categoryId;//קטגוריה
        //private string _Photo;//תמונה
        //private int _price;//*מחיר*/
        

        
        //private StoreService service;

        //public ICommand AddJewleryCommand { get; protected set; }


        

        //public string Name
        //{
        //    get => _Name;

        //    set { _Name = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
           
        //}
        //public int Id
        //{
        //    get => _Id;

        //    set => _Id = value;
        //}

//        public int CategoryId
//        {
//            get => _CategoryId;
//            set { _CategoryId = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
//        }
//        public string Photo
//        {
//            get => _Photo;
//            set { _Photo = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
//        }
//        public int Price
//        {
//            get => _price;
//            set { _price = value; ((Command)AddJewleryCommand).ChangeCanExecute(); }
//        }

        
        

        

        

//        public AdminPageViewModel(StoreService storeService)
//        {
//            service = storeService;
//            AddJewleryCommand = new Command(async =>
//            {
//                //try
//                //{
//                //    var response = await service.RegisterAsync(new User() { });
//                //}


//            }

//            );

//            AddJewleryCommand = new Command(AddJewlery, EnableAddJewlery);
//        }

//        private bool EnableRegister()
//        {
//            return !(string.IsNullOrEmpty(Name) || int.IsNullOrEmpty(CategoryId) || string.IsNullOrEmpty(Photo) || string.IsNullOrEmpty(price) );
//        }

//        private async void Register()
//        {

//            //ToDo
//            User u = new User() { Email = Email, FirstName = FirstName, LastName = LastName, UserPswd = Password, UserName = UserName };

//            bool result = await service.RegisterAsync(u);
//            if (result == true)
//            {
//                await Shell.Current.GoToAsync("///HomePage");
//            }
//        }

//        private bool ValidateUser()
//        {
//            return !(string.IsNullOrEmpty(UserName) || UserName.Length < 3);
//        }
//        private bool ValidatePassWord()
//        {
//            return !string.IsNullOrEmpty(Password);
//        }

//        private bool ValidatePage()
//        {
//            return ValidateUser() && ValidatePassWord();
//        }

//    }
//}

//    }
//}
