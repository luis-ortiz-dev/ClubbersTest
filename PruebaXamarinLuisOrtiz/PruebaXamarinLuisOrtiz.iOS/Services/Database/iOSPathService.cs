using System;
using System.IO;
using PruebaXamarinLuisOrtiz.Helpers;
using PruebaXamarinLuisOrtiz.iOS.Services.Database;
using PruebaXamarinLuisOrtiz.Services.DataBase;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSPathService))]

namespace PruebaXamarinLuisOrtiz.iOS.Services.Database
{
    public class iOSPathService : IPathService
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

