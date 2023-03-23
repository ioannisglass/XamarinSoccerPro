using System;
using FFImageLoading.Forms.Platform;
using Foundation;
using LabelHtml.Forms.Plugin.iOS;
using Newtonsoft.Json;
using UIKit;
using WindowsAzure.Messaging;

namespace SokkerPro.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            HtmlLabelRenderer.Initialize();
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            LoadApplication(new App());

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                var authOptions = UserNotifications.UNAuthorizationOptions.Alert | UserNotifications.UNAuthorizationOptions.Badge | UserNotifications.UNAuthorizationOptions.Sound;
                UserNotifications.UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, new NSSet());
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                var notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound | UIRemoteNotificationType.NewsstandContentAvailability;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }

            //UILocalNotification notification = new UILocalNotification();
            //notification.FireDate = NSDate.Now;
            //notification.AlertBody = new NSString("Init Alert");
            //notification.TimeZone = NSTimeZone.DefaultTimeZone;
            //notification.SoundName = UILocalNotification.DefaultSoundName;
            //UIApplication.SharedApplication.ScheduleLocalNotification(notification);

            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            //ProcessNotification(userInfo, false);
            UILocalNotification notification = new UILocalNotification();
            notification.FireDate = NSDate.Now;
            notification.AlertBody = new NSString("New notification from server");
            notification.TimeZone = NSTimeZone.DefaultTimeZone;
            notification.SoundName = UILocalNotification.DefaultSoundName;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;

                if (aps.ContainsKey(new NSString("alert")))
                    alert = (aps[new NSString("alert")] as NSString).ToString();

                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        NSString alertKey = new NSString("alert");
                        UILocalNotification notification = new UILocalNotification();
                        notification.FireDate = NSDate.Now;
                        notification.AlertBody = aps.ObjectForKey(alertKey) as NSString;
                        notification.TimeZone = NSTimeZone.DefaultTimeZone;
                        notification.SoundName = UILocalNotification.DefaultSoundName;
                        UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                    }
                }
            }
        }
    }
}
