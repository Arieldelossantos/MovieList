using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MovieList.Views
{
    public partial class MovieListPage : ContentPage
    {
        public MovieListPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return; // don't do anything if we just de-selected the row
            // do something with e.SelectedItem
            ((Xamarin.Forms.ListView)sender).SelectedItem = null; // de-select the ro
        }
    }
}
