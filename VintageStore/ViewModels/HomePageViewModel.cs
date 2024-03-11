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
    public class HomePageViewModel: ViewModel
    {
        private StoreService storeService;

        public ObservableCollection<Jewelry> Jewleries { get; set; }= new ObservableCollection<Jewelry>();

        private List<Jewelry> _FullList;

        public ICommand FilterCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }

        public HomePageViewModel(StoreService storeService)
        {
            this.storeService = storeService;
          
            FilterCommand = new Command<int>(async (x) => await Filter(x));
            ClearFilterCommand = new Command(async () => await ClearFilter());
        }

        private async Task Filter(int categoryId)
        {
            var filtered=_FullList.Where(x=>x.Category.Id == categoryId).ToList();
            Jewleries.Clear();
            foreach (var item in filtered)
            {
                Jewleries.Add(item);
            }
        }

        private async Task ClearFilter()
        {
            
            Jewleries.Clear();
            if(_FullList != null)
            foreach (var item in _FullList)
            {
                Jewleries.Add(item);
            }
        }

        public async Task LoadJewels()
        {
            Jewleries.Clear();
           _FullList= await storeService.GetJewlsAsync();
            if (_FullList != null)
                foreach (var item in _FullList)
                {
                    Jewleries.Add(item);
                }
            else
                await AppShell.Current.DisplayAlert("error", "error", "Ok");
        }
    }
}
