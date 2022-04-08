namespace User.Domain.Entities.Models
{
    public class Token
    {
        public Token(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}