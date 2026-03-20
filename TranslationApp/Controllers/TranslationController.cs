using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslationApp_Application.DTO;
using TranslationApp_Application.InterfaceService;

namespace TranslationApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        private readonly ILogService _logService;

        public TranslationController(ITranslationService translationService, ILogService logService)
        {
            _translationService = translationService;
            _logService = logService;
        }

        [HttpPost("translate")]
        public async Task<IActionResult> Translate([FromBody] TranslationRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _translationService.TranslateTextAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("translation-logs")]
        public async Task<IActionResult> GetLogs(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? translator = null,
            [FromQuery] bool? isSuccess = null,
            [FromQuery] DateTime? fromUtc = null,
            [FromQuery] DateTime? toUtc = null,
            [FromQuery] string? searchInput = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            // Kontroler nic nie wie o Repozytorium ani o encjach
            var response = await _logService.GetLogsAsync(
                page, pageSize, translator, isSuccess, fromUtc, toUtc, searchInput);

            return Ok(response);
        }
    }
}
