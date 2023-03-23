using I18NPortable;
using SokkerPro.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SokkerPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var vm = new LoginViewModel();
            vm.CheckToken();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += (str) => DisplayAlert("Error".Translate(), str, "OK".Translate());
            vm.GotoMainPage += () => App.Current.MainPage = new NavigationPage(new MainPage());
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }
    }
}