using QUICKSALES.Models;
using QUICKSALES.Repositorio;
using System;
using Xamarin.Forms;


namespace QUICKSALES.Views
{
    public partial class RegisterPage : ContentPage
    {
        private UserRepository _userRepository;

        public RegisterPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(usernameEntry.Text) ||
                string.IsNullOrWhiteSpace(passwordEntry.Text) ||
                string.IsNullOrWhiteSpace(confirmPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(emailEntry.Text) ||
                string.IsNullOrWhiteSpace(phoneEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            // Validar que las contraseñas coincidan
            if (passwordEntry.Text != confirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            // Validar el formato del correo electrónico
            if (!IsValidEmail(emailEntry.Text))
            {
                await DisplayAlert("Error", "Invalid email address", "OK");
                return;
            }

            var user = new Usuario
            {
                Nombre = usernameEntry.Text,
                Contreseña = passwordEntry.Text,
                CorreoElectronico = emailEntry.Text,
                Telefono = phoneEntry.Text
            };

            var existingUser = _userRepository.GetUserByUsername(user.Nombre);

            if (existingUser == null)
            {
                _userRepository.SaveUser(user);
                await DisplayAlert("Success", "User registered successfully", "OK");

                // Redirigir a la página de inicio de sesión
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Username already exists", "OK");
            }
        }

        // Método para validar el formato del correo electrónico
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
