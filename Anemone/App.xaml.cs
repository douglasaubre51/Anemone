using Microsoft.Extensions.DependencyInjection;

namespace Anemone;

public partial class App : Application
{
    private Window? _window;

    public App()
    {
        InitializeComponent();
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceStore.Services = serviceCollection.BuildServiceProvider();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Add Services:
        services.AddSingleton<ProjectServices>();


        // Add ViewModels:
        services.AddSingleton<ProjectsViewModel>();
        services.AddSingleton<TaskDetailsViewModel>();
    }

}
