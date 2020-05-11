using UpService.Validator;
using Xamarin.Forms;

namespace UpService.TriggersAction
{
    public class CEPValidationTriggerAction:TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            bool Valido;

            Valido = Validates.IsCEP(sender.Text);
            sender.TextColor = Valido ? Color.FromHex("#002e6c") : Color.Red;
        }
    }
}
