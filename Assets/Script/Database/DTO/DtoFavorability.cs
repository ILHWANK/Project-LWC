using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Database.Dto
{
    [Serializable]
    public class DtoFavorability : DtoBase
    {
        public int OrientalFavorability;
        public int RenaissanceFavorability;
        public int AnimalsKingdomFavorability;
        public int ScientificCityFavorability;
    }
}
