using Foundation;
using SokkerPro.iOS;
using SokkerPro.Services;
using SQLite;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseService))]
namespace SokkerPro.iOS
{
    public class DatabaseService : IDBInterface
    {
        public DatabaseService()
        {
        }

        public SQLite.SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "sokkerpro.db";

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string path = Path.Combine(libFolder, sqliteFilename);

            // This is where we copy in the pre-created database
            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("sokkerpro", "db");
                File.Copy(existingDb, path);
            }

            //var iOSPlatform = new SQLite.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLiteConnection(path);

            // Return the database connection 
            return connection;
        }
    }
}