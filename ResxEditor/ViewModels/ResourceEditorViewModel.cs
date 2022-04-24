using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ResxEditor.Entities;
using ResxEditor.Enumerations;
using ResxEditor.Interfaces;

namespace ResxEditor.ViewModels
{
    public partial class ResourceEditorViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ResourceAsset _resourceAsset; 

        private readonly IResourceAssetManager resourceAssetManager;
        private readonly IErrorHandler errorHandler;
        private readonly ITranslator translator;
        private readonly IDialogHandler dialogHandler;

        public ResourceEditorViewModel(IResourceAssetManager resourceAssetManager, IErrorHandler errorHandler, ITranslator translator, IDialogHandler dialogHandler)
        {
            this.resourceAssetManager = resourceAssetManager;
            this.errorHandler = errorHandler;
            this.translator = translator;
            this.dialogHandler = dialogHandler;
        }

        [ICommand]
        private async Task CreateNewResourceAsset()
        {
            try
            {
                ResourceAsset = null;
                ResourceAsset = new ResourceAsset()
                {
                    Items = new Dictionary<string, ObservableCollection<Translation>>()
                   {
                       { "TOSAcceptButtonText" , new ObservableCollection<Translation>()
                       {
                           new Translation() { Culture = "hu", Value = "Általános szerződési feltételek elfogadása" },
                           new Translation() { Culture = "en", Value = "" },
                           new Translation() { Culture = "de", Value = "" }
                        }
                        },
                       { "BackButtonText" , new ObservableCollection<Translation>()
                       {
                           new Translation() { Culture = "hu", Value = "" },
                           new Translation() { Culture = "en", Value = "" },
                           new Translation() { Culture = "de", Value = "Zurück" }
                        }
                        },
                }
                };
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not create resource asset, exception occured", "Creation failure");
            }
        }

        [ICommand]
        private async Task AddNewLanguage()
        {
            try
            {
                if (ResourceAsset == null)
                {
                    return;
                }

                string newCultureName = await dialogHandler.DisplayPrompt("Add new language", "Provide the culture name", "Add");

                if (string.IsNullOrWhiteSpace(newCultureName))
                {
                    return;
                }

                foreach (var item in ResourceAsset.Items)
                {
                    item.Value.Add(new Translation()
                    {
                        Culture = newCultureName
                    });
                }
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not add new language, exception occured", "Add new lang failure");
            }
        }

        [ICommand]
        private async Task AutoFillAllTranslations()
        {
            try
            {
                foreach (var node in ResourceAsset.Items)
                {
                    await AutoFillTranslations(node);
                }
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not autofill all, exception occured", "AutoFill failure");
            }
        }

        [ICommand]
        private async Task AutoFillTranslations(KeyValuePair<string, ObservableCollection<Translation>> translation)
        {
            try
            {
                Translation existing = translation.Value.FirstOrDefault(z => string.IsNullOrWhiteSpace(z.Value) == false);

                if (existing == null)
                {
                    _ = dialogHandler.DisplayAlert("Input needed", "Please provide at least one translation", "Confirm");
                    return;
                }

                foreach (var trans in translation.Value.Where(z => string.IsNullOrWhiteSpace(z.Value)))
                {
                    trans.Value = await translator.TranslateAsync(GetLanguage(existing.Culture), GetLanguage(trans.Culture), existing.Value);
                }
            }
            catch (Exception ex)
            {
                errorHandler.HandleException(ex, "Could not autofill, exception occured", "AutoFill failure");
            }
        }

        [ICommand]
        private async Task ChooseResourceFromFile()
        {
            try
            {
                FileResult pickResult = await FilePicker.PickAsync(new PickOptions()
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

        private Language GetLanguage(string culture)
        {
            // TODO implement properly
            switch (culture)
            {
                case "hu":
                    return Language.Hungarian;
                case "de":
                    return Language.German;
                case "en":
                    return Language.EnglishGB;
                case "es":
                    return Language.Spanish;
                case "bg":
                    return Language.Bulgarian;
                default:
                    return Language.Bulgarian;
                    break;
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

