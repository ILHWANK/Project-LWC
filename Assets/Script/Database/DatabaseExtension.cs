using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Database.Dto;
using WHDle.Server;
using WHDle.Util;

namespace WHDle.Database {
    using BackEnd;
    using WHDle.Database.Vo;

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
                user.VoAccount = new VOAccount(dtoAccount);
                CheckCompleteCount(complete);
            });

            ReadMyData<DtoInventory>(dtoInventory =>
            {
                user.VoInventory = new VOInventory(dtoInventory);
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

                var dtoAccount = new DtoAccount { UId = bro["gamerId"].ToString(), Gold = 10000, Day = 0 };

                WriteMyData(dtoAccount, new Where(), () =>
                {
                    user.VoAccount = new VOAccount(dtoAccount);
                    CheckCompleteCount(complete);
                });

                var dtoInventory = new DtoInventory();

                WriteMyData(dtoInventory, new Where(), () =>
                {
                    user.VoInventory = new VOInventory(dtoInventory);
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