using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TranslationApp_Application.DTO
{
    public class TanslationRequestDto
    {
        [Required(ErrorMessage = "Text is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 500 characters.")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "Translator is required.")]
        public string Translator { get; set; } = string.Empty;
    }
}
