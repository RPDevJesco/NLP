namespace NLP;

public class Program
{
    public static void Main(string[] args)
    {
        //TextProcessorMain();
        //StopWordsRemoverMain();
        //StemmingMain();
        //FrequencyModelMain();
        //PartsOfSpeechHMMTaggingMain();
        NamedEntityMain();
        //SentimentAnalysisMain();
        //TextGenerationMain();
    }
    
    private static void TextProcessorMain()
    {
        string sampleText = "Hello, World! This is an example of text processing.";
        List<string> tokens = TextProcessor.Tokenize(sampleText);

        Console.WriteLine("Tokens:");
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }

    private static void StopWordsRemoverMain()
    {
        string sampleText = "This is an example of text processing in C#.";
        List<string> tokens = TextProcessor.Tokenize(sampleText);
        List<string> filteredTokens = StopWordsRemover.RemoveStopWords(tokens);

        Console.WriteLine("Filtered Tokens:");
        foreach (var token in filteredTokens)
        {
            Console.WriteLine(token);
        }
    }

    private static void StemmingMain()
    {
        string sampleText = "This is an example of text processing in C#.";
        List<string> tokens = TextProcessor.Tokenize(sampleText);
        List<string> filteredTokens = StopWordsRemover.RemoveStopWords(tokens);
        List<string> stemmedTokens = Stemmer.StemWords(filteredTokens);

        Console.WriteLine("Stemmed Tokens:");
        foreach (var token in stemmedTokens)
        {
            Console.WriteLine(token);
        }
    }

    private static void FrequencyModelMain()
    {
        string sampleText = "This is an example of text processing in C#. Processing text is fun.";
        List<string> tokens = TextProcessor.Tokenize(sampleText);
        List<string> filteredTokens = StopWordsRemover.RemoveStopWords(tokens);
        List<string> stemmedTokens = Stemmer.StemWords(filteredTokens);
        Dictionary<string, int> frequencyModel = FrequencyModel.BuildFrequencyModel(stemmedTokens);

        Console.WriteLine("Word Frequencies:");
        foreach (var kvp in frequencyModel)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    private static void PartsOfSpeechTaggingMain()
    {
        string sampleSentence = "The quick brown dog jumps over the lazy cat and runs.";
        List<(string word, POSTag tag)> taggedSentence = POSTagger.TagSentence(sampleSentence);

        Console.WriteLine("Tagged Sentence:");
        foreach (var item in taggedSentence)
        {
            Console.WriteLine($"{item.word}: {item.tag}");
        }
    }

    private static void PartsOfSpeechHMMTaggingMain()
    {
        // Path to the training data file
        var trainingDataFilePath = "training_data.txt";
        
        // Load training data
        var trainingData = TrainingDataLoader.LoadTrainingData(trainingDataFilePath);
        
        // Train HMM model
        var trainer = new HMMTrainer(trainingData);
        
        // Initialize POS tagger with the trained model
        var tagger = new HMMPOSTagger(trainer);
        
        // Sample sentence to tag
        string sampleSentence = "This is a sample sentence to tag.";
        var taggedSentence = tagger.TagSentence(sampleSentence);

        Console.WriteLine("Tagged Sentence:");
        foreach (var item in taggedSentence)
        {
            Console.WriteLine($"{item.word}: {item.tag}");
        }
    }

    private static void NamedEntityMain()
    {
        var tagger = new HMMPOSTagger(new HMMTrainer(TrainingDataLoader.LoadTrainingData("training_data.txt")));
        string sampleSentence = "John Doe visited New York.";
        var taggedSentence = tagger.TagSentence(sampleSentence);
        
        var ner = new NamedEntityRecognizer();
        var entities = ner.RecognizeEntities(taggedSentence);

        Console.WriteLine("Named Entities:");
        foreach (var item in entities)
        {
            Console.WriteLine($"{item.word}: {item.entity}");
        }
    }

    private static void SentimentAnalysisMain()
    {
        var tagger = new HMMPOSTagger(new HMMTrainer(TrainingDataLoader.LoadTrainingData("training_data.txt")));
        string sampleSentence = "The movie was excellent and I loved it.";
        var taggedSentence = tagger.TagSentence(sampleSentence);
        
        var analyzer = new SentimentAnalyzer();
        var sentiment = analyzer.AnalyzeSentiment(taggedSentence);

        Console.WriteLine($"Sentiment: {sentiment}");
    }

    private static void TextGenerationMain()
    {
        // Path to the training data file
        var trainingDataFilePath = "training_data.txt";
        
        // Initialize the Markov Chain model with the training data
        var markovChain = new MarkovChain(trainingDataFilePath);
        
        // Generate a new sentence
        string generatedSentence = markovChain.GenerateSentence();

        Console.WriteLine($"Generated Sentence: {generatedSentence}");
    }
}