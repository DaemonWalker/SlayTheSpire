using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SlayTheSpire.Shared
{
    public class CommonInfoModel
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Text")]
        public string Text { get; set; }
    }
}
