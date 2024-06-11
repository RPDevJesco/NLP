namespace NLP;

/// <summary>
/// Performs contextual analysis on a tagged sentence.
/// </summary>
public class ContextualAnalyzer
{
    private static readonly HashSet<string> PositiveModifiers = new HashSet<string> { "very", "extremely", "really", "incredibly" };
    private static readonly HashSet<string> NegativeModifiers = new HashSet<string> { "not", "barely", "hardly", "rarely" };
    private static readonly HashSet<string> NegationWords = new HashSet<string> { "not", "no", "never", "neither", "nor" };

    public string AnalyzeContext(List<(string word, string tag)> taggedSentence)
    {
        bool hasPositiveModifier = false;
        bool hasNegativeModifier = false;
        bool hasNegation = false;
        string contextualInfo = "";

        for (int i = 0; i < taggedSentence.Count; i++)
        {
            var (word, tag) = taggedSentence[i];

            if (PositiveModifiers.Contains(word.ToLower()))
            {
                hasPositiveModifier = true;
                contextualInfo += "Positive modifier: " + word + "\n";
            }
            else if (NegativeModifiers.Contains(word.ToLower()))
            {
                hasNegativeModifier = true;
                contextualInfo += "Negative modifier: " + word + "\n";
            }
            else if (NegationWords.Contains(word.ToLower()))
            {
                hasNegation = true;
                contextualInfo += "Negation word: " + word + "\n";
            }

            // Check for slang words and their contexts
            if (IsSlangWord(word, taggedSentence, i))
            {
                contextualInfo += "Slang word: " + word + "\n";
            }
        }

        if (hasPositiveModifier)
        {
            contextualInfo += "The sentence contains positive modifiers, intensifying the sentiment.\n";
        }
        if (hasNegativeModifier)
        {
            contextualInfo += "The sentence contains negative modifiers, diminishing the sentiment.\n";
        }
        if (hasNegation)
        {
            contextualInfo += "The sentence contains negation words, potentially inverting the sentiment.\n";
        }

        return contextualInfo;
    }

    private bool IsSlangWord(string word, List<(string word, string tag)> taggedSentence, int index)
    {
        // Implement your slang word detection logic here
        // Analyze the surrounding words and the overall context
        // Return true if the word is used as a slang word, false otherwise
        // You can use a predefined list of slang words, machine learning models, or rule-based approaches

        // Example rule: If the word "lit" is followed by a noun or an adjective, consider it a slang word
        if (word.ToLower() == "lit" && index < taggedSentence.Count - 1)
        {
            var (nextWord, nextTag) = taggedSentence[index + 1];
            if (nextTag == "NN" || nextTag == "JJ")
            {
                return true;
            }
        }

        return false;
    }
}