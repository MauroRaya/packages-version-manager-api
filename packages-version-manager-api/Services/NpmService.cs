using packages_version_manager_api.Models;

namespace packages_version_manager_api.Services
{
    public class NpmService
    {
        private readonly GithubService _githubService;

        public NpmService(GithubService githubService)
        {
            _githubService = githubService;
        }

        public async Task<string> GetPackageLatestVersion(
            string packageName, 
            CancellationToken token)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"https://registry.npmjs.org/{packageName}", token);
            response.EnsureSuccessStatusCode();

            NpmPackage? package = await response.Content.ReadFromJsonAsync<NpmPackage>(token);
            if (package is null) throw new Exception();

            return package.DistTags["latest"];
        }

        public async Task<Dictionary<string, string>> GetPackages(
            string username,
            string repository,
            string branch,
            CancellationToken token)
        {
            string filePath = "package.json";

            HttpResponseMessage response = await _githubService.GetPackageManagerFile(username, repository, branch, filePath, token);
            response.EnsureSuccessStatusCode();

            PackageJson? packageJson = await response.Content.ReadFromJsonAsync<PackageJson>(token);
            if (packageJson is null) throw new Exception();

            return packageJson.Dependencies;
        }
    }
}
