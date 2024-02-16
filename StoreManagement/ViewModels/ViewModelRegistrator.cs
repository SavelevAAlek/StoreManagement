using Microsoft.Extensions.DependencyInjection;

namespace StoreManagement.ViewModels
{
    static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddScoped<MainWindowViewModel>();
    }
}
