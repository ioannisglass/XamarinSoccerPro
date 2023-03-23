using SokkerPro.iOS;
using SokkerPro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_iOS))]
namespace SokkerPro.iOS
{
    public class AdBanner_iOS : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //BannerView bannerView = null;
                //bannerView = new BannerView(AdSizeCons.SmartBannerPortrait, new CGPoint(0, 0));

                //// TODO: change this id to your admob id  
                //bannerView.AdUnitId = "ca-app-pub-3494872771459121/7525316787";
                //foreach (UIWindow uiWindow in UIApplication.SharedApplication.Windows)
                //{
                //    if (uiWindow.RootViewController != null)
                //    {
                //        bannerView.RootViewController = uiWindow.RootViewController;
                //    }
                //}

                //var request = Request.GetDefaultRequest();
                //bannerView.LoadRequest(request);
                //SetNativeControl(bannerView);
            }

        }

    }
}
