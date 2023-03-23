using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Java.Util;
using SokkerPro.Droid;
using SokkerPro.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDevice))]
namespace SokkerPro.Droid
{
    class AndroidDevice : IDevice
    {
        public string GetIdentifier()
        {
            return "this-is-android-xamarin-test";
        }
    }
}