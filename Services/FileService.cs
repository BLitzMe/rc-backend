using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RepairConsole.Services
{
    public class FileService
    {
        public async Task SaveFiles(ICollection<IFormFile> files, string directory)
        {
            directory ??= string.Empty;
            Directory.CreateDirectory(directory);

            foreach (var file in files)
            {
                if (file.Length <= 0)
                    continue;

                var path = $"{directory}/{file.FileName}";
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }

        public FileStream GetFileStream(string file, string directory)
        {
            directory ??= string.Empty;

            var path = $"{directory}/{file}";
            var stream = new FileStream(path, FileMode.Open);

            return stream;
        }
    }
}