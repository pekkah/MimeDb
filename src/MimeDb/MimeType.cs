namespace MimeDb
{
    public readonly struct MimeType
    {
        public string Type { get;}
        
        public string Source { get;}

        public string[] Extensions { get; }

        public MimeType(string type, string source, string[] extensions)
        {
            Type = type;
            Source = source;
            Extensions = extensions;
        }

        public static bool TryGet(string extension, out MimeType type)
        {
            if (extension.StartsWith("."))
                extension = extension.Substring(1);
            
            return Generated.Db.TryGetValue(extension, out type);
        }
    }
}