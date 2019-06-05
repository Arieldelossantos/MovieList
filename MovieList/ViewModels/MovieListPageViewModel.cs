using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MovieList.Helpers;
using MovieList.Models;
using MovieList.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;


namespace MovieList.ViewModels
{
    public class MovieListPageViewModel : BasePageViewModel, INavigationAware
    {
        List<Search> Data;
        public ObservableCollection<Search> MovieList { get; set; }
        public DelegateCommand RefreshDataCommand { get; set; }
        public DelegateCommand LoadDataCommand { get; set; }


        string _searchParam;
        public string SearchParam
        {
            get { return _searchParam; }
            set
            {
                _searchParam = value;
                DoSearch();
            }
        }

        Search _selectedMovie;
        public Search SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                if (_selectedMovie != null)
                {
                    NavigateToMovieInfo(_selectedMovie);
                }
                SelectedMovie = null;
            }
        }

        IDeviceService _deviceService;
        public MovieListPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDbService dbService, IWLApiService WLApiService, IDeviceService deviceService) : base(navigationService, pageDialogService, dbService, WLApiService)
        {
            Title = "Movies";
            RefreshDataCommand = new DelegateCommand(DoRefreshData);
            LoadDataCommand = new DelegateCommand(DoLoadData);
            _deviceService = deviceService;

            if (Data == null)
                Data = new List<Search>();
        }

        void DoSearch()
        {
            var paramIsEmpty = string.IsNullOrWhiteSpace(_searchParam);
            var _data = Data.Where(x => paramIsEmpty || x.Title.ToLower().Contains(_searchParam.ToLower()) || x.Year.Contains(_searchParam));

            MovieList = new ObservableCollection<Search>(_data.OrderByDescending(x => x.Year).Take(20));
        }


        private async void DoLoadData()
        {
            try
            {
                if (IsBusy) return;

                _deviceService.BeginInvokeOnMainThread(() => IsBusy = true);
                SearchParam = string.Empty;

                await LoadData();
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

        private async void DoRefreshData()
        {
            try
            {
                _deviceService.BeginInvokeOnMainThread(() => IsBusy = true);
                SearchParam = string.Empty;

                Data = new List<Search>();

                var movielist1 = await _dbService.MoviesResult(_WLApiService, "batman");

                if (movielist1.Any())
                {
                    Data.AddRange(movielist1);
                }
                MovieList = new ObservableCollection<Search>(Data.OrderByDescending(x => x.Year).Take(20));
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

        async Task LoadData()
        {

            var movielist1 = await _dbService.MoviesResult(_WLApiService, "avengers");
            var movielist2 = await _dbService.MoviesResult(_WLApiService, "spider");
            var movielist3 = await _dbService.MoviesResult(_WLApiService, "iron");

            Data = new List<Search>();

            if (movielist1.Any())
            {
                Data.AddRange(movielist1);
            }

            if (movielist2.Any())
            {
                Data.AddRange(movielist2);
            }

            if (movielist3.Any())
            {
                Data.AddRange(movielist3);
            }

            MovieList = new ObservableCollection<Search>(Data.OrderByDescending(x => x.Year).Take(20));

        }

        private async void NavigateToMovieInfo(Search selectedMovie)
        {
            var navParam = new NavigationParameters
            {
                { "movie", selectedMovie.imdbID }
            };

            await _navigationService.NavigateAsync(NavigationConstant.MovieInfoPage, navParam);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!Data.Any())
            {
                DoLoadData();
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}

