using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Models;
using QUICKSALES.Repositorio;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterSalePage : ContentPage
    {
        private readonly ProductsRepository _productRepository;
        private readonly ClientsRepository _clientsRepository;
        private readonly SalesRepository _salesRepository;
        public RegisterSalePage()
        {
            InitializeComponent();
            _productRepository = new ProductsRepository();
            _clientsRepository = new ClientsRepository();
            _salesRepository = new SalesRepository();
            BindingContext = this; 
            CargarDatos();
        }

        private async void CargarDatos()
        {
            try
            {
                var productos = _productRepository.GetProducts()?.ToList();
                if (productos == null || !productos.Any())
                {
                    Debug.WriteLine("No se encontraron productos.");
                    await DisplayAlert("Error", "No se encontraron productos.", "Aceptar");
                    return;
                }

                // Validar que cada producto tenga un NombreP no nulo
                productos = productos.Where(p => !string.IsNullOrEmpty(p.NombreP)).ToList();

                ProductPicker.ItemsSource = productos;

                var clientes = _clientsRepository.GetClients()?.ToList();
                if (clientes == null || !clientes.Any())
                {
                    Debug.WriteLine("No se encontraron clientes.");
                    await DisplayAlert("Error", "No se encontraron clientes.", "Aceptar");
                    return;
                }

                ClientPicker.ItemsSource = clientes;

                var colores = productos.Select(p => p.Color).Distinct().ToList();
                if (colores == null || !colores.Any())
                {
                    Debug.WriteLine("No se encontraron colores.");
                    await DisplayAlert("Error", "No se encontraron colores.", "Aceptar");
                    return;
                }

                ColorPicker.ItemsSource = colores;

                var tallas = productos.Select(p => p.Talla).Distinct().ToList();
                if (tallas == null || !tallas.Any())
                {
                    Debug.WriteLine("No se encontraron tallas.");
                    await DisplayAlert("Error", "No se encontraron tallas.", "Aceptar");
                    return;
                }

                SizePicker.ItemsSource = tallas;

                // Manejador de eventos para el Picker
                ProductPicker.SelectedIndexChanged += OnProductSelected;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar datos: {ex.Message}");
                await DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "Aceptar");
            }
        }

        private void OnProductSelected(object sender, EventArgs e)
        {
            try
            {
                var picker = sender as Picker;
                if (picker == null)
                {
                    Debug.WriteLine("El Picker es nulo.");
                    return;
                }

                var selectedProduct = picker.SelectedItem as Producto;
                if (selectedProduct == null)
                {
                    Debug.WriteLine("No se seleccionó ningún producto.");
                    return;
                }

                Debug.WriteLine($"Producto seleccionado: {selectedProduct.NombreP}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción en OnProductSelected: {ex.Message}");
            }
        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            try
            {
                var clienteSeleccionado = (Cliente)ClientPicker.SelectedItem;
                if (clienteSeleccionado == null)
                {
                    await DisplayAlert("Error", "Por favor, seleccione un cliente", "Aceptar");
                    return;
                }

                var productoSeleccionado = (Producto)ProductPicker.SelectedItem;
                if (productoSeleccionado == null)
                {
                    await DisplayAlert("Error", "Por favor, seleccione un producto", "Aceptar");
                    return;
                }

                if (!int.TryParse(QuantityEntry.Text, out int cantidad) || cantidad <= 0)
                {
                    await DisplayAlert("Error", "Por favor, ingrese una cantidad válida", "Aceptar");
                    return;
                }

                var colorSeleccionado = ColorPicker.SelectedItem?.ToString();
                var tallaSeleccionada = SizePicker.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(colorSeleccionado) || string.IsNullOrEmpty(tallaSeleccionada))
                {
                    await DisplayAlert("Error", "Por favor, seleccione un color y una talla válidos", "Aceptar");
                    return;
                }

                // Crear una nueva venta y guardarla en la base de datos
                var nuevaVenta = new Sale 
                {
                    Cliente = clienteSeleccionado.Nombre,
                   NombreProducto = productoSeleccionado.NombreP,
                    Cantidad = cantidad,
                    Color = colorSeleccionado,
                    Talla = tallaSeleccionada,
                    Fecha = DateTime.Now
                };

               _salesRepository.AddSale(nuevaVenta);

                await DisplayAlert("Éxito", "Venta registrada correctamente", "Aceptar");

                // Limpiar los campos después de la inserción
                ClientPicker.SelectedItem = null;
                ProductPicker.SelectedItem = null;
                ColorPicker.SelectedItem = null;
                SizePicker.SelectedItem = null;
                QuantityEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción: {ex.Message}");
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Aceptar");
            }
        }
    

private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var clienteSeleccionado = (Cliente)ClientPicker.SelectedItem;
                Debug.WriteLine(clienteSeleccionado != null ? $"Cliente seleccionado: {clienteSeleccionado.Nombre}" : "No se seleccionó un cliente.");

                if (clienteSeleccionado == null)
                {
                    await DisplayAlert("Error", "Por favor, seleccione un cliente", "Aceptar");
                    return;
                }

                var productoSeleccionado = (Producto)ProductPicker.SelectedItem;
                Debug.WriteLine(productoSeleccionado != null ? $"Producto seleccionado: {productoSeleccionado.NombreP}" : "No se seleccionó un producto.");

                if (productoSeleccionado == null)
                {
                    await DisplayAlert("Error", "Por favor, seleccione un producto", "Aceptar");
                    return;
                }

                if (!int.TryParse(QuantityEntry.Text, out int cantidad) || cantidad <= 0)
                {
                    await DisplayAlert("Error", "Por favor, ingrese una cantidad válida", "Aceptar");
                    return;
                }
                Debug.WriteLine($"Cantidad seleccionada: {cantidad}");

                var colorSeleccionado = ColorPicker.SelectedItem?.ToString();
                var tallaSeleccionada = SizePicker.SelectedItem?.ToString();
                Debug.WriteLine($"Color seleccionado: {colorSeleccionado}");
                Debug.WriteLine($"Talla seleccionada: {tallaSeleccionada}");

                if (string.IsNullOrEmpty(colorSeleccionado) || string.IsNullOrEmpty(tallaSeleccionada))
                {
                    await DisplayAlert("Error", "Por favor, seleccione un color y una talla válidos", "Aceptar");
                    return;
                }

                // Aquí va el código para registrar la venta
                await DisplayAlert("Éxito", "Venta registrada correctamente", "Aceptar");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción: {ex.Message}");
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Aceptar");
            }
        }
    }
}
