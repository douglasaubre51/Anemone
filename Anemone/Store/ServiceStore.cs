using Microsoft.Extensions.DependencyInjection;

namespace Anemone.Store;

public static class ServiceStore
{
    public static ServiceProvider Services { get; set; } = null!;
    public static Frame RootFrame { get; set; } = null!;
}
