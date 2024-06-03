namespace NLP;

public class TrainingDataLoader
{
    public static List<(string word, string tag)> LoadTrainingData(string filePath)
    {
        var trainingData = new List<(string word, string tag)>();
        
        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('\t');
            if (parts.Length == 2)
            {
                trainingData.Add((parts[0], parts[1]));
            }
        }
        
        return trainingData;
    }
}