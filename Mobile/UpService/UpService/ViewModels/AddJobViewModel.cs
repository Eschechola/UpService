using UpService.Models;

namespace UpService.ViewModels
{
    public class AddJobViewModel : BaseViewModel
    {
        private Client _Usuario;

        public Client Usuario
        {
            get => _Usuario;
            set => SetProperty<Client>(ref _Usuario, value, nameof(Usuario));
        }
    }
}
