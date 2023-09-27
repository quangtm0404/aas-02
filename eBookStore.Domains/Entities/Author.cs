namespace eBookStore.Domains.Entities
{
	public class Author : BaseEntity
	{
		public string LastName { get; set; } = default!;
		public string FirstName { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string Address { get; set; } = default!;
		public string City { get; set; } = default!;
		public string State { get; set; } = default!;
		public string Zip { get; set; } = default!;
		public string Email { get; set; } = default!;
		public ICollection<BookAuthor> BookAuthors { get; set; } = default!;
	}
}