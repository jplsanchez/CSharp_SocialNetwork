using User.Shared.Events.Base;

namespace User.Shared.Events
{
    public class CreatedUser : BaseEvent
    {
        public string? Name { get; set; }
    }
}