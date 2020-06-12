using System;

namespace UpService.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int FkIdClientJobRequester { get; set; }
        public int? FkIdClientJobProvider { get; set; }
        public string Hash { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? ConclusionDate { get; set; }
        public double JobMaxValue { get; set; }
        public string State { get; set; }
    }
}
