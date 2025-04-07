using FluentResults;

namespace Services.ResultErrors
{
    public class FieldError : Error
    {
        public string Field { get; }

        public FieldError(string field, string message)
            : base(message)
        {
            Field = field;
        }
    }
}
