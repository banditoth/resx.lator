using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ResxEditor.Entities;
using ResxEditor.Interfaces;

namespace ResxEditor.ViewModels
{
    [INotifyPropertyChanged]
    public partial class ResourceEditorViewModel
    {
        [ObservableProperty]
        private ResourceAsset _resourceAsset;

        private readonly IFilePicker filePicker;
        private readonly IResourceAssetManager resourceAssetManager;
        private readonly IErrorHandler errorHandler;

        public ResourceEditorViewModel(IFilePicker filePicker, IResourceAssetManager resourceAssetManager, IErrorHandler errorHandler)
        {
            this.filePicker = filePicker;
            this.resourceAssetManager = resourceAssetManager;
            this.errorHandler = errorHandler;
        }

        [ICommand]
        private async Task ChooseResourceFromFile()
        {
            try
            {
                FileResult pickResult = await filePicker.PickAsync(new PickOptions()
                {
                    // TODO filter file types!
                });

                if (string.IsNullOrWhiteSpace(pickResult?.FullPath))
                    return;

                resourceAssetManager.GetResourceAssetFromFiles(GetSimilarResourceFilesFor(pickResult.FullPath));
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not choose resource from file, exception occured", "File pick failure");
            }
        }

        private string[] GetSimilarResourceFilesFor(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutCulture = Path.GetFileNameWithoutExtension(filePath);

            // AppResource.en-GB.resx -> 0: AppResource, 1: en-GB
            string[] fileNameSplitted = fileNameWithoutCulture.Split('.');

            List<string> results = new List<string>();
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Path.GetFileName(file).Contains(fileNameSplitted[0]))
                {
                    results.Add(file);
                }
            }

            return results.Count == 0 ? null : results.ToArray();
        }
    }
}

