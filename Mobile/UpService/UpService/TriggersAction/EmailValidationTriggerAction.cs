using UpService.Validator;
using Xamarin.Forms;

namespace UpService.TriggersAction
{
    public class EmailValidationTriggerAction : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            bool Valido;
            Valido = Validates.IsEmail(sender.Text);
            sender.TextColor = Valido ? Color.FromHex("#17569B") : Color.Red;
        }
    }
}
