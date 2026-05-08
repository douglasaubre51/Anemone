
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Anemone.ViewModels;

public partial class ProjectsViewModel : BaseViewModel
{
    private readonly ProjectServices _projectServ;

    [ObservableProperty]
    private Project? selectedProject;

    [ObservableProperty]
    private ObservableRangeCollection<Project> projects = [];

    [ObservableProperty]
    private bool isPageLoading;

    public ProjectsViewModel()
    {
        _projectServ = ServiceStore.Services.GetRequiredService<ProjectServices>();
    }

    public void OnProjectClicked(Object sender, ItemClickEventArgs e)
    {
        Debug.WriteLine("Selected project:::");
        Project project = e.ClickedItem as Project;

        ServiceStore.RootFrame.Navigate(typeof(TaskDetails), project);
    }

    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        IsBusy = true;

        try
        {
            if (Projects.Count is not 0)
                return;

            var projects = await _projectServ.GetAllWithAvailableTasksCount() ?? [];
            Projects.AddRange(projects.OrderBy(project => project.Title));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsPageLoading = false;
            IsBusy = false;
        }
    }
}
