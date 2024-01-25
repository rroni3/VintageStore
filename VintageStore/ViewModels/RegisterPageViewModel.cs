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

            set => _userName = value;
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
            set => _FirstName = value;
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
            BackToLoginCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("Login");
            });
            RegisterCommand = new Command(Register);
        }

        private void Register()
        {
            //ToDo
            User u= new User() { Email = "ToDo", FirstName= "todo", LastName="todo", Id=123, UserPswd="e"
                                                                  };
            service.RegisterAsync(u);
            
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
