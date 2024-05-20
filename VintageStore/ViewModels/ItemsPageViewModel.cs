using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VintageStore.Models;
using VintageStore.Services;

namespace VintageStore.ViewModels
{
    public class ItemsPageViewModel:ViewModel
    {
        public ObservableCollection<Jewelry> jewlerys { get; set; } = new ObservableCollection<Jewelry>();
        private StoreService storeService;
        public User _currentu { get; set; }
        public ObservableCollection<Order> orders { get; set; } = new ObservableCollection<Order>();
        private List<Order> _FullList;
        public ICommand BackToProfileCommand {  get; set; }


        public ItemsPageViewModel()
        {
            BackToProfileCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("Profile");
            });

        }

        public async Task LoadJewleries()
        {
            
            jewlerys.Clear();

            int id = storeService.GetCurrentUser().Id;
            _FullList = await storeService.GetOrdersAsync(id);
            if (_FullList != null)
                foreach (var item in _FullList)
                {
                    orders.Add(item);
                    foreach (var item2 in item.OrderItems)
                    {
                        jewlerys.Add(item2);
                    }
                }



        }

    }
}
