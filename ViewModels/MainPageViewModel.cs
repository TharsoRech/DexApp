using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DexApp.Repository.Interfaces;
using DexApp.Services.BackGroundTask;
using GetYourPlaceApp.Helpers;
namespace DexApp.ViewModels;

public partial class MainPageViewModel:BaseViewModel
{
    #region variables
    IDEXRepository _dexRepository;
    BackgroundTaskRunner<bool> _sendFileRunner;
    #endregion
    
    [ObservableProperty] private bool _isLoading;
    
    public MainPageViewModel()
    {
        if (_dexRepository is null)
            _dexRepository = ServiceHelper.GetService<IDEXRepository>();
    }
    
    public async Task SendDaxFile()
        {
            IsLoading = true;

            try
            {
                _sendFileRunner = new BackgroundTaskRunner<bool>();
                _sendFileRunner.StatusChanged += (serder, e) =>
                {
                    if (e.TaskStatus == BackgroundTaskStatus.Completed && e.Result != null)
                    {
              
       
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
                        });
                    }
                    else if (e.TaskStatus == BackgroundTaskStatus.Failed ||
                             (e.TaskStatus == BackgroundTaskStatus.Completed))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
                        });
                    }
                };
                _sendFileRunner.RunInBackground(async () => await _dexRepository.SendDEXFile());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
    
    [RelayCommand]
    public async Task SendFirstFile()
    {
       
    }
    
    [RelayCommand]
    public async Task SendSecondFile()
    {
       
    }
    
    
}