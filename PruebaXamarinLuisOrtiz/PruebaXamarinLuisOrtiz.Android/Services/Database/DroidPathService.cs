using System;
using System.IO;
using PruebaXamarinLuisOrtiz.Droid.Services.Database;
using PruebaXamarinLuisOrtiz.Helpers;
using PruebaXamarinLuisOrtiz.Services.DataBase;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidPathService))]

namespace PruebaXamarinLuisOrtiz.Droid.Services.Database
{
    public class DroidPathService : IPathService
    {
        public string GetDatabasePath()
        {
            var pathFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(pathFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, GlobalConstants.DatabaseName);
        }
    }
}

