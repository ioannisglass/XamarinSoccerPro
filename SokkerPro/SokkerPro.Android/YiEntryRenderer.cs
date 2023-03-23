using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using SokkerPro.Droid;
using SokkerPro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(YiEntry), typeof(YiEntryRenderer))]
namespace SokkerPro.Droid
{
    public class YiEntryRenderer : EntryRenderer
    {
        public YiEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);
                Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}