using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using VintageStore.Models;
using VintageStore.Services;
using VintageStore.Views;

namespace VintageStore.ViewModels
{
    public class StorePageViewModel:ViewModel
    {

        #region Fields
       
        #endregion

        #region Service
       readonly private StoreService _StoreService;
        #endregion

        #region Properties
       

      
        #endregion

        #region Commands
        public ICommand SearchCommand { get; protected set; }
        public ICommand UploadPhoto { get; protected set; }

        public ICommand TakePictureCommand { get; protected set; }

        public ICommand ChangePhoto { get; protected set; }
        #endregion

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="storeService"></param>
        public StorePageViewModel(StoreService StoreService)
        {
            _StoreService = StoreService;
           

        }

      

    }
}
