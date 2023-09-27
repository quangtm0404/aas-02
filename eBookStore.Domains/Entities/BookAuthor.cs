namespace eBookStore.Domains.Entities;
public class BookAuthor : BaseEntity
{
	public Guid BookId { get; set; } = default!;
	public Book Book { get; set; } = default!;
	public double AuthorOrder { get; set; }
	public double Royality_Percentage { get; set; }
	public Guid AuthorId { get; set; } = default!;
	public Author Author { get; set; } = default!;
}