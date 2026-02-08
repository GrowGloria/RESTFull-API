using Moq;
using Xunit;
using RESTFull_API.Repositories.Interface;

namespace RESTFull_API.Moq
{
    public class RollStatsMoqTests
    {
        [Fact]
        public async Task GetStatsAsyncWhenRollsZero()
        {
            var repoMock = new Mock<IRollRepository>();

            repoMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .

        }
    }
}
