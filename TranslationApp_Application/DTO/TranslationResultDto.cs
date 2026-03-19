using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationApp_Application.DTO
{
    public class TranslationResultDto
    {
        public string? TranslatedText { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
