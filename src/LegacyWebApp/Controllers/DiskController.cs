using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LegacyWebApp.Controllers
{
    [RoutePrefix("api/disk")]
    public class DiskController : ApiController
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _legacyWebAppFilesPath;

        public DiskController(IFileSystem fileSystem, IConfiguration configuration)
        {
            _fileSystem = fileSystem;
            _legacyWebAppFilesPath = $"{configuration["LegacyWebAppFilesPath"] ?? "c:\\temp"}";
        }

        // GET: Disk
        [HttpGet()]
        [Route("", Name = nameof(Index))]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult Index()
        {
            return Ok(_fileSystem.Directory.EnumerateFileSystemEntries(_legacyWebAppFilesPath));
        }

        [HttpGet]
        [Route("new-file")]
        [ResponseType(typeof(string))]
        public IHttpActionResult CreateFile()
        {
            var fileName = Guid.NewGuid().ToString();
            var filePath = $"{_legacyWebAppFilesPath}\\{fileName}.txt";

            _fileSystem.File.WriteAllText(filePath, fileName);

            return CreatedAtRoute(nameof(Index), null,$"File created: {filePath}");
        }
    }
}