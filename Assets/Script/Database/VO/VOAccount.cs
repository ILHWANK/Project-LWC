using System;
using WHDle.Database.Dto;

namespace WHDle.Database.Vo
{
    [Serializable]
    public class VOAccount : VOBase
    {
        public string UId;
        public int Gold;
        public int Day;

        public VOAccount(DtoAccount dtoAccount)
        {
            UId = dtoAccount.UId;
            Gold = dtoAccount.Gold;
            Day = dtoAccount.Day;
        }
    }
}
