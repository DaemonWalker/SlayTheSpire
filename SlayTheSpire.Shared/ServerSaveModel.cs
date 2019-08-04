using System;
using System.Collections.Generic;
using System.Text;

namespace SlayTheSpire.Shared
{
    public class ServerSaveModel
    {
        public string Save { get; set; }
        public Dictionary<string, CardInfoModel> CardInfo { get; set; }
        public ServerSaveModel()
        {
            CardInfo = new Dictionary<string, CardInfoModel>();
        }
    }
}
