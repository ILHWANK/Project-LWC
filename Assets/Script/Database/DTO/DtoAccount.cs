using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.Dto
{
    [Serializable]
    public class DtoAccount : DtoBase
    {
        public string UId;
        public int Gold;
        public int Day;
    }
}
