using MediatR;

namespace User.Domain.Entities.Commands.Follow
{
    public record FollowCommand : IRequest<string>
    {
        public Guid UserId { get; init; }
        public Guid TargetId { get; init; }
    }
}