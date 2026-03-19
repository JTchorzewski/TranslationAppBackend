using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationApp_Application.DTO
{
    public class TranslationResponseDto
    {
        public string? TranslatedText { get; set; }
        public string Translator { get; set; } = string.Empty;
        public Guid RequestId { get; set; }
        public int DurationMs { get; set; }
    }
}
