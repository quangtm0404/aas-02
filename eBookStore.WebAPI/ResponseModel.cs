using System.ComponentModel.DataAnnotations;

namespace eBookStore.WebAPI;
public class ResponseModel 
{
    public string Message {get; set;} = default!;
    public object? Result {get; set;}
    public bool IsSuccess {get; set;} = false;
}