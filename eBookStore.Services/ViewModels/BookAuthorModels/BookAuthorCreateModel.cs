namespace eBookStore.Services.ViewModels.BookAuthorModels;
public class BookAuthorCreateModel
{
    public double Royality_Percentage { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
}