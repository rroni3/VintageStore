using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using VintageStore.Models;
using VintageStore.Services;

namespace VintageStore.ViewModels
{
    public class ProfilePageViewModel: ViewModel
    {
       
    public User _currentu;
    private StoreService storeService;
    public ProfilePageViewModel(StoreService storeService)
    {
        this.storeService = storeService;
    }

    public async Task LoadUser()
    {

            _currentu = storeService.GetCurrentUser();
        if (_currentu == null)
        {

            await AppShell.Current.DisplayAlert("error", "error", "Ok");
        }
    }
}
}
