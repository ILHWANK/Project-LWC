using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHDle.Database.Vo
{
    using Dto;
    using System;

    [Serializable]
    public class VOFavorability : VOBase
    {
        public int OrientalFavorability;
        public int RenaissanceFavorability;
        public int AnimalsKingdomFavorability;
        public int ScientificCityFavorability;

        public VOFavorability(DtoFavorability dtoFavorability)
        {
            OrientalFavorability = dtoFavorability.OrientalFavorability;
            RenaissanceFavorability = dtoFavorability.RenaissanceFavorability;
            AnimalsKingdomFavorability = dtoFavorability.AnimalsKingdomFavorability;
            ScientificCityFavorability = dtoFavorability.ScientificCityFavorability;
        }
    }
}
