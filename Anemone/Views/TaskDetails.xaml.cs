using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Navigation;

namespace Anemone.Views;

public sealed partial class TaskDetails : Page
{
    public TaskDetailsViewModel ViewModel = null!;
    private readonly ProjectServices _projectServ = null!;

    public TaskDetails()
    {
        InitializeComponent();
        ViewModel = ServiceStore.Services.GetRequiredService<TaskDetailsViewModel>();
        _projectServ = ServiceStore.Services.GetRequiredService<ProjectServices>();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        ViewModel.CurrentProject = e.Parameter as Project;
        ViewModel.IsPageLoading = true;
        ViewModel.IsBusy = false;
    }

    public async void OnMarkAsUnDoneBtnClicked(object sender, RoutedEventArgs e)
    {
        var button = (AppBarButton)sender;
        var context = button.DataContext as Models.Task;

        ViewModel.IsBusy = true;
        try
        {
            await _projectServ.MarkTaskAsUnDone(context!);
            await ViewModel.ReloadTaskCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("On mark as undone error: " + ex.Message);
        }
        finally
        {
            ViewModel.IsBusy = false;
        }
    }
    public async void OnMarkAsDoneBtnClicked(object sender, RoutedEventArgs e)
    {
        var button = (AppBarButton)sender;
        var context = button.DataContext as Models.Task;

        ViewModel.IsBusy = true;
        try
        {
            await _projectServ.MarkTaskAsDone(context!);
            await ViewModel.ReloadTaskCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("On mark as done error: " + ex.Message);
        }
        finally
        {
            ViewModel.IsBusy = false;
        }
    }

    private async void OnDeleteTaskBtnClicked(object sender, RoutedEventArgs e)
    {
        var button = (AppBarButton)sender;
        var context = button.DataContext as Models.Task;

        ViewModel.IsBusy = true;
        try
        {
            await _projectServ.Delete(context!.Id);
            await ViewModel.ReloadTaskCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("On delete btn error: " + ex.Message);
        }
        finally
        {
            ViewModel.IsBusy = false;
        }
    }
}
