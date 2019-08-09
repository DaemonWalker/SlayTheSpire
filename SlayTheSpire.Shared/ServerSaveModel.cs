using System;
using System.Collections.Generic;
using System.Text;

namespace SlayTheSpire.Shared
{
    public class ServerSaveModel
    {
        public string Save { get; set; }
        public Dictionary<string, object> TableInfo { get; set; }
        public ServerSaveModel()
        {
            TableInfo = new Dictionary<string, object>();
        }
    }
}
