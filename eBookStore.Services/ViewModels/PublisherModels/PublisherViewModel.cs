namespace eBookStore.Services.ViewModels.PublisherModels;
public class PublisherViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Country { get; set; } = default!;
}