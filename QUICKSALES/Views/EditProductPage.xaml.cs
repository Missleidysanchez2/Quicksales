using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Models;
using QUICKSALES.Repositorio;
using System;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductPage : ContentPage
    {
        private Producto _producto;
        private ProductsRepository _productRepository;

        public EditProductPage(Producto producto)
        {
            InitializeComponent();
            _producto = producto;
            _productRepository = new ProductsRepository();
            BindingContext = _producto;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            _productRepository.UpdateProduct(_producto);
            await DisplayAlert("Guardado", "Producto guardado con éxito", "OK");
            await Navigation.PopAsync();
        }
    }
}
