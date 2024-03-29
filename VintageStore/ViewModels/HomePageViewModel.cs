﻿using System;
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

        private bool isVisble;
        public ObservableCollection<Jewelry> SelectedJewls { get; set; } = new ObservableCollection<Jewelry>();

        public bool IsVisble
        {
            get => isVisble;
            set { if (isVisble != value) { isVisble = value; OnPropertyChange(); } }
        }
        private List<Jewelry> _FullList;

        public ICommand FilterCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        public ICommand ShowButtonCommand {  get; private set; }
        public ICommand OrderCommand { get; private set; }  
        public HomePageViewModel(StoreService storeService)
        {
            this.storeService = storeService;
            IsVisble = false;
              FilterCommand = new Command<string>(async (x) => await Filter(int.Parse(x)));
            ClearFilterCommand = new Command(async () => await ClearFilter());
            ShowButtonCommand = new Command(ShowButton);
            OrderCommand = new Command(async () => await Order());
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

        public async void ShowButton()
        {
            IsVisble = true;
        }
        public async Task Order()
        {
            int totalp = 0;
            foreach (var item in SelectedJewls)
            {
                totalp = totalp + item.Price;
            }
            Order newOrder = new Order() { Date = DateTime.Now, jewelries = SelectedJewls.ToList(), TotalPrice = totalp, User = storeService.GetCurrentUser() };
            await storeService.AddOrder(newOrder);
        }
    }
}
