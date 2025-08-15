using System;
using Xamarin.Essentials;

namespace QUICKSALES.Services
{
    public class EnviadorWhatsApp
    {
        public void EnviarWhatsApp(string numero, string mensaje)
        {
            var uri = new Uri($"https://api.whatsapp.com/send?phone={numero}&text={mensaje}");
            Launcher.OpenAsync(uri);
        }
    }
}
