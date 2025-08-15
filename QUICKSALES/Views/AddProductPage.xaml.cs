using System;
using Xamarin.Forms;
using QUICKSALES.Models;
using QUICKSALES.Repositorio;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        private ProductsRepository _productsRepository;
        private ImageSource ImageSource;
        private string imagePath;
        public AddProductPage()
        {
            InitializeComponent();
            _productsRepository = new ProductsRepository();
            this.ImageSource = ImageSource.FromFile("Diseño sin titulo");
        }

       
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var product = new Producto
                {
                    NombreP = NameEntry.Text,
                    Talla = SizeEntry.Text,
                    Color = ColorEntry.Text,
                    Precio = decimal.Parse(PriceEntry.Text),
                    Stock = int.Parse(StockEntry.Text),
                  
                };
                _productsRepository.AddProduct(product);
                await DisplayAlert("Éxito", "Producto agregado correctamente", "OK");
                LimpiarCampos();
               
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "OK");
            }
        }
        private void LimpiarCampos()
        {
            NameEntry.Text = string.Empty;
            SizeEntry.Text = string.Empty;
            ColorEntry.Text = string.Empty;
            PriceEntry.Text = string.Empty;
            StockEntry.Text = string.Empty;
        }
    }
}
