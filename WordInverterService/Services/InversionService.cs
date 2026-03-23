namespace WordInverterService.Services
{
    public class InversionService
    {
        public string InvertWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var invertedWords = words.Select(word => new string(word.Reverse().ToArray()));
            return string.Join(' ', invertedWords);
        }
    }
}
