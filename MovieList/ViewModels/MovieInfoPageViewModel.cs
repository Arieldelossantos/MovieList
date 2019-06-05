using System;
using MovieList.Helpers;
using MovieList.Models;
using MovieList.Services;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace MovieList.ViewModels
{
    public class MovieInfoPageViewModel : BasePageViewModel, INavigatingAware
    {
        public MovieInfo Info { get; set; }
        IDeviceService _deviceService;
        public MovieInfoPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDbService dbService, IWLApiService WLApiService, IDeviceService deviceService) : base(navigationService, pageDialogService, dbService, WLApiService)
        {
            Title = "Movie Details";
            Info = new MovieInfo();
            _deviceService = deviceService;
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("movie"))
            {
                try
                {
                    _deviceService.BeginInvokeOnMainThread(() => IsBusy = true);

                    var imdbId = parameters.GetValue<string>("movie");

                    var info = await _dbService.MovieInfo(_WLApiService, imdbId);

                    if (info != null)
                    {
                        Info = info;
                    }
                }
                catch (Exception ex)
                {
                    _deviceService.BeginInvokeOnMainThread(() => IsBusy = false);
                    await _pageDialogService.DisplayAlertAsync("Error " + AppConstants.AppName, ex.Message, "OK");
                }
                finally
                {
                    _deviceService.BeginInvokeOnMainThread(() => IsBusy = false);
                }
            }
        }
    }
}

