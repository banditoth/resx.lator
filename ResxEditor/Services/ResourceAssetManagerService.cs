using System;
using System.Collections.ObjectModel;
using System.Resources.NetStandard;
using ResxEditor.Entities;
using ResxEditor.Interfaces;

namespace ResxEditor.Services
{
    public class ResourceAssetManagerService : IResourceAssetManager
    {
        private const string UndefinedResxCultureCode = "Undefined";

        public ResourceAsset GetResourceAssetFromFiles(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return null;

            ResourceAsset assetResult = new ResourceAsset()
            {
                ResourceFilePaths = paths,
                Items = new Dictionary<string, ObservableCollection<Translation>>()
            };

            foreach (var resourceFilePath in paths)
            {
                if (string.IsNullOrWhiteSpace(resourceFilePath))
                    continue;

                if (File.Exists(resourceFilePath) == false)
                    throw new FileNotFoundException("Resource file not found: " + resourceFilePath);

                string culture = GetCultureFromFileName(resourceFilePath);

                using (ResXResourceReader reader = new ResXResourceReader(resourceFilePath))
                {
                    var node = reader.GetEnumerator();
                    while (node.MoveNext())
                    {
                        if (assetResult.Items.ContainsKey(node.Key?.ToString()))
                        {
                            assetResult.Items[node.Key.ToString()].Add(new Translation()
                            {
                                Culture = culture,
                                Value = node.Value?.ToString()
                            });
                        }
                        else
                        {
                            assetResult.Items.Add(node.Key.ToString(), new ObservableCollection<Translation>()
                                {
                                new Translation()
                                {
                                    Culture = culture,
                                    Value = node.Value?.ToString()
                                }
                            }
                            );
                        }
                    }
                }
            }

            return assetResult.Items.Count == 0 ? null : assetResult;
        }

        public void SaveResourceAssetToFiles(ResourceAsset toSave)
        {
            if (toSave == null)
                return;

            foreach (var pathToSave in toSave.ResourceFilePaths)
            {
                string culture = GetCultureFromFileName(pathToSave);

                using (ResXResourceWriter writer = new ResXResourceWriter(pathToSave))
                {
                    foreach (KeyValuePair<string, ObservableCollection<Translation>> node in toSave.Items.Where(z=> z.Value.Any(z=> z.Culture == culture)))
                    {
                        writer.AddResource(node.Key, node.Value.Single(z=> z.Culture == culture).Value);
                    }


                    writer.Generate();
                    writer.Close();
                }
            }
        }

        private string GetCultureFromFileName(string fileName)
        {
            var splittedFileNameParts = Path.GetFileNameWithoutExtension(fileName).Split('.');

            if (splittedFileNameParts.Length < 2)
                return UndefinedResxCultureCode;

            // AppResource.en-GB.resx -> 0: AppResource, 1: en-GB
            return splittedFileNameParts[splittedFileNameParts.Length - 1];
        }
    }
}

