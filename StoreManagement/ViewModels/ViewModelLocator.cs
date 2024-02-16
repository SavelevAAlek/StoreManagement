using Microsoft.Extensions.DependencyInjection;

namespace StoreManagement.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>(); 
    }
}
