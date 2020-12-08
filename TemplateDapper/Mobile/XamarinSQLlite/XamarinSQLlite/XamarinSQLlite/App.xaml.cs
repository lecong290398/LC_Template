using Microsoft.Data.Sqlite;
using Prism;
using Prism.Ioc;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
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

        public string DbConnectionString { get; set; } = "";
        public string DbPath { get; set; } = "";


        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SimpleLoginPage");
        }

     

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SimpleLoginPage, LoginPageViewModel>();
        }
    }
}
