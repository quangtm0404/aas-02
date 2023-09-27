namespace eBookStore.Domains.Entities;
public class User : BaseEntity
{
	public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string Source { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public Guid RoleId { get; set; } = default!;
	public Role Role { get; set; } = default!;
	public Guid PublisherId { get; set; } = default!;
	public Publisher Publisher { get; set; } = default!;
	public DateTime HireDate { get; set; } = DateTime.Now;
}
