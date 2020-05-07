using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using UpServiceAPI.Infra.DAO;
using UpServiceAPI.Infra.Entities;
using UpServiceAPI.Infra.Interfaces;

namespace UpServiceAPI.Infra.Repository
{
    public class ClientRepository : IClientRepository, IDisposable
    {
        public readonly MySqlConnection _connection;

        public ClientRepository(IMySqlConnection connection)
        {
            _connection = connection.GetConnection();
            _connection.Open();
        }

        public Client Get(int id)
        {
            var query = @"
                            SELECT
                            
                            id Id,
                            fk_id_address FkIdAddress,
                            client_name Name,
                            client_password Password,
                            client_email Email,
                            client_cpf Cpf,
                            client_telephone Telephone,
                            client_type Type,
                            client_country Country,
                            client_state State,
                            client_city City,
                            client_zip_code ZipCode,
                            client_street Street,
                            client_home_number HomeNumber
                            
                            FROM up_client
                            WHERE id = @id
                        ";


            return _connection.Query<Client>(query, new { id })
                .ToList()
                .FirstOrDefault();
        }


        public void Insert(Client client)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", client.Name);
            parameters.Add("@Password", client.Password);
            parameters.Add("@Email", client.Email);
            parameters.Add("@CPF", client.CPF);
            parameters.Add("@Telephone", client.Telephone);
            parameters.Add("@Type", client.Type);
            parameters.Add("@Country", client.Country);
            parameters.Add("@State", client.State);
            parameters.Add("@City", client.City);
            parameters.Add("@ZipCode", client.ZipCode);
            parameters.Add("@Street", client.Street);
            parameters.Add("@HomeNumber", client.HomeNumber);

            var query = @"
                            INSERT INTO up_client
                            VALUES
                            
                            (
                                default,
                                @Name,
                                @Password,
                                @Email,
                                @CPF,
                                @Telephone,
                                @Type,
                                @Country,
                                @State,
                                @City,
                                @ZipCode,
                                @Street,
                                @HomeNumber
                            );  
                        ";

            _connection.Query(query, parameters);
        }


        public Client GetByCPF(string cpf)
        {
            var query = @"
                            SELECT
                            
                            id Id,
                            client_name Name,
                            client_password Password,
                            client_email Email,
                            client_cpf Cpf,
                            client_telephone Telephone,
                            client_type Type,
                            client_country Country,
                            client_state State,
                            client_city City,
                            client_zip_code ZipCode,
                            client_street Street,
                            client_home_number HomeNumber
                            
                            FROM up_client
                            WHERE client_cpf = @cpf
                        ";


            return _connection.Query<Client>(query, new { cpf })
                .ToList()
                .FirstOrDefault();
        }


        public Client GetByEmail(string email)
        {
            var query = @"
                            SELECT
                            
                            id Id,
                            client_name Name,
                            client_password Password,
                            client_email Email,
                            client_cpf Cpf,
                            client_telephone Telephone,
                            client_type Type,
                            client_country Country,
                            client_state State,
                            client_city City,
                            client_zip_code ZipCode,
                            client_street Street,
                            client_home_number HomeNumber
                            
                            FROM up_client
                            WHERE client_email = @email
                        ";


            return _connection.Query<Client>(query, new { email })
                .ToList()
                .FirstOrDefault();
        }


        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
