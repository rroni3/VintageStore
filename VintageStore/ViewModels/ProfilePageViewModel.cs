using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VintageStore.Models;
using VintageStore.Services;

using VintageStore.Views;

namespace VintageStore.ViewModels
{
    public class ProfilePageViewModel: ViewModel
    {

        public User _currentu {  get; set; }    
        public ObservableCollection<Order> orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Jewelry> jewlerys { get; set; } = new ObservableCollection<Jewelry>();
        private StoreService storeService;
        private List<Order> _FullList;
        public ICommand ShowItemsCommand {  get; set; }
        public ProfilePageViewModel(StoreService storeService)
    {
        this.storeService = storeService;
            _currentu = storeService.GetCurrentUser();
            LoadOrders();
            ShowItemsCommand = new Command<Order>(async (o) =>
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("orderItems", o);
                await Shell.Current.GoToAsync("ItemsPage",dict);

            });

        }

   

        public async Task LoadOrders()
        {
            orders.Clear();
            //jewlerys.Clear();

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
