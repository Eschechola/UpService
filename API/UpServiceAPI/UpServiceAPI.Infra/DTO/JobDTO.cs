using System;
using UpServiceAPI.Infra.Entities;

namespace UpServiceAPI.Infra.DTO
{
    public class JobDTO
    {
        #region Entity Properties

        public int Id { get; set; }
        public int FkIdClientJobRequester { get; set; }
        public int FkIdClientJobProvider { get; set; }
        public string Hash { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ConclusionDate { get; set; }
        public double MaxValue { get; set; }
        public string State { get; set; }

        #endregion


        #region DTO Properties

        public Client Client { get; set; }

        #endregion
    }
}
