using QUICKSALES.Models;
using System.Linq;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaleDetailPage : ContentPage
    {
        private Sale _sale;

        public SaleDetailPage(Sale sale)
        {
            InitializeComponent();
            _sale = sale ?? throw new ArgumentNullException(nameof(sale));
            BindSaleDetails();
        }

        private void CargarDetalles(Venta venta)
        {
            ClienteLabel.Text = venta.Cliente;
            FechaLabel.Text = venta.Fecha.ToString("dd/MM/yyyy");
            ProductosListView.ItemsSource = venta.ProductosVendidos.Select(p => new { Nombre = p.NombreP, CantidadVendida = p.Stock }); // Ajusta esto según tus propiedades
            TotalLabel.Text = venta.Total.ToString("C");
        }
        private void BindSaleDetails()
        {
            ClienteLabel.Text = _sale.Cliente;
            FechaLabel.Text = _sale.Fecha.ToString("dd/MM");
            ProductosListView.ItemsSource = _sale.ProductosVendidos;
            TotalLabel.Text = _sale.Total.ToString("C");
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
