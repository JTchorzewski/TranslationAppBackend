using System;
using System.Collections.Generic;
using System.Text;
using TranslationApp_Application.DTO;
using TranslationApp_Application.InterfaceRepository;
using TranslationApp_Application.InterfaceService;

namespace TranslationApp_Application.Service
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<LogPageResponseDto> GetLogsAsync(
            int page, int pageSize, string? translator, bool? isSuccess,
            DateTime? fromUtc, DateTime? toUtc, string? searchInput)
        {
            // Pobieramy z bazy (tu dostajemy encje domenowe)
            var (items, totalCount) = await _logRepository.GetLogsAsync(
                page, pageSize, translator, isSuccess, fromUtc, toUtc, searchInput);

            // Mapujemy na DTO (żeby nie wyciekła Encja)
            var dtoItems = items.Select(x => new LogItemDto
            {
                Id = x.Id,
                CreatedAtUtc = x.CreatedAtUtc,
                Translator = x.Translator,
                InputText = x.InputText,
                OutputText = x.OutputText,
                IsSuccess = x.IsSuccess,
                DurationMs = x.DurationMs
            }).ToList();

            return new LogPageResponseDto
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = dtoItems
            };
        }
    }
}
