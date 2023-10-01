using System.ComponentModel.DataAnnotations;

namespace eBookStore.WebAPI;
public class ResponseModel 
{
    [Key]
    public Guid Id {get;set;} = Guid.NewGuid();
    public string Message {get; set;} = default!;
    public object? Result {get; set;}
}