using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Sever.Extenssions;
using SlayTheSpire.Shared;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SlayTheSpire.Sever.Services
{
    public class SaveCheater : ISaveCheater
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public string GenerateSave(SaveModel saveModel)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(saveModel, Formatting.None, jsonSerializerSettings);
            return json.Encode();
        }

        public byte[] GenerateZip(ExportModel model)
        {
            var guid = Guid.NewGuid().ToString("N");
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "_Zip");
            var dirPath = Path.Combine(baseDir, guid);
            var dir = new DirectoryInfo(dirPath);
            Directory.CreateDirectory(dirPath);
            System.IO.File.WriteAllText(Path.Combine(dirPath, model.SaveName), model.New.Encode());
            System.IO.File.WriteAllText(
                Path.Combine(dirPath, $"{model.SaveName}.{DateTime.Now.ToString("yyyyMMddHHmmss")}.bak"),
                model.Bak.Encode());
            var zipName = Path.Combine(baseDir, $"{guid}.zip");
            using (var stream = new FileStream(zipName, FileMode.Create))
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    dir.GetFiles().ToList().ForEach(file =>
                    {
                        archive.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.NoCompression);
                        System.IO.File.Delete(file.FullName);
                    });
                }
            }
            Directory.Delete(dirPath);
            byte[] fileBytes = null;
            using (var stream = new FileStream(zipName, FileMode.OpenOrCreate))
            {
                var bytes = new byte[1024];
                fileBytes = new byte[stream.Length];
                for (int i = 0; i < stream.Length; i = i + 1024)
                {
                    var start = i;
                    var end = Math.Min(i + 1024, stream.Length);
                    stream.Read(fileBytes, start, Convert.ToInt32(end - start));
                }
            }
            File.Delete(zipName);
            return fileBytes;
        }

        public string GetSaveModel(string saveString)
        {
            var json = saveString.Decode();
            return json;
        }
    }
}
