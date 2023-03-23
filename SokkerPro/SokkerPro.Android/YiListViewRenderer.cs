using Android.Content;
using SokkerPro.Droid;
using SokkerPro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(YiListView), typeof(YiListViewRenderer))]
namespace SokkerPro.Droid
{
    public class YiListViewRenderer : ListViewRenderer
    {
        Context _context;

        public YiListViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
        }
    }
}