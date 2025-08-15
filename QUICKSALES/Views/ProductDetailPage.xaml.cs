using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Models;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage(Producto product)
        {
            InitializeComponent();

            // Mostrar los detalles del producto en las etiquetas correspondientes
            ProductNameLabel.Text = product.NombreP;
            ProductPriceLabel.Text = product.Precio.ToString("C"); // Formato de moneda
            // Agrega más asignaciones de propiedades para otros detalles del producto si es necesario
        }
    }
}
