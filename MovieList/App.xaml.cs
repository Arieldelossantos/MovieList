using System;
using MovieList.Helpers;
using MovieList.Services;
using MovieList.ViewModels;
using MovieList.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieList
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer = null) : base(initializer) { }


        protected override void OnInitialized()
        {

            InitializeComponent();

            NavigationService.NavigateAsync(NavigationConstant.MovieListPage);

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.Register<IDbService, DbService>();
            containerRegistry.Register<IWLApiService, WLApiService>();


            //Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MovieListPage, MovieListPageViewModel>();
            containerRegistry.RegisterForNavigation<MovieInfoPage, MovieInfoPageViewModel>();


        }
    }
}
