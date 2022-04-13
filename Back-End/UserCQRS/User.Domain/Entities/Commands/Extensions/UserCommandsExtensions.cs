using User.Domain.Commands.User;

namespace User.Domain.Entities.Commands.Extensions
{
    public static class UserCommandsExtensions
    {
        public static bool IsValid(this RegisterUserCommand command)
        {
            if (command == null) return false;
            return true;
        }
    }
}