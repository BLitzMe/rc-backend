using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairConsole.Data.Models;
using RepairConsole.Services;

namespace RepairConsole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepairDevicesController : Controller
    {
        private readonly IRepairDeviceRepository _repairDeviceRepository;
        private readonly IRepairDocumentRepository _repairDocumentRepository;
        private readonly FileService _fileService;

        public RepairDevicesController(IRepairDeviceRepository repairDeviceRepository, IRepairDocumentRepository repairDocumentRepository, FileService fileService)
        {
            _repairDeviceRepository = repairDeviceRepository;
            _repairDocumentRepository = repairDocumentRepository;
            _fileService = fileService;
        }

        [HttpGet("{id}")]
        public IActionResult GetRepairDevice([FromRoute] int id)
        {
            var device = _repairDeviceRepository.GetRepairDevice(id);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost()]
        public IActionResult PostRepairDevice([FromBody] RepairDevice device)
        {
            if (device == null)
                return BadRequest();

            device = _repairDeviceRepository.AddRepairDevice(device);
            return CreatedAtAction(nameof(GetRepairDevice), new { id = device.Id }, device);
        }

        [HttpGet()]
        public IActionResult GetAllRepairDevices()
        {
            var devices = _repairDeviceRepository.GetAllRepairDevices();

            if (devices == null || devices.Count < 1)
                return NotFound();

            return Ok(devices);
        }

        [HttpPost("{repairDeviceId}/document"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadRepairDocuments([FromRoute] int repairDeviceId, [FromForm(Name = "files")] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest(new { message = "No files attached" });

            var repairDevice = _repairDeviceRepository.GetRepairDevice(repairDeviceId);
            if (repairDevice == null)
                return NotFound();

            await _fileService.SaveFiles(files, "uploads");

            var docs = files.Select(file => new RepairDocument
                {
                    RepairDevice = repairDevice,
                    RepairDeviceId = repairDevice.Id,
                    FileName = file.FileName
                })
                .ToList();
            await _repairDocumentRepository.AddMultipleRepairDocumentsAsync(docs);

            return Ok(docs);
        }

        [HttpGet("file/{id}")]
        public IActionResult DownloadRepairDocument([FromRoute] int id)
        {
            var doc = _repairDocumentRepository.GetRepairDocument(id);
            if (doc == null)
                return NotFound();

            var stream = _fileService.GetFileStream(doc.FileName, "uploads");

            if (stream == null)
                return NotFound();

            var file = File(stream, "application/octet-stream");
            file.FileDownloadName = doc.FileName;

            return file;
        }
    }
}