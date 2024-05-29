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

    [QueryProperty(nameof(O), "orderItems")]
    public class ItemsPageViewModel:ViewModel
    {
        public Order O {  get; set; }   
        public ObservableCollection<Jewelry> jewlerys { get; set; } = new ObservableCollection<Jewelry>();
        private StoreService storeService;
        public User _currentu { get; set; }
      
        public ICommand BackToProfileCommand {  get; set; }


        public ItemsPageViewModel()

        {
            LoadJewleries();
            BackToProfileCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("Profile");
            });

        }

        public void LoadJewleries()
        {
            jewlerys.Clear();
            foreach (var item in O.OrderItems) 
            {
               jewlerys.Add(item);
            }
        }

        
    }
}
