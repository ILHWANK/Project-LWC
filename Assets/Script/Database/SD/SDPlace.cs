using System;
using System.Collections.Generic;

namespace WHDle.Database.SD
{
    using Util.Define;

    [Serializable]
    public class SDPlace : StaticBase
    {
        public string PlaceNumber;
        public string PlaceNameEN;
        public string PlaceNameKR;
        public string PlaceExplain;
        public PlaceLocation PlaceLocation;
        public int PlaceSpawnerCount;
        public List<string> PlaceSpawner;
        public List<float> PlaceSpawnerPer;
    }
}
