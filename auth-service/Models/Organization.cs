namespace auth_service.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plan { get; set; } = "Basic";
        public int MaxUsers { get; set; }
    }

}
