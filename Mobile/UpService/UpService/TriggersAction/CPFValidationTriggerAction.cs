using Xamarin.Forms;
using UpService.Validator;

namespace UpService.TriggersAction
{
    public class CPFValidationTriggerAction : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            bool Valido;
            /*if (sender.Text.Length == 3 || sender.Text.Length == 7)
            {
                sender.Text += ".";
            }
            else if (sender.Text.Length == 11)
            {
                sender.Text += "-";
            }*/
            Valido = Validates.IsCPF(sender.Text);
            sender.TextColor = Valido ? Color.FromHex("#002e6c") : Color.Red;
        }
    }
}
