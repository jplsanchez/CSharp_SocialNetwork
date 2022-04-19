using System.Threading.Tasks;
using User.Domain.Handlers.CommandHandler;
using Xunit;

namespace User.Domain.Tests.UserCommands
{
    [Collection(nameof(CreateUserCollection))]
    public class CreateUserHandlerTests
    {
        private readonly CreateUserCommandHandler _commandHandler;

        public CreateUserHandlerTests(CreateUserHandlerTestsFixture userTestsFixture)
        {

        }

        [Fact]
        public async Task Handle_ValidRequest_CreateUser()
        {
            //Arrange

            //Act

            //Assert
            await Task.FromResult(0);
        }
    }
}