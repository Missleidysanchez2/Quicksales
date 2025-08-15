using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Models;
using QUICKSALES.Repositorio;

namespace QUICKSALES.Views
{
    public partial class EditSalePage : ContentPage
    {
        private SalesRepository _salesRepository;
        private Sale _sale;

        public EditSalePage(Sale sale)
        {
            InitializeComponent();
            _sale = sale;

            _salesRepository = new SalesRepository();
            BindingContext = _sale;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Actualizar la venta en la base de datos
            _salesRepository.UpdateSale(_sale);

            // Navegar de vuelta a la página anterior
            await Navigation.PopAsync();
        }
    }
}
