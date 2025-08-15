
using QUICKSALES.Data;
using QUICKSALES.Models;
using QUICKSALES.Views;
using System;
using System.IO;
using Xamarin.Forms;
using SQLite;
namespace QUICKSALES
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }
        public static DataBase DataBase { get; set; }
        public App()
        {
            InitializeComponent();
            IntializeDatabase();

            MainPage = new NavigationPage(new MainPage ());

           
        }

        private void IntializeDatabase()
        {
            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbpath = System.IO.Path.Combine(folderApp,"UserDatabase.db3" );
            DataBase = new DataBase(dbpath);

                }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}