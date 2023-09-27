namespace eBookStore.Domains.Entities;
public class Book : BaseEntity
{
	public string Title { get; set; } = default!;
	public string Type { get; set; } = default!;
	public double Price { get; set; }
	public string Advance { get; set; } = default!;
	public string Royalty { get; set; } = default!;
	public DateTime PublishedDate { get; set; } = default!;
	public string Notes { get; set; } = default!;
	public Guid PublisherId { get; set; } = default!;
	public Publisher Publisher { get; set; } = default!;
	public ICollection<BookAuthor> BookAuthors { get; set; } = default!;
}