using Microsoft.Extensions.Configuration;
using System;
using System.IO.Abstractions;
using System.Web.Mvc;

namespace LegacyWebApp.Controllers
{
    public class DiskController : Controller
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _legacyWebAppFilesPath;

        public DiskController(IFileSystem fileSystem, IConfiguration configuration)
        {
            _fileSystem = fileSystem;
            _legacyWebAppFilesPath = $"{configuration["LegacyWebAppFilesPath"] ?? "c:\\temp"}";
        }

        // GET: Disk
        [HttpGet]
        public ActionResult Index()
        {
            return Json(_fileSystem.Directory.EnumerateFileSystemEntries(_legacyWebAppFilesPath), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ActionName("new-file")]
        public ActionResult WriteFile()
        {
            var fileName = Guid.NewGuid().ToString();
            var filePath = $"{_legacyWebAppFilesPath}\\{fileName}.txt";

            _fileSystem.File.WriteAllText(filePath, fileName);

            return Json($"File created: {filePath}", JsonRequestBehavior.AllowGet);
        }
    }
}