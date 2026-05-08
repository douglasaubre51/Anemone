using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Anemone.ViewModels;

public partial class TaskDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private Models.Task currentTask;
    [ObservableProperty]
    private Project currentProject;

    [ObservableProperty]
    private ObservableRangeCollection<Models.Task> allTasks = [];

    [ObservableProperty]
    private string newTaskContent = string.Empty;

    [ObservableProperty]
    private bool isPageLoading;

    private readonly ProjectServices _projectServices;

    [ObservableProperty]
    private bool isTaskListEmpty;

    public TaskDetailsViewModel()
    {
        _projectServices = ServiceStore.Services.GetRequiredService<ProjectServices>();
    }

    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        IsBusy = true;
        try
        {
            await ReloadTaskCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Task view load error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
            IsPageLoading = false;
        }
    }

    public async System.Threading.Tasks.Task ReloadTaskCollection()
    {
        AllTasks.Clear();

        var projects = await _projectServices.GetAllTasksByProject(CurrentProject.Id) ?? [];
        IsTaskListEmpty = false;

        if (projects.Count is 0)
        {
            // Hide empty list text !
            IsTaskListEmpty = true;

            return;
        }

        // Change finished tasks color !
        projects.Where(task => task.IsDone == true)
            .ToList()
            .ForEach(task => task.StatusColor = "Green");

        var orderedProjects = projects.OrderByDescending(task => task.IsDone);

        AllTasks.AddRange(orderedProjects);
    }

    public async void OnAddBtnClicked(Object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NewTaskContent)) return;

            IsBusy = true;

            Models.Task newTask = new Models.Task
            {
                ProjectId = CurrentProject.Id,
                Content = NewTaskContent
            };

            bool result = await _projectServices.AddTask(newTask);
            if (result is false) return;

            NewTaskContent = string.Empty;

            await ReloadTaskCollection();

        }
        catch (Exception ex)
        {
            Debug.WriteLine("On add error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    public void OnBackBtnClicked(Object sender, RoutedEventArgs e)
    {
        ServiceStore.RootFrame.GoBack();
    }
}
