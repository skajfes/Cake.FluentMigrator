using System;
using Cake.Core;
using Cake.Core.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Cake.FluentMigrator.Tests
{
    [TestClass]
    public class FluentMigratorToolResolverTests
    {
        [TestMethod]
        public void ResolvePath_40()
        {
            var fixture = new ToolResolverFixture(PlatformFamily.Windows, true, Version.Parse("4.0.0"));
            var rootPath = new DirectoryPath("c:/tests/tools/FluentMigrator.Console/tools/net40");
            fixture.ToolLocator.Resolve("net40/Migrate.exe").Returns(rootPath.CombineWithFilePath("Migrate.exe"));
            fixture.FileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

            var toolResolver = new FluentMigratorToolResolver(fixture.FileSystem, fixture.Environment, fixture.ToolLocator);

            var toolPath = toolResolver.ResolvePath();

            Assert.AreEqual(rootPath.CombineWithFilePath("Migrate.exe").FullPath, toolPath.FullPath);
        }

        [TestMethod]
        public void ResolvePath_45()
        {
            var fixture = new ToolResolverFixture(PlatformFamily.Windows, true, Version.Parse("4.5.0"));
            var rootPath = new DirectoryPath("c:/tests/tools/FluentMigrator.Console/tools/net45");
            fixture.ToolLocator.Resolve("net45/Migrate.exe").Returns(rootPath.CombineWithFilePath("Migrate.exe"));
            fixture.FileSystem.Exist(Arg.Any<FilePath>()).Returns(true);

            var toolResolver = new FluentMigratorToolResolver(fixture.FileSystem, fixture.Environment, fixture.ToolLocator);

            var toolPath = toolResolver.ResolvePath();

            Assert.AreEqual(rootPath.CombineWithFilePath("Migrate.exe").FullPath, toolPath.FullPath);
        }
    }
}