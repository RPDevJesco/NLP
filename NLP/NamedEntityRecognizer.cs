namespace NLP;

/// <summary>
/// Step 6:
/// Identify entities such as names of people, organizations, locations, dates, etc.
/// POS tags can help in building features for NER systems.
/// </summary>
public class NamedEntityRecognizer
{
    // Todo: Add PERSON names, Location and Organizations to training data.
    private static readonly Dictionary<string, string> NamedEntities = new Dictionary<string, string>
    {
        { "john", "PERSON" },
        { "doe", "PERSON" },
        { "new york", "LOCATION" },
        { "microsoft", "ORGANIZATION" },
        // Add more named entities and their types
    };

    public List<(string word, string entity)> RecognizeEntities(List<(string word, string tag)> taggedSentence)
    {
        var entities = new List<(string word, string entity)>();
        
        for (int i = 0; i < taggedSentence.Count; i++)
        {
            var word = taggedSentence[i].word.ToLower();
            var entity = "O"; // Default: Outside any named entity

            if (NamedEntities.ContainsKey(word))
            {
                entity = NamedEntities[word];
            }
            else if (i < taggedSentence.Count - 1)
            {
                // Check for multi-word entities
                var nextWord = taggedSentence[i + 1].word.ToLower();
                var combinedWord = word + " " + nextWord;

                if (NamedEntities.ContainsKey(combinedWord))
                {
                    entity = NamedEntities[combinedWord];
                    entities.Add((combinedWord, entity));
                    i++; // Skip the next word since it's part of the entity
                    continue;
                }
            }

            entities.Add((word, entity));
        }
        
        return entities;
    }
}