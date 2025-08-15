 
using QUICKSALES.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QUICKSALES;
using QUICKSALES.Repositorio;
namespace QUICKSALES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private UserRepository _userRepository;

        public LoginPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "Aceptar");
                return;
            }

            var user = _userRepository.GetUserByUsername(username);

            if (user != null)
            {
                if (user.Contreseña == password)
                {
                    await DisplayAlert("Éxito", "Inicio de sesión exitoso", "Aceptar");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Error", "Contraseña incorrecta", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Usuario no encontrado", "Aceptar");
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}