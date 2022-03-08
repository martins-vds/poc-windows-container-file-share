using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
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
        [ActionName("write-file")]
        public HttpResponseMessage WriteFile()
        { 
            var fileName = Guid.NewGuid().ToString();
            var filePath = $"{_legacyWebAppFilesPath}\\{fileName}.txt";

            _fileSystem.File.WriteAllText(filePath, fileName);

            return new HttpResponseMessage(System.Net.HttpStatusCode.Created)
            {
                Content = new StringContent(filePath, Encoding.UTF8, "application/json")
            };
        }
    }
}