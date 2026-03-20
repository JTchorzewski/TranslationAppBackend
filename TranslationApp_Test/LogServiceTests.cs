using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.InterfaceRepository;
using TranslationApp_Application.Service;
using TranslationApp_Domain.Model;

namespace TranslationApp_Test
{
    public class LogServiceTests
    {
        [Fact]
        public async Task GetLogsAsync_CalculatesTotalPagesCorrectly_AndMapsToDto()
        {
            var mockRepository = new Mock<ILogRepository>();
            var fakeDbItems = new List<RequestLog>
            {
                new RequestLog { Id = Guid.NewGuid(), InputText = "Test 1", Translator = "yoda", IsSuccess = true },
                new RequestLog { Id = Guid.NewGuid(), InputText = "Test 2", Translator = "yoda", IsSuccess = false }
            };
            var totalFakeCount = 15;
            var pageSize = 10;
            var page = 1;

            mockRepository
                .Setup(r => r.GetLogsAsync(page, pageSize, null, null, null, null, null))
                .ReturnsAsync((fakeDbItems, totalFakeCount));

            var service = new LogService(mockRepository.Object);

            var result = await service.GetLogsAsync(page, pageSize, null, null, null, null, null);

            Assert.NotNull(result);
            Assert.Equal(totalFakeCount, result.TotalCount);
            Assert.Equal(2, result.TotalPages);
            Assert.Equal(page, result.Page);
            Assert.Equal(pageSize, result.PageSize);

            Assert.NotEmpty(result.Items);
            Assert.Contains(result.Items, item => item.InputText == "Test 1");
        }
    }
}
