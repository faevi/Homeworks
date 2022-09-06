namespace ConsoleApp
{
    internal class FileWordCounter
    {
        private Dictionary<string, int> wordToAmount = new Dictionary<string, int>();

        public FileWordCounter(string filePath)
        {
            StreamReader? streamReader = null;
            try
            {
                using (streamReader = new(filePath))
                {
                    char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                    string[] subs = streamReader.ReadToEnd().Split(delimiterChars);
                    foreach (string element in subs)
                    {
                        if (!wordToAmount.ContainsKey(element))
                        {
                            wordToAmount.Add(element, 1);
                        }
                        else
                        {
                            wordToAmount[element]++;
                        }
                    }
                }                    
            }
            finally
            {
                streamReader?.Dispose();
            }
        }

        public void ShowWords()
        {
            foreach (KeyValuePair<string,int> word in wordToAmount)
            {
                Console.WriteLine($"The word {word.Key} contains in text {word.Value} times");
            }
        }
    }
}
