
namespace WalkForum.Domain.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; set; }
    public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation error has been found") {

        this.Errors = errors;
    }   
}
