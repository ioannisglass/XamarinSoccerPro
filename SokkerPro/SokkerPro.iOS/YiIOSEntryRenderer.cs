using SokkerPro.iOS;
using SokkerPro.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(YiEntry), typeof(YiIOSEntryRenderer))]
namespace SokkerPro.iOS
{
    class YiIOSEntryRenderer : EntryRenderer
    {
        public YiIOSEntryRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;

            }
        }
    }
}