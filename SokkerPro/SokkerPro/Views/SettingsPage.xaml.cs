using I18NPortable;
using SokkerPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SokkerPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        MainPage RootPage { get => ((NavigationPage)Application.Current.MainPage).RootPage as MainPage; }
        public SettingsPage()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            initOptions(App.Current.Properties["Lang"].ToString(), false);

            PTICommand.Command = PTCommand.Command = new Command((p) =>
            {
                initOptions("pt", true);
            });
            ENICommand.Command = ENCommand.Command = new Command((p) =>
            {
                initOptions("en", true);
            });

            PushNoti_PremiumTips.IsToggled = (bool)App.Current.Properties["PushNoti_PremiumTips"];
            PushNoti_PremiumTips.Toggled += new EventHandler<ToggledEventArgs>((s, e) =>
            {
                App.Current.Properties["PushNoti_PremiumTips"] = PushNoti_PremiumTips.IsToggled;
            });
            PushNoti_TipsByTipsters.IsToggled = (bool)App.Current.Properties["PushNoti_TipsByTipsters"];
            PushNoti_TipsByTipsters.Toggled += new EventHandler<ToggledEventArgs>((s, e) =>
            {
                App.Current.Properties["PushNoti_TipsByTipsters"] = PushNoti_TipsByTipsters.IsToggled;
            });
            PushNoti_Favorite.IsToggled = (bool)App.Current.Properties["PushNoti_Favorite"];
            PushNoti_Favorite.Toggled += new EventHandler<ToggledEventArgs>((s, e) =>
            {
                App.Current.Properties["PushNoti_Favorite"] = PushNoti_Favorite.IsToggled;
            });
            //PushNoti_LiveAlert.IsToggled = (bool)App.Current.Properties["PushNoti_LiveAlert"];
            //PushNoti_LiveAlert.Toggled += new EventHandler<ToggledEventArgs>((s, e) =>
            //{
            //    App.Current.Properties["PushNoti_LiveAlert"] = PushNoti_LiveAlert.IsToggled;
            //});
            PushNoti_RaceToGoal.IsToggled = (bool)App.Current.Properties["PushNoti_RaceToGoal"];
            PushNoti_RaceToGoal.Toggled += new EventHandler<ToggledEventArgs>((s, e) =>
            {
                App.Current.Properties["PushNoti_RaceToGoal"] = PushNoti_RaceToGoal.IsToggled;
            });
        }

        public void initOptions(string lang, bool bUpdate)
        {
            if (lang == "en")
            {
                PTImage.Source = "radio_unselect.png";
                ENImage.Source = "radio_select.png";
            }
            else
            {
                PTImage.Source = "radio_select.png";
                ENImage.Source = "radio_unselect.png";
            }
            App.Current.Properties["Lang"] = lang;
            App.Current.SavePropertiesAsync();
            I18N.Current.Locale = lang;

            if(bUpdate)
                RootPage.UpdateI18N();
            
        }

        public void UpdateI18N()
        {
            ((BaseViewModel)BindingContext).UpdateI18N();
        }
    }
}