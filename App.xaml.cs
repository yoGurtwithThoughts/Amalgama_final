using Amalgama.Core;
using Amalgama.Servis;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Amalgama
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            FontLoader.LoadFonts();

            var dbContext = new DBConnection.ApplicationDbContext();
            dbContext.InitializeDatabaseAsync();
        }
    }

}
