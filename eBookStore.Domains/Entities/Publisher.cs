namespace eBookStore.Domains.Entities;
public class Publisher : BaseEntity
{
	public string Name { get; set; } = default!;
	public string City { get; set; } = default!;
	public string State { get; set; } = default!;
	public string Country { get; set; } = default!;
	public ICollection<User> Users { get; set; } = default!;
	public ICollection<Book> Books { get; set; } = default!;
}