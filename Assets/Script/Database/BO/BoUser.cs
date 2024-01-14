using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.BO
{
    [Serializable]
    public class BoUser : BoBase
    {
        public BoAccount boAccount;
        public BoInventory boInventory;
    }
}
