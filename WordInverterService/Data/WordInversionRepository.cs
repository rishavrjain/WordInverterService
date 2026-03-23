using Microsoft.EntityFrameworkCore;
using WordInverterService.DataContext;
using WordInverterService.Models;

namespace WordInverterService.Data
{
    public class WordInversionRepository : IWordInversionRepository
    {
        private AppDbContext context;

        public WordInversionRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<WordInversionRecord> AddRecordAsync(WordInversionRecord record)
        {
            context.WordInversionRecords.Add(record);
            await context.SaveChangesAsync();
            return record;
        }

        public async Task<IEnumerable<WordInversionRecord>> GetAllRecordsAsync()
        {
            var records = await context.WordInversionRecords.ToListAsync();
            return records;
        }

        public async Task<IEnumerable<WordInversionRecord>> FindRecordsByWordAsync(string word)
        {
            var records = await context.WordInversionRecords
                .Where(r => r.OriginalRequest.Contains(word) || r.InvertedResult.Contains(word))
                .ToListAsync();
            return records.Where(r => Matches(r.OriginalRequest, word) || Matches(r.InvertedResult, word));
        }

        public bool Matches(string text, string word)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.Any(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
        }
    }
}
