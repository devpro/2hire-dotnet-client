using Devpro.Twohire.Abstractions.Providers;
using Devpro.Twohire.Client.UnitTests.Fakes;
using Moq;
using Withywoods.UnitTesting;

namespace Devpro.Twohire.Client.UnitTests.Repositories
{
    public abstract class RepositoryTestBase : HttpRepositoryTestBase
    {
        protected RepositoryTestBase()
            : base()
        {
            Configuration = new FakeConfiguration();
            TokenProviderMock = new Mock<ITokenProvider>();
        }

        protected ITwohireRestApiConfiguration Configuration { get; private set; }

        protected Mock<ITokenProvider> TokenProviderMock { get; private set; }
    }
}
