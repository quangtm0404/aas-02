using eBookStore.Services.Services.Interfaces;

namespace eBookStore.Services.Services;
public class CurrentTime : ICurrentTime
{
    public DateTime GetCurrentTime() => DateTime.UtcNow;
}