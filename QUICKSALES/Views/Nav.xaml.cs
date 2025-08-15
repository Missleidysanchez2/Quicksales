using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QUICKSALES.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Nav : ContentPage
	{
		public Nav ()
		{
			InitializeComponent ();
		}
        private async void OnRegisterSaleButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterSalePage());
        }

        private async void OnAddProductButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductPage());
        }
        private async void OnClientesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientPage());
        }
        private async void OnViewClientsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientListPage());
        }
    }
}