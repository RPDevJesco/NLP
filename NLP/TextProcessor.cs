using System.Text.RegularExpressions;

namespace NLP;

/// <summary>
/// Step 1:
/// The first step in NLP is to process the text by cleaning it and breaking it down into tokens (words).
/// This involves removing punctuation, converting text to lowercase, and splitting it into words.
/// </summary>
public class TextProcessor
{
    public static List<string> Tokenize(string text)
    {
        // Convert to lowercase
        text = text.ToLower();

        // Remove punctuation using regex
        text = Regex.Replace(text, @"[^\w\s]", "");

        // Split text into words (tokens)
        List<string> tokens = new List<string>(text.Split(' '));

        // Remove empty tokens
        tokens.RemoveAll(token => token == "");

        return tokens;
    }
}