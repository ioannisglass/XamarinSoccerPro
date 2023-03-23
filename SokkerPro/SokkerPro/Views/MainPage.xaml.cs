using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SokkerPro.Models;
using SokkerPro.ViewModels;

namespace SokkerPro.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void UpdateI18N()
        {
            ((BaseViewModel)BindingContext).UpdateI18N();
            Dashboard.UpdateI18N();
            Live.UpdateI18N();
            Favorite.UpdateI18N();
            Tips.UpdateI18N();
            Settings.UpdateI18N();
        }

        public void UpdateFavorite(int id, bool isFav)
        {
            if (id > 0)
            {
                Dashboard.UpdateFavorite(id, isFav);
                Live.UpdateFavorite(id, isFav);
            }
            Favorite.UpdateFavorite(id, isFav);
        }
    }
}