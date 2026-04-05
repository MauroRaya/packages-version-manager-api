namespace packages_version_manager_api.Services
{
    public class GithubService
    {
        public async Task<HttpResponseMessage> GetRawFile(
            string username,
            string repository,
            string branch,
            string filePath,
            CancellationToken token)
        {
            using HttpClient client = new HttpClient();
            string url = "https://raw.githubusercontent.com";
            return await client.GetAsync($"{url}/{username}/{repository}/refs/heads/{branch}/{filePath}", token);
        }
    }
}
