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

namespace SokkerPro.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TipsPage : ContentPage
    {
        MainPage RootPage { get => ((NavigationPage)Application.Current.MainPage).RootPage as MainPage; }

        List<Tip> tipItems;
        int tipMode = 0;

        public TipsPage()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Device.StartTimer(TimeSpan.FromSeconds(App.APP_FREQ), () =>
            {
                // Do something
                LoadTips();
                return true; // True = Repeat again, False = Stop the timer
            });
            LoadTips();
        }
        public void UpdateI18N()
        {
            ((BaseViewModel)BindingContext).UpdateI18N();
        }
        void OnTipSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem;
            if (item == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
        public async void LoadTips()
        {
            try
            {
                TimeSpan offset = DateTime.UtcNow - DateTime.Now;
                var request = WebRequest.Create(App.BACKEND_URL + "/tipsApi/" + tipMode) as HttpWebRequest;
                request.Method = "GET";
                string responseContent = null;
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine(responseContent);
                        tipItems = JsonConvert.DeserializeObject<List<Tip>>(responseContent);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                TipsList.ItemsSource = tipItems;
            }
        }

        private void SelectPremiumTips(object sender, EventArgs e)
        {
            twoTabImage.Source = "two_tab_left.png";
            tipMode = 0;
            LoadTips();
        }
        private void SelectTipstersTips(object sender, EventArgs e)
        {
            twoTabImage.Source = "two_tab_right.png";
            tipMode = 1;
            LoadTips();
        }
    }
}