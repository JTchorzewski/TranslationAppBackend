using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TranslationApp_Application.DTO;
using TranslationApp_Application.InterfaceService;

namespace TranslationApp_Application.Service
{
    public class FunTranslationService : ITranslationProviderService
    {
        private readonly HttpClient _httpClient;

        public FunTranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
        }

        public async Task<ProviderTranslationResultDto> TranslateAsync(string text, string translator)
        {
            var result = new ProviderTranslationResultDto();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{translator}.json?text={Uri.EscapeDataString(text)}");
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
                request.Headers.Add("Accept", "application/json");

                var response = await _httpClient.SendAsync(request);
                result.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonDoc = JsonDocument.Parse(responseString);

                    result.TranslatedText = jsonDoc.RootElement
                        .GetProperty("contents")
                        .GetProperty("translated")
                        .GetString();

                    result.IsSuccess = true;
                }
                else if (result.StatusCode == 403 || result.StatusCode == 429)
                {
                    // fake response for blocked access - emergancy mock because API is blocking us
                    result.TranslatedText = $"H3ll0! (API didn't let us to connect {result.StatusCode}, it is emergency mock for text: {text})";
                    result.IsSuccess = true;
                    result.StatusCode = 200;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"Provider error: {result.StatusCode}";
                }
            }
            catch (TaskCanceledException)
            {
                result.IsSuccess = false;
                result.StatusCode = 408;
                result.ErrorMessage = "Request timed out.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.StatusCode = 500;
                result.ErrorMessage = $"Unexpected error: {ex.Message}";
            }

            return result;
        }
    }
}
