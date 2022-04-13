namespace User.Shared.Events.Base
{
    public abstract class BaseEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}