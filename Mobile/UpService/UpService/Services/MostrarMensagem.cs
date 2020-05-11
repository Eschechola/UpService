using Xamarin.Forms;

namespace UpService.Services
{
    public class MostrarMensagem
    {
        public static void Mostrar(string mensagem)
        {
            MessagingCenter.Send(mensagem, "Mensagem");
        }
    }
}
