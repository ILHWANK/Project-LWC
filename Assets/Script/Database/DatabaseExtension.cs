using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Database.Dto;
using WHDle.Server;
using WHDle.Util;

namespace WHDle.Database {
    using BackEnd;
    using BO;

    public partial class DatabaseManager
    {
        #region User Data 초기 데이터 삽입/유저 데이터 읽기
        private int currentCompleteCount;
        private int maxCompleteCount;
        private object syncObject = new object();

        public void LoaduserData(Action complete = null)
        {
            if (ServerManager.Instance.isFirstLogin)
            {
                WriteDefaultUserData(complete);

                return;
            }

            var user = GameManager.User;
            InitCompleteCount(2);

            ReadMyData<DtoAccount>(dtoAccount =>
            {
                user.boAccount = new BoAccount(dtoAccount);
                CheckCompleteCount(complete);
            });

            ReadMyData<DtoInventory>(dtoInventory =>
            {
                user.boInventory = new BoInventory(dtoInventory);
                CheckCompleteCount(complete);
            });
        }

        private void WriteDefaultUserData(Action complete = null)
        {
            var user = GameManager.User;

            InitCompleteCount(2);

            InitUserAccount();

            void InitUserAccount()
            {
                var bro = Backend.BMember.GetUserInfo().GetReturnValuetoJSON()["row"];

                var dtoAccount = new DtoAccount { UId = bro["gamerId"].ToString(), Nickname = string.Empty, Gold = 10000, Day = 0 };

                WriteMyData(dtoAccount, new Where(), () =>
                {
                    user.boAccount = new BoAccount(dtoAccount);
                    CheckCompleteCount(complete);
                });

                var dtoInventory = new DtoInventory { ItemAmounts = string.Empty, ItemIndexes = string.Empty };

                WriteMyData(dtoInventory, new Where(), () =>
                {
                    user.boInventory = new BoInventory(dtoInventory);
                    CheckCompleteCount(complete);
                });
            }
        }

        #endregion

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
}