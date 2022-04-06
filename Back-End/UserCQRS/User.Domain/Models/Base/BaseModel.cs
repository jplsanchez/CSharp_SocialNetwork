namespace User.Domain.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}