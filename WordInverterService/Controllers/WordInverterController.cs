using Microsoft.AspNetCore.Mvc;
using WordInverterService.Data;
using WordInverterService.Dtos;
using WordInverterService.Models;
using WordInverterService.Services;

namespace WordInverterService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordInverterController : ControllerBase
    {
        private IWordInversionRepository repository;
        private InversionService service;

        public WordInverterController(IWordInversionRepository repository, InversionService service)
        {
            this.repository = repository;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> InvertWords([FromBody] InversionRequest inversionRequest)
        {
            if (inversionRequest == null || string.IsNullOrWhiteSpace(inversionRequest.Request))
                return BadRequest("Request cannot be empty.");

            var invertedResult = service.InvertWords(inversionRequest.Request);

            // Convert dto request to model and push to database
            var record = new WordInversionRecord
            {
                OriginalRequest = inversionRequest.Request,
                InvertedResult = invertedResult,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddRecordAsync(record);
            return Ok(new { InvertedResult = invertedResult });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var records = await repository.GetAllRecordsAsync();
            
            if (records == null || !records.Any())
                return NotFound("No records found.");

            return Ok(records);
        }

        [HttpGet("findByWord")]
        public async Task<IActionResult> FindByWord([FromQuery] string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return BadRequest("Word query parameter cannot be empty.");

            var records = await repository.FindRecordsByWordAsync(word);
            
            if (records == null || !records.Any())
                return NotFound($"No records found containing the word '{word}'.");
            
            return Ok(records);
        }
    }
}
