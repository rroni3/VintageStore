using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageStore.Models;

namespace VintageStore.ViewModels
{
    public class HomePageViewModel: ViewModel
    {
        public ObservableCollection<Jewelry> Jewleries { get; set; }= new ObservableCollection<Jewelry>();

      
    }
}
