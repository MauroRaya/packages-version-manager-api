using Microsoft.AspNetCore.Mvc;
using packages_version_manager_api.Services;

namespace packages_version_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NpmController : ControllerBase
    {
        private readonly NpmService _npmService;

        public NpmController(NpmService npmService)
        {
            _npmService = npmService;
        }

        [HttpGet(Name = "GetNpmPackages")]
        public async Task<IActionResult> GetPackages(
            [FromQuery] string username,
            [FromQuery] string repository,
            [FromQuery] string branch,
            CancellationToken token)
        {
            var packages = await _npmService.GetPackages(username, repository, branch, token);

            List<string> outdatedPackages = new();
            List<string> updatedPackages = new();

            foreach (var package in packages)
            {
                string packageName = package.Key;
                string packageVersion = package.Value;

                string latestVersion = await _npmService.GetPackageLatestVersion(packageName, token);

                if (packageVersion.EndsWith(latestVersion))
                {
                    updatedPackages.Add($"{packageName} - Esperado: {latestVersion} - Obtido: {packageVersion}");
                } 
                else
                {
                    outdatedPackages.Add($"{packageName} - Esperado: {latestVersion} - Obtido: {packageVersion}");
                }
            }

            return Ok(new 
            { 
                outdatedPackages, 
                updatedPackages, 
            });
        }
    }
}
