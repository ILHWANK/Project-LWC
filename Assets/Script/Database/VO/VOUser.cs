using System;

namespace WHDle.Database.Vo
{
    [Serializable]
    public class VOUser : VOBase
    {
        public VOAccount VoAccount;
        public VOInventory VoInventory;
        public VOFavorability VoFavorability;
    }
}
