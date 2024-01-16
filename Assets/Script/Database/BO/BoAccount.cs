using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHDle.Database.Dto;

namespace WHDle.Database.BO
{
    [Serializable]
    public class BoAccount : BoBase
    {
        public string UId;
        public string Nickname;
        public int Gold;
        public int Day;

        public BoAccount(DtoAccount dtoAccount)
        {
            UId = dtoAccount.UId;
            Nickname = dtoAccount.Nickname;
            Gold = dtoAccount.Gold;
            Day = dtoAccount.Day;
        }
    }
}
