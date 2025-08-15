using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES.Models;
using System;

namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        private Usuario _currentUser;

        public UserProfilePage(Usuario currentUser)
        {
            InitializeComponent();

            // Guardar el perfil de usuario actual
            _currentUser = currentUser;

            // Mostrar los detalles del perfil de usuario en los campos de entrada
            NameEntry.Text = _currentUser.Nombre;
            EmailEntry.Text = _currentUser.CorreoElectronico;
            // Agrega más asignaciones de propiedades para otros detalles del perfil de usuario si es necesario
        }

        private void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            // Guardar los cambios realizados en el perfil de usuario
            _currentUser.Nombre = NameEntry.Text;
            _currentUser.CorreoElectronico = EmailEntry.Text;

            // Implementa la lógica para guardar los cambios en la base de datos u otra fuente de datos

            // Mostrar un mensaje de éxito al usuario
            DisplayAlert("Éxito", "Los cambios se han guardado correctamente", "Aceptar");
        }
    }
}
