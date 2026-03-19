namespace CentralStore.Shared.Dtos.Admin
{
    public abstract class CustomerBase
    {
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
