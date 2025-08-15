using QUICKSALES.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QUICKSALES
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Master = new Nav();
            this.Detail = new NavigationPage(new DashboardPage());
            App.MasterDet= this;
        }

    }
}

