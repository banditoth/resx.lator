using System;
namespace ResxEditor.Entities
{
    public class ResourceAsset
    {
        public string[] ResourceFilePaths { get; set; }

        public Dictionary<string, List<Translation>> Items { get; set; }
    }
}

