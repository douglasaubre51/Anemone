namespace Anemone.Services;

public class ProjectServices
{
    string _projectUrl = MioApiStore.BaseUrl + "/Project";
    string _taskUrl = MioApiStore.BaseUrl + "/Task";

    HttpClient _httpClient = new();

    public async System.Threading.Tasks.Task<bool> MarkTaskAsUnDone(Models.Task task)
    {
        task.IsDone = false;
        var response = await _httpClient.PutAsJsonAsync<Models.Task>(
            _taskUrl + $"/{task.Id}",
            task
            );
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Failed to mark Task as UnDone!");
            Debug.WriteLine(response.StatusCode);
            return false;
        }

        return true;
    }
    public async System.Threading.Tasks.Task<bool> MarkTaskAsDone(Models.Task task)
    {
        task.IsDone = true;
        var response = await _httpClient.PutAsJsonAsync<Models.Task>(
            _taskUrl + $"/{task.Id}",
            task
            );
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Failed to mark Task as Done!");
            Debug.WriteLine(response.StatusCode);
            return false;
        }

        return true;
    }

    public async System.Threading.Tasks.Task<bool> AddTask(Models.Task task)
    {
        var response = await _httpClient.PostAsJsonAsync<Models.Task>(
            _taskUrl,
            task);

        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine(response.StatusCode);
            return false;
        }

        return true;
    }

    public async Task<List<Models.Task>?> GetAllTasksByProject(int projectId)
    {
        var response = await _httpClient.GetAsync(_taskUrl + $"/{projectId}/project/all-tasks");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Failed to get Tasks!");
            Debug.WriteLine(response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<Models.Task>>();
    }

    public async Task<List<Project>?> GetAll()
    {
        var response = await _httpClient.GetAsync(_projectUrl + "/all");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Failed to get Projects!");
            Debug.WriteLine(response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<Project>>();
    }
    public async Task<List<Project>?> GetAllWithAvailableTasksCount()
    {
        var response = await _httpClient.GetAsync(_projectUrl + "/all/unfinished/task-count");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Failed to get Projects!");
            Debug.WriteLine(response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<Project>>();
    }

    public async System.Threading.Tasks.Task<bool> Delete(int taskId)
    {
        var response = await _httpClient.DeleteAsync(_taskUrl + $"/{taskId}");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine($"{taskId} couldnot be deleted: " + response.StatusCode);
            return false;
        }

        return true;
    }
}
