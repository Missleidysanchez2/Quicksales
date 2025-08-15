using System;
using Xamarin.Forms;
using QUICKSALES.Models;
using QUICKSALES.Repositorio;
using Xamarin.Forms.Xaml;


namespace QUICKSALES.Views
{
    public partial class AddClientPage : ContentPage
    {
        private ClientsRepository _clientsRepository;

        public AddClientPage()
        {
            InitializeComponent();
            _clientsRepository = new ClientsRepository();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string nombre = ClientNameEntry.Text;
            string email = ClientEmailEntry.Text;
            string telefono = ClientPhoneEntry.Text;

            // Crear un nuevo objeto Cliente con los datos ingresados
            Cliente nuevoCliente = new Cliente
            {
                Nombre = nombre,
                Email = email,
                Telefono = telefono
            };

            // Agregar el nuevo cliente a la base de datos
            _clientsRepository.AgregarCliente(nuevoCliente);

            // Mostrar un mensaje de éxito al usuario
            DisplayAlert("Éxito", "Cliente agregado correctamente", "Aceptar");

            // Limpiar los campos después de agregar el cliente
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            ClientNameEntry.Text = string.Empty;
            ClientEmailEntry.Text = string.Empty;
            ClientPhoneEntry.Text = string.Empty;
        }
    }
}

