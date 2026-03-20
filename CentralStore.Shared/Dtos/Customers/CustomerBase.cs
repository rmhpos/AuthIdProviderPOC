namespace CentralStore.Shared.Dtos.Admin
{
    public record CustomerDtoBase
    {
        public CustomerDtoBase() { }

        public CustomerDtoBase(Guid id,
            string firstName,
            string lastName,
            string email,
            string password,
            DateTime createdAt,
            DateTime updatedAt,
            Guid storeId,
            Guid concurrencyToken)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ConcurrencyToken = concurrencyToken;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ConcurrencyToken { get; set; }
    }
}
