namespace Anemone.ViewModels;

public partial class BaseViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    public string pageTitle = string.Empty;
}
