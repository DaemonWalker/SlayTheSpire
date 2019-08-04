using Newtonsoft.Json.Linq;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Abstracts
{
    public interface ISaveCheater
    {
        ServerSaveModel GetSaveModel(string saveString, ICardService cardService);
        string GenerateSave(SaveModel saveModel);
        byte[] GenerateZip(ExportModel model);
    }
}
