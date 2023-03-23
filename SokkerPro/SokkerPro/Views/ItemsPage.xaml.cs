using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

using SokkerPro.Models;
using SokkerPro.ViewModels;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SokkerPro.Services;
using System.Collections.ObjectModel;
using static SokkerPro.Views.LivePage;
using I18NPortable;

namespace SokkerPro.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        MainPage RootPage { get => ((NavigationPage)Application.Current.MainPage).RootPage as MainPage; }

        ItemsViewModel viewModel;
        ObservableCollection<HomeMenuItem> leagueList = new ObservableCollection<HomeMenuItem>();

        DateTime w_selDate;

        public ItemsPage()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new ItemsViewModel("Dashboard");

            w_selDate = DateTime.Now;

            Device.StartTimer(TimeSpan.FromSeconds(App.APP_FREQ), () =>
            {
                // Do something
                LoadEvents();
                return true; // True = Repeat again, False = Stop the timer
            });
            LoadEvents();
        }

        public void UpdateFavorite(int id, bool isFav)
        {
            foreach(HomeMenuItem leagues in leagueList)
            {
                for (int i = 0; i < leagues.Count; i++)
                    if (leagues[i].id == id)
                        leagues[i].isFav = isFav;
            }
        }

        public void UpdateI18N()
        {
            ((BaseViewModel)BindingContext).UpdateI18N();
        }
        async void OnLeagueSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as Fixture;
            if (item == null || item.id == 0)
                return;
            await App.Current.MainPage.Navigation.PushAsync(new MatchPage(item.id));
        }
        public async Task LoadEvents()
        {
            int totalLive = 0, totalMatch = 0;
            ObservableCollection<HomeMenuItem> newlist = new ObservableCollection<HomeMenuItem>();
            try
            {
                TimeSpan offset = DateTime.Now - DateTime.UtcNow;

                
                var request = WebRequest.Create(App.BACKEND_URL + "/eventApi/" + w_selDate.ToString("yyyy-MM-dd") + "/" + Convert.ToInt32(offset.TotalSeconds) / 60 + "/" + App.token) as HttpWebRequest;
                request.Method = "GET";
                string responseContent = null;
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine(responseContent);
                        FixtureResponse result = JsonConvert.DeserializeObject<FixtureResponse>(responseContent);
                        List<Fixture> fixtures = result.data;

                        Dictionary<int, bool> dict = new Dictionary<int, bool>();
                        foreach (HomeMenuItem menuItem in leagueList)
                        {
                            dict[menuItem.League_Id] = menuItem.IsExpanded;
                        }
                        newlist = new ObservableCollection<HomeMenuItem>();
                        HomeMenuItem item = new HomeMenuItem
                        {
                            League_Id = 0,
                            LiveCnt = 0,
                            TotalCnt = 0
                        };
                        foreach (Fixture fixture in fixtures)
                        {
                            fixture.isFav = DatabaseManager.Instance.UpdateFavorite(fixture);
                            fixture.FavoriteCommand = new Command(p =>
                            {
                                Fixture match = (Fixture)p;
                                match.isFav = !match.isFav;
                                if (match.isFav)
                                    DatabaseManager.Instance.AddFavorite(new Favorite
                                    {
                                        fixture_id = match.id,
                                        raw = JsonConvert.SerializeObject(match)
                                    });
                                else
                                    DatabaseManager.Instance.DeleteFavorite(new Favorite { fixture_id = match.id });
                                RootPage.UpdateFavorite(match.id, match.isFav);
                            });
                            if (item.League_Id != fixture.league_id)
                            {
                                if (item.TotalCnt > 0)
                                {
                                    newlist.Add(item);
                                }
                                item = new HomeMenuItem
                                {
                                    SeasonId = fixture.season_id,
                                    CountryName = fixture.country_name,
                                    CountryFlag = App.BACKEND_URL + "/assets/flags/" + fixture.country_id + ".png",
                                    League_Id = fixture.league_id,
                                    LeagueName = fixture.league_name,
                                    LiveCnt = 0,
                                    TotalCnt = 0,
                                    IsExpanded = dict.ContainsKey(fixture.league_id) ? dict[fixture.league_id] : false,
                                    SeasonStatCommand = new Command(p =>
                                    {
                                        HomeMenuItem menuItem = (HomeMenuItem)p;
                                        Navigation.PushAsync(new SeasonStat(menuItem.SeasonId, menuItem.CountryFlag, menuItem.LeagueName));
                                        //App.Current.MainPage = new NavigationPage(new SeasonStat());
                                    }),
                                    ExpandCommand = new Command(p =>
                                    {
                                        HomeMenuItem menuItem = (HomeMenuItem)p;
                                        menuItem.Clear();
                                        if (!menuItem.IsExpanded)
                                        {
                                            foreach (Fixture mitem in menuItem.resource)
                                                menuItem.Add(mitem);
                                        }
                                        menuItem.IsExpanded = !menuItem.IsExpanded;
                                    })
                                };
                            }
                            if (fixture.isLive)
                            {
                                item.LiveCnt++;
                                totalLive++;
                            }
                            item.TotalCnt++; totalMatch++;
                            item.resource.Add(fixture);
                            if (item.IsExpanded)
                                item.Add(fixture);
                        }
                        if (item.TotalCnt > 0)
                        {
                            newlist.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (TotalMatch.Text != totalMatch.ToString())
                    TotalMatch.Text = totalMatch.ToString();
                if (TotalLive.Text != totalLive.ToString())
                    TotalLive.Text = totalLive.ToString();
                leagueList = newlist;
                LeaguesList.ItemsSource = leagueList;

                RootPage.UpdateFavorite(0, false);
            }
        }

        //private bool checkSame(ObservableCollection<HomeMenuItem> leagueList, ObservableCollection<HomeMenuItem> newlist)
        //{
        //    if (leagueList.Count != newlist.Count)
        //        return false;
        //    for (int i = 0; i < leagueList.Count; i++)
        //    {
        //        HomeMenuItem leagueitem = leagueList[i];
        //        HomeMenuItem newitem = newlist[i];
        //        if (leagueitem.League_Id != newitem.League_Id || leagueitem.Count != newitem.Count)
        //            return false;
        //        if (leagueitem.LiveCnt != newitem.LiveCnt)
        //            leagueitem.LiveCnt = newitem.LiveCnt;
        //        for (int j = 0; j < leagueitem.Count; j++)
        //        {
        //            Fixture leaguematch = leagueitem[j];
        //            Fixture newmatch = newitem[j];
        //            if (leaguematch.id != newmatch.id)
        //                return false;
        //            if (leaguematch.status != newmatch.status ||
        //                leaguematch.isLive() != newmatch.isLive())
        //            {
        //                leagueitem[j] = newitem[j];
        //                leagueitem.resource = newitem.resource;
        //            }
        //        }
        //    }
        //    return true;
        //}

        private void Calendar_Tapped(object sender, EventArgs e)
        {
            overlay.IsVisible = true;
            dateLayout.IsVisible = true;
        }

        private void PrevDay_Tapped(object sender, EventArgs e)
        {
            w_selDate = DateTime.Now.AddDays(-1);
            curDate.Text = "Dashboard_Yesterday".Translate();
            yestday.FontAttributes = FontAttributes.Bold; yestday.BackgroundColor = Color.FromHex("F2F2F2");
            today.FontAttributes = FontAttributes.None; today.BackgroundColor = Color.White;
            tomorrow.FontAttributes = FontAttributes.None; tomorrow.BackgroundColor = Color.White;
            dateLayout.IsVisible = false;
            overlay.IsVisible = false;
            LoadEvents();
        }

        private void NextDay_Tapped(object sender, EventArgs e)
        {
            w_selDate = DateTime.Now.AddDays(1);
            curDate.Text = "Dashboard_Tomorrow".Translate();
            tomorrow.FontAttributes = FontAttributes.Bold; tomorrow.BackgroundColor = Color.FromHex("F2F2F2");
            today.FontAttributes = FontAttributes.None; today.BackgroundColor = Color.White;
            yestday.FontAttributes = FontAttributes.None; yestday.BackgroundColor = Color.White;
            dateLayout.IsVisible = false;
            overlay.IsVisible = false;
            LoadEvents();
        }

        private void Today_Tapped(object sender, EventArgs e)
        {
            w_selDate = DateTime.Now;
            curDate.Text = "Dashboard_Today".Translate();
            yestday.FontAttributes = FontAttributes.None; yestday.BackgroundColor = Color.White;
            today.FontAttributes = FontAttributes.Bold; today.BackgroundColor = Color.FromHex("F2F2F2");
            tomorrow.FontAttributes = FontAttributes.None; tomorrow.BackgroundColor = Color.White;
            dateLayout.IsVisible = false;
            overlay.IsVisible = false;
            LoadEvents();
        }

        private void ExpandMatches(object sender, EventArgs e)
        {
            try
            {
                bool bFound = false;
                foreach(HomeMenuItem item in leagueList)
                {
                    if (item.IsExpanded == false)
                    {
                        bFound = true;
                        break;
                    }
                    item.IsExpanded = false;
                    item.ExpandCommand.Execute(item);
                }
                foreach (HomeMenuItem item in leagueList)
                {
                    item.IsExpanded = !bFound;
                    item.ExpandCommand.Execute(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        
        private void HideCalendar(object sender, EventArgs e)
        {
            dateLayout.IsVisible = false;
            overlay.IsVisible = false;
        }
    }
}