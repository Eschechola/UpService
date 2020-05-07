using Dapper;
using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using UpServiceAPI.Infra.DAO;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Infra.Repository
{
    public class JobRepository : IJobRepository, IDisposable
    {
        public readonly MySqlConnection _connection;

        public JobRepository(IMySqlConnection connection)
        {
            _connection = connection.GetConnection();
            _connection.Open();
        }

        public Job Get(int id)
        {
            var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value MaxValue,
                            job_state State
                            
                            FROM up_job
                            WHERE id = @id
                        ";


            return _connection.Query<Job>(query, new { id })
                .ToList()
                .FirstOrDefault();
        }


        public void Insert(Job job)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@FkIdClientJobRequester", job.FkIdClientJobRequester);
            parameters.Add("@FkIdClientJobProvider", job.FkIdClientJobProvider);
            parameters.Add("@Hash", job.Hash);
            parameters.Add("@Title", job.Title);
            parameters.Add("@Description", job.Description);
            parameters.Add("@PublicationDate", job.PublicationDate);
            parameters.Add("@ConclusionDate", job.ConclusionDate);
            parameters.Add("@MaxValue", job.MaxValue);
            parameters.Add("@State", job.State);

            var query = @"
                            INSERT INTO up_job
                            VALUES
                            
                            (
                                default,
                                @FkIdClientJobRequester,
                                @FkIdClientJobProvider,
                                @Hash
                                @Title,
                                @Description,
                                @PublicationDate,
                                @ConclusionDate,
                                @MaxValue,
                                @State
                            );       
                        ";

            _connection.Query(query, parameters);
        }


        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
