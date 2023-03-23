using Newtonsoft.Json;
using SokkerPro.Models;
using SokkerPro.Services;
using SokkerPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SokkerPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritePage : ContentPage
    {
        public ObservableCollection<LiveList> favGames;
        MainPage RootPage { get => ((NavigationPage)Application.Current.MainPage).RootPage as MainPage; }
        public FavoritePage()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            InitFavorites();
        }
        public void UpdateI18N()
        {
            ((BaseViewModel)BindingContext).UpdateI18N();
        }

        internal void UpdateFavorite(int id, bool isFav)
        {
            InitFavorites();
        }

        private void InitFavorites()
        {
            favGames = new ObservableCollection<LiveList>();
            List<Favorite> favorites = DatabaseManager.Instance.GetFavorite();
            int prevLeague = -1;
            LiveList newitem = new LiveList() { };
            foreach (Favorite fav in favorites)
            {
                Fixture match = JsonConvert.DeserializeObject<Fixture>(fav.raw);
                match.isFav = true;
                match.FavoriteCommand = new Command(p =>
                {
                    Fixture fix = (Fixture)p;
                    fix.isFav = !fix.isFav;
                    if (fix.isFav)
                        DatabaseManager.Instance.AddFavorite(new Favorite
                        {
                            fixture_id = fix.id,
                            raw = JsonConvert.SerializeObject(fix)
                        });
                    else
                        DatabaseManager.Instance.DeleteFavorite(new Favorite { fixture_id = fix.id });
                    RootPage.UpdateFavorite(fix.id, fix.isFav);
                });
                if (match.league_id != prevLeague)
                {
                    if (newitem.Count > 0)
                    {
                        favGames.Add(newitem);
                    }
                    newitem = new LiveList
                    {
                        CountryName = match.country_name,
                        CountryFlag = App.BACKEND_URL + "/assets/flags/" + match.country_id + ".png",
                        League_Id = match.league_id,
                        LeagueName = match.league_name
                    };
                    prevLeague = match.league_id;
                }
                newitem.Add(match);
            }
            if (newitem.Count > 0)
            {
                favGames.Add(newitem);
            }

            FavoriteList.ItemsSource = favGames;

            if (favGames.Count > 0)
            {
                NoGameMsg.IsVisible = false;
                FavoriteList.IsVisible = true;
            }
            else
            {
                NoGameMsg.IsVisible = true;
                FavoriteList.IsVisible = false;
            }
        }

        private async void OnFavoriteSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as Fixture;
            if (item == null || item.id == 0)
                return;
            await App.Current.MainPage.Navigation.PushAsync(new MatchPage(item.id));
        }
    }
}