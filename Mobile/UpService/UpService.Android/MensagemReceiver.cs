using Android.Content;
using Android.Widget;

namespace UpService.Droid
{
    [BroadcastReceiver(Enabled = true)]
    class MensagemReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var mensagem = intent.GetStringExtra("mensagem"); //Recebe a mensagem 
            Toast.MakeText(context, mensagem, ToastLength.Long).Show(); //Exibe a mensagem no toast
        }
    }
}