using System;
using System.Collections.ObjectModel;

namespace ResxEditor.Entities
{
    public class ResourceAsset
    {
        public string[] ResourceFilePaths { get; set; }

        public Dictionary<string, ObservableCollection<Translation>> Items { get; set; }
    }
}

