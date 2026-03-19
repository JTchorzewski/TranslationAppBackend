using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TranslationApp_Application.DTO;
using TranslationApp_Application.InterfaceRepository;
using TranslationApp_Application.InterfaceService;
using TranslationApp_Domain.Model;

namespace TranslationApp_Application.Service
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationProviderService _providerService;
        private readonly ILogRepository _repository;

        public TranslationService(ITranslationProviderService provider, ILogRepository repository)
        {
            _providerService = provider;
            _repository = repository;
        }

        public async Task<TranslationResponseDto> TranslateTextAsync(TranslationRequestDto request)
        {
            var correlationId = Guid.NewGuid();
            var stopwatch = Stopwatch.StartNew();
            var providerResult = await _providerService.TranslateAsync(request.Text, request.Translator);

            stopwatch.Stop();
            var duration = (int)stopwatch.ElapsedMilliseconds;

            var log = new RequestLog
            {
                CorrelationId = correlationId,
                Translator = request.Translator,
                InputText = request.Text.Length > 500 ? request.Text.Substring(0, 500) : request.Text,
                OutputText = providerResult.TranslatedText,
                ProviderStatusCode = providerResult.StatusCode,
                IsSuccess = providerResult.IsSuccess,
                ErrorMessage = providerResult.ErrorMessage,
                DurationMs = duration
            };

            await _repository.AddAsync(log);

            if (!providerResult.IsSuccess)
            {
                throw new Exception($"Translation failed: {providerResult.ErrorMessage}");
            }

            return new TranslationResponseDto
            {
                RequestId = log.Id,
                TranslatedText = providerResult.TranslatedText,
                Translator = request.Translator,
                DurationMs = duration
            };
        }
    }
}
