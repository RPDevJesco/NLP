namespace NLP;

/// <summary>
/// Step 3:
/// Stemming reduces words to their root form, which helps in understanding the core meaning of the words.
/// </summary>
public class Stemmer
{
    public static string Stem(string word)
    {
        // Simple example of stemming logic (just for demonstration purposes)
        if (word.EndsWith("ing"))
        {
            return word.Substring(0, word.Length - 3);
        }
        else if (word.EndsWith("ed"))
        {
            return word.Substring(0, word.Length - 2);
        }
        return word;
    }

    public static List<string> StemWords(List<string> words)
    {
        List<string> stemmedWords = new List<string>();
        foreach (var word in words)
        {
            stemmedWords.Add(Stem(word));
        }
        return stemmedWords;
    }
}