namespace NLP;

/// <summary>
/// Step 8:
/// Text Generation
/// </summary>
public class TextGenerator
{
    private readonly Random random = new Random();

    public string GenerateSentence(List<(string word, string tag)> taggedSentence)
    {
        var sentence = new List<string>();
        
        foreach (var (word, tag) in taggedSentence)
        {
            sentence.Add(word);
        }
        
        return string.Join(" ", sentence);
    }
}
