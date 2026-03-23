using WordInverterService.Models;

namespace WordInverterService.Data
{
    public interface IWordInversionRepository
    {
        Task<WordInversionRecord> AddRecordAsync(WordInversionRecord record);
        Task<IEnumerable<WordInversionRecord>> GetAllRecordsAsync();
        Task<IEnumerable<WordInversionRecord>> FindRecordsByWordAsync(string word);
    }
}
