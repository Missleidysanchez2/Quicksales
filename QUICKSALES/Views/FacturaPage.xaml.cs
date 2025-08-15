using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Repositorio;
using QUICKSALES.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using QUICKSALES.Services;
using System.IO;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf;
using Xamarin.Essentials;
using Syncfusion.Drawing;
using System.Threading.Tasks;
using Syncfusion.Licensing;
using PointF = Syncfusion.Drawing.PointF;
using RectangleF = Syncfusion.Drawing.RectangleF;
using SizeF = Syncfusion.Drawing.SizeF;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacturaPage : ContentPage
    {
        private Sale _venta;
        private GeneradorFacturas _generadorFacturas;
        private static int _numeroFacturaActual = 1000;
        private ObservableCollection<Producto> _productosSeleccionados;
        private ClientsRepository _clientsRepository;
        private ProductsRepository _productRepository;

        public FacturaPage(Sale venta)
        {
            InitializeComponent();
            _venta = venta ?? throw new ArgumentNullException(nameof(venta), "La venta no puede ser nula.");
            _generadorFacturas = new GeneradorFacturas();
            _clientsRepository = new ClientsRepository();
            _productRepository = new ProductsRepository();          
            _productosSeleccionados = new ObservableCollection<Producto>();
            productosListView.ItemsSource = _productosSeleccionados;
            numeroFacturaLabel.Text = (++_numeroFacturaActual).ToString();
            LoadClientes();
            LoadProductos();
            ActualizarTotal();
        }

        private void AgregarProducto_Clicked(object sender, EventArgs e)
        {
            if (productoPicker.SelectedItem is Producto productoSeleccionado)
            {
                if (int.TryParse(cantidadEntry.Text, out int cantidad) && cantidad > 0)
                {
                    // Verificar si hay suficiente stock
                    if (productoSeleccionado.Stock >= cantidad)
                    {
                        // Restar la cantidad del producto del stock
                        if (_productRepository.RestarCantidadProducto(productoSeleccionado.Id, cantidad))
                        {
                            // Agregar el producto a la lista de productos seleccionados en la venta
                            _productosSeleccionados.Add(new Producto
                            {
                                Id = productoSeleccionado.Id,
                                NombreP = productoSeleccionado.NombreP,
                                Precio = productoSeleccionado.Precio,
                                Cantidad = cantidad
                            });

                            // Limpiar campos después de agregar el producto
                            productoPicker.SelectedIndex = -1;
                            precioEntry.Text = string.Empty;
                            cantidadEntry.Text = string.Empty;

                            ActualizarTotal();
                        }
                        else
                        {
                            DisplayAlert("Error", "No se pudo restar la cantidad del producto del stock.", "OK");
                        }
                    }
                    else
                    {
                        DisplayAlert("Error", "No hay suficiente stock disponible.", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Ingrese una cantidad válida.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Seleccione un producto.", "OK");
            }
        }



        private void ActualizarTotal()
        {
            decimal subtotal = 0;
            foreach (var producto in _productosSeleccionados)
            {

                subtotal += producto.Precio * producto.Cantidad;
            }

            decimal iva = subtotal * 0.15m; // Tasa de IVA del 15%
            decimal total = subtotal + iva;

            subtotalLabel.Text = $"C${subtotal:N2}";
            ivaLabel.Text = $"C${iva:N2}";
            totalLabel.Text = $"C${total:N2} (IVA incluido)";
            Console.WriteLine($"Total actualizado: Subtotal = {subtotal:N2}, IVA = {iva:N2}, Total = {total:N2}");
        }

        private void OnClienteSelected(object sender, EventArgs e)
        {
            if (clientePicker.SelectedItem is Cliente cliente)
            {
                // Asignar el cliente a la venta
                _venta.Cliente = cliente.Nombre;
                Console.WriteLine($"Cliente seleccionado: {cliente.Nombre}");

                // Actualizar el número de teléfono
                telefonoEntry.Text = cliente.Telefono; // Ajusta el nombre de la propiedad según tu modelo
            }
            else
            {
                Console.WriteLine("Error: Cliente no válido.");
            }
        }

        private void LoadClientes()
        {
            try
            {
                var clientes = _clientsRepository.GetClients().ToList();
                clientePicker.ItemsSource = clientes;
                clientePicker.SelectedItem = clientes.FirstOrDefault(c => c.Nombre == _venta.Cliente);
                Console.WriteLine("Clientes cargados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar clientes: {ex.Message}");
            }
        }
        private void LoadProductos()
        {
            try
            {
                var productos = _productRepository.GetProducts().ToList();
                if (productos != null && productos.All(p => p != null))
                {
                    productoPicker.ItemsSource = productos;
                    Console.WriteLine("Productos cargados correctamente.");
                }
                else
                {
                    Console.WriteLine("Error: Algunos productos son nulos o no se encontraron productos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar productos: {ex.Message}");
            }
        }
        private async void OnProductoSelected(object sender, EventArgs e)
        {
            if (productoPicker.SelectedItem is Producto productoSeleccionado)
            {
                precioEntry.Text = productoSeleccionado.Precio.ToString("C");
                cantidadEntry.Text= productoSeleccionado.Stock.ToString("C");
            }
        }


        private async Task CompartirPdfPorWhatsApp(string pdfPath, string numeroTelefono)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Enviar Factura",
                File = new ShareFile(pdfPath)
            });
            Console.WriteLine("Factura compartida por WhatsApp.");
        }

        private string GenerarPdfFactura(Factura factura)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
            PdfFont regularFont = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
            PdfFont thankYouFont = new PdfStandardFont(PdfFontFamily.Helvetica, 16);

            string imagePath = "QUICKSALES.Resources.logo.jpeg"; // Ruta relativa al archivo de imagen
            Stream logoStream = typeof(FacturaPage).Assembly.GetManifestResourceStream(imagePath);
            if (logoStream != null)
            {
                PdfBitmap logo = new PdfBitmap(logoStream);
                graphics.DrawImage(logo, new RectangleF(20, 20, 100, 50));
            }

            graphics.DrawString("FACTURA QUICKSALES", headerFont, PdfBrushes.Black, new PointF(150, 60));
            graphics.DrawString($"Cliente: {factura.Cliente}", regularFont, PdfBrushes.Black, new PointF(0, 100));
            graphics.DrawString($"Número de Factura: {factura.NumeroFactura}", regularFont, PdfBrushes.Black, new PointF(0, 120));
            graphics.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", regularFont, PdfBrushes.Black, new PointF(0, 140));

            PdfGrid productGrid = new PdfGrid();
            productGrid.Columns.Add(4);
            productGrid.Headers.Add(1);
            PdfGridRow productHeader = productGrid.Headers[0];
            productHeader.Cells[0].Value = "Producto";
            productHeader.Cells[1].Value = "Cantidad";
            productHeader.Cells[2].Value = "Precio Unitario";
            productHeader.Cells[3].Value = "Precio Total";

            foreach (var producto in factura.Productos)
            {
                PdfGridRow row = productGrid.Rows.Add();
                row.Cells[0].Value = producto.NombreP;
                row.Cells[1].Value = producto.Cantidad.ToString();
                row.Cells[2].Value = producto.Precio.ToString("C");
                row.Cells[3].Value = (producto.Precio * producto.Cantidad).ToString("C");
            }

            productGrid.Draw(graphics, new PointF(0, 180));

            float yPosition = 180 + (factura.Productos.Count + 1) * 20;
            graphics.DrawString($"Subtotal: {factura.Subtotal:C}", regularFont, PdfBrushes.Black, new PointF(0, yPosition));
            yPosition += 20;
            graphics.DrawString($"IVA (15%): {factura.IVA:C}", regularFont, PdfBrushes.Black, new PointF(0, yPosition));
            yPosition += 40;
            string thankYouMessage = "¡GRACIAS POR SU COMPRA!";
            SizeF textSize = thankYouFont.MeasureString(thankYouMessage);
            float xPosition = (page.GetClientSize().Width - textSize.Width) / 2;
            graphics.DrawString(thankYouMessage, thankYouFont, PdfBrushes.Black, new PointF(xPosition, yPosition));

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FacturaQuicksales.pdf");
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                document.Save(stream);
            }
            document.Close(true);

            Console.WriteLine("PDF de factura generado.");
            return filePath;
        }

        public static int ObtenerProximoNumeroFactura()
        {
            return ++_numeroFacturaActual;
        }

        private void Imprimir_Clicked(object sender, EventArgs e)
        {
            var factura = _generadorFacturas.GenerarFactura(_venta.Cliente, new List<Producto>(_productosSeleccionados));
            DependencyService.Get<IImpresora>().Imprimir(factura.ToString());
            Console.WriteLine("Factura impresa.");
        }

        private async void EnviarWhatsApp_Clicked(object sender, EventArgs e)
        {
            var factura = _generadorFacturas.GenerarFactura(_venta.Cliente, new List<Producto>(_productosSeleccionados));
            var pdfPath = GenerarPdfFactura(factura);

            string numeroTelefono = telefonoEntry.Text;
            if (!string.IsNullOrEmpty(numeroTelefono))
            {
                try
                {
                    await CompartirPdfPorWhatsApp(pdfPath, numeroTelefono);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                    Console.WriteLine($"Error al compartir por WhatsApp: {ex.Message}");
                }
            }
            else
            {
                await DisplayAlert("Error", "Ingrese un número de teléfono válido.", "OK");
                Console.WriteLine("Error: Número de teléfono no válido.");
            }
        }
    }
}
