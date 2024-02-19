using MathCore.ViewModels;

namespace StoreManagement.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Заголовок главного окна
        private string _title = "Управление магазином";
        public string Title { get => _title; set => Set(ref _title, value); }
        #endregion

        public MainWindowViewModel()
        {
            
        }
    }
}
