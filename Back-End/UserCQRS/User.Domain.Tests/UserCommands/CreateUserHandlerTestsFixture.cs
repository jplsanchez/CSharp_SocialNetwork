using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace User.Domain.Tests.UserCommands
{
    [CollectionDefinition(nameof(CreateUserCollection))]
    public class CreateUserCollection : ICollectionFixture<CreateUserHandlerTestsFixture>
    { }

    public class CreateUserHandlerTestsFixture
    {
    }
}
