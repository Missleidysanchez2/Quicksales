using Android.Content;
using QUICKSALES.Droid;
using QUICKSALES.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImpresoraImplementation))]
namespace QUICKSALES.Droid
{
    public class ImpresoraImplementation : IImpresora
    {
        public void Imprimir(string contenido)
        {
            var printIntent = new Intent(Intent.ActionSend);
            printIntent.SetType("text/plain");
            printIntent.PutExtra(Intent.ExtraText, contenido);
            var chooserIntent = Intent.CreateChooser(printIntent, "Imprimir usando...");
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(chooserIntent);
        }
    }
}
