using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ResxEditor.Entities
{
    [INotifyPropertyChanged]
    public partial class Translation
    {
        [ObservableProperty]
        private string _culture;

        [ObservableProperty]
        public string _value;
    }
}

