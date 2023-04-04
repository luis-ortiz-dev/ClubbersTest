using System;
using LiteDB;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.Services.DataBase
{
    public class DatabaseProvider
    {
        private static readonly Lazy<DatabaseProvider> instance = new Lazy<DatabaseProvider>(() =>
        {
            return new DatabaseProvider();
        });

        private DatabaseProvider()
        {
        }

        public static DatabaseProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public static LiteDatabase LiteDatabase { get; set; }

        private static LiteDatabase GetDatabaseProvider()
        {
            if (LiteDatabase == null)
            {
                var path = DependencyService
                    .Get<IPathService>()
                    .GetDatabasePath();
                LiteDatabase = new LiteDatabase(path);
            }

            return LiteDatabase;
        }

        public LiteDatabase Current
        {
            get
            {
                var instance = GetDatabaseProvider();
                return instance;
            }
        }
    }
}

