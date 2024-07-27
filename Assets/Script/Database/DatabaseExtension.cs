/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Database.Dto;
using WHDle.Util;

namespace WHDle.Database {
    using BackEnd;
    using WHDle.Database.Vo;

    public partial class DatabaseManager
    {
        private void InitCompleteCount(int maxCompleteCount)
        {
            currentCompleteCount = 0;
            this.maxCompleteCount = maxCompleteCount;
        }

        private void CheckCompleteCount(Action complete = null)
        {
            lock(syncObject)
            {
                ++currentCompleteCount;

                if(currentCompleteCount >= maxCompleteCount)
                {
                    complete?.Invoke();
                }
            }
        }


    }
}*/