namespace eBookStore.Services.ViewModels.BookModels;
public class BookCreateModel
{
    public string Title { get; set; } = default!;
    public string Type { get; set; } = default!;
    public double Price { get; set; }
    public string Advance { get; set; } = default!;
    public string Royalty { get; set; } = default!;
    public DateTime PublishedDate { get; set; } = default!;
    public string Notes { get; set; } = default!;
    public Guid PublisherId { get; set; } = default!;
}