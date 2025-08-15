using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Repositorio;
using QUICKSALES.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage, INotifyPropertyChanged
    {
        private SalesRepository _salesRepository;
        private ProductsRepository _productRepository;
        private ObservableCollection<Sale> _recentSales;

        public DashboardPage()
        {
            InitializeComponent();
            _salesRepository = new SalesRepository();
            _productRepository = new ProductsRepository();
            Ventas = new ObservableCollection<Sale>();
            RecentSales = new ObservableCollection<Sale>();

            MessagingCenter.Subscribe<Application>(this, "UpdateRecentSales", (sender) =>
            {
                UpdateRecentSales();
            });

            LoadData();
        }

        private ObservableCollection<Sale> _ventas;

        public ObservableCollection<Sale> Ventas
        {
            get => _ventas;
            set
            {
                _ventas = value;
                OnPropertyChanged(nameof(Ventas));
            }
        }

        public void AgregarVenta(Sale venta)
        {
            Ventas.Add(venta);
            OnPropertyChanged(nameof(Ventas));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Sale> RecentSales
        {
            get => _recentSales;
            set
            {
                _recentSales = value;
                OnPropertyChanged(nameof(RecentSales));
                OnPropertyChanged(nameof(Totales));
            }
        }

        public decimal Totales => RecentSales.Sum(s => s.Total);

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateRecentSales();
            LoadData();
        }

        private void UpdateRecentSales()
        {
            RecentSales = new ObservableCollection<Sale>(_salesRepository.GetRecentSales(10));
        }

        private void LoadData()
        {
            var recentSales = _salesRepository.GetRecentSales(10);
            var availableProducts = _productRepository.GetProducts();

            RecentSalesCollectionView.ItemsSource = recentSales;
            AvailableProductsCollectionView.ItemsSource = availableProducts;
        }

        private async void OnDetailButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Producto producto)
            {
                await Navigation.PushAsync(new ProductDetailPage(producto));
            }
        }

        private async void OnSaleTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Sale sale)
            {
                await Navigation.PushAsync(new SaleDetailPage(sale));
            }
        }

        private async void OnProductTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Producto producto)
            {
                await Navigation.PushAsync(new ProductDetailPage(producto));
            }
        }

        private void OnProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Producto selectedProduct)
            {
                Navigation.PushAsync(new ProductDetailPage(selectedProduct));
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Sale selectedSale)
            {
                Navigation.PushAsync(new SaleDetailPage(selectedSale));
            }
        }

        private async void OnEditProductClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Producto producto)
            {
                await Navigation.PushAsync(new EditProductPage(producto));
            }
        }

        private async void OnEliminSaleClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Sale sale)
            {
                var confirm = await DisplayAlert("Confirmar", "¿Está seguro de que desea eliminar esta venta?", "Sí", "No");
                if (confirm)
                {
                    System.Diagnostics.Debug.WriteLine($"Eliminando venta con ID: {sale.Id}");
                    _salesRepository.DeleteSale(sale.Id);
                    foreach (var productoVendido in sale.ProductosVendidos)
                    {
                        var producto = _productRepository.GetProductById(productoVendido.Id);
                        producto.Stock += productoVendido.Stock;
                        _productRepository.UpdateProduct(producto);
                    }
                    LoadData();
                }
            }
        }

        private async void OnGenerateInvoiceButtonClicked(object sender, EventArgs e)
        {
            var venta = ObtenerVentaSeleccionada();
            if (venta != null)
            {
                await Navigation.PushAsync(new FacturaPage(venta));
            }
            else
            {
                await DisplayAlert("Error", "No se encontró ninguna venta seleccionada.", "Aceptar");
            }
        }

        private Sale ObtenerVentaSeleccionada()
        {
            var venta = _salesRepository.GetLastSale();
            return venta;
        }

        private async void OnEditSaleClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Sale sale)
            {
                await Navigation.PushAsync(new EditSalePage(sale));
            }
        }

        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Producto producto)
            {
                var confirm = await DisplayAlert("Confirmar", "¿Está seguro de que desea eliminar este producto?", "Sí", "No");
                if (confirm)
                {
                    System.Diagnostics.Debug.WriteLine($"Eliminando producto con ID: {producto.Id}");
                    _productRepository.DeleteProduct(producto.Id);
                    var products = _productRepository.GetProducts();
                    if (!products.Any(p => p.Id == producto.Id))
                    {
                        System.Diagnostics.Debug.WriteLine($"Producto con ID: {producto.Id} eliminado correctamente.");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Fallo al eliminar el producto con ID: {producto.Id}.");
                    }
                    LoadData();
                }
            }
        }
    }
}
