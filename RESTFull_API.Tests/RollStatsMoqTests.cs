using Moq;
using Xunit;
using RESTFull_API.Repositories.Interface;
using RESTFull_API.Models;
using RESTFull_API.Services;
using RESTFull_API.DTOs;

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
                .ReturnsAsync(new List<Roll>());

            var service = new RollService(repoMock.Object);

            var from = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var to = new DateTimeOffset(2026, 1, 31, 23, 59, 59, TimeSpan.Zero);

            var stats = await service.GetStatsAsync(new RollStatsQuery { From = from, To = to }, CancellationToken.None);

            Assert.Equal(0, stats.AddedCount);
            Assert.Equal(0, stats.RemovedCount);
            Assert.Null(stats.AverageLength);
            Assert.Null(stats.AverageWeight);

            repoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WhenRollExists_SetsRemovedAt_AndUpdates()
        {
            // Arrange
            var id = Guid.NewGuid();

            var roll = new Roll
            {
                Id = id,
                Length = 10m,
                Weight = 100m,
                AddedAt = DateTimeOffset.UtcNow.AddDays(-2),
                RemovedAt = null
            };

            var repoMock = new Mock<IRollRepository>();

            repoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(roll);

            repoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Roll>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Roll x, CancellationToken _) => x);

            var service = new RollService(repoMock.Object);

            // Act
            var result = await service.RemoveAsync(id, CancellationToken.None);

            // Assert
            Assert.NotNull(result.RemovedAt);

            // Verify: UpdateAsync вызвался 1 раз, и в него передали Roll с нужным Id
            repoMock.Verify(r => r.UpdateAsync(
                It.Is<Roll>(x => x.Id == id && x.RemovedAt != null),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
