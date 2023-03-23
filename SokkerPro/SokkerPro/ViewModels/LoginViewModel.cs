using I18NPortable;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using SokkerPro.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SokkerPro.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Action<string> DisplayInvalidLoginPrompt;
        public Action GotoMainPage;
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                SetProperty(ref email, value);
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, App.BACKEND_URL + "/loginApi");
                request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("token", App.token)
                });
                var response = await new HttpClient().SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                if (content == "Success")
                    GotoMainPage();
                else
                    DisplayInvalidLoginPrompt(content);
            }
            catch (Exception ex)
            {
            }
        }

        public async void CheckToken()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, App.BACKEND_URL + "/checkToken");
                request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("token", App.token)
                });
                var response = await new HttpClient().SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                if (content == "Success")
                    GotoMainPage();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
