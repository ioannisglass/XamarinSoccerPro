using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

using SokkerPro.Models;
using SokkerPro.ViewModels;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SokkerPro.Services;
using System.Collections.ObjectModel;

namespace SokkerPro.Views
{
    public class LiveList : ObservableCollection<Fixture>
    {
        public string CountryName { get; set; }
        public String CountryFlag { get; set; }
        public int League_Id { get; set; } = 0;
        public string LeagueName { get; set; }
        public ObservableCollection<Fixture> Matches => this;
    }
    public class Filter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        MainPage RootPage { get => ((NavigationPage)Application.Current.MainPage).RootPage as MainPage; }
        public int Index { get; set; }
        public string key;
        public string Title { get; set; }
        public string _option = "None";
        public string Option
        {
            get
            {
                return _option;
            }
            set
            {
                _option = value;
                OnPropertyChanged("Option");
                OnPropertyChanged("IsVisible");
            }
        }
        public string _value = "+";
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("LeftImage");
                OnPropertyChanged("RightImage");
            }
        }
        public bool IsVisible
        {
            get
            {
                return Option != "None";
            }
        }
        public string LeftImage
        {
            get
            {
                if (Value == "+")
                    return "radio_select.png";
                return "radio_unselect.png";
            }
        }
        public string RightImage
        {
            get
            {
                if (Value == "-")
                    return "radio_select.png";
                return "radio_unselect.png";
            }
        }
        public Command MoreCommand { get; set; }
        public Command LessCommand { get; set; }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LivePage : ContentPage
    {
        MainPage RootPage { get => ((NavigationPage)Application.Current.MainPage).RootPage as MainPage; }

        ObservableCollection<LiveList> liveGames = new ObservableCollection<LiveList>();
        ObservableCollection<LiveAlert> liveAlerts = new ObservableCollection<LiveAlert>();
        List<Fixture> toFilter = new List<Fixture>();
        ObservableCollection<Filter> filters = new ObservableCollection<Filter>()
        {
            new Filter() { Index = 0, Title="Ball Possession", key="possessiontime" },
            new Filter() { Index = 1, Title="Goal", key="goal" },
            new Filter() { Index = 2, Title="Corner Kicks", key="corners" },
            new Filter() { Index = 3, Title="Dangerous Attacks", key="attacks-dangerous_attacks" },
            new Filter() { Index = 4, Title="Goal Attempts", key="goal_attempts" },
            new Filter() { Index = 5, Title="Fouls", key="fouls" },
        };
        bool bRaceToGoal = false;

        public LivePage()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            GamesList.ItemsSource = liveGames;
            AlertList.ItemsSource = liveAlerts;

            for (int i = 0; i < filters.Count; i++)
            {
                filters[i].MoreCommand = new Command(p => {
                    SetFilter((int)p, "+");
                });
                filters[i].LessCommand = new Command(p => {
                    SetFilter((int)p, "-");
                });
            }

            FilterList.ItemsSource = filters;

            Device.StartTimer(TimeSpan.FromSeconds(App.APP_FREQ), () =>
            {
                // Do something
                LoadLive();
                return true; // True = Repeat again, False = Stop the timer
            });
            LoadLive();
        }
        public void UpdateI18N()
        {
            ((BaseViewModel)BindingContext).UpdateI18N();
        }
        
        async void OnGameSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as Fixture;
            if (item == null || item.id == 0)
                return;
            await App.Current.MainPage.Navigation.PushAsync(new MatchPage(item.id));
        }
        public async void LoadLive()
        {
            try
            {
                var request = WebRequest.Create(App.BACKEND_URL + "/liveApi/" + App.token) as HttpWebRequest;
                request.Method = "GET";
                string responseContent = null;
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine(responseContent);
                        var resp = JsonConvert.DeserializeObject<FixtureResponse>(responseContent);
                        toFilter = resp.data;

                        foreach (Fixture item in toFilter)
                        {
                            item.isFav = DatabaseManager.Instance.UpdateFavorite(item);
                            item.FavoriteCommand = new Command(p =>
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                toFilter = new List<Fixture>();
            }
            finally
            {
                FilterGames();
            }
        }
        public void UpdateFavorite(int id, bool isFav)
        {
            foreach (LiveList leagues in liveGames)
            {
                for (int i = 0; i < leagues.Count; i++)
                    if (leagues[i].id == id)
                        leagues[i].isFav = isFav;
            }
        }

        private bool CheckFilter(Fixture match)
        {
            int home_goal, away_goal;
            home_goal = match.getScore(true);
            away_goal = match.getScore(false);

            if (bRaceToGoal)
            {
                return match.race_to_goal != 0;
            }

            for (int i = 0; i < filters.Count; i++)
            {
                if (filters[i].Option == "None")
                    continue;

                bool bHomeMore = false;
                if (filters[i].Option == "Home" && filters[i].Value == "+" || filters[i].Option == "Away" && filters[i].Value == "-")
                    bHomeMore = true;

                if (i == 1)
                {
                    if (bHomeMore && home_goal <= away_goal || !bHomeMore && away_goal <= home_goal)
                        return false;
                    continue;
                }
                var home_value = match.getStat(filters[i].key, true);
                var away_value = match.getStat(filters[i].key, false);
                
                if (bHomeMore && home_value <= away_value || !bHomeMore && away_value <= home_value)
                    return false;
            }
            return true;
        }

        private void FilterGames()
        {
            ObservableCollection<LiveList> newlive = new ObservableCollection<LiveList>();
            int prevLeagueId = 0;
            LiveList newitem = new LiveList() { };
            foreach (Fixture match in toFilter)
            {
                if (!CheckFilter(match))
                    continue;

                if (match.league_id != prevLeagueId)
                {
                    if (newitem.Count > 0)
                    {
                        newlive.Add(newitem);
                    }
                    newitem = new LiveList
                    {
                        CountryName = match.country_name,
                        CountryFlag = App.BACKEND_URL + "/assets/flags/" + match.country_id + ".png",
                        League_Id = match.league_id,
                        LeagueName = match.league_name
                    };
                    prevLeagueId = match.league_id;
                }
                newitem.Add(match);
            }
            if (newitem.Count > 0)
            {
                newlive.Add(newitem);
            }
            liveGames = newlive;
            GamesList.ItemsSource = liveGames;

            if (liveGames.Count > 0)
            {
                GamesList.IsVisible = true;
                NoGameMsg.IsVisible = false;
                liveGif.IsVisible = true;
            }
            else
            {
                GamesList.IsVisible = false;
                NoGameMsg.IsVisible = true;
                liveGif.IsVisible = false;
            }
        }

        private void SelectLiveGames(object sender, EventArgs e)
        {
            twoTabImage.Source = "two_tab_left.png";
            liveGameLayout.IsVisible = true;
            liveAlertLayout.IsVisible = false;
        }
        private async void SelectLiveAlert(object sender, EventArgs e)
        {
            twoTabImage.Source = "two_tab_right.png";

            liveGameLayout.IsVisible = false;
            liveAlertLayout.IsVisible = true;

            try
            {
                var request = WebRequest.Create(App.BACKEND_URL + "/liveAlerts") as HttpWebRequest;
                request.Method = "GET";
                string responseContent = null;
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine(responseContent);
                        ObservableCollection < LiveAlert > temp = JsonConvert.DeserializeObject<ObservableCollection<LiveAlert>>(responseContent);
                        AlertList.ItemsSource = temp;
                        if(temp.Count > 0)
                        {
                            AlertList.IsVisible = true;
                            NoAlertMsg.IsVisible = false;
                        }
                        else
                        {
                            AlertList.IsVisible = false;
                            NoAlertMsg.IsVisible = true;
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
            }
        }

        private void AdvSearch(object sender, EventArgs e)
        {
            advSearchLayout.IsVisible = true;
            overlay.IsVisible = true;
        }

        private void RaceToGoal(object sender, EventArgs e)
        {
            bRaceToGoal = !bRaceToGoal;
            if (bRaceToGoal)
                raceToGoalBtn.Source = "race_to_goal_white.png";
            else
                raceToGoalBtn.Source = "race_to_goal_black.png";
            FilterGames();
        }

        private void HideMatchList(object sender, EventArgs e)
        {
            advSearchLayout.IsVisible = false;
            overlay.IsVisible = false;
        }

        private void SetFilter(int Index, string Value)
        {
            filters[Index].Value = Value;
        }

        private void Search(object sender, EventArgs e)
        {
            advSearchLayout.IsVisible = false;
            overlay.IsVisible = false;
            bRaceToGoal = false;
            raceToGoalBtn.Source = "race_to_goal_black.png";
            FilterGames();
        }

        private void Clear(object sender, EventArgs e)
        {
            advSearchLayout.IsVisible = false;
            overlay.IsVisible = false;
            bRaceToGoal = false;
            raceToGoalBtn.Source = "race_to_goal_black.png";
            for (int i = 0; i < filters.Count; i++)
                filters[i].Option = "None";
            FilterGames();
        }
    }
}