using Calc.Maui.Resources.Styles; 
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

namespace Calc.Maui.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    public void SetTheme(string themeName)
    {
        ResourceDictionary theme = themeName switch
        {
            "iOS" => new Theme_IOS(),
            "Samsung" => new Theme_Samsung(),
            _ => new Theme_Android()
        };

        Application.Current!.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(theme);

        Preferences.Set("SelectedTheme", themeName);
    }
}