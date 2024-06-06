using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using  VintageStore.Models;
using VintageStore.Services;

namespace VintageStore.ViewModels
{
    public class RegisterPageViewModel: ViewModel
    {
        private string _userName;//שם משתמש
        private bool _showUserNameError;//האם להציג שדה שגיאת שם משתמש
        private string _userErrorMessage;//תאור שגיאת שם משתמש
        private string _password;//סיסמה
        private string _FirstName;
        private string _LastName;
        private string _Email;
        private int _Id;

        private bool _showPasswordError;//האם להציג שגיאת סיסמה
        private string _passwordErrorMessage;
        private bool _showLoginError;//
        private string _loginErrorMessage;
        private StoreService service;

        public ICommand RegisterCommand { get; protected set; }


        public ICommand BackToLoginCommand { get; protected set; }

        public string UserName
        {
            get => _userName;

            set { _userName = value; ((Command)RegisterCommand).ChangeCanExecute(); }
            //set { if (_userName != value)
            //    { _userName = value;
            //        if (!ValidateUser())
            //        { ShowUserNameError = true;
            //            UserErrorMessage = ErrorMessages.INVALID_USERNAME; }
            //        else { ShowUserNameError = false; 
            //            UserErrorMessage = string.Empty; } 
            //        OnPropertyChange(); 
            //        (RegisterCommand as Command).ChangeCanExecute(); } }
        }
        public int Id
        {
            get => _Id;

            set => _Id = value;
        }

        public string FirstName
        {
            get => _FirstName;
            set { _FirstName = value;((Command)RegisterCommand).ChangeCanExecute(); }
        }
        public string LastName
        {
            get => _LastName;
            set { _LastName = value; ((Command)RegisterCommand).ChangeCanExecute(); }
            }
        public string Email
        {
            get => _Email;
            set { _Email = value; ((Command)RegisterCommand).ChangeCanExecute(); }
            }

        public bool ShowUserNameError
        {
            get => _showUserNameError;
            set
            {
                if (_showUserNameError != value)
                {
                    _showUserNameError = value;
                }
                OnPropertyChange();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value; if (!ValidatePassWord())
                    {
                        ShowPasswordError = true;
                        PasswordErrorMessage = ErrorMessages.INVALID_PASSWORD;
                    }
                    else
                    {
                        PasswordErrorMessage = string.Empty;
                        ShowPasswordError = false;
                    };
                    OnPropertyChange();
                    (RegisterCommand as Command).ChangeCanExecute();
                }
            }
        }

        public string UserErrorMessage
        {
            get => _userErrorMessage;
            set { if (_userErrorMessage != value) { _userErrorMessage = value; } OnPropertyChange(); }
        }


        public bool ShowPasswordError { get => _showPasswordError; set { if (_showPasswordError != value) { _showPasswordError = value; OnPropertyChange(); } } }
        public string PasswordErrorMessage { get => _passwordErrorMessage; set { if (_passwordErrorMessage != value) { _passwordErrorMessage = value; OnPropertyChange(); } } }

        public bool ShowLoginError { get => _showLoginError; set { if (_showLoginError != value) { _showLoginError = value; } OnPropertyChange(); } }
        public string LoginErrorMessage { get => _loginErrorMessage; set { if (_loginErrorMessage != value) { _loginErrorMessage = value; OnPropertyChange(); } } }

        public RegisterPageViewModel (StoreService storeService)
        {
            service = storeService;
            RegisterCommand=new Command(async=>
            {
                //try
                //{
                //    var response = await service.RegisterAsync(new User() { });
                //}
                

            }
            
            );
            BackToLoginCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("Login");
            });
            RegisterCommand = new Command(async()=>await Register());
        }

        public bool EnableRegister()
        {
        return !(string.IsNullOrEmpty(FirstName)||string.IsNullOrEmpty(LastName)||string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UserName));
        }

        private async Task Register()
        {

            
            if (!EnableRegister())
            {
                await Shell.Current.DisplayAlert("register", "אחד או יותר מהשדות לא תקינים, אנא וודא שבכולם אחד או יותר תווים.", "ok");
                return;
            }
            User u = new User() { Email = Email, FirstName= FirstName, LastName=LastName,  UserPswd=Password, UserName = UserName };

          bool result= await service.RegisterAsync(u);
            if(result==true)
            {
                await Shell.Current.GoToAsync("///HomePage");
                await Shell.Current.DisplayAlert("register", "ההרשמה בוצעה בהצלחה", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("register", "אחד או יותר מהשדות תפוסים, אנא שנה אותם עבור הרשמה.", "ok");
            }
        }

        private bool ValidateUser()
        {
            return !(string.IsNullOrEmpty(UserName) || UserName.Length < 3);
        }
        private bool ValidatePassWord()
        {
            return !string.IsNullOrEmpty(Password);
        }

        private bool ValidatePage()
        {
            return ValidateUser() && ValidatePassWord();
        }

    }
}
