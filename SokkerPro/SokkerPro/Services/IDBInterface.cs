using SQLite;

namespace SokkerPro.Services
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}
