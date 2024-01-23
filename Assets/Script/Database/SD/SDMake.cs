using System;
using System.Collections.Generic;
using WHDle.Util.Define;

namespace WHDle.Database.SD
{
    [Serializable]
    public class SDMake : StaticBase
    {
        public string MakeItemNumber;
        public string MakeItemNameEN;
        public string MakeItemNameKR;
        public string MakeItemExplain;
        public MakeItemLocation MakeItemLocation;
        public List<string> MakeItemMixRequire;
        public MakeItemMixDivision MakeItemMixDivision;
        public List<int> MakeItemMixCountMin;
    }
}
