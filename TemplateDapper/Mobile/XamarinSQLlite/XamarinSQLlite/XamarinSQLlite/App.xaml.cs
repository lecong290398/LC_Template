using Microsoft.Data.Sqlite;
using Prism;
using Prism.Ioc;
using SQLite;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using XamarinSQLlite.Model;
using XamarinSQLlite.ViewModels;
using XamarinSQLlite.ViewModels.Forms;
using XamarinSQLlite.Views;
using XamarinSQLlite.Views.Forms;

namespace XamarinSQLlite
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        public SQLiteConnection _connection { get; set; } = null;

        protected override async void OnInitialized()
        {
            InitializeComponent();
            InitialiseConnection();
            if (_connection != null)
            {
                await NavigationService.NavigateAsync("NavigationPage/SimpleLoginPage");
            }
            else
            {
                ///Alert disconection Databases
                await NavigationService.NavigateAsync("NavigationPage/SimpleLoginPage");
            }
        }


        public async void InitialiseConnection()
        {
            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LcXamainSqlLite.db");
            //File.Delete(databasePath);
            //if (File.Exists(databasePath) == false)
            //{
            //    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            //    Stream stream = assembly.GetManifestResourceStream("XamarinSQLlite.Db.LcXamainSqlLite.db");

            //    using (var reader = new System.IO.MemoryStream())
            //    {
            //        await stream.CopyToAsync(reader);
            //        File.WriteAllBytes(databasePath, reader.GetBuffer());
            //    }
            //}
            var databasePath1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LcXamainSqlLite.db");

            var db = new SQLiteConnection(databasePath1);
            db.CreateTable<LC_User>();

            _connection = db;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SimpleLoginPage, LoginPageViewModel>();
        }
    }
}
