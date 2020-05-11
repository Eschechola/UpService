using System.Text.RegularExpressions;
using UpService.Validator;
using Xamarin.Forms;

namespace UpService.Behaviors
{
    public class TelephoneBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool Digitando;
            if (args.OldTextValue == null || args.OldTextValue.Length < args.NewTextValue.Length)
            {
                Digitando = true;
            }
            else
            {
                Digitando = false;
            }

            if (Digitando)
            {
                
                if (args.NewTextValue.Length == 1)
                {
                    if (!Regex.IsMatch(args.NewTextValue, "[,]"))
                    {
                        ((Entry)sender).Text = "(" + args.NewTextValue;
                    }
                }
                if (args.NewTextValue.Length == 3)
                {
                    ((Entry)sender).Text += ")";
                }
                if (args.NewTextValue.Length == 4)
                {
                    string value = args.NewTextValue;
                    char[] array = value.ToCharArray();

                    if (array[3] != ')')
                    {
                        char aux = array[3];
                        array[3] = ')';

                        string s = string.Empty;
                        for(short i = 0; i<=4; i++)
                        {
                            if (i != 4)
                            {
                                s += array[i];
                            }
                            else
                            {
                                s += aux;
                            }
                        }
                        ((Entry)sender).Text = s;
                    }
                }
                if (args.NewTextValue.Length == 8)
                {
                    ((Entry)sender).Text += "-";
                }
                if (args.NewTextValue.Length == 14)
                {
                    string newValue = args.NewTextValue.Replace(",", "");
                    char[] array = newValue.ToCharArray();
                    if(array[8] == '-')
                    {
                        char aux = array[8];
                        array[8] = array[9];
                        array[9] = aux;
                        string s = string.Empty;
                        foreach(char a in array)
                        {
                            s += a.ToString();
                        }
                        ((Entry)sender).Text = s;
                    }
                    else { return; }
                }
            }
            else
            {
                char[] array = args.NewTextValue.ToCharArray();
                if (array.Length == 13 && array[9] == '-')
                {
                    char aux = array[8];
                    array[8] = array[9];
                    array[9] = aux;
                    string s = string.Empty;
                    foreach (char a in array)
                    {
                        s += a.ToString();
                    }
                    ((Entry)sender).Text = s;
                }
            }

            bool Valido = Validates.IsTelephone(args.NewTextValue);
            ((Entry)sender).TextColor = Valido ? Color.FromHex("#002e6c") : Color.Red;
        }
    }
}
