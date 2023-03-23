using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace SokkerPro.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class MainFirebaseMessagingService : FirebaseMessagingService
    {
        const string Channel = "SokkerPRO";
        const int NOTIFICATION_ID = 1998;
        public override void OnMessageReceived(RemoteMessage message)
        {
            System.Diagnostics.Debug.WriteLine(message.Data.ToString());
            SendNotification(message.Data);
        }

        void SendNotification(IDictionary<string, string> data)
        {
            string title = "SokkerPRO";
            Log.Debug("open mynoti", data["title"] + " : " + data["content"]);
            if (data["type"] == "premium tips")
            {
                title = "Premium Tips";
                if ((bool)App.Current.Properties["PushNoti_PremiumTips"] == false)
                    return;
            }
            if (data["type"] == "tips by tipsters")
            {
                title = "Tips By Tipsters";
                if ((bool)App.Current.Properties["PushNoti_TipsByTipsters"] == false)
                    return;
            }
            if (data["type"] == "live alert")
            {
                title = data["title"];
                if ((bool)App.Current.Properties["PushNoti_Favorite"] == false)
                    return;
                List<int> fav = (new DatabaseService()).CreateConnection().Query<int>("Select count(*) From [favorite] Where [match_id] = " + data["match_id"]);
                if (fav.Count == 1 && fav[0] == 0)
                    return;
            }
            if(data["type"] == "race to goal")
            {
                title = data["title"];
                if ((bool)App.Current.Properties["PushNoti_RaceToGoal"] == false)
                    return;
            }
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }
            var pendingIntent = PendingIntent.GetActivity(this, NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);
            var builder = new NotificationCompat.Builder(this, Channel)
                    .SetContentTitle(title)
                    .SetContentText(data["content"])
                    .SetColorized(true)
                    //.SetLargeIcon(BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.logo))
                    .SetSmallIcon(Resource.Drawable.noti)
                    .SetColor(65280)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);
            var notification = builder.Build();
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Notify(new Random().Next(NOTIFICATION_ID, NOTIFICATION_ID * 2), notification);
            Log.Debug("close mynoti", data["title"] + " : " + data["content"]);
        }
    }
}