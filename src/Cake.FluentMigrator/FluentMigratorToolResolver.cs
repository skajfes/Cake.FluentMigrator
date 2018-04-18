using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.FluentMigrator
{
    /// <summary>
    /// Contains FluentMigrator resolver functionality
    /// </summary>
    public class FluentMigratorToolResolver : IFluentMigratorToolResolver
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly IToolLocator _toolLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentMigratorToolResolver" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="toolLocator">The tool locator.</param>
        public FluentMigratorToolResolver(IFileSystem fileSystem, ICakeEnvironment environment, IToolLocator toolLocator)
        {
            _fileSystem = fileSystem;
            _environment = environment;
            _toolLocator = toolLocator;
        }

        /// <summary>
        /// Resolves the path to Migrate.exe.
        /// </summary>
        /// <returns>The path to Migrate.exe.</returns>
        public FilePath ResolvePath()
        {
            var version = this._environment.Runtime.TargetFramework.Version;
            if (version.Major < 4)
                throw new CakeException($"Framework version not supported: {version}");

            var frameworkVersion = version.Major == 4 && version.Minor < 5 ? "net40" : "net45";

            var toolPath = _toolLocator.Resolve($"{frameworkVersion}/Migrate.exe");

            if (toolPath == null || !_fileSystem.Exist(toolPath))
            {
                throw new CakeException($"Could not locate Migrate.exe for {frameworkVersion}");
            }

            return toolPath;
        }
    }
}