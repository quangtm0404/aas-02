namespace eBookStore.Domains.Entities;

public class Role : BaseEntity
{
	public string RoleName { get; set; } = default!;
	public ICollection<User> Users { get; set; } = default!;
}