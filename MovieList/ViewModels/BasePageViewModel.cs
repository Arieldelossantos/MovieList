using System;

using Xamarin.Forms;
using PropertyChanged;
using Prism.Navigation;
using Prism.Services;
using MovieList.Services;

namespace MovieList.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BasePageViewModel
    {
        public string Title { get; set; }
        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;

        protected INavigationService _navigationService;
        protected IPageDialogService _pageDialogService;
        protected IDbService _dbService;
        protected IWLApiService _WLApiService;
        public BasePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDbService dbService, IWLApiService WLApiService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _dbService = dbService;
            _WLApiService = WLApiService;
        }

    }
}

