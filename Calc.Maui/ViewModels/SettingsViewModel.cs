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
        ResourceDictionary newTheme = themeName switch
        {
            "iOS" => new Theme_IOS(),
            "Samsung" => new Theme_Samsung(),
            _ => new Theme_Android()
        };

        var merged = Application.Current!.Resources.MergedDictionaries;

        // Vi letar upp det befintliga temat (det som är en instans av våra temaklasser)
        // och byter ut det utan att röra Colors.xaml eller Styles.xaml
        var existingTheme = merged.FirstOrDefault(d => d is Theme_Android || d is Theme_IOS || d is Theme_Samsung);

        if (existingTheme != null)
            merged.Remove(existingTheme);

        merged.Insert(0, newTheme); // Lägg till det nya temat först

        Preferences.Set("SelectedTheme", themeName);
    }
}