using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.FirebasePushNotification;

namespace SokkerPro.Droid
{
    //You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "1998";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "SokkerPRO";
            }
            
            FirebasePushNotificationManager.Initialize(this, true);
            
        }

    }
}
