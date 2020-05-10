namespace UpServiceAPI.Services.DTO
{
    public class ClientDTO
    {

        #region Entity Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Telephone { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }

        #endregion
    }
}
