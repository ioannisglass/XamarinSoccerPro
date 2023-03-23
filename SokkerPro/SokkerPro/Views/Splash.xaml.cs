using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using SokkerPro.Services;
using SokkerPro.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SokkerPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Splash : ContentPage
    {

        string[] productIds = { "monthly.sokkerpro.ios", "featured.sokkerpro.ios" };

        public Splash()
        {
            BindingContext = new BaseViewModel();
            InitializeComponent();
            CheckUpdate();
            //CheckPurchaseAndBuy();
        }

        private async void CheckUpdate()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, App.BACKEND_URL + "/filter/checkUpdate");
                var response = await new HttpClient().SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                if (content == "yes")
                {
                    IDevice device = DependencyService.Get<IDevice>();
                    App.token = device.GetIdentifier();
                    App.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                    App.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
        }

        //private void Monthly_Clicked(object sender, EventArgs e)
        //{
        //    Buyitems_ItemTapped(productIds[0]);
        //}
        //private void Featured_Clicked(object sender, EventArgs e)
        //{
        //    Buyitems_ItemTapped(productIds[1]);
        //}
        ////private void Yearly_Clicked(object sender, EventArgs e)
        ////{
        ////    Buyitems_ItemTapped(productIds[2]);
        ////}

        //private async void CheckPurchaseAndBuy()
        //{
        //    if (await CheckPurchase())
        //    {
        //        Application.Current.MainPage = new NavigationPage(new MainPage());
        //    }
        //    else
        //    {
        //        var items = await GetItems();
        //        if (items == null || !items.Any())
        //        {
        //            await DisplayAlert("Init", "Cannot connect AppStore", "Ok");
        //            return;
        //        }
        //        logo.IsVisible = false;
        //        buyitems.IsVisible = true;
        //    }
        //}

        //private async void Buyitems_ItemTapped(String ProductId)
        //{
        //    if (await PurchaseItem(ProductId))
        //    {
        //        Application.Current.MainPage = new NavigationPage(new MainPage());
        //    }
        //    else
        //    {
        //        //await DisplayAlert("Buy", ProductId, "OK");
        //    }
        //}

        //private async Task<bool> CheckPurchase()
        //{
        //    bool resp = false;
        //    var billing = CrossInAppBilling.Current;
        //    try
        //    {
        //        var connected = await billing.ConnectAsync(ItemType.InAppPurchase);

        //        if (!connected)
        //        {
        //            //Couldn't connect
        //            return false;
        //        }

        //        //check purchases
        //        var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

        //        //check for null just incase
        //        if (purchases == null)
        //        {

        //        }
        //        else
        //        {
        //            resp = true;
        //        }

        //    }
        //    catch (InAppBillingPurchaseException purchaseEx)
        //    {
        //        //Billing Exception handle this based on the type
        //        //await DisplayAlert("Check : InAppBillingPurchaseException", purchaseEx.ToString(), "OK");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Something has gone wrong
        //        //await DisplayAlert("Check : Exception", ex.ToString(), "OK");
        //    }
        //    finally
        //    {
        //        await billing.DisconnectAsync();
        //    }

        //    return resp;
        //}

        //private async Task<IEnumerable<InAppBillingProduct>> GetItems()
        //{
        //    IEnumerable<InAppBillingProduct> resp = null;
        //    var billing = CrossInAppBilling.Current;
        //    try
        //    {

        //        //You must connect
        //        var connected = await billing.ConnectAsync(ItemType.InAppPurchase);

        //        if (!connected)
        //        {
        //            //Couldn't connect
        //            return null;
        //        }

        //        //check purchases
        //        resp = await billing.GetProductInfoAsync(ItemType.InAppPurchase, productIds);
        //    }
        //    catch (InAppBillingPurchaseException purchaseEx)
        //    {
        //        var message = string.Empty;
        //        switch (purchaseEx.PurchaseError)
        //        {
        //            case PurchaseError.AppStoreUnavailable:
        //                message = "Currently the app store seems to be unavailble. Try again later.";
        //                break;
        //            case PurchaseError.BillingUnavailable:
        //                message = "Billing seems to be unavailable, please try again later.";
        //                break;
        //            case PurchaseError.PaymentInvalid:
        //                message = "Payment seems to be invalid, please try again.";
        //                break;
        //            case PurchaseError.PaymentNotAllowed:
        //                message = "Payment does not seem to be enabled/allowed, please try again.";
        //                break;
        //        }

        //        //Decide if it is an error we care about
        //        if (!string.IsNullOrWhiteSpace(message)) {
        //            //Display message to user
        //            //await DisplayAlert("Check : InAppBillingPurchaseException", message, "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Something else has gone wrong, log it
        //        //await DisplayAlert("Check : Exception", ex.ToString(), "OK");
        //    }
        //    finally
        //    {
        //        await billing.DisconnectAsync();
        //    }
        //    return resp;
        //}

        //private async Task<bool> PurchaseItem(string productId, string payload = "SokkerPRO")
        //{
        //    bool resp = false;
        //    var billing = CrossInAppBilling.Current;
        //    try
        //    {
        //        var connected = await billing.ConnectAsync(ItemType.Subscription);
        //        if (!connected)
        //        {
        //            //we are offline or can't connect, don't try to purchase
        //            return false;
        //        }

        //        //check purchases
        //        var purchase = await billing.PurchaseAsync(productId, ItemType.Subscription, payload);

        //        //possibility that a null came through.
        //        if (purchase == null)
        //        {
        //            //did not purchase
        //        }
        //        else
        //        {
        //            //purchased!
        //            resp = true;
        //        }
        //    }
        //    catch (InAppBillingPurchaseException purchaseEx)
        //    {
        //        //Billing Exception handle this based on the type
        //        //await DisplayAlert("Check : InAppBillingPurchaseException", purchaseEx.ToString(), "OK");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Something has gone wrong
        //        //await DisplayAlert("Check : Exception", ex.ToString(), "OK");
        //    }
        //    finally
        //    {
        //        await billing.DisconnectAsync();
        //    }
        //    return resp;
        //}
    }
}