using System.Collections.Generic;

namespace UpService.Models
{
    public class JobGroup : List<Job>
    {
        public string Nome { get; private set; }
        public JobGroup(string Nome, List<Job> jobs) : base(jobs)
        {
            this.Nome = Nome;
        }
    }
}
