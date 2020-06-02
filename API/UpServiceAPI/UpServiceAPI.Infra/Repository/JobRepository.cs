using Dapper;
using System.Linq;
using MySql.Data.MySqlClient;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;
using System.Collections.Generic;

namespace UpServiceAPI.Infra.Repository
{
    public class JobRepository : IJobRepository
    {
        public readonly MySqlConnection _connection;

        public JobRepository(IMySqlConnection connection)
        {
            _connection = connection.GetConnection();
        }

        public Job Get(int id)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State
                            
                            FROM up_job
                            WHERE id = @id
                        ";


                return connection.Query<Job>(query, new { id })
                    .ToList()
                    .FirstOrDefault();
            }
        }


        public void FinishJob(Job job)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", job.Id);
                parameters.Add("@JobState", job.State);
                parameters.Add("@ConclusionDate", job.ConclusionDate);

                var query = @"
                            UPDATE up_job
                            SET
                            
                            job_conclusion_date = @ConclusionDate,
                            job_state = @JobState


                            WHERE
                            id = @Id;
                         ";

                connection.Query(query, parameters);
            }
        }

        public void AcceptOffer(string jobHash, int providerId)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Hash", jobHash);
                parameters.Add("@ProviderID", providerId);

                var query = @"
                            UPDATE up_job
                            SET
                            
                            fk_id_client_job_provider = @ProviderID,
                            job_state = 'AC'


                            WHERE
                            job_hash = @Hash;
                         ";

                connection.Query(query, parameters);
            }
        }

        public Job GetPublishedJob(int id)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State
                            
                            FROM up_job
                            WHERE 

                            id = @id
                            AND
                            job_state = 'PB'
                        ";


                return connection.Query<Job>(query, new { id })
                    .ToList()
                    .FirstOrDefault();
            }
        }

        public IList<Job> GetAllPublishedJobs(int startIndex = 1, int mounOfPage = 1)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@StartIndex", startIndex);
                parameters.Add("@MountOfPage", mounOfPage);

                var query = @"
                            SELECT 
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State

                            FROM up_job
                            
                            WHERE 
                            job_state = 'PB'


                            LIMIT @StartIndex, @MountOfPage
                        ";


                return connection.Query<Job>(query, parameters)
                    .ToList();
            }
        }

        public IList<Job> GetAllFinishedJobs(int clientID)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var query = @"
                            SELECT 
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State

                            FROM up_job


                            WHERE 

                            job_state = 'FS'
                            
                            AND

                            (
                                fk_id_client_job_requester = @clientID
                                OR
                                fk_id_client_job_provider = @clientID
                            )
                        ";

                return connection.Query<Job>(query, new { clientID })
                    .ToList();
            }
        }

        public IList<Job> GetAllAcceptedJobs(int clientID)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var query = @"
                            SELECT 
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State

                            FROM up_job


                            WHERE 

                            job_state = 'AC'
                            
                            AND

                            (
                                fk_id_client_job_requester = @clientID
                                OR
                                fk_id_client_job_provider = @clientID
                            )
                        ";

                return connection.Query<Job>(query, new { clientID })
                    .ToList();
            }
        }


        public int GetMountOfJobsPublisheds()
        {
            using (var connection = _connection)
            {
                connection.Open();

                var query = @"
                            SELECT
                            COUNT(*)

                            FROM up_job

                            WHERE
                            job_state = 'PB'
                        ";

                return connection.Query<int>(query)
                    .ToList()
                    .FirstOrDefault();
            }
        }

        public Job GetByHash(string hash)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State
                            
                            FROM up_job
                            WHERE job_hash = @hash
                        ";


                return connection.Query<Job>(query, new { hash })
                    .ToList()
                    .FirstOrDefault();
            }
        }


        public void Insert(Job job)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@FkIdClientJobRequester", job.FkIdClientJobRequester);
                parameters.Add("@FkIdClientJobProvider", job.FkIdClientJobProvider);
                parameters.Add("@Hash", job.Hash);
                parameters.Add("@Title", job.Title);
                parameters.Add("@Description", job.Description);
                parameters.Add("@PublicationDate", job.PublicationDate);
                parameters.Add("@ConclusionDate", job.ConclusionDate);
                parameters.Add("@JobMaxValue", job.JobMaxValue);
                parameters.Add("@State", job.State);

                var query = @"
                            INSERT INTO up_job
                            VALUES
                            
                            (
                                default,
                                @FkIdClientJobRequester,
                                @FkIdClientJobProvider,
                                @Hash,
                                @Title,
                                @Description,
                                @PublicationDate,
                                @ConclusionDate,
                                @JobMaxValue,
                                @State
                            );       
                        ";

                connection.Query(query, parameters);
            }
        }


        public IList<Job> SearchByTitle(string title)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Title", $"%{title}%");

                var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State

                            FROM up_job
                            
                            WHERE
                            LOWER(job_title) LIKE @Title

                        ";

                return connection.Query<Job>(query, parameters)
                    .ToList();
            }
        }

        public IList<Job> SearchByCity(string city)
        {
            using (var connection = _connection)
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@City", $"%{city}%");

                var query = @"
                            SELECT
                            
                            up_job.id Id,
                            up_job.fk_id_client_job_requester FkIdClientJobRequester,
                            up_job.fk_id_client_job_provider FkIdClientJobProvider,
                            up_job.job_hash Hash,
                            up_job.job_title Title,
                            up_job.job_description Description,
                            up_job.job_publication_date PublicationDate,
                            up_job.job_conclusion_date ConclusionDate,
                            up_job.job_max_value JobMaxValue,
                            up_job.job_state State

                            FROM up_job

                            JOIN up_client
                            ON up_job.fk_id_client_job_requester = up_client.id
                            
                            
                            WHERE
                            LOWER(up_client.client_city) LIKE @City
                        ";

                return connection.Query<Job>(query, parameters)
                    .ToList();
            }
        }

        public IList<Job> GetAllPublishedJobsByClient(int clientID)
        {
            using (var connection = _connection)
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", clientID);

                var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_client_job_requester FkIdClientJobRequester,
                            fk_id_client_job_provider FkIdClientJobProvider,
                            job_hash Hash,
                            job_title Title,
                            job_description Description,
                            job_publication_date PublicationDate,
                            job_conclusion_date ConclusionDate,
                            job_max_value JobMaxValue,
                            job_state State

                            FROM up_job
                            
                            WHERE
                            fk_id_client_job_requester = @Id

                        ";

                return connection.Query<Job>(query, parameters)
                    .ToList();
            }
        }
    }
}
