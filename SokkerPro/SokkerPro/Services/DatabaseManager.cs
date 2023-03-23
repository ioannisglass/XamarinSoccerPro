using Newtonsoft.Json;
using SokkerPro.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SokkerPro.Services
{
    public class DatabaseManager
    {
        private static DatabaseManager instance = null;
        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }
                return instance;
            }
        }

        SQLiteConnection dbConnection;
        public DatabaseManager()
        {
            dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();
        }

        public List<Favorite> GetFavorite()
        {
            return dbConnection.Query<Favorite>("Select * From [favorites]");
        }

        public void AddFavorite(Favorite fav)
        {
            dbConnection.Execute("Insert Into [favorites] (fixture_id, raw) Values(?, ?)", new object[] { fav.fixture_id, fav.raw });
        }

        public void DeleteFavorite(Favorite fav)
        {
            dbConnection.Execute("Delete From [favorites] Where [fixture_id] = ?", new object[] { fav.fixture_id });
        }

        internal bool UpdateFavorite(Fixture fixture)
        {
            List<Favorite> favs = dbConnection.Query<Favorite>("Select * From [favorites] Where [fixture_id] = ?", new object[] { fixture.id });
            if(favs.Count > 0)
            {
                dbConnection.Execute("Update [favorites] Set [raw] = ? Where [fixture_id] = ?", new object[] { JsonConvert.SerializeObject(fixture), fixture.id });
                return true;
            }
            return false;
        }
    }
}
