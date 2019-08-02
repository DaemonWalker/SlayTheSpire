using System;
using System.Collections.Generic;
using System.Text;

namespace SlayTheSpire.Shared
{
    public class SaveModel
    {
        public int CurrentMoney { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public List<CardModel> Cards { get; set; }
    }
}
