using BackEnd;
using System;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Database.Dto;
using WHDle.Util;
using WHDle.Util.Define;

namespace WHDle.Database
{
    public partial class DatabaseManager : Singleton<DatabaseManager>
    {
        #region All User DB Access

        public void ReadData<T>(Action<List<T>> complete, string userName = null,
            Where where = null, int limit = 10, string[] select = null) where T : DtoBase
        {
            var dbName = GetDBName<T>();

            if (select == null)
                Backend.GameData.Get(dbName, where != null ? where : new Where(), limit, ReadDataMultiple);
            else
                Backend.GameData.Get(dbName, where != null ? where : new Where(), select, limit, ReadDataMultiple);

            void ReadDataMultiple(BackendReturnObject callback)
            {
                if (callback.IsSuccess())
                {
                    var rows = callback.GetReturnValuetoJSON()["rows"];

                    var dtoDatas = new List<T>();

                    for (int i = 0; i < rows.Count; ++i)
                    {
                        var dtoData = SerializationUtil.JsonToObject<T>(rows[i], DeserializeType.DTO);

                        dtoDatas.Add(dtoData);
                    }

                    complete?.Invoke(dtoDatas);
                }
                else
                {
                    Debug.Log($"### Faild {typeof(T).Name} DB Read at My DB ###\n{callback}");
                }
            }
        }

        public void UpdateData<T>(T dtoData, string inDate, Action complete = null) where T : DtoBase
        {
            var dbName = GetDBName<T>();

            var param = SerializationUtil.DtoToParam(dtoData);

            Backend.GameData.UpdateV2(dbName, inDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                    complete?.Invoke();
                else
                    Debug.Log($"### Failed {typeof(T).Name} Data Update at Other User DB ###");
            });
        }

        #endregion

        #region My DB Access

        public void ReadMyData<T>(Action<T> complete, Where where = null, int limit = 10, string[] select = null) where T : DtoBase
        {
            var dbName = GetDBName<T>();

            if (select == null)
                Backend.GameData.GetMyData(dbName, where != null ? where : new Where(), limit, ReadDataProgress);
            else
                Backend.GameData.GetMyData(dbName, where != null ? where : new Where(), select, limit, ReadDataProgress);

            void ReadDataProgress(BackendReturnObject callback)
            {
                if(callback.IsSuccess())
                {
                    var rows = callback.GetReturnValuetoJSON()["rows"];
                    if(rows.Count == 0)
                    {
                        Debug.Log($"### Successed {typeof(T).Name} DB Data Acess\n" +
                            $"But There is No Data Coreesponding to the Condition ###");

                        return;
                    }

                    var dtoData = SerializationUtil.JsonToObject<T>(rows[0], DeserializeType.DTO);

                    complete?.Invoke(dtoData);
                }
                else
                {
                    Debug.Log($"### Failed {typeof(T).Name} Data Read My DB ###\n{callback}");
                }
            }
        }

        public void UpdateMyData<T>(Param param, Where where = null, Action complete = null) where T : DtoBase
        {
            var dbName = GetDBName<T>();

            Backend.GameData.Update(dbName, where != null ? where : new Where(), param, callback =>
            {
                if (callback.IsSuccess())
                    complete?.Invoke();
                else
                    Debug.LogError($"### Failed {typeof(T).Name} DB Update at My DB ###\n{callback}");
            });
        }

        public void WriteMyData<T>(T dtoData, Where where = null, Action Complete = null) where T : DtoBase
        {
            var dbName = GetDBName<T>();

            Backend.GameData.Insert(dbName, SerializationUtil.DtoToParam(dtoData), callback =>
            {
                if (callback.IsSuccess())
                    Complete?.Invoke();
                else
                    Debug.Log($"### Failed {typeof(T).Name} DB Write at My DB ###\n{callback}");
            });
        }


        #endregion

        private string GetDBName<T>() where T : DtoBase
        {
            return typeof(T).Name.Remove(0, 3);
        }
    }
}
