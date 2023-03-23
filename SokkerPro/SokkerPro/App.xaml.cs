using System;
using Xamarin.Forms;
using SokkerPro.Services;
using SokkerPro.Views;
using I18NPortable;
using System.Reflection;
using Plugin.FirebasePushNotification;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SokkerPro
{
    public partial class App : Application
    {
        public static string BACKEND_URL = "http://api.sokkerpro.com/new";
        public static int APP_FREQ = 10;
        public static string token = "ios";


        public App()
        {

            InitializeComponent();
            
            CrossFirebasePushNotification.Current.Subscribe("SokkerPROv1.2");

            var currentAssembly = GetType().GetTypeInfo().Assembly;

            if (!Current.Properties.ContainsKey("PushNoti_PremiumTips"))
                Current.Properties.Add("PushNoti_PremiumTips", false);
            if (!Current.Properties.ContainsKey("PushNoti_TipsByTipsters"))
                Current.Properties.Add("PushNoti_TipsByTipsters", false);
            if (!Current.Properties.ContainsKey("PushNoti_Favorite"))
                Current.Properties.Add("PushNoti_Favorite", false);
            if (!Current.Properties.ContainsKey("PushNoti_RaceToGoal"))
                Current.Properties.Add("PushNoti_RaceToGoal", false);

            I18N.Current
                .SetLogger(text => Console.WriteLine(text))
                .SetFallbackLocale("en")
                .Init(currentAssembly);
            if (!Current.Properties.ContainsKey("Lang"))
            { 
                Current.Properties.Add("Lang", "en");
            }
            I18N.Current.Locale = Current.Properties["Lang"].ToString();
            App.Current.SavePropertiesAsync();

            DependencyService.Register<MockDataStore>();
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new Splash();
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
