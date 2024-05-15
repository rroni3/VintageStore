using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public User _currentu {  get; set; }    
        public ObservableCollection<Order> orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Jewelry> jewlerys { get; set; } = new ObservableCollection<Jewelry>();
        private StoreService storeService;
        private List<Order> _FullList;
        public ProfilePageViewModel(StoreService storeService)
    {
        this.storeService = storeService;
            LoadUser();
            LoadOrders();
    }

    public async Task LoadUser()
    {

            _currentu = storeService.GetCurrentUser();
        if (_currentu == null)
        {

            await AppShell.Current.DisplayAlert("error", "error", "Ok");
        }
    }

        public async Task LoadOrders()
        {
            orders.Clear();
            jewlerys.Clear();

            int id = storeService.GetCurrentUser().Id;
             _FullList = await storeService.GetOrdersAsync(id);
            if (_FullList != null)
                foreach (var item in _FullList)
                {
                    orders.Add(item);
                    foreach(var item2 in item.OrderItems)
                    {
                        jewlerys.Add(item2);
                    }
                }
            
            
           
        }


}
}
