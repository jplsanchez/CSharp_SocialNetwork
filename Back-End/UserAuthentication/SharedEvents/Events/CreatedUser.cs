using User.Shared.Events.Base;

namespace User.Shared.Events
{
    public class CreatedUser : BaseEvent
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}