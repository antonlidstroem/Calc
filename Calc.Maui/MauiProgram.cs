using CommunityToolkit.Maui;
using Calc.Core.Interfaces;
using Calc.Infrastructure.Services;
using Calc.Infrastructure.Repositories;
using Calc.App.ViewModels;
using Calc.App.Views;
using Calc.App.Services;

namespace Calc.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 1. Register Infrastructure Services
        builder.Services.AddSingleton<FileService>();
        builder.Services.AddSingleton<ICalculatorService, CalculatorService>();
        builder.Services.AddSingleton<IArticleRepository, ArticleRepository>();
        builder.Services.AddSingleton<SecurityService>();

        // 2. Register App Services
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        // 3. Register ViewModels
        builder.Services.AddTransient<CalculatorViewModel>();
        builder.Services.AddTransient<ArticlesViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<EditorViewModel>();
        // ArticleDetail is handled by QueryProperty, but VM registration helps if needed

        // 4. Register Views
        builder.Services.AddTransient<CalculatorView>();
        builder.Services.AddTransient<ArticlesView>();
        builder.Services.AddTransient<SettingsView>();
        builder.Services.AddTransient<EditorView>();
        builder.Services.AddTransient<ArticleDetailView>();

        return builder.Build();
    }
}
