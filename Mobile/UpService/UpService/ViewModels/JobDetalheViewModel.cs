using UpService.Models;

namespace UpService.ViewModels
{
    public class JobDetalheViewModel : BaseViewModel
    {
        private Job _job;

        public Job JobInDetail
        {
            get => _job;
            set => SetProperty<Job>(ref _job, value, nameof(JobInDetail));
        }
    }
}
