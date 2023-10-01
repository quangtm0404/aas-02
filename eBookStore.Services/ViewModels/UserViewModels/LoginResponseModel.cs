namespace eBookStore.Services.ViewModels.UserViewModels;
public class LoginResponseModel 
{
    public UserViewModel? User {get; set;} = default!;
    public string Token {get; set;} = default!;
}