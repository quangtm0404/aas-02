namespace eBookStore.Services.Utilities;
public class JwtOption
{
    public string Issuer {get ; set;} = default!;
    public string Audience {get; set;} = default!;
    public string Secret {get; set;} = default!;
}