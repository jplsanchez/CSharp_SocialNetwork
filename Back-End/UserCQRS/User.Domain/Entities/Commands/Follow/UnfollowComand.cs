using MediatR;

namespace User.Domain.Entities.Commands.Follow
{
    public record UnfollowComand : IRequest<string>
    {
        public Guid UserId { get; init; }
        public Guid TargetId { get; init; }
    }
}