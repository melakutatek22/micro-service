namespace Shared.Events;

public class JobSeekerCreated
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string[] Skills { get; set; } = Array.Empty<string>();
}
