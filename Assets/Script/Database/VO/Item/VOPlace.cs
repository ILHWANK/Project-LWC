using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDle.Database.Vo
{
    using SD;

    [Serializable]
    public class VOPlace : VOBase
    {
        public SDPlace sdPlace;

        public VOPlace() { }
        public VOPlace(SDPlace sdPlace) 
        { 
            this.sdPlace = sdPlace;
        }
    }
}
