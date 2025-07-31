using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordStats
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the name or path of the file you'd like to analyse: ");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput) || !File.Exists(userInput))
            {
                Console.WriteLine("Sorry, couldn’t find that file. Try again with a valid path.");
                return;
            }

            try
            {
                Dictionary<string, int> wordSummary = ProcessFile(userInput);
                DisplayWordSummary(wordSummary);
            }
            catch (Exception error)
            {
                Console.WriteLine("Oops! Something went wrong: " + error.Message);
            }
        }

        static Dictionary<string, int> ProcessFile(string path)
        {
            var wordTracker = new Dictionary<string, int>();

            foreach (string line in File.ReadLines(path))
            {
                string cleanText = Regex.Replace(line, @"[^\w\s]", "").ToLower();
                string[] splitWords = cleanText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in splitWords)
                {
                    if (wordTracker.ContainsKey(word))
                        wordTracker[word]++;
                    else
                        wordTracker[word] = 1;
                }
            }

            return wordTracker;
        }

        static void DisplayWordSummary(Dictionary<string, int> words)
        {
            Console.WriteLine("\n--- Word Frequency Report ---\n");

            foreach (var pair in words.OrderBy(x => x.Key))
            {
                Console.WriteLine($"Word: '{pair.Key}' | Count: {pair.Value}");
            }

            Console.WriteLine($"\nTotal unique words found: {words.Count}");
        }
    }
}
