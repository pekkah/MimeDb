using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MimeDb.Generator
{
    internal class MimeTypeClassWriter
    {
        private readonly StreamWriter _writer;

        private const string Namespace = "MimeDb";
        private const string Name = "Generated";
        private const string InternalDictionaryName = "Db";

        public MimeTypeClassWriter(StreamWriter writer)
        {
            _writer = writer;
        }

        public void Write(Dictionary<string, MimeEntry> map)
        {
            StartFile();
            StartClass();
            StartDictionary();

            var last = map.Last().Key;
            foreach (var entry in map)
            {
                var extensions = entry.Value.Extensions;
                
                // were only generating mappings for types with extensions
                if (extensions == null || extensions.Length == 0)
                    continue;
                
                foreach (var extension in extensions)
                {
                    WriteAddEntry(extension, entry);
                    _writer.WriteLine(",");
                }
            }

            EndDictionary();
            EndClass();
            EndFile();
        }

        private void EndFile()
        {
            _writer.WriteLine("}");
        }

        private void StartFile()
        {
            _writer.WriteLine("using System.Collections.Generic;");
            _writer.WriteLine();
            _writer.WriteLine($"namespace {Namespace} {{");
        }

        private void WriteAddEntry(string extension, in KeyValuePair<string, MimeEntry> entry)
        {
            var name = $"\"{entry.Key}\"";
            var extensions = BuildExtensionsArray(entry.Value.Extensions);
            var source = $"\"{entry.Value.Source}\"";
            
            _writer.Write($"[\"{extension}\"] = new MimeType({name}, {source}, {extensions})");
        }

        private string BuildExtensionsArray(string[] extensions)
        {
            var b = new StringBuilder();
            b.Append("new string[] {");
            var last = extensions.Last();
            foreach (var extension in extensions)
            {
                b.Append($"\"{extension}\"");

                if (extension != last)
                    b.Append(", ");
            }

            b.Append("}");
            return b.ToString();
        }

        private void EndDictionary()
        {
            _writer.WriteLine("};");
        }

        private void StartDictionary()
        {
            _writer.WriteLine($"internal static Dictionary<string, MimeType> {InternalDictionaryName} = new Dictionary<string, MimeType>()");
            _writer.WriteLine("{");
        }

        private void EndClass()
        {
            _writer.WriteLine("}");
        }

        private void StartClass()
        {
            _writer.WriteLine($"internal class {Name}");
            _writer.WriteLine("{");
        }
    }
}