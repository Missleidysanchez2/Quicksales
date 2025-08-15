//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using QUICKSALES.Repositorio;
//using QUICKSALES.Models;

//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace QUICKSALES.Views
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class ClientListPage : ContentPage
//    {
//        public ClientListPage()
//        {
//            InitializeComponent();
//        }
//    }
//}

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Repositorio;
using QUICKSALES.Models;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientListPage : ContentPage
    {
        private ClientsRepository _clientsRepository;

        public ClientListPage()
        {
            InitializeComponent();
            _clientsRepository = new ClientsRepository();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadClients();
        }

        private void LoadClients()
        {
            var clients = _clientsRepository.GetClients();
            ClientListView.ItemsSource = clients;
        }
    }
}
