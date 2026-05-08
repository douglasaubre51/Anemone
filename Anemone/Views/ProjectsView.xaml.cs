namespace Anemone.Views;

public sealed partial class ProjectsView : Page
{
    public ProjectsViewModel ViewModel { get; set; }

    public ProjectsView()
    {
        InitializeComponent();

        ViewModel = new ProjectsViewModel();

        ViewModel.IsPageLoading = true;
    }
}
