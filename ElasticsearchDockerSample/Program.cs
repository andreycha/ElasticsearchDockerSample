using System;
using System.Threading.Tasks;

namespace ElasticsearchDockerSample
{
    class Program
    {
        private const string ElasticsearchConnectionString = "http://localhost:9200";

        private static readonly Storage storage = new Storage(ElasticsearchConnectionString);

        static async Task Main(string[] args)
        {
            Console.WriteLine("Type 'save TEXT' to store a document. Type 'search TEXT' to search documents. Type 'q' to quit.");

            bool shouldQuit = false;

            do
            {
                var input = Console.ReadLine();

                (var command, var argument) = ParseInput(input);

                switch (command.ToLower())
                {
                    case "save":
                        await storage.SaveAsync(argument);
                        Console.WriteLine("Document has been saved");
                        break;

                    case "search":
                        var results = await storage.SearchAsync(argument);
                        foreach (var result in results)
                        {
                            Console.WriteLine(result);
                        }
                        break;

                    case "q":
                        shouldQuit = true;
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            } while (!shouldQuit);
        }

        private static (string, string) ParseInput(string input)
        {
            var separatorIndex = input.IndexOf(' ');
            if (separatorIndex < 0)
            {
                return (input, null);
            }
            var command = input.Substring(0, separatorIndex);
            var argument = input.Substring(separatorIndex + 1);
            return (command, argument);
        }
    }
}
