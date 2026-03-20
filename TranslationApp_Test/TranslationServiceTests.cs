using Moq;
using System;
using System.Threading.Tasks;
using TranslationApp_Application.DTO;
using TranslationApp_Application.InterfaceRepository;
using TranslationApp_Application.InterfaceService;
using TranslationApp_Application.Service;
using TranslationApp_Domain.Model;
using Xunit;

namespace TranslationApp_Test
{
    public class TranslationServiceTests
    {
        [Fact]
        public async Task TranslateTextAsync_WhenProviderSucceeds_ReturnsCorrectDtoAndSavesLog()
        {
            var mockProvider = new Mock<ITranslationProviderService>();
            var mockRepository = new Mock<ILogRepository>();

            var requestDto = new TranslationRequestDto
            {
                Text = "Hello world",
                Translator = "leetspeak"
            };

            var expectedResult = new ProviderTranslationResultDto
            {
                TranslatedText = "H3ll0 w0rld",
                IsSuccess = true,
                StatusCode = 200
            };

            mockProvider
                .Setup(p => p.TranslateAsync(requestDto.Text, requestDto.Translator))
                .ReturnsAsync(expectedResult);

            var service = new TranslationService(mockProvider.Object, mockRepository.Object);

            var result = await service.TranslateTextAsync(requestDto);

            Assert.NotNull(result);
            Assert.Equal(expectedResult.TranslatedText, result.TranslatedText);
            Assert.Equal(requestDto.Translator, result.Translator);

            mockRepository.Verify(r => r.AddAsync(It.Is<RequestLog>(log =>
                log.InputText == requestDto.Text &&
                log.OutputText == expectedResult.TranslatedText &&
                log.IsSuccess == true)), Times.Once);
        }
    }
}
