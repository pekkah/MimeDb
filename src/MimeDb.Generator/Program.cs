using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MimeDb.Generator
{
    public class Program
    {
        private static async Task<int> Main(string[] args)
        {
            if (args.Length != 2)
                return 1;

            var inputFilePath = Path.GetFullPath(args[0]);
            var outputFilePath = Path.GetFullPath(args[1]);
            Console.WriteLine($"Input: {inputFilePath}");
            Console.WriteLine($"Output: {outputFilePath}");

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Could not open file {inputFilePath}");
            }

            if (File.Exists(outputFilePath))
            {
                Console.WriteLine($"Deleting existing output file {outputFilePath}");
                File.Delete(outputFilePath);
            }

            await using var fileStream = File.OpenRead(inputFilePath);

            var map = await JsonSerializer
                .DeserializeAsync<Dictionary<string, MimeEntry>>(fileStream);
            
            await using var textWriter = new StreamWriter(outputFilePath, false);

            var mimeTypeClassWriter = new MimeTypeClassWriter(textWriter);
            mimeTypeClassWriter.Write(map);
            await textWriter.FlushAsync();
            
            
            return 0;
        }
    }
}