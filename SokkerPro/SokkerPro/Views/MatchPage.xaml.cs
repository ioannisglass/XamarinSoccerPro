using SokkerPro.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SokkerPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchPage : ContentPage
    {
        int match_id;

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        MatchsViewModel viewModel;
        public MatchPage(int match_id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new MatchsViewModel();

            this.match_id = match_id;

            Device.StartTimer(TimeSpan.FromSeconds(App.APP_FREQ), () =>
            {
                // Do something
                viewModel.LoadChartAsync(this.match_id);
                viewModel.LoadMatchInfo(this.match_id, false);
                return true; // True = Repeat again, False = Stop the timer
            });
            viewModel.LoadMatchInfo(this.match_id, true);


        }

        private void SelectTimeline(object sender, EventArgs e)
        {
            fiveTabImage.Source = "five_tab_1.png";
            timelineLayout.IsVisible = true;
            predictionLayout.IsVisible = false;
            statisticLayout.IsVisible = false;
            lineupLayout.IsVisible = false;
            standingLayout.IsVisible = false;

            Grid.SetColumn(selFrame, 0);
            timelineLabel.TextColor = Color.White;
            predictionLabel.TextColor = Color.Black;
            statisticLabel.TextColor = Color.Black;
            lineupLabel.TextColor = Color.Black;
            rankLabel.TextColor = Color.Black;
        }
        private void SelectPrediction(object sender, EventArgs e)
        {
            fiveTabImage.Source = "five_tab_2.png";

            timelineLayout.IsVisible = false;
            predictionLayout.IsVisible = true;
            statisticLayout.IsVisible = false;
            lineupLayout.IsVisible = false;
            standingLayout.IsVisible = false;

            Grid.SetColumn(selFrame, 1);
            timelineLabel.TextColor = Color.Black;
            predictionLabel.TextColor = Color.White;
            statisticLabel.TextColor = Color.Black;
            lineupLabel.TextColor = Color.Black;
            rankLabel.TextColor = Color.Black;
        }
        private void SelectStatistic(object sender, EventArgs e)
        {
            fiveTabImage.Source = "five_tab_3.png";

            timelineLayout.IsVisible = false;
            predictionLayout.IsVisible = false;
            statisticLayout.IsVisible = true;
            lineupLayout.IsVisible = false;
            standingLayout.IsVisible = false;

            Grid.SetColumn(selFrame, 2);
            timelineLabel.TextColor = Color.Black;
            predictionLabel.TextColor = Color.Black;
            statisticLabel.TextColor = Color.White;
            lineupLabel.TextColor = Color.Black;
            rankLabel.TextColor = Color.Black;
        }
        private void SelectLineup(object sender, EventArgs e)
        {
            fiveTabImage.Source = "five_tab_4.png";

            timelineLayout.IsVisible = false;
            predictionLayout.IsVisible = false;
            statisticLayout.IsVisible = false;
            lineupLayout.IsVisible = true;
            standingLayout.IsVisible = false;

            Grid.SetColumn(selFrame, 3);
            timelineLabel.TextColor = Color.Black;
            predictionLabel.TextColor = Color.Black;
            statisticLabel.TextColor = Color.Black;
            lineupLabel.TextColor = Color.White;
            rankLabel.TextColor = Color.Black;
        }
        private void SelectRank(object sender, EventArgs e)
        {
            fiveTabImage.Source = "five_tab_5.png";

            timelineLayout.IsVisible = false;
            predictionLayout.IsVisible = false;
            statisticLayout.IsVisible = false;
            lineupLayout.IsVisible = false;
            standingLayout.IsVisible = true;

            Grid.SetColumn(selFrame, 4);
            timelineLabel.TextColor = Color.Black;
            predictionLabel.TextColor = Color.Black;
            statisticLabel.TextColor = Color.Black;
            lineupLabel.TextColor = Color.Black;
            rankLabel.TextColor = Color.White;
        }
        private void BackScreen(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}