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
      
        public ICommand BackToProfileCommand {  get; set; }


        public ItemsPageViewModel()
        {
            BackToProfileCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("Profile");
            });

        }

        public async Task LoadJewleries(int orderId)
        {
            
            jewlerys.Clear();

            int id = storeService.GetCurrentUser().Id;
            jewlerys = await storeService.GetOrderJewleriesAsync(orderId);
            if (jewlerys != null)
                foreach (var item in jewlerys)
                {
                    jewlerys.Add(item);
                    
                }



        }

    }
}
