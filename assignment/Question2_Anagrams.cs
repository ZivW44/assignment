using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


public class Question2_Anagrams
{
    public int[] substitutions(string[] words, string[] phrases)
    {
        //Validaton
        validateInput(words, phrases);
        Dictionary<string, HashSet<string>> anagramKeyToWords = new Dictionary<string, HashSet<string>>();

        //Group the anagrams
        foreach (string word in words)
        {
            var anagramKey = new string(word.OrderBy(c => c).ToArray());
            if (!anagramKeyToWords.TryGetValue(anagramKey, out var optionalWords))
            {
                optionalWords = new HashSet<string>();
                anagramKeyToWords.Add(anagramKey, optionalWords);
            }
            optionalWords.Add(word);
        }

        //
        var results = new int[phrases.Length];
        for (int i = 0; i < phrases.Length; i++)
        {
            results[i] = countPhraseVariantsUsingRegex(phrases[i], anagramKeyToWords);
        }

        return results;
    }
    //Validation
    bool validateInput(string[] words, string[] phrases)
    {
        if (words == null || words.Length == 0)
            throw new ArgumentException("Words array must not be null or empty.");

        if (phrases == null || phrases.Length == 0)
            throw new ArgumentException("Phrases array must not be null or empty.");

        // Ensure all words are valid
        var validWords = new HashSet<string>();
        foreach (var word in words)
        {
            if (string.IsNullOrWhiteSpace(word) || word.Length > 20 || !Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                throw new ArgumentException($"Invalid word: '{word}'. Words must be 1-20 alphabetic characters.");
            validWords.Add(word);
        }

        foreach (var phrase in phrases)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                throw new ArgumentException("Phrase cannot be null or empty.");

            var phraseWords = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (phraseWords.Length < 3 || phraseWords.Length > 20)
                throw new ArgumentException($"Phrase '{phrase}' must contain between 3 and 20 words.");

            foreach (var word in phraseWords)
            {
                if (!validWords.Contains(word))
                    throw new ArgumentException($"Phrase '{phrase}' contains unknown word '{word}' not found in words list.");
            }
        }

        return true; // Passed all validations
    }

    //Helper method - regex finder
    int countPhraseVariantsUsingRegex(string phrase, Dictionary<string, HashSet<string>> anagramKeyToWords)
    {
        // Pattern to match each word in the phrase
        string pattern = @"\b[a-zA-Z]+\b";

        long count = 1;

        foreach (Match match in Regex.Matches(phrase, pattern))
        {
            string word = match.Value;

            string anagramKey = new string(word.OrderBy(c => c).ToArray());

            if (anagramKeyToWords.TryGetValue(anagramKey, out HashSet<string> anagrams))
            {
                count *= anagrams.Count;
            }
        }

        if (count > int.MaxValue)
            throw new OverflowException("Too many combinations.");

        return (int)count;
    }
}
