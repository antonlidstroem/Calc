public partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    public void SetTheme(string themeName)
    {
        ResourceDictionary theme;
        switch (themeName)
        {
            case "iOS": theme = new Theme_iOS(); break;
            case "Samsung": theme = new Theme_Samsung(); break;
            default: theme = new Theme_Android(); break;
        }

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(theme);
        
        // Spara valet
        Preferences.Set("SelectedTheme", themeName);
    }
}
